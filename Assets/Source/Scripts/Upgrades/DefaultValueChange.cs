using UnityEngine;

namespace Upgrades
{
    public class DefaultValueChange : MonoBehaviour, IValueChangeAlgorithm
    {
        [SerializeField] private int _addedValue;

        public int GetChanged(int currentValue)
        {
            return currentValue + _addedValue;
        }
    }
}