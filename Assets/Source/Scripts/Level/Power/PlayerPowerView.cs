namespace PowerSystem
{
    public class PlayerPowerView : PowerView
    {
        public void UpdateUI(int powerValue)
        {
            switch (powerValue)
            {
                case > 0:
                    SetColor(PowerColorsConfig.Positive);
                    break;
                case < 0:
                    SetColor(PowerColorsConfig.Negative);
                    break;
                default:
                    SetColor(PowerColorsConfig.Neutral);
                    break;
            }

            SetValue($"{powerValue}");
        }
    }
}