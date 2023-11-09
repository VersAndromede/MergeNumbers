using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public enum Winner
{
    Player,
    Boss
}

public class GameOverController : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private float _delayToResult;
    [SerializeField] private UnityEvent _gameOver;
    [SerializeField] private UnityEvent _winning;
    [SerializeField] private UnityEvent _defeat;

    private WaitForSeconds _waitTime;
    private Boss _boss;

    public event Action<Winner> GameOver;

    public void Init(Boss boss)
    {
        _boss = boss;
        _waitTime = new WaitForSeconds(_delayToResult);

        _player.Health.Died += OnPlayerDied;
        _player.Power.Over += OnPlayerDied;
        _boss.BossHealth.Health.Died += OnBossDied;
    }

    private void OnDestroy()
    {
        _player.Health.Died -= OnPlayerDied;
        _player.Power.Over -= OnPlayerDied;
        _boss.BossHealth.Health.Died -= OnBossDied;
    }

    private void OnBossDied()
    {
        StartCoroutine(AssignVictory(Winner.Player));
    }

    private void OnPlayerDied()
    {
        _boss.BossHealth.MakeInvulnerable();
        StartCoroutine(AssignVictory(Winner.Boss));
    }

    private IEnumerator AssignVictory(Winner winner)
    {
        yield return _waitTime;
        GameOver?.Invoke(winner);
        _gameOver?.Invoke();
        yield return new WaitForFixedUpdate();

        if (winner == Winner.Player)
            _winning?.Invoke();
        else
            _defeat?.Invoke();
    }
}
