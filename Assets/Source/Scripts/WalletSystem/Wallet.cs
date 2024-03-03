using System;

namespace Scripts.WalletSystem
{
    public class Wallet
    {
        public event Action CoinsChanged;
        public event Action Loaded;

        public int Coins { get; private set; }

        public void Init(int coins)
        {
            Coins = coins;
            Loaded?.Invoke();
        }

        public void AddCoins(uint count)
        {
            ChangeCoins((int)count);
        }

        public void RemoveCoins(uint count)
        {
            ChangeCoins((int)-count);
        }

        public bool IsSolvent(int price)
        {
            return Coins >= price;
        }

        private void ChangeCoins(int count)
        {
            Coins += count;
            CoinsChanged?.Invoke();
        }
    }
}