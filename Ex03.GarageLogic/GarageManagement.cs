using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class GarageManagement
    {
        private readonly List<VehiclePackage> r_VehiclesList = new List<VehiclePackage>();
        public List<VehiclePackage> Vehicles
        {
            get
            {
                return r_VehiclesList;
            }
        }
        public VehiclePackage CurrentCustomer { get; private set; }

        public bool AddNewCar(string i_OwnerFullName, string i_OwnerPhoneNumber, Vehicle i_Vehicle)
        {
            VehiclePackage vehiclePackageToAdd = new VehiclePackage(
                i_OwnerFullName,
                i_OwnerPhoneNumber,
                i_Vehicle);
            r_VehiclesList.Add(vehiclePackageToAdd);
            return true;
        }

        public bool IsLicenseNumberMatch(string i_LicenseNumberToCheck)
        {
            bool isMatch = false;
            foreach(VehiclePackage vehiclePackage in Vehicles)
            {
                if(i_LicenseNumberToCheck == vehiclePackage.Vehicle.LicenseNumber)
                {
                    isMatch = true;
                    CurrentCustomer = vehiclePackage;
                    break;
                }
            }

            return isMatch;
        }
        public bool IsLicenseNumberMatch(string i_LicenseNumberToCheck, ref VehiclePackage i_VehiclePackage)
        {
            bool isMatch = false;
            foreach (VehiclePackage vehiclePackage in Vehicles)
            {
                if (i_LicenseNumberToCheck == vehiclePackage.Vehicle.LicenseNumber)
                {
                    isMatch = true;
                    CurrentCustomer = vehiclePackage;
                    i_VehiclePackage = vehiclePackage;
                    break;
                }
            }

            return isMatch;
        }

        public List<VehiclePackage> GetVehicleByStatus()
        {
            List<VehiclePackage> sortedList = new List<VehiclePackage>();
            sortedList = r_VehiclesList.OrderBy(vehiclePackage => vehiclePackage.Status).ToList();
            return sortedList;
        }

        public bool FillAirToMax(string i_LicenseNumber)
        {
            VehiclePackage vehiclePackage = null;
            IsLicenseNumberMatch(i_LicenseNumber, ref vehiclePackage);
            vehiclePackage.Vehicle.FillAirToMax();
            return false;
        }


        public void ChargeElectricVehicle(string i_LicenseNumber, float i_AmountOfTimeToCharge)
        {
            VehiclePackage vehiclePackage = null;
            IsLicenseNumberMatch(i_LicenseNumber, ref vehiclePackage);
            float currentAmountOfTime = ((IElectrical)vehiclePackage.Vehicle).BatteryTimeLeft;
            float maxBatteryTime = ((IElectrical)vehiclePackage.Vehicle).MaxBatteryTime - currentAmountOfTime;
            float minTimeToCharge = 0;
            if (i_AmountOfTimeToCharge <= maxBatteryTime)
            {
                (vehiclePackage.Vehicle as IElectrical)?.FillBattery(i_AmountOfTimeToCharge);
            }
            else
            {
                throw new GarageExceptions.ValueOutOfRangeException(minTimeToCharge, maxBatteryTime);
            }
        }

        public void ChangeVehicleStatus(string i_LicenseNumber, GarageEnums.eVehicleStatus i_VehicleStatus)
        {
            VehiclePackage vehiclePackage = null;
            IsLicenseNumberMatch(i_LicenseNumber, ref vehiclePackage);
            ////TODO: use property instead set()
            vehiclePackage?.SetStatus(i_VehicleStatus);
        }

        public void FillFuelForMotorized(string i_LicenseNumber, GarageEnums.eFuelType i_FuelType, float i_AmountOfFuelToRefuel)
        {
            //// TODO: duplicate methology to chrage, etc.
            VehiclePackage vehiclePackage = null;
            if(IsLicenseNumberMatch(i_LicenseNumber, ref vehiclePackage) == false)
            {
                throw new GarageExceptions.VehicleDoNotExist(i_LicenseNumber);
            }

            if(!(vehiclePackage.Vehicle is IMotorized))
            {
                throw new ArgumentException("You can not fuel this vehicle!");
            }


            float currentAmountOfFuel = ((IMotorized)vehiclePackage.Vehicle).CurrentAmountOfFuel;
            float maxAmountToRefuel = ((IMotorized)vehiclePackage.Vehicle).MaxAmountOfFuel - currentAmountOfFuel;
            if(!(i_AmountOfFuelToRefuel <= maxAmountToRefuel))
            {
                throw new GarageExceptions.ValueOutOfRangeException(0, maxAmountToRefuel);
            }
            if (i_FuelType != ((IMotorized)vehiclePackage.Vehicle).FuelType)
            {
                throw new ArgumentException("Wrong fuel type!");
            }
            (vehiclePackage.Vehicle as IMotorized)?.FillFuel(i_FuelType, i_AmountOfFuelToRefuel);
        }

        public bool CheckIfLicenseNumberIsMatch(string i_LicenseNumber)
        {
            ////TODO: dont use console
            bool isMatch = false;
            if (IsLicenseNumberMatch(i_LicenseNumber))
            {
                Console.WriteLine("This vehicle belong to: ");
                Console.WriteLine($"Name: {CurrentCustomer.OwnerFullName}");
                Console.WriteLine($"Phone Number: {CurrentCustomer.OwnerPhoneNumber}");
                isMatch = true;
            }

            return isMatch;
        }
    }
}
