namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        private GarageEnums.eColor m_Color;
        private GarageEnums.eNumberOfDoor m_NumberOfDoor;

        public GarageEnums.eColor Color => m_Color;

        public GarageEnums.eNumberOfDoor NumberOfDoors => m_NumberOfDoor;
       
        public Car(
            string i_ModelName,
            string i_LicenseNumber,
            float i_EnergyLeft,
            string i_WheelManufacturerName)
            : base(i_ModelName, i_LicenseNumber, i_EnergyLeft, i_WheelManufacturerName, 4, 32)
        {
        }

        public void AddCarFields(GarageEnums.eColor i_Color, GarageEnums.eNumberOfDoor i_NumberOfDoor)
        {
            m_NumberOfDoor = i_NumberOfDoor;
            m_Color = i_Color;
        }
    }
}
