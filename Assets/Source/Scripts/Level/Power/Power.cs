using System;
using UnityEngine;

public class Power : MonoBehaviour
{
    public event Action Changed;
    public event Action Over;

    public int Value { get; private set; }

    public void Add(int count)
    {
        Value += count;
        Changed?.Invoke();
        TryOver();
    }

    public void Divide(int divider)
    {
        if (divider < 1)
            throw new ArgumentOutOfRangeException("In this context it is incorrect to divide by a number less than 1.");

        Value /= divider;
        Changed?.Invoke();
        TryOver();
    }

    private void TryOver()
    {
        if (Value < 0)
            Over?.Invoke();
    }
}
