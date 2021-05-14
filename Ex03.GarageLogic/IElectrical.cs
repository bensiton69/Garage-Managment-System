namespace Ex03.GarageLogic
{
    public interface IElectrical
    {
        float BatteryTimeLeft
        {
            get;
            set;
        }

        float MaxBatteryTime
        {
            get;
            set;
        }
        bool FillBattery(float i_AmountOfTimeToCharge);

        void AddElectricalFields(float i_BatteryTimeLeft);
    }
}
