using BossSystem;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace HitDamage
{
    public class AttackReadinessIndicator : MonoBehaviour
    {
        [SerializeField] private Slider _indicator;

        private BossHealth _bossHealth;
        private Tween _doValueIndicatorJob;

        private void OnDestroy()
        {
            _bossHealth.InvulnerabilityHasBegun -= OnInvulnerabilityHasBegun;
        }

        public void Init(BossHealth bossHealth)
        {
            _bossHealth = bossHealth;
            _bossHealth.InvulnerabilityHasBegun += OnInvulnerabilityHasBegun;
        }

        private void OnInvulnerabilityHasBegun(float invulnerabilityTime)
        {
            _doValueIndicatorJob?.Kill();
            _indicator.gameObject.SetActive(true);
            _indicator.value = 0;
            _doValueIndicatorJob = _indicator.DOValue(1, invulnerabilityTime).SetEase(Ease.Linear).OnComplete(() =>
            {
                _indicator.gameObject.SetActive(false);
            });
        }
    }
}