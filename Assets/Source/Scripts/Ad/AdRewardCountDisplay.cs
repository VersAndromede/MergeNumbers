using TMPro;
using UnityEngine;

namespace Scripts.Ad
{
    public class AdRewardCountDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        
        private RewardForVideoAd _rewardForVideoAd;

        private void OnDestroy()
        {
            _rewardForVideoAd.Changed -= OnChanged;
        }

        public void Init(RewardForVideoAd rewardForVideoAd)
        {
            _rewardForVideoAd = rewardForVideoAd;
            _rewardForVideoAd.Changed += OnChanged;
        }

        private void OnChanged()
        {
            _text.text = $"+{_rewardForVideoAd.Count}";
        }
    }
}