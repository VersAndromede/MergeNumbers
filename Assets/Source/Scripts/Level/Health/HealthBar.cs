using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _text;

    public void UpdateUI(int count, int maxCount)
    {
        _slider.value = (float)count / maxCount;
        _text.text = $"{count}/{maxCount}";
    }

    public void UpdateSmoothUI(int count, int maxCount)
    {
        const float Duration = 0.25f;

        float currentHealth = (float)count / maxCount;

        _slider.DOValue(currentHealth, Duration).SetEase(Ease.InOutCubic);
        _text.text = $"{count}/{maxCount}";
    }
}
