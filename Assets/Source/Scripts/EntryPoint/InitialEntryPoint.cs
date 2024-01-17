using Agava.YandexGames;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitialEntryPoint : MonoBehaviour
{
    private const string GameSceneName = "Game";

    private IEnumerator Start()
    {
        if (Application.isEditor == false)
        {
            yield return YandexGamesSdk.Initialize();
            SceneManager.LoadScene(GameSceneName);
        }
    }
}
