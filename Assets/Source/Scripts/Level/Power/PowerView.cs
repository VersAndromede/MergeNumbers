using TMPro;
using UnityEngine;

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
