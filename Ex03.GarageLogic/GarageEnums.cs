namespace Ex03.GarageLogic
{
    public class GarageEnums
    {
        public enum eChoice
        {
            No = 0,
            Yes = 1
        }

        public enum eFuelType
        {
            Soler = 1,
            Octan95 = 2,
            Octan96 = 3,
            Octan98 = 4
        }

        public enum eColor
        {
            Red = 1,
            Silver = 2,
            White = 3,
            Black = 4
        }

        public enum eUserAction
        {
            Exit = 0,
            AddNewVehicle = 1,
            DisplayAllLicenseNumbersInGarage = 2,
            ChangeVehicleState = 3,
            FillAirToMax = 4,
            RefuelingMotorized = 5,
            ChargeElectrical = 6,
            DisplayFullInfo = 7
        }

        public enum eVehicleStatus
        {
            InProgress = 1,
            Repaired = 2,
            Paid = 3
        }

        public enum eLicenseType
        {
            A = 1,
            B1 = 2,
            AA = 3,
            BB = 4
        }

        public enum eNumberOfDoor
        {
            Two = 2,
            Three = 3,
            Four = 4,
            Five = 5
        }
    }
}
