namespace Ex03.GarageLogic
{
    public class Truck : Vehicle, IMotorized
    {
        private bool m_IsDanger;
        private float m_MaxWeight;

        public bool IsDanger
        {
            get => m_IsDanger;
            private set => m_IsDanger = value;
        }

        public float MaxWeight
        {
            get => m_MaxWeight;
            private set => m_MaxWeight = value;
        }

        public GarageEnums.eFuelType FuelType { get; set; }

        public float CurrentAmountOfFuel { get; set; }

        public float MaxAmountOfFuel { get; set; }

        public bool FillFuel(GarageEnums.eFuelType i_FuelType, float i_AmountOfFuelToRefuel)
        {
            CurrentAmountOfFuel = i_AmountOfFuelToRefuel;
            return true;
        }

        public void AddMotorizedFields(float i_CurrentAmountOfFuel)
        {
            CurrentAmountOfFuel = i_CurrentAmountOfFuel;
        }

        public void AddTruckFields(bool i_IsDanger, float i_MaxWeight)
        {
            IsDanger = i_IsDanger;
            MaxWeight = i_MaxWeight;
        }

        public Truck(
            string i_ModelName,
            string i_LicenseNumber,
            float i_EnergyLeft,
            string i_WheelManufacturerName)
            : base(i_ModelName, i_LicenseNumber, i_EnergyLeft, i_WheelManufacturerName, 16, 26)
        {
            FuelType = GarageEnums.eFuelType.Soler;
            MaxAmountOfFuel = 120;
        }
    }
}
