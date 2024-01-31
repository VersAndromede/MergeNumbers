using System;
using UnityEngine;
using WalletSystem;

namespace BossAchievements 
{
    [Serializable]
    public class BossAward
    {
        [field: SerializeField] public uint Count { get; private set; }
        [field: SerializeField] public bool IsTaken { get; private set; }
        [field: SerializeField] public bool CanBeTaken { get; private set; }
        [field: SerializeField] public int Id { get; private set; }

        public event Action<BossAward> Taken;

        public BossAward(uint count, int id)
        {
            Count = count;
            Id = id;
        }

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