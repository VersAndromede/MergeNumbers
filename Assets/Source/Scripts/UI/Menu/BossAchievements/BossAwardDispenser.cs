using System;
using Scripts.WalletSystem;

namespace Scripts.UI.Menu.BossAchievements
{
    public class BossAwardDispenser
    {
        private readonly BossAward _bossAward;
        private readonly Wallet _wallet;

        public BossAwardDispenser(BossAward bossAward, Wallet wallet)
        {
            _bossAward = bossAward;
            _wallet = wallet;
        }

        public event Action Received;

        public bool AwardTaken => _bossAward.IsTaken;

        public void Give()
        {
            if (_bossAward.TryTake(_wallet))
                Received?.Invoke();
        }
    }
}
