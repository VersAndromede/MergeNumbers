using UnityEngine;
using Scripts.UpgradeSystem;

namespace Scripts.Level.PlayerSystem
{
    public class PlayerDamage : MonoBehaviour
    {
        [field: SerializeField] public int Count { get; private set; }

        public void Upgrade(UpgradeWithMultiplicationValuePolicy damageUpgrade)
        {
            Count += damageUpgrade.BonusValue;
        }
    }
}