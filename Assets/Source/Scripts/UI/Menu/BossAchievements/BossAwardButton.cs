using Scripts.WalletSystem;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Scripts.UI.Menu.BossAchievements
{
    public class BossAwardButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private UnityEvent _taken;

        private BossAward _bossAward;
        private Wallet _wallet;

        public event Action Initialized;
        public event Action AwardReceived;

        public bool AwardTaken => _bossAward.IsTaken;

        public void Init(BossAward bossAward, Wallet wallet)
        {
            _bossAward = bossAward;

            if (_bossAward.CanBeTaken == false)
                Destroy(gameObject);

            _wallet = wallet;
            Initialized?.Invoke();

            if (_bossAward.IsTaken)
                _button.interactable = false;
        }

        public void GetAward()
        {
            if (_bossAward.TryTake(_wallet))
            {
                _button.interactable = false;
                AwardReceived?.Invoke();
                _taken?.Invoke();
            }
        }
    }
}
