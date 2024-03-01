using System;
using UnityEngine;
using UnityEngine.Events;

namespace Scripts.UI.Menu
{
    public class GameStarter : MonoBehaviour
    {
        [SerializeField] private UnityEvent _gameStarted;

        public event Action GameStarted;

        public void StartGame()
        {
            GameStarted?.Invoke();
            _gameStarted?.Invoke();
        }
    }
}
