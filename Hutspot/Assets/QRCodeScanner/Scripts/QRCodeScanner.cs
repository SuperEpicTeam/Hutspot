using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZXing;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QRCodeScanner : MonoBehaviour
{
    [SerializeField] private RawImage rawImageBackground;
    [SerializeField] private AspectRatioFitter aspectRatioFitter;
    [SerializeField] private RectTransform scanZone;

    [SerializeField] private bool isCamAvailible;

    private WebCamTexture cameraTexture;
    private Canvas uiCanvas;

    // Start is called before the first frame update
    void Awake()
    {
        uiCanvas = GetComponentInChildren<Canvas>();
        SetUpCamera();
    }

    // Update is called once per frame
    void Update()
    {
        if (isCamAvailible)
        {
            UpdateCameraRenderer();
            Scan();
        }
    }

    private void UpdateCameraRenderer()
    {
        if (isCamAvailible == false)
        {
            return;
        }
        float ratio = (float)cameraTexture.width / (float)cameraTexture.height;
        aspectRatioFitter.aspectRatio = ratio;

        int orientation = -cameraTexture.videoRotationAngle;
        rawImageBackground.rectTransform.localEulerAngles = new Vector3(0, 0, orientation);
    }

    private void SetUpCamera()
    {
        WebCamDevice[] devices = WebCamTexture.devices;

        if (devices.Length == 0)
        {
            isCamAvailible = false;
            return;
        }
        for (int i = 0; i < devices.Length; i++)
        {
            if (devices[i].isFrontFacing == false)
            {
                cameraTexture = new WebCamTexture(devices[i].name, (int)scanZone.rect.width, (int)-scanZone.rect.height);
            }
        }

        cameraTexture.Play();
        rawImageBackground.texture = cameraTexture;
        isCamAvailible = true;
    }

    public void OnClickScan()
    {
        Scan();
    }

    private void Scan()
    {
        try
        {
            IBarcodeReader barcodeReader = new BarcodeReader();
            Result result = barcodeReader.Decode(cameraTexture.GetPixels32(), cameraTexture.width, cameraTexture.height);
            if (result != null)
            {
                StopScanning();
                //SceneManager.LoadScene(result.Text);
            }
            else
            {
                Debug.Log("Failed to read QR code");
            }
        }
        catch
        {
            Debug.Log("Failed in try");
        }
    }

    private void StopScanning()
    {
        rawImageBackground.texture = new Texture2D(0, 0);
        isCamAvailible = false;
        uiCanvas.gameObject.SetActive(false);
        cameraTexture.Stop();
    }
}
