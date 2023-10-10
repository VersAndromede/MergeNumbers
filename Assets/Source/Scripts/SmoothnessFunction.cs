using System;

public static class SmoothnessFunction
{
    public static float GetEaseInOutCubic(float x)
    {
        return (float)(x < 0.5 ? 4 * x * x * x : 1 - Math.Pow(-2 * x + 2, 3) / 2);
    }
}