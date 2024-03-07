using System;
using UnityEngine;

namespace Scripts.Level.GameInput
{
    public class DesktopInput : MonoBehaviour, IInput
    {
        public event Action<Direction> Received;

        private void Update()
        {
            float horizontal = Input.GetAxisRaw(Axes.Horizontal);
            float vertical = Input.GetAxisRaw(Axes.Vertical);
            Direction direction = GetDirection(horizontal, vertical);
            
            Received?.Invoke(direction);
        }

        private Direction GetDirection(float horizontal, float vertical)
        {
            if (horizontal < 0)
                return Direction.Left;
            else if (horizontal > 0)
                return Direction.Right;
            else if (vertical < 0)
                return Direction.Down;
            else if (vertical > 0)
                return Direction.Up;
            else
                return Direction.None;
        }
    }
}