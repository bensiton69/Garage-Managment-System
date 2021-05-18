namespace Ex03.GarageLogic
{
    public class VehiclePackage
    {
        private readonly string r_OwnerFullName;
        private readonly string r_OwnerPhoneNumber;
        private readonly Vehicle r_Vehicle;
        private GarageEnums.eVehicleStatus m_Status = GarageEnums.eVehicleStatus.InProgress;

        public GarageEnums.eVehicleStatus Status
        {
            get
            {
                return m_Status;
            }
            set
            {
                m_Status = value;
            }
        }
        public Vehicle Vehicle
        {
            get
            {
                return r_Vehicle;
            }
        }
        public string OwnerFullName => r_OwnerFullName;
        public string OwnerPhoneNumber => r_OwnerPhoneNumber;
        public VehiclePackage(string i_OwnerFullName, string i_OwnerPhoneNumber, Vehicle i_Vehicle)
        {
            r_OwnerFullName = i_OwnerFullName;
            r_OwnerPhoneNumber = i_OwnerPhoneNumber;
            r_Vehicle = i_Vehicle;
        }
        public (string, string, GarageEnums.eVehicleStatus) GetOwnerInfo()
        {
            return (OwnerFullName, OwnerPhoneNumber, Status);
        }
    }
}
