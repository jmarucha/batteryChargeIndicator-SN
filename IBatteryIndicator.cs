namespace batteryMod
{
    internal interface IBatteryIndicator
    {
        void Hide();
        void SetPercentage(float? val);
    }
}