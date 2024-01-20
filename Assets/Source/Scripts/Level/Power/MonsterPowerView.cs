using TMPro;
using UnityEngine;

public class MonsterPowerView : MonoBehaviour
{
    [SerializeField] private Monster _monster;
    [SerializeField] private TextMeshPro _text;
    [SerializeField] private Color _negativeColorPower;
    [SerializeField] private Color _positiveColorPower;

    private void Start()
    {
        if (_monster is MonsterSubtractive)
        {
            _text.color = _negativeColorPower;
            _text.text = $"{_monster.PowerCount}";
        }
        else if (_monster is MonsterDivider)
        {
            _text.color = _negativeColorPower;
            _text.text = $"/{_monster.PowerCount}";
        }
        else
        {
            _text.color = _positiveColorPower;
            _text.text = $"{_monster.PowerCount}";
        }
    }
}

