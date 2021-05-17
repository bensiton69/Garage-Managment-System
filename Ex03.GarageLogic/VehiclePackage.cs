namespace Ex03.GarageLogic
{
    public class VehiclePackage
    {
        private string m_OwnerFullName;
        private string m_OwnerPhoneNumber;
        private Vehicle m_Vehicle;
        private GarageEnums.eVehicleStatus m_Status = GarageEnums.eVehicleStatus.InProgress;
        public GarageEnums.eVehicleStatus Status => m_Status;
        public void SetStatus(GarageEnums.eVehicleStatus i_VehicleStatus)
        {
            m_Status = i_VehicleStatus;
        }
        public Vehicle Vehicle
        {
            get
            {
                return m_Vehicle;
            }
        }
        public string OwnerFullName
        {
            get
            {
                return m_OwnerFullName;
            }
        }
        public string OwnerPhoneNumber
        {
            get
            {
                return m_OwnerPhoneNumber;
            }
        }
        public VehiclePackage(string i_OwnerFullName, string i_OwnerPhoneNumber, Vehicle i_Vehicle)
        {
            m_OwnerFullName = i_OwnerFullName;
            m_OwnerPhoneNumber = i_OwnerPhoneNumber;
            m_Vehicle = i_Vehicle;
        }
        public (string, string, GarageEnums.eVehicleStatus) GetOwnerInfo()
        {
            return (m_OwnerFullName, m_OwnerPhoneNumber, m_Status);
        }
    }
}
