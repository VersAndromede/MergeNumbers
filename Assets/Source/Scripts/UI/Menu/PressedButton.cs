using System;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.Menu
{
    public abstract class PressedButton : MonoBehaviour
    {
        [SerializeField] private Button _button;

        public event Action Pressed;

        private void OnEnable()
        {
            _button.onClick.AddListener(Press);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(Press);
        }

        public void EnableInteractable()
        {
            _button.interactable = true;
        }

        public void DisableInteractable()
        {
            _button.interactable = false;
        }

        private void Press()
        {
            Pressed?.Invoke();
        }
    }
}