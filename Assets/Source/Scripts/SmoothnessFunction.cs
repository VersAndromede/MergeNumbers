using UnityEngine;

namespace SmoothnessFunctions
{
    public static class SmoothnessFunction
    {
        public static float GetEaseInOutCubic(float x)
        {
            const float FullAnimationProgress = 1f;
            const float HalfAnimationProgress = 0.5f;
            const float CubicCoefficient = 4f;
            const float CubicPower = 3f;
            const float Divider = 2f;

            float cubicBase = -2 * x + 2f;

            if (x < HalfAnimationProgress)
                return CubicCoefficient * x * x * x;
            else
                return FullAnimationProgress - (Mathf.Pow(cubicBase, CubicPower) / Divider);
        }
    }
}