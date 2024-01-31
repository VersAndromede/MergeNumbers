using TMPro;
using UnityEngine;

namespace Ad
{
    public class NumberVideoAdReward : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private RewardButton _rewardButton;

        private void OnEnable()
        {
            _rewardButton.RewardChanged += OnRewardChanged;
        }

        private void OnDisable()
        {
            _rewardButton.RewardChanged -= OnRewardChanged;
        }

        private void OnRewardChanged()
        {
            _text.text = $"+{_rewardButton.RewardCount}";
        }
    }
}