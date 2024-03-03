using Lean.Localization;
using Scripts.Level.BossSystem;
using Scripts.WalletSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.Menu.BossAchievements
{
    public class BossMapItem : MonoBehaviour
    {
        [SerializeField] private LeanLocalizedTextMeshProUGUI _localizedTextMeshPro;
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _health;
        [SerializeField] private TextMeshProUGUI _damage;
        [SerializeField] private TextMeshProUGUI _award;
        [SerializeField] private BossAwardButton _button;
        [SerializeField] private GameObject _unlockContainer;
        [SerializeField] private GameObject _lockContainer;
        [SerializeField] private Image _panel;
        [SerializeField] private Color _lockPanelColor;
        [SerializeField] private Color _unlockPanelColor;

        public void Init(BossData bossData, Wallet wallet, BossAward bossAward)
        {
            _localizedTextMeshPro.TranslationName = LeanLocalization.GetTranslation(bossData.TranslationName).Name;
            _health.text = bossData.Health.ToString();
            _damage.text = bossData.Damage.ToString();
            _award.text = bossData.Award.ToString();
            _button.Init(bossAward, wallet);
        }

        public void Lock()
        {
            _lockContainer.SetActive(true);
            _unlockContainer.SetActive(false);
            _panel.color = _lockPanelColor;
        }

        public void Unlock()
        {
            _lockContainer.SetActive(false);
            _unlockContainer.SetActive(true);
            _panel.color = _unlockPanelColor;
        }
    }
}