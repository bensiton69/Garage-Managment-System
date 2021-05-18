namespace Ex03.GarageLogic
{
    public class MotorizedMotorcycle: Motorcycle, IMotorized
    {
        public GarageEnums.eFuelType FuelType { get; set; }

        public float CurrentAmountOfFuel { get; set; }

        public float MaxAmountOfFuel { get; set; }

        public bool FillFuel(GarageEnums.eFuelType i_FuelType, float i_AmountOfFuelToRefuel)
        {
            CurrentAmountOfFuel += i_AmountOfFuelToRefuel;
            return true;
        }

        public void AddMotorizedFields(float i_CurrentAmountOfFuel)
        {
            CurrentAmountOfFuel += i_CurrentAmountOfFuel;
        }

        public MotorizedMotorcycle(string i_ModelName, string i_LicenseNumber, float i_EnergyLeft, string i_WheelManufacturerName)
            : base(i_ModelName, i_LicenseNumber, i_EnergyLeft, i_WheelManufacturerName)
        {
            FuelType = GarageEnums.eFuelType.Octan98;
            MaxAmountOfFuel = 6;
        }
    }
}
