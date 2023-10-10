using System;

public interface IInput
{
    public event Action<Direction> Received;
}