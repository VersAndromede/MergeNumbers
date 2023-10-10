using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _text;

    private void Start()
    {
        _slider.value = (float)_health.Value / _health.MaxValue;
        _text.text = $"{_health.Value}/{_health.MaxValue}";
    }

    private void OnDestroy()
    {
        _health.Changed -= OnHealthChanged;
    }

    public void Init(Health health)
    {
        _health = health;
        _health.Changed += OnHealthChanged;
    }

    private void OnHealthChanged()
    {
        const float Duration = 0.25f;

        float currentHealth = (float)_health.Value / _health.MaxValue;

        _slider.DOValue(currentHealth, Duration).SetEase(Ease.InOutCubic);
        _text.text = $"{_health.Value}/{_health.MaxValue}";
    }
}
