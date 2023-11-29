using UnityEngine;

public class MonsterPowerView : PowerView
{    [field: SerializeField] public Monster Monster { get; private set; }

    protected override void OnChanged()
    {
        if (Monster.Type == MonsterType.Divider)
        {
            Text.color = NegativeColorPower;
            Text.text = $"/{Power.Value}";
            return;
        }

        base.OnChanged();
    }
}
