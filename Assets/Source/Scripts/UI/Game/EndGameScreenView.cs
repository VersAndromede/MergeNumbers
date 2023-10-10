using Lean.Localization;
using TMPro;
using UnityEngine;

public class EndGameScreenView : MonoBehaviour
{
    [SerializeField] private GameOverController _victoryController;
    [SerializeField] private LeanLocalizedTextMeshProUGUI _localizedTextMeshPro;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private ParticleSystem _particle;

    [Header("Translation Victory")]
    [SerializeField, LeanTranslationName] private string _translationVictory;
    [Header("Translation Defeat")]
    [SerializeField, LeanTranslationName] private string _translationDefeat;

    private void OnEnable()
    {
        _victoryController.GameOver += OnGameOver;
    }

    private void OnDisable()
    {
        _victoryController.GameOver -= OnGameOver;
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
