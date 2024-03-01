using DG.Tweening;
using UnityEngine;

namespace Scripts.Level.BossSystem
{
    public class BossTakeDamageAnimation : MonoBehaviour
    {
        [SerializeField] private Boss _boss;
        [SerializeField] private BossAudioSystem _bossAudioSystem;
        [SerializeField] private float _punchDurationOnHit;

        private Vector3 _punchPower;
        private Tween _doPunchScaleTween;

        public void Start()
        {
            _punchPower = new Vector3(_punchDurationOnHit, _punchDurationOnHit, _punchDurationOnHit);
            _boss.BossHealth.DamageReceived += OnDamageReceived;
        }

        private void OnDestroy()
        {
            _boss.BossHealth.DamageReceived -= OnDamageReceived;
        }

        private void OnDamageReceived(int damage)
        {
            _bossAudioSystem.PlayHit();
            _doPunchScaleTween?.Kill();
            _doPunchScaleTween = transform.DOPunchScale(_punchPower, _punchDurationOnHit).SetEase(Ease.Linear);
        }
    }
}