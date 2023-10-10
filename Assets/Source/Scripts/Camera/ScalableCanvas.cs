using UnityEngine;
using UnityEngine.UI;

public class ScalableCanvas : MonoBehaviour
{
    [SerializeField] private CanvasScaler _canvasScaler;
    [SerializeField] private CameraConstantWidth _cameraConstantWidth;
    [SerializeField] private float _scaleForPortraitOrientation;

    private void OnEnable()
    {
        _cameraConstantWidth.ScalerMatchChanged += OnScalerMatchChanged;
    }

    private void OnDisable()
    {
        _cameraConstantWidth.ScalerMatchChanged -= OnScalerMatchChanged;
    }

    private void OnScalerMatchChanged()
    {
        Vector2 newResolution = _cameraConstantWidth.Resolution;
        Vector2 newResolutionForPortraitOrientation = newResolution / _scaleForPortraitOrientation;
        _canvasScaler.matchWidthOrHeight = _cameraConstantWidth.ScalerMatch;

        if (_cameraConstantWidth.IsPortraitOrientation)
            _canvasScaler.referenceResolution = newResolutionForPortraitOrientation;
        else
            _canvasScaler.referenceResolution = new Vector2(newResolution.x, newResolution.y);
    }
}