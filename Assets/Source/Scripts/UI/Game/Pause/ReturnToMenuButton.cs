using Ad;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GameButtons
{
    public class ReturnToMenuButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private InterstitialAdsDisplay _interstitialAdsDisplay;

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnReturnToMenu);
        }

        public void Init()
        {
            _button.onClick.AddListener(OnReturnToMenu);
        }

        private void OnReturnToMenu()
        {
            _interstitialAdsDisplay.ShowAd(() =>
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            });
        }
    }
}
