using UnityEngine;

namespace Upgrades
{
    public class ValueMultiplicationWithMinBorder : MonoBehaviour, IValueChangeAlgorithm
    {
        [SerializeField] private float _multiplier;

        [field: SerializeField] public int MinBorder { get; private set; }

        public int GetChanged(int currentValue)
        {
            int newBonusValue = (int)Mathf.Round(currentValue * _multiplier);
            return Mathf.Clamp(newBonusValue, MinBorder, int.MaxValue);
        }
    }
}