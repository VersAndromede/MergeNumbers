using Scripts.Level.MonsterSystem;
using UnityEngine;

namespace Scripts.Level.PowerSystem
{
    public class MonsterPowerView : PowerView
    {
        private const string FirstSymbolForDivider = "/";

        [SerializeField] private Monster _monster;

        private void Start()
        {
            switch (_monster)
            {
                case MonsterAdding:
                    UpdateView(PowerColorsConfig.Positive);
                    break;
                case MonsterSubtractive:
                    UpdateView(PowerColorsConfig.Negative);
                    break;
                case MonsterDivider:
                    UpdateView(PowerColorsConfig.Negative, FirstSymbolForDivider);
                    break;
            }
        }

        private void UpdateView(Color textColor, string firstSymbol = "")
        {
            SetColor(textColor);
            SetValue($"{firstSymbol}{_monster.PowerCount}");
        }
    }
}