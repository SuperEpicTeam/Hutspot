using UnityEngine;
using ZXing;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QRCodeScanner : MonoBehaviour
{
    [SerializeField] private RawImage _rawImageBackground;
    [SerializeField] private AspectRatioFitter _aspectRatioFitter;
    [SerializeField] private RectTransform _scanZone;

    [SerializeField] private bool _isCamAvailible;

    [SerializeField] private Canvas _uiCanvas;

    private WebCamTexture _cameraTexture;

    // Update is called once per frame
    void Update()
    {
        if (_isCamAvailible)
        {
            UpdateCameraRenderer();
            Scan();
        }
    }

    private void UpdateCameraRenderer()
    {
        if (_isCamAvailible == false)
        {
            return;
        }

        float ratio = (float)_cameraTexture.width / (float)_cameraTexture.height;
        _aspectRatioFitter.aspectRatio = ratio;

        int orientation = -_cameraTexture.videoRotationAngle;
        _rawImageBackground.rectTransform.localEulerAngles = new Vector3(0, 0, orientation);
    }

    public void  Camera()
    {
        _uiCanvas.gameObject.SetActive(true);
        WebCamDevice[] devices = WebCamTexture.devices;

        if (devices.Length == 0)
        {
            _isCamAvailible = false;
            return;
        }

        for (int i = 0; i < devices.Length; i++)
        {
            if (devices[i].isFrontFacing == false)
            {
                _cameraTexture = new WebCamTexture(devices[i].name, (int)_scanZone.rect.width, (int)-_scanZone.rect.height);
            }
        }

        _cameraTexture.Play();
        _rawImageBackground.texture = _cameraTexture;
        _isCamAvailible = true;
    }

    private void Scan()
    {
        try
        {
            IBarcodeReader barcodeReader = new BarcodeReader();
            Result result = barcodeReader.Decode(_cameraTexture.GetPixels32(), _cameraTexture.width, _cameraTexture.height);

            if (result != null)
            {
                SceneManager.LoadScene(result.Text);
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

    public void StopScanning()
    {
        _rawImageBackground.texture = new Texture2D(0, 0);
        _uiCanvas.gameObject.SetActive(false);
        _cameraTexture.Stop();
    }
}
