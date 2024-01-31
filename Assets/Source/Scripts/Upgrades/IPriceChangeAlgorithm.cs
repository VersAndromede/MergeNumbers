namespace Upgrades
{
    public interface IPriceChangeAlgorithm
    {
        int GetChanged(int currentPrice);
    }
}