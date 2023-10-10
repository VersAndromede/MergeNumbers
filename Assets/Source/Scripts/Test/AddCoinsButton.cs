using UnityEngine;

public class AddCoinsButton : MonoBehaviour
{
    [SerializeField] private Wallet _wallet;

    public void Add(int count)
    {
        _wallet.AddCoins((uint)count);
        // _wallet.Save();
    }
}
