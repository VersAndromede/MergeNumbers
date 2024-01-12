using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    private readonly bool _initialized;

    public event Action Changed;
    public event Action Died;

    public int MaxValue { get; private set; }
    public int Value { get; private set; }
    public bool IsDied { get; private set; }

    public void Init(int maxHealthValue)
    {
        if (_initialized)
            throw new InvalidOperationException("You cannot re-initialize.");

        if (maxHealthValue < 0)
            throw new ArgumentOutOfRangeException();

        MaxValue = maxHealthValue;
        Value = MaxValue;
        Changed?.Invoke();
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

    private void TryDie()
    {
        if (Value <= 0)
        {
            IsDied = true;
            Died?.Invoke();
        }
    }
}