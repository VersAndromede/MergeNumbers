using System.Collections;
using Agava.YandexGames;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.EntryPoint
{
    public class PreGameEntryPoint : MonoBehaviour
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
}