using UnityEngine;

namespace WalletSystem
{
    public class WalletSetup : MonoBehaviour
    {
        [SerializeField] private WalletView _menuWallet;
        [SerializeField] private WalletView _gameWallet;

        private WalletPresenter _menuWalletPresenter;
        private WalletPresenter _gameWalletPresenter;

        private void OnDestroy()
        {
            _menuWalletPresenter.Diasble();
            _gameWalletPresenter.Diasble();
        }

        public void Init(Wallet wallet)
        {
            _menuWalletPresenter = new WalletPresenter(wallet, _menuWallet);
            _gameWalletPresenter = new WalletPresenter(wallet, _gameWallet);
            _menuWalletPresenter.Enable();
            _gameWalletPresenter.Enable();
        }
    }
}