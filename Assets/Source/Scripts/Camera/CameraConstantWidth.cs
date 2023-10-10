using System;
using UnityEngine;

public class CameraConstantWidth : MonoBehaviour
{
    [SerializeField] private Camera _componentCamera;

    [field: SerializeField] public Vector2 AspectRatio { get; private set; }

    private float _initialSize;
    private float _targetAspect;
    private float _initialFov;
    private float _horizontalFov = 120f;

    public event Action ScalerMatchChanged;

    public Vector2 Resolution { get; private set; }
    public float ScalerMatch { get; private set; }
    public bool IsPortraitOrientation => Screen.width / (float)Screen.height * AspectRatio.x <= AspectRatio.y;

    private void Start()
    {
        _initialSize = _componentCamera.orthographicSize;
        _initialFov = _componentCamera.fieldOfView;
        IdentifyNewResolution();
        _horizontalFov = CalcVerticalFov(_initialFov, 1 / _targetAspect);

        float constantWidthFov = CalcVerticalFov(_horizontalFov, _componentCamera.aspect);
        _componentCamera.fieldOfView = Mathf.Lerp(constantWidthFov, _initialFov, ScalerMatch);
    }

    private void Update()
    {
        IdentifyNewResolution();

        if (_componentCamera.orthographic)
        {
            float constantWidthSize = _initialSize * (_targetAspect / _componentCamera.aspect);
            _componentCamera.orthographicSize = Mathf.Lerp(constantWidthSize, _initialSize, ScalerMatch);
        }
        else
        {
            float constantWidthFov = CalcVerticalFov(_horizontalFov, _componentCamera.aspect);
            _componentCamera.fieldOfView = Mathf.Lerp(constantWidthFov, _initialFov, ScalerMatch);
        }
    }

    private float CalcVerticalFov(float hFovInDeg, float aspectRatio)
    {
        float hFovInRads = hFovInDeg * Mathf.Deg2Rad;
        float vFovInRads = 2 * Mathf.Atan(Mathf.Tan(hFovInRads / 2) / aspectRatio);
        return vFovInRads * Mathf.Rad2Deg;
    }

    private void IdentifyNewResolution()
    {
        Vector2Int targetResolution = new Vector2Int(1920, 1080);

        if (IsPortraitOrientation)
            SetNewResolution(targetResolution.y, targetResolution.x, 0);
        else
            SetNewResolution(targetResolution.x, targetResolution.y, 1);
    }

    private void CalculateHorizontalFov(int resolutionWidth, int resolutionHeight)
    {
        Resolution = new Vector2(resolutionWidth, resolutionHeight);
        _targetAspect = Resolution.x / Resolution.y;
        _horizontalFov = CalcVerticalFov(_initialFov, 1 / _targetAspect);
    }

    private void SetNewResolution(int width, int height, int scalerMatch)
    {
        CalculateHorizontalFov(width, height);
        ScalerMatch = scalerMatch;
        ScalerMatchChanged?.Invoke();
    }
}