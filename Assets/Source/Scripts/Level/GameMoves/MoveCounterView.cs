using TMPro;
using UnityEngine;

public class MoveCounterView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private MoveCounter _moveCounter;

    private void Start()
    {
        OnChanged();
    }

    private void OnEnable()
    {
        _moveCounter.Changed += OnChanged;
    }

    private void OnDisable()
    {
        _moveCounter.Changed -= OnChanged;
    }

    private void OnChanged()
    {
        _text.text = _moveCounter.Count.ToString();
    }
}
