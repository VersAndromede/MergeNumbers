using System;

namespace Scripts.Level.GameInput
{
    public interface IInput
    {
        public event Action<Direction> Received;
    }
}