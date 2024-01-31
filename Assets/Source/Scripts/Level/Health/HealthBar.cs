using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

namespace HealthSystem
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private TextMeshProUGUI _text;

        public void UpdateUIWithInstantTransition(int count, int maxCount)
        {
            _slider.value = GetHealthUpdate(count, maxCount);
        }

        public void UpdateUIWithSmoothTransition(int count, int maxCount)
        {
            const float Duration = 0.25f;

            float currentHealth = GetHealthUpdate(count, maxCount);
            _slider.DOValue(currentHealth, Duration).SetEase(Ease.InOutCubic);
        }

        private float GetHealthUpdate(int count, int maxCount)
        {
            float currentHealth = (float)count / maxCount;
            _text.text = $"{count}/{maxCount}";
            return currentHealth;
        }
    }
}