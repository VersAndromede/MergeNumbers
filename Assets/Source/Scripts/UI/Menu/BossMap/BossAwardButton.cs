using Lean.Localization;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using WalletSystem;

namespace BossAchievements
{
    public class BossAwardButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private LeanLocalizedTextMeshProUGUI _localizedTextMeshPro;
        [SerializeField] private UnityEvent _taken;

        [Header("Translation Received String")]
        [SerializeField, LeanTranslationName] private string _translationReceivedString;
        [Header("Translation Take String")]
        [SerializeField, LeanTranslationName] private string _translationTakeString;

        private BossAward _bossAward;
        private Wallet _wallet;

        public void Init(BossAward bossAward, Wallet wallet)
        {
            _bossAward = bossAward;

            if (_bossAward.CanBeTaken == false)
                Destroy(gameObject);

            _wallet = wallet;
            UpdateText();

            if (_bossAward.IsTaken)
                _button.interactable = false;
        }

        public void GetAward()
        {
            if (_bossAward.TryTake(_wallet))
            {
                _button.interactable = false;
                UpdateText();
                _taken?.Invoke();
            }
        }

        private void UpdateText()
        {
            if (_bossAward.IsTaken)
                _localizedTextMeshPro.TranslationName = LeanLocalization.GetTranslation(_translationReceivedString).Name;
            else
                _localizedTextMeshPro.TranslationName = LeanLocalization.GetTranslation(_translationTakeString).Name;
        }
    }
}
