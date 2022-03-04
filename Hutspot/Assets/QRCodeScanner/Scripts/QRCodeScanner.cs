using UnityEngine;
using ZXing;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class QRCodeScanner : MonoBehaviour
{
	[SerializeField] private RawImage _rawImageBackground;
	[SerializeField] private AspectRatioFitter _aspectRatioFitter;
	[SerializeField] private RectTransform _scanZone;

	[SerializeField] private bool _isCamAvailible;

	[SerializeField] private Canvas _uiCanvas;
	[SerializeField] private TextMeshProUGUI _scannerPlayerFeedback;

	[SerializeField] private Button _activationButton;
	[SerializeField] private Button _deactivationButton;

	private WebCamTexture _cameraTexture;

	private void Awake()
	{
		_activationButton.onClick.AddListener(SetUpCamera);
		_deactivationButton.onClick.AddListener(StopScanning);
	}

	void Update()
	{
		if (_isCamAvailible)
		{
			UpdateCameraRenderer();
			Scan();
		}
	}

	/// <summary>
	/// Update the orientation and aspect ratio of the camera footage.
	/// </summary>
	private void UpdateCameraRenderer()
	{
		float ratio = _cameraTexture.width / _cameraTexture.height;
		_aspectRatioFitter.aspectRatio = ratio;

		int orientation = -_cameraTexture.videoRotationAngle;
		_rawImageBackground.rectTransform.localEulerAngles = new Vector3(0, 0, orientation);
	}

	/// <summary>
	/// Activate Front facing camera on device and show the camera view to the user
	/// </summary>
	private void SetUpCamera()
	{
		_uiCanvas.gameObject.SetActive(true);
		WebCamDevice[] devices = WebCamTexture.devices;

		if (devices.Length == 0)
		{
			_isCamAvailible = false;
		}
		else 
		{
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
				_scannerPlayerFeedback.text = "Failed to read QR code";
			}
		}
		catch
		{
			_scannerPlayerFeedback.text = "Failed to read QR code";
		}
	}

	private void StopScanning()
	{
		_rawImageBackground.texture = new Texture2D(0, 0);
		_uiCanvas.gameObject.SetActive(false);
		_cameraTexture.Stop();
	}
}
