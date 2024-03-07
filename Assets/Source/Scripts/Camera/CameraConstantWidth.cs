using System;
using UnityEngine;

namespace Scripts.CameraSystem
{
    public class CameraConstantWidth : MonoBehaviour
    {
        [SerializeField] private OrientationGetter _orientationGetter;
        [SerializeField] private Camera _camera;

        private float _initialSize;
        private float _targetAspect;
        private float _initialFov;
        private float _horizontalFov = 120f;

        public event Action ScalerMatchChanged;

        public Orientation Orientation => _orientationGetter.Get();

        public Vector2 Resolution { get; private set; }

        public float ScalerMatch { get; private set; }

        private float AspectRatioForVerticalFov => 1 / _targetAspect;

        private void Start()
        {
            _initialSize = _camera.orthographicSize;
            _initialFov = _camera.fieldOfView;
            IdentifyNewResolution();

            _horizontalFov = GetHorizontalFov(_initialFov, AspectRatioForVerticalFov);
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

        private void SetFov()
        {
            float constantWidthFov = GetHorizontalFov(_horizontalFov, _camera.aspect);
            _camera.fieldOfView = GetLerpFov(constantWidthFov);
        }

        private float GetLerpFov(float fov)
        {
            return Mathf.Lerp(fov, _initialFov, ScalerMatch);
        }

        private void CalculateHorizontalFov(int resolutionWidth, int resolutionHeight)
        {
            Resolution = new Vector2(resolutionWidth, resolutionHeight);
            _targetAspect = Resolution.x / Resolution.y;

            _horizontalFov = GetHorizontalFov(_initialFov, AspectRatioForVerticalFov);
        }

        private float GetHorizontalFov(float horizontalFovInDeg, float aspectRatio)
        {
            const float Factor = 2f;

            float halfHorizontalFovInRads = horizontalFovInDeg * Mathf.Deg2Rad / Factor;
            float horizontalFovInRads = Factor * Mathf.Atan(Mathf.Tan(halfHorizontalFovInRads) / aspectRatio);
            return horizontalFovInRads * Mathf.Rad2Deg;
        }

        private void IdentifyNewResolution()
        {
            const int PortraitScalerMatch = 0;
            const int LandscapeScalerMatch = 1;
            const int TargetWidth = 1920;
            const int TargetHeight = 1080;

            switch (Orientation)
            {
                case Orientation.Portrait:
                    SetNewResolution(TargetHeight, TargetWidth, PortraitScalerMatch);
                    break;
                default:
                    SetNewResolution(TargetWidth, TargetHeight, LandscapeScalerMatch);
                    break;
            }
        }

        private void SetNewResolution(int width, int height, int scalerMatch)
        {
            CalculateHorizontalFov(width, height);
            ScalerMatch = scalerMatch;
            ScalerMatchChanged?.Invoke();
        }
    }
}