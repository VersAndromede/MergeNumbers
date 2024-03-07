using System;
using Scripts.Level.PowerSystem;
using UnityEngine;
using UnityEngine.Events;

namespace Scripts.Level.MonsterSystem
{
    public abstract class Monster : MonoBehaviour
    {
        [SerializeField] private UnityEvent _died;

        private Power _power;

        public event Action<Monster> Died;

        public int PowerCount => _power.Value;

        public abstract void SetEffect(Power target);

        public void Init(int power)
        {
            _power = new Power(power);
        }

        public void Die()
        {
            _died?.Invoke();
            Died?.Invoke(this);
            Destroy(gameObject);
        }
    }
}