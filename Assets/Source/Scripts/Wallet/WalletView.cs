using TMPro;
using UnityEngine;

public class WalletView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    public void UpdateUI(int coins)
    {
        _text.text = coins.ToString();
    }
}
