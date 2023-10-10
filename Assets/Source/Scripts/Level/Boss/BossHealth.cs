using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class BossHealth : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private float _invulnerabilityTime;

    [field: SerializeField] public Health Health { get; private set; }

    private Coroutine _makeVulnerableJob;
    private WaitForSeconds _rechargeTime;
    private int _damageTaken;
    private bool _isInvulnerable = true;

    public event Action<int> DamageReceived;

    public void Init(int maxHealth, int damageTaken)
    {
        if (damageTaken < 0)
            throw new ArgumentOutOfRangeException();

        _rechargeTime = new WaitForSeconds(_invulnerabilityTime);
        _damageTaken = damageTaken;
        Health.Init(maxHealth);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_isInvulnerable == false)
        {
            Health.TakeDamage(_damageTaken);
            DamageReceived?.Invoke(_damageTaken);
            _isInvulnerable = true;
            _makeVulnerableJob = StartCoroutine(MakeVulnerable());
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
