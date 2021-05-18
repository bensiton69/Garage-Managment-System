namespace Ex03.GarageLogic
{
    public class Motorcycle: Vehicle
    {
        private GarageEnums.eLicenseType m_LicenseType ;
        private int m_EngineVolumeCC;

        public int EngineVolumeCC => m_EngineVolumeCC;
        public GarageEnums.eLicenseType LicenseType => m_LicenseType;

        public Motorcycle(
            string i_ModelName,
            string i_LicenseNumber,
            float i_EnergyLeft,
            string i_WheelManufacturerName)
            : base(i_ModelName, i_LicenseNumber, i_EnergyLeft, i_WheelManufacturerName, 2, 30)
        {

        }
        public void AddMotorcycleFields(GarageEnums.eLicenseType i_LicenseType, int i_MotorcycleVolume)
        {
            m_LicenseType = i_LicenseType;
            m_EngineVolumeCC = i_MotorcycleVolume;
        }
    }
}
