using Lean.Localization;
using TMPro;
using UnityEngine;

public class LeaderboardElement : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _rank;
    [SerializeField] private TextMeshProUGUI _score;
    [SerializeField] private LeanLocalizedTextMeshProUGUI _localizedAnonymousName;

    [Header("Translation Anonymous")]
    [SerializeField, LeanTranslationName] private string _translationAnonymous;

    public void Init(int rank, string name, int score)
    {
        _rank.text = rank.ToString();
        _score.text = score.ToString();

        if (name == Leaderboard.AnonymousName)
        {
            _localizedAnonymousName.TranslationName = LeanLocalization.GetTranslation(_translationAnonymous).Name;
            return;
        }

        _name.text = name;
    }
}
