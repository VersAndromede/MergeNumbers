using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private GameMoves _gameMoves;
    [SerializeField] private PlayerMovement _movement;
    [SerializeField] private MonsterObserver _observer;
    [SerializeField] private HealthSetup _healthSetup;
    [SerializeField] private UnityEvent _died;

    [field: SerializeField] public int Damage { get; private set; }

    private Health _health;
    private Power _power;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Monster monster))
            Merge(monster);
    }

    private void OnDestroy()
    {
        _power.Over -= Die;
        _movement.StartedMoving -= OnStartedMoving;
        _gameMoves.Ended -= OnGameMovesEnded;
        
        if (_health != null)
            _health.Died -= Die;
    }

    public void Init(Health health, Power power)
    {
        _health = health;
        _power = power;

        _power.Over += Die;
        _movement.StartedMoving += OnStartedMoving;
        _gameMoves.Ended += OnGameMovesEnded;
    }

    public void UpgradeDamage(DamageUpgrade damageUpgrade)
    {
        Damage += damageUpgrade.BonusValue;
    }

    private void Merge(Monster monster)
    {
        monster.Die();
        monster.SetEffect(_power);
    }

    private void Die()
    {
        _died?.Invoke();
    }

    private void OnStartedMoving()
    {
        _observer.Clear();
    }

    private void OnGameMovesEnded()
    {
        _health.SetMax((uint)_power.Value);
        _healthSetup.Init(_health);
        _health.Died += Die;
    }
}