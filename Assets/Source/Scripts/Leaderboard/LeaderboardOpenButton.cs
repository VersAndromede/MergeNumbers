using Agava.YandexGames;
using UnityEngine;
using UnityEngine.Events;

namespace MainLeaderboard
{
    public class LeaderboardOpenButton : MonoBehaviour
    {
        [SerializeField] private UnityEvent _authorized;
        [SerializeField] private UnityEvent _authorizationRequested;
        [SerializeField] private UnityEvent _authorizationPerformed;

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

        public void Authorize()
        {
            PlayerAccount.Authorize(() => _authorizationPerformed?.Invoke());
        }
    }
}