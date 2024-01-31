using System;
using UnityEngine;

namespace CameraSystems
{
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
        public bool IsLandscapeOrientation => Screen.height / (float)Screen.width * AspectRatio.x <= AspectRatio.y;

        private float AspectRatioForVerticalFov => 1 / _targetAspect;

        private void Start()
        {
            _initialSize = _componentCamera.orthographicSize;
            _initialFov = _componentCamera.fieldOfView;
            IdentifyNewResolution();

            _horizontalFov = CalculateVerticalFov(_initialFov, AspectRatioForVerticalFov);

            float constantWidthFov = CalculateVerticalFov(_horizontalFov, _componentCamera.aspect);
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
                float constantWidthFov = CalculateVerticalFov(_horizontalFov, _componentCamera.aspect);
                _componentCamera.fieldOfView = Mathf.Lerp(constantWidthFov, _initialFov, ScalerMatch);
            }
        }

        private float CalculateVerticalFov(float horizontalFovInDeg, float aspectRatio)
        {
            const float Factor = 2f;

            float halfHorizontalFovInRads = horizontalFovInDeg * Mathf.Deg2Rad / 2;
            float verticalFovInRads = Factor * Mathf.Atan(Mathf.Tan(halfHorizontalFovInRads) / aspectRatio);
            return verticalFovInRads * Mathf.Rad2Deg;
        }

        private void IdentifyNewResolution()
        {
            const int PortraitScalerMatch = 0;
            const int LandscapeScalerMatch = 1;

            Vector2Int targetResolution = new Vector2Int(1920, 1080);

            if (IsPortraitOrientation)
                SetNewResolution(targetResolution.y, targetResolution.x, PortraitScalerMatch);
            else
                SetNewResolution(targetResolution.x, targetResolution.y, LandscapeScalerMatch);
        }

        private void CalculateHorizontalFov(int resolutionWidth, int resolutionHeight)
        {
            Resolution = new Vector2(resolutionWidth, resolutionHeight);
            _targetAspect = Resolution.x / Resolution.y;

            _horizontalFov = CalculateVerticalFov(_initialFov, AspectRatioForVerticalFov);
        }

        private void SetNewResolution(int width, int height, int scalerMatch)
        {
            CalculateHorizontalFov(width, height);
            ScalerMatch = scalerMatch;
            ScalerMatchChanged?.Invoke();
        }
    }
}