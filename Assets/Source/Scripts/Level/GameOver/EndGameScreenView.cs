using Lean.Localization;
using TMPro;
using UnityEngine;

namespace Scripts.Level.GameOver
{
    public class EndGameScreenView : MonoBehaviour
    {
        [SerializeField] private GameOverHandler _gameOverHandler;
        [SerializeField] private LeanLocalizedTextMeshProUGUI _localizedTextMeshPro;
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private ParticleSystem _particle;

        [Header("Translation Victory")]
        [LeanTranslationName]
        [SerializeField] private string _translationVictory;

        [Header("Translation Defeat")]
        [LeanTranslationName]
        [SerializeField] private string _translationDefeat;

        private void OnEnable()
        {
            _gameOverHandler.GameOver += OnGameOver;
        }

        private void OnDisable()
        {
            _gameOverHandler.GameOver -= OnGameOver;
        }

        private void OnGameOver(Winner winner)
        {
            if (winner == Winner.Player)
            {
                _localizedTextMeshPro.TranslationName = LeanLocalization.GetTranslation(_translationVictory).Name;
                _particle.Play();
                return;
            }

            _localizedTextMeshPro.TranslationName = LeanLocalization.GetTranslation(_translationDefeat).Name;
        }
    }
}