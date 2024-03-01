using System;
using UnityEngine;

namespace Scripts.UpgradeSystem
{
    public abstract class Upgrade : MonoBehaviour
    {
        [SerializeField] private int _id;

        [field: SerializeField] public int MaxLevel { get; private set; }
        [field: SerializeField] public int Price { get; private set; }
        [field: SerializeField] public int BonusValue { get; private set; }

        private const int MinLevel = 1;

        private IPriceChangeAlgorithm _priceChangeAlgorithm;
        private IValueChangeAlgorithm _valueChangeAlgorithm;

        public event Action LevelChanged;

        public int Level { get; private set; } = MinLevel;
        public bool CanImprove => Level < MaxLevel;

        private void OnValidate()
        {
            const int MaxId = 2;

            if (_id < 0 || _id > MaxId)
                _id = Mathf.Clamp(_id, 0, MaxId);
        }

        protected virtual void OnInit() { }

        public void Init(int level, int price, int bonusValue)
        {
            if (level < MinLevel)
            {
                OnInit();
                return;
            }

            Level = level;
            Price = price;
            BonusValue = bonusValue;
            OnInit();
        }

        public void Improve()
        {
            BonusValue = _valueChangeAlgorithm.GetChanged(BonusValue);
            Price = _priceChangeAlgorithm.GetChanged(Price);
            Level++;
            LevelChanged?.Invoke();

            if (Level > MaxLevel)
                throw new ArgumentOutOfRangeException();
        }

        protected void SetPriceChangeAlgorithm(IPriceChangeAlgorithm priceChangeAlgorithm)
        {
            _priceChangeAlgorithm = priceChangeAlgorithm;
        }

        protected void SetValueChangeAlgorithm(IValueChangeAlgorithm valueChangeAlgorithm)
        {
            _valueChangeAlgorithm = valueChangeAlgorithm;
        }
    }
}