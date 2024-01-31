using System;

namespace GameInput
{
    public interface IInput
    {
        public event Action<Direction> Received;
    }
}