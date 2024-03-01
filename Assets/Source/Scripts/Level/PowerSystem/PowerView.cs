using TMPro;
using UnityEngine;

namespace Scripts.Level.PowerSystem
{
    public abstract class PowerView : MonoBehaviour
    {
        [SerializeField] protected PowerColorsConfig PowerColorsConfig;

        [SerializeField] private TextMeshPro _text;

        protected void SetValue(string value)
        {
            _text.text = value;
        }

        protected void SetColor(Color color)
        {
            _text.color = color;
        }
    }
}