using System;
using UnityEngine;
using UnityEngine.Events;

namespace Scripts.UI.Menu.BossAchievements
{
    public class BossAwardButton : PressedButton
    {
        [SerializeField] private UnityEvent _taken;

        public event Action Initialized;

        public void Init(bool awardCanBeTaken, bool awardIsTaken)
        {
            if (awardCanBeTaken == false)
                Destroy(gameObject);

            Initialized?.Invoke();

            if (awardIsTaken)
                DisableInteractable();
        }

        public void ReportReceipt()
        {
            _taken?.Invoke();
        }
    }
}
