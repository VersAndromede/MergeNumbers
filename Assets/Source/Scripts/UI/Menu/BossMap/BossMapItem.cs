using Lean.Localization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    public void SetLocked(bool value)
    {
        if (value)
        {
            SetLocked(true, false, _lockPanelColor);
            return;
        }

        SetLocked(false, true, _unlockPanelColor);
    }

    private void SetLocked(bool lockEnabled, bool unlockEnabled, Color color)
    {
        _lockContainer.SetActive(lockEnabled);
        _unlockContainer.SetActive(unlockEnabled);
        _panel.color = color;
    }
}
