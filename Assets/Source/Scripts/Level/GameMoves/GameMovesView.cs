using TMPro;
using UnityEngine;

public class GameMovesView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private GameMoves _gameMoves;

    private void Start()
    {
        OnChanged();
    }

    private void OnEnable()
    {
        _gameMoves.Changed += OnChanged;
    }

    private void OnDisable()
    {
        _gameMoves.Changed -= OnChanged;
    }

    private void OnChanged()
    {
        _text.text = _gameMoves.Count.ToString();
    }
}
