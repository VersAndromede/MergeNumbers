using Scripts.UI.Menu;
using UnityEngine;
using UnityEngine.Events;

namespace Scripts.Ad
{
    public class RewardButton : PressedButton
    {
        [SerializeField] private UnityEvent _rewardReceived;

        public void ReportViewedVideoAd()
        {
            _rewardReceived?.Invoke();
        }
    }
}