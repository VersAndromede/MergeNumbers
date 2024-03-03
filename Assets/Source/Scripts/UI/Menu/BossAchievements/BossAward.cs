using Scripts.WalletSystem;
using System;
using UnityEngine;

namespace Scripts.UI.Menu.BossAchievements
{
    [Serializable]
    public class BossAward
    {
        public BossAward(uint count, int id)
        {
            Count = count;
            Id = id;
        }

        public event Action<BossAward> Taken;

        [field: SerializeField] public uint Count { get; private set; }

        [field: SerializeField] public bool IsTaken { get; private set; }

        [field: SerializeField] public bool CanBeTaken { get; private set; }

        [field: SerializeField] public int Id { get; private set; }

        public void LetTake()
        {
            CanBeTaken = true;
        }

        public bool TryTake(Wallet wallet)
        {
            if (CanBeTaken == false || IsTaken)
                return false;

            wallet.AddCoins(Count);
            IsTaken = true;
            Taken?.Invoke(this);
            return true;
        }
    }
}