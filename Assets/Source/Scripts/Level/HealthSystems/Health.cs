using System;
using UnityEngine;

namespace Scripts.Level.HealthSystems
{
    public class Health
    {
        public Health(uint maxHealthValue)
        {
            SetMax(maxHealthValue);
        }

        public event Action Changed;
        public event Action Died;

        public int MaxValue { get; private set; }

        public int Value { get; private set; }

        public bool IsDied => Value <= 0;

        public void TakeDamage(int damage)
        {
            if (damage < 0)
                throw new ArgumentOutOfRangeException();

            Value -= damage;
            Value = Mathf.Clamp(Value, 0, MaxValue);
            Changed?.Invoke();
            Die();
        }

        public void SetMax(uint count)
        {
            MaxValue = (int)count;
            Value = MaxValue;
            Changed?.Invoke();
        }

        private void Die()
        {
            if (IsDied)
                Died?.Invoke();
        }
    }
}