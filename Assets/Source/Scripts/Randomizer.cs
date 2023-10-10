using UnityEngine;

public static class Randomizer
{
    public static bool GetBool()
    {
        return Random.Range(0, 2) == 0;
    }

    public static bool CheckProbability(int probability)
    {
        float randomNumber = Random.Range(0.0f, 101.0f);
        return randomNumber < probability;
    }
}