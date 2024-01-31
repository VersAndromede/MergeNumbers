using Lean.Localization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Upgrades;

public class UpgradeView : MonoBehaviour
{
    [SerializeField] private Upgrade _upgrade;
    [SerializeField] private TextMeshProUGUI _textPrice;
    [SerializeField] private TextMeshProUGUI _textLevel;
    [SerializeField] private Button _button;
    [SerializeField] private Slider _slider;

    [Header("Translation Max Level")]
    [SerializeField, LeanTranslationName] private string _translationMaxLevel;
    [Header("Translation Level")]
    [SerializeField, LeanTranslationName] private string _translationLevel;

    private void OnEnable()
    {
        _upgrade.LevelChanged += OnLevelChanged;
    }

    private void Start()
    {
        OnLevelChanged();
    }

    private void OnDisable()
    {
        _upgrade.LevelChanged -= OnLevelChanged;
    }

    private void OnLevelChanged()
    {
        _slider.value = (float)_upgrade.Level / _upgrade.MaxLevel;
        UpdateLevelText();

        if (_upgrade.CanImprove)
        {
            _textPrice.text = _upgrade.Price.ToString();
            return;
        }

        _textPrice.text = "-";
        _button.interactable = false;
    }

    private void UpdateLevelText()
    {
        if (_upgrade.CanImprove)
            _textLevel.text = $"{_upgrade.Level} {LeanLocalization.GetTranslationText(_translationLevel)}";
        else
            _textLevel.text = LeanLocalization.GetTranslationText(_translationMaxLevel);
    }
}