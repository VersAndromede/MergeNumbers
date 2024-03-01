using UnityEngine;

namespace Scripts.WalletSystem
{
    public class WalletInitialization : MonoBehaviour
    {
        [SerializeField] private WalletSetup _walletSetup;

        public Wallet Wallet { get; private set; }

        public void Init(int coins)
        {
            Wallet wallet = new Wallet();
            _walletSetup.Init(wallet);
            wallet.Init(coins);
        }
    }
}