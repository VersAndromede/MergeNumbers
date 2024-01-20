using TMPro;
using UnityEngine;

public class PowerView : MonoBehaviour
{
    [SerializeField] private Color _positiveColorPower;
    [SerializeField] private Color _negativeColorPower;
    [SerializeField] private Color _neutralColorPower;
    [SerializeField] private TextMeshPro _text;

    private void Start()
    {
        UpdateUI(0);
    }

    public void UpdateUI(int powerValue)
    {
        if (powerValue > 0)
            _text.color = _positiveColorPower;
        else if (powerValue < 0)
            _text.color = _negativeColorPower;
        else
            _text.color = _neutralColorPower;

        _text.text = $"{powerValue}";
    }
}