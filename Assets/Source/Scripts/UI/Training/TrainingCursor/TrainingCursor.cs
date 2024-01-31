using BossSystem;
using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace TrainingSystem
{
    public class TrainingCursor : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Vector2 _punch;
        [SerializeField] private float _duration;
        [SerializeField] private float _daley;
        [SerializeField] private float _inFadeDuration;
        [SerializeField] private float _outFadeDuration;
        [SerializeField] private uint _hitCountOnBossToDestroy;

        private Coroutine _startAmimationJob;
        private WaitForSeconds _waitForSeconds;
        private BossHealth _bossHealth;
        private int _currentBossIndex;
        private uint _hitCountOnBoss;
        private bool _enabled;

        private void OnDestroy()
        {
            _bossHealth.DamageReceived -= OnBossHealthDamageReceived;
        }

        public void Init(BossHealth bossHealth, int currentBossIndex)
        {
            _bossHealth = bossHealth;
            _bossHealth.DamageReceived += OnBossHealthDamageReceived;
            _currentBossIndex = currentBossIndex;
            _spriteRenderer.color = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, 0);
        }

        public void Enable()
        {
            if (_currentBossIndex > 0)
            {
                Destroy(gameObject);
                return;
            }

            _enabled = true;
            _waitForSeconds = new WaitForSeconds(_daley);
            _startAmimationJob = StartCoroutine(StartAmimation());
        }

        private IEnumerator StartAmimation()
        {
            yield return _waitForSeconds;
            _spriteRenderer.DOFade(1, _inFadeDuration).SetEase(Ease.Linear);

            while (_enabled)
            {
                yield return transform.DOPunchScale(_punch, _duration).SetEase(Ease.Linear);
                yield return _waitForSeconds;
            }
        }

        private void OnBossHealthDamageReceived(int damage)
        {
            _hitCountOnBoss++;

            if (_hitCountOnBoss >= _hitCountOnBossToDestroy)
            {
                _spriteRenderer.DOFade(0, _outFadeDuration).SetEase(Ease.Linear).OnComplete(() =>
                {
                    _enabled = false;
                    StopCoroutine(_startAmimationJob);
                    Destroy(gameObject);
                });
            }
        }
    }
}
