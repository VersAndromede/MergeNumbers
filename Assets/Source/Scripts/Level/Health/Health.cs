using System;
using UnityEngine;

namespace HealthSystem
{
    public class Health
    {
        public event Action Changed;
        public event Action Died;

        public int MaxValue { get; private set; }
        public int Value { get; private set; }
        public bool IsDied => Value <= 0;

        public Health(uint maxHealthValue)
        {
            SetMax(maxHealthValue);
        }

        public void TakeDamage(int damage)
        {
            if (damage < 0)
                throw new ArgumentOutOfRangeException();

            Value -= damage;
            Value = Mathf.Clamp(Value, 0, MaxValue);
            Changed?.Invoke();
            TryDie();
        }

        public void SetMax(uint count)
        {
            MaxValue = (int)count;
            Value = MaxValue;
            Changed?.Invoke();
        }

        private void TryDie()
        {
            if (IsDied)
                Died?.Invoke();
        }
    }
}