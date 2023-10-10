public class WalletPresenter
{
    private readonly Wallet _wallet;
    private readonly WalletView _walletView;

    public WalletPresenter(Wallet wallet, WalletView walletView)
    {
        _wallet = wallet;
        _walletView = walletView;
    }

    public void Enable()
    {
        _wallet.Loaded += OnWalletChanged;
        _wallet.CoinsChanged += OnWalletChanged;
    }

    public void Diasble()
    {
        _wallet.Loaded -= OnWalletChanged;
        _wallet.CoinsChanged -= OnWalletChanged;
    }

    private void OnWalletChanged()
    {
        _walletView.UpdateUI(_wallet.Coins);
    }
}