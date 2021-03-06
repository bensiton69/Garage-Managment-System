namespace Ex03.GarageLogic
{
    public class ElectricCar : Car, IElectrical
    {
        public float BatteryTimeLeft { get; set; }

        public float MaxBatteryTime { get; set; }

        public bool FillBattery(float i_AmountOfTimeToCharge)
        {
            BatteryTimeLeft += i_AmountOfTimeToCharge;
            return true;
        }

        public void AddElectricalFields(float i_BatteryTimeLeft)
        {
            BatteryTimeLeft = i_BatteryTimeLeft;
        }

        public ElectricCar(
            string i_ModelName,
            string i_LicenseNumber,
            float i_EnergyLeft,
            string i_WheelManufacturerName)
            : base(i_ModelName, i_LicenseNumber, i_EnergyLeft, i_WheelManufacturerName)
        {
            MaxBatteryTime = 3.2f;
        }
    }
}
