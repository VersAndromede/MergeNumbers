using Agava.YandexGames;
using System.Collections;
using UnityEngine;

public class InterstitialAdsDisplay : MonoBehaviour
{
    [SerializeField] private GameMoves _gameMoves;
    [SerializeField] private int _waitTimeInSeconds;

    private int _currentTimeInSeconds;
    private Coroutine _startWaitTimerJob;
    private WaitForSeconds _waiting;

    private void Start()
    {
        _waiting = new WaitForSeconds(_waitTimeInSeconds);
        _startWaitTimerJob = StartCoroutine(StartWaitTimer());
    }

    public void OnEnable()
    {
        _gameMoves.Ended += OnMovesEnded;
    }

    public void OnDisable()
    {
        _gameMoves.Ended -= OnMovesEnded;
    }

    public void OnMovesEnded()
    {
        if (_currentTimeInSeconds >= _waitTimeInSeconds)
        {
            StopCoroutine(_startWaitTimerJob);
            _currentTimeInSeconds = 0;
            InterstitialAd.Show();
            _startWaitTimerJob = StartCoroutine(StartWaitTimer());
        }
    }

    private IEnumerator StartWaitTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            _currentTimeInSeconds++;
        }
    }
}