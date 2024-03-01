using Agava.YandexGames;
using UnityEngine;
using UnityEngine.Events;

namespace Scripts.MainLeaderboard
{
    public class AuthorizationService : MonoBehaviour
    {
        [SerializeField] private UnityEvent _authorizationPerformed;

        public void Authorize()
        {
            PlayerAccount.Authorize(() => _authorizationPerformed?.Invoke());
        }
    }
}