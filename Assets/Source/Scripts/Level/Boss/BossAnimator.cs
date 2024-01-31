using DG.Tweening;
using UnityEngine;

public class BossAnimator : MonoBehaviour
{
    [SerializeField] private Boss _boss;
    [SerializeField] private Animator _animator;
    [SerializeField] private BossAudioSystem _bossAudioSystem;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private float _punchDurationOnHit;

    private readonly int _wakesUpAnimation = Animator.StringToHash("Boss Wakes Up");
    private readonly int _dieAnimation = Animator.StringToHash("Boss Die");

    private MoveCounter _moveCounter;
    private Vector3 _punchPower;
    private Tween _doPunchScaleTween;

    public void Init(MoveCounter moveCounter)
    {
        _moveCounter = moveCounter;
        _moveCounter.Ended += OnGameMovesEnded;
        _boss.BossHealth.Died += OnBossDied;
        _boss.BossHealth.DamageReceived += OnDamageReceived;
        _punchPower = new Vector3(_punchDurationOnHit, _punchDurationOnHit, _punchDurationOnHit);
    }

    private void OnDestroy()
    {
        _moveCounter.Ended -= OnGameMovesEnded;
        _boss.BossHealth.Died -= OnBossDied;
        _boss.BossHealth.DamageReceived -= OnDamageReceived;
    }

    private void OnGameMovesEnded()
    {
        _animator.Play(_wakesUpAnimation);
    }

    private void OnBossDied()
    {
        _animator.Play(_dieAnimation);
        _particleSystem.Play();
    }

    private void OnDamageReceived(int damage)
    {
        _bossAudioSystem.PlayHit();
        _doPunchScaleTween?.Kill();
        _doPunchScaleTween = transform.DOPunchScale(_punchPower, _punchDurationOnHit).SetEase(Ease.Linear);
    }
}