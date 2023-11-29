using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private GameMoves _gameMoves;
    [SerializeField] private UnityEvent _died;

    [field: SerializeField] public PlayerMovement Movement { get; private set; }
    [field: SerializeField] public PlayerRotation Rotation { get; private set; }
    [field: SerializeField] public Power Power { get; private set; }
    [field: SerializeField] public MonsterObserver Observer { get; private set; }
    [field: SerializeField] public Health Health { get; private set; }
    [field: SerializeField] public int Damage { get; private set; }

    private void OnEnable()
    {
        Movement.StartedMoving += OnStartedMoving;
        Power.Over += Die;
        Health.Died += Die;
        _gameMoves.Ended += OnGameMovesEnded;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Monster monster))
            Merge(monster);
    }

    private void OnDisable()
    {
        Movement.StartedMoving -= OnStartedMoving;
        Power.Over -= Die;
        Health.Died -= Die;
        _gameMoves.Ended -= OnGameMovesEnded;
    }

    public void UpgradeDamage(DamageUpgrade damageUpgrade)
    {
        Damage += damageUpgrade.BonusValue;
    }

    private void Merge(Monster monster)
    {
        monster.Die();

        if (monster.Type == MonsterType.Adding)
            Power.Add(monster.Power.Value);
        else if (monster.Type == MonsterType.Divider)
            Power.Divide(monster.Power.Value);
    }

    private void OnStartedMoving()
    {
        Observer.Clear();
    }

    private void Die()
    {
        _died?.Invoke();
    }

    private void OnGameMovesEnded()
    {
        Health.Init(Power.Value);
    }
}