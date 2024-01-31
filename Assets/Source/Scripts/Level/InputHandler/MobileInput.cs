using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameInput
{
    public class MobileInput : MonoBehaviour, IDragHandler, IInput
    {
        [SerializeField] private float _minDelta;

        public event Action<Direction> Received;

        public void OnDrag(PointerEventData eventData)
        {
            if (eventData.delta.x < -_minDelta)
                Received?.Invoke(Direction.Left);
            else if (eventData.delta.x > _minDelta)
                Received?.Invoke(Direction.Right);
            else if (eventData.delta.y < -_minDelta)
                Received?.Invoke(Direction.Down);
            else if (eventData.delta.y > _minDelta)
                Received?.Invoke(Direction.Up);
        }
    }
}