using System;
using Random = UnityEngine.Random;

namespace Scripts.Extensions.Randomizer
{
    public static class Randomizer
    {
        public static bool GetBool()
        {
            const int DownBorderForRange = 0;
            const int UpBorderForRange = 2;
            const int ResultForSuccess = 1;

            return Random.Range(DownBorderForRange, UpBorderForRange) == ResultForSuccess;
        }

        public static bool TryProbability(float probability)
        {
            const float MinProbability = 0;
            const float MaxProbability = 100;

            if (probability < MinProbability || probability > MaxProbability)
                throw new ArgumentException();

            if (probability == MinProbability)
                return false;

            float randomNumber = Random.Range(MinProbability, MaxProbability);
            return randomNumber <= probability;
        }
    }
}