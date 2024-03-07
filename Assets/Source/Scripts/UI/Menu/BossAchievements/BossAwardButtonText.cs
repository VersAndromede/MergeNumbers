using Lean.Localization;
using TMPro;
using UnityEngine;

namespace Scripts.UI.Menu.BossAchievements
{
    public class BossAwardButtonText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private LeanLocalizedTextMeshProUGUI _localizedTextMeshPro;

        [Header("Translation Received String")]
        [LeanTranslationName]
        [SerializeField] private string _translationReceivedString;

        [Header("Translation Take String")]
        [LeanTranslationName]
        [SerializeField] private string _translationTakeString;

        public void SetReceived()
        {
            _localizedTextMeshPro.TranslationName = LeanLocalization.GetTranslation(_translationReceivedString).Name;
        }

        public void SetTake()
        {
            _localizedTextMeshPro.TranslationName = LeanLocalization.GetTranslation(_translationTakeString).Name;
        }
    }
}
