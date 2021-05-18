namespace Ex03.GarageLogic
{
    public class MotorizedCar: Car,IMotorized
    {
        public GarageEnums.eFuelType FuelType { get; set; }

        public float CurrentAmountOfFuel { get; set; }

        public float MaxAmountOfFuel { get; set; }

        public bool FillFuel(GarageEnums.eFuelType i_FuelType, float i_AmountOfFuel)
        {

            CurrentAmountOfFuel += i_AmountOfFuel;
            return true;
        }

        public void AddMotorizedFields(float i_CurrentAmountOfFuel)
        {
            CurrentAmountOfFuel = i_CurrentAmountOfFuel;
        }

        public MotorizedCar(
            string i_ModelName,
            string i_LicenseNumber,
            float i_EnergyLeft,
            string i_WheelManufacturerName)
            : base(i_ModelName, i_LicenseNumber, i_EnergyLeft, i_WheelManufacturerName)
        {
            FuelType = GarageEnums.eFuelType.Octan95;
            MaxAmountOfFuel = 45;
        }
    }
}
