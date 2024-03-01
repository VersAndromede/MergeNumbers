using Agava.YandexGames;
using UnityEngine;
using UnityEngine.Events;

namespace Scripts.MainLeaderboard
{
    public class LeaderboardOpenButton : MonoBehaviour
    {
        [SerializeField] private UnityEvent _authorized;
        [SerializeField] private UnityEvent _authorizationRequested;

        public void OpenLeaderboard()
        {
            if (PlayerAccount.IsAuthorized)
            {
                PlayerAccount.RequestPersonalProfileDataPermission();
                _authorized?.Invoke();
                return;
            }

            _authorizationRequested?.Invoke();
        }
    }
}