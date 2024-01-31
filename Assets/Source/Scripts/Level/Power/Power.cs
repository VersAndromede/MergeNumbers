using System;

public class Power 
{
    public event Action Changed;
    public event Action Over;

    public Power(int value)
    {
        Value = value;
    }

    public int Value { get; private set; }

    public void Add(int count)
    {
        Value += count;
        Changed?.Invoke();
        HandleOver();
    }

    public void Divide(int divider)
    {
        if (divider < 1)
            throw new ArgumentOutOfRangeException("In this context it is incorrect to divide by a number less than 1.");

        Value /= divider;
        Changed?.Invoke();
        HandleOver();
    }

    private void HandleOver()
    {
        if (Value < 0)
            Over?.Invoke();
    }
}
