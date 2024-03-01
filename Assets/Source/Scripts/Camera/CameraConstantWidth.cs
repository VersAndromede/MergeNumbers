using System;
using UnityEngine;

namespace Scripts.CameraSystem
{
    public partial class CameraConstantWidth : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Vector2 _aspectRatio;

        private float _initialSize;
        private float _targetAspect;
        private float _initialFov;
        private float _horizontalFov = 120f;

        public event Action ScalerMatchChanged;

        public Vector2 Resolution { get; private set; }
        public float ScalerMatch { get; private set; }

        private float AspectRatioForVerticalFov => 1 / _targetAspect;

        private void Start()
        {
            _initialSize = _camera.orthographicSize;
            _initialFov = _camera.fieldOfView;
            IdentifyNewResolution();

            _horizontalFov = CalculateVerticalFov(_initialFov, AspectRatioForVerticalFov);
            SetFov();
        }

        private void Update()
        {
            IdentifyNewResolution();

            if (_camera.orthographic)
            {
                float constantWidthSize = _initialSize * (_targetAspect / _camera.aspect);
                _camera.orthographicSize = GetLerpFov(constantWidthSize);
                return;
            }

            SetFov();
        }

        public Orientation GetTargetOrientation()
        {
            if (Screen.width / (float)Screen.height * _aspectRatio.x <= _aspectRatio.y)
                return Orientation.Portrait;
            else if (Screen.height / (float)Screen.width * _aspectRatio.x <= _aspectRatio.y)
                return Orientation.Landscape;
            else 
                return Orientation.Default;
        }

        private void SetFov()
        {
            float constantWidthFov = CalculateVerticalFov(_horizontalFov, _camera.aspect);
            _camera.fieldOfView = GetLerpFov(constantWidthFov);
        }

        private float GetLerpFov(float fov)
        {
            return Mathf.Lerp(fov, _initialFov, ScalerMatch);
        }

        private float CalculateVerticalFov(float horizontalFovInDeg, float aspectRatio)
        {
            const float Factor = 2f;

            float halfHorizontalFovInRads = horizontalFovInDeg * Mathf.Deg2Rad / Factor;
            float verticalFovInRads = Factor * Mathf.Atan(Mathf.Tan(halfHorizontalFovInRads) / aspectRatio);
            return verticalFovInRads * Mathf.Rad2Deg;
        }

        private void IdentifyNewResolution()
        {
            const int PortraitScalerMatch = 0;
            const int LandscapeScalerMatch = 1;
            const int TargetWidth = 1920;
            const int TargetHeight = 1080;

            if (GetTargetOrientation() == Orientation.Portrait)
                SetNewResolution(TargetHeight, TargetWidth, PortraitScalerMatch);
            else
                SetNewResolution(TargetWidth, TargetHeight, LandscapeScalerMatch);
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