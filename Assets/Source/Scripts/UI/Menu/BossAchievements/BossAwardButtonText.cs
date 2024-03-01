using Lean.Localization;
using TMPro;
using UnityEngine;

namespace Scripts.UI.Menu.BossAchievements
{
    public class BossAwardButtonText : MonoBehaviour
    {
        [SerializeField] private BossAwardButton _bossAwardButton;
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private LeanLocalizedTextMeshProUGUI _localizedTextMeshPro;

        [Header("Translation Received String")]
        [LeanTranslationName] 
        [SerializeField] private string _translationReceivedString;

        [Header("Translation Take String")]
        [LeanTranslationName] 
        [SerializeField] private string _translationTakeString;

        private void Start()
        {
            _bossAwardButton.Initialized += OnInitialized;
            _bossAwardButton.AwardReceived += OnAwardReceived;
        }

        private void OnDestroy()
        {
            _bossAwardButton.Initialized -= OnInitialized;
            _bossAwardButton.AwardReceived -= OnAwardReceived;
        }

        private void UpdateText()
        {
            if (_bossAwardButton.AwardTaken)
                _localizedTextMeshPro.TranslationName = LeanLocalization.GetTranslation(_translationReceivedString).Name;
            else
                _localizedTextMeshPro.TranslationName = LeanLocalization.GetTranslation(_translationTakeString).Name;
        }

        private void OnInitialized()
        {
            UpdateText();
        }

        private void OnAwardReceived()
        {
            UpdateText();
        }
    }
}
