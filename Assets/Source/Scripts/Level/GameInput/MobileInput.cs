using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Scripts.Level.GameInput
{
    public class MobileInput : MonoBehaviour, IDragHandler, IInput
    {
        [SerializeField] private float _minDelta;

        public event Action<Direction> Received;

        public void OnDrag(PointerEventData eventData)
        {
            Direction direction = GetDirection(eventData.delta);
            Received?.Invoke(direction);
        }

        private Direction GetDirection(Vector2 delta)
        {
            if (delta.x < -_minDelta)
                return Direction.Left;
            else if (delta.x > _minDelta)
                return Direction.Right;
            else if (delta.y < -_minDelta)
                return Direction.Down;
            else if (delta.y > _minDelta)
                return Direction.Up;
            else 
                return Direction.None;
        }
    }
}