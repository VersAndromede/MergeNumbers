using TMPro;
using UnityEngine;

public class PowerView : MonoBehaviour
{
    [SerializeField] protected Color PositiveColorPower;
    [SerializeField] protected Color NegativeColorPower;
    [SerializeField] protected Color NeutralColorPower;
    [SerializeField] protected TextMeshPro Text;
    [SerializeField] protected Power Power;

    private void Start()
    {
        OnChanged();
    }

    private void OnEnable()
    {
        Power.Changed += OnChanged;
    }

    private void OnDisable()
    {
        Power.Changed -= OnChanged;
    }

    protected virtual void OnChanged()
    {
        if (Power.Value > 0)
            Text.color = PositiveColorPower;
        else if (Power.Value < 0)
            Text.color = NegativeColorPower;
        else
            Text.color = NeutralColorPower;

        Text.text = $"{Power.Value}";
    }
}