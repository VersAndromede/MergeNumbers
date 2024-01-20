using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class BossHealth : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private float _invulnerabilityTime;

    private Health _health;
    private Coroutine _makeVulnerableJob;
    private WaitForSeconds _rechargeTime;
    private int _damageTaken;
    private bool _isInvulnerable = true;

    public event Action<int> DamageReceived;

    public event Action<float> InvulnerabilityHasBegun;
    public event Action Died;

    public int HealthCount => _health.Value;

    public void Init(HealthSetup healthSetup, int maxHealth, int damageTaken)
    {
        if (damageTaken < 0)
            throw new ArgumentOutOfRangeException();

        _rechargeTime = new WaitForSeconds(_invulnerabilityTime);
        _damageTaken = damageTaken;
        _health = new Health((uint)maxHealth);
        healthSetup.Init(_health);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_isInvulnerable == false && _health.IsDied == false)
        {
            _health.TakeDamage(_damageTaken);
            DamageReceived?.Invoke(_damageTaken);
            _isInvulnerable = true;
            _makeVulnerableJob = StartCoroutine(MakeVulnerable());
            InvulnerabilityHasBegun.Invoke(_invulnerabilityTime);

            if (_health.IsDied)
                Died?.Invoke();
        }
    }

    public IEnumerator MakeVulnerable()
    {
        yield return _rechargeTime;
        _isInvulnerable = false;
    }

    public void MakeInvulnerable()
    {
        if (_makeVulnerableJob != null)
            StopCoroutine(_makeVulnerableJob);

        _isInvulnerable = true;
    }
}
