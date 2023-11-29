using Agava.YandexGames;
using UnityEngine;
using UnityEngine.Events;

public class LeaderboardOpenButton : MonoBehaviour
{
    [SerializeField] private UnityEvent _authorized;
    [SerializeField] private UnityEvent _authorizationRequested;
    [SerializeField] private UnityEvent _authorizationPerformed;

    public void TryOpenLeaderboard()
    {
        if (PlayerAccount.IsAuthorized)
        {
            PlayerAccount.RequestPersonalProfileDataPermission();
            Debug.Log("RequestPersonalProfileDataPermission");
            _authorized?.Invoke();
            return;
        }

        _authorizationRequested?.Invoke();
    }

    public void TryAuthorize()
    {
        PlayerAccount.Authorize(() => _authorizationPerformed?.Invoke());
    }
}