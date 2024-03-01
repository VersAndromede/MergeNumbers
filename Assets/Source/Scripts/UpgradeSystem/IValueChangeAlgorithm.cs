namespace Scripts.UpgradeSystem
{
    public interface IValueChangeAlgorithm
    {
        int GetChanged(int currentValue);
    }
}