namespace Scripts.UpgradeSystem
{
    public interface IPriceChangeAlgorithm
    {
        int GetChanged(int currentPrice);
    }
}