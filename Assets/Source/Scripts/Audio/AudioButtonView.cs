using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Audio
{
    public class AudioButtonView : MonoBehaviour
    {
        [SerializeField] private AudioButton _button;
        [SerializeField] private Image _image;
        [SerializeField] private Color _disableColor;
        [SerializeField] private Color _enableColor;

        private void OnEnable()
        {
            _button.EnabledChanged += OnEnabledChanged;
        }

        private void OnDisable()
        {
            _button.EnabledChanged -= OnEnabledChanged;
        }

        private void OnEnabledChanged()
        {
            _image.color = _button.Enabled ? _enableColor : _disableColor;
        }
    }
}