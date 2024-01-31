using System;
using UnityEngine;

namespace GameInput
{
    public class DesktopInput : MonoBehaviour, IInput
    {
        public event Action<Direction> Received;

        private void Update()
        {
            float horizontal = Input.GetAxisRaw(Axes.Horizontal);
            float vertical = Input.GetAxisRaw(Axes.Vertical);

            if (horizontal < 0)
                Received?.Invoke(Direction.Left);
            else if (horizontal > 0)
                Received?.Invoke(Direction.Right);
            else if (vertical < 0)
                Received?.Invoke(Direction.Down);
            else if (vertical > 0)
                Received?.Invoke(Direction.Up);
        }
    }
}