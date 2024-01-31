using TMPro;
using UnityEngine;

namespace BossSystem
{
    public class BossDamageView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;

        public void Init(Boss boss)
        {
            _text.text = boss.Data.Damage.ToString();
        }
    }
}