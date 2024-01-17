using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReturnToMenuButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private InterstitialAdsDisplay _InterstitialAdsDisplay;

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(ReturnToMenu);
    }

    public void Init()
    {
        _button.onClick.AddListener(ReturnToMenu);
    }

    private void ReturnToMenu()
    {
        _InterstitialAdsDisplay.TryShowAd(() =>
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        });
    }
}
