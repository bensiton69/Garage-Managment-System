using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class GarageManagement
    {
        //// TODO:
        //// 1. make lists as delegates
        //// 2. nullable
        //// 3. dont use direct cast
        //// 4. io_ instead of i_ where ref
        //// 5. move eVehicleType to Factory?

        private readonly List<VehiclePackage> r_VehiclesList = new List<VehiclePackage>();
        public VehiclePackage CurrentCustomer { get; private set; }
        public List<VehiclePackage> Vehicles
        {
            get
            {
                return r_VehiclesList;
            }
        }
        public List<VehiclePackage> GetVehicleByStatus()
        {
            return r_VehiclesList.OrderBy(vehiclePackage => vehiclePackage.Status).ToList();
        }
        public bool IsEmpty()
        {
            bool isEmpty = true;
            if(r_VehiclesList.Count != 0)
            {
                isEmpty = false;
            }

            return isEmpty;
        }
        public bool AddNewCar(string i_OwnerFullName, string i_OwnerPhoneNumber, Vehicle i_Vehicle)
        {
            VehiclePackage vehiclePackageToAdd = new VehiclePackage(
                i_OwnerFullName,
                i_OwnerPhoneNumber,
                i_Vehicle);
            r_VehiclesList.Add(vehiclePackageToAdd);
            return true;
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
        public bool FillAirToMax(Vehicle i_Vehicle)
        {
            i_Vehicle.FillAirToMax();
            return false;
        }
        public void ChangeVehicleStatus(VehiclePackage i_VehiclePackage, GarageEnums.eVehicleStatus i_VehicleStatus)
        {
            i_VehiclePackage.SetStatus(i_VehicleStatus);
        }
        public void FillFuelForMotorized(Vehicle i_Vehicle, GarageEnums.eFuelType i_FuelType, float i_AmountOfFuelToRefuel)
        {
            if (!(i_Vehicle is IMotorized))
            {
                throw new ArgumentException("You can not fuel this vehicle!");
            }
            float currentAmountOfFuel = ((IMotorized)i_Vehicle).CurrentAmountOfFuel;
            float maxAmountToRefuel = ((IMotorized)i_Vehicle).MaxAmountOfFuel - currentAmountOfFuel;
            if (!(i_AmountOfFuelToRefuel <= maxAmountToRefuel))
            {
                throw new GarageExceptions.ValueOutOfRangeException(0, maxAmountToRefuel);
            }
            if (i_FuelType != ((IMotorized)i_Vehicle).FuelType)
            {
                throw new ArgumentException("Wrong fuel type!");
            }
            (i_Vehicle as IMotorized)?.FillFuel(i_FuelType, i_AmountOfFuelToRefuel);
        }
        public void ChargeElectricVehicle(Vehicle i_Vehicle, float i_AmountOfTimeToCharge)
        {
            if (!(i_Vehicle is IElectrical))
            {
                if (!(i_Vehicle is IMotorized))
                {
                    throw new ArgumentException("You can not Charge this vehicle!");
                }
            }
            float currentAmountOfTime = ((IElectrical)i_Vehicle).BatteryTimeLeft;
            float maxBatteryTime = ((IElectrical)i_Vehicle).MaxBatteryTime - currentAmountOfTime;
            float minTimeToCharge = 0;
            if (i_AmountOfTimeToCharge <= maxBatteryTime)
            {
                (i_Vehicle as IElectrical)?.FillBattery(i_AmountOfTimeToCharge);
            }
            else
            {
                throw new GarageExceptions.ValueOutOfRangeException(minTimeToCharge, maxBatteryTime);
            }
        }

        public string VehiclePackageToDisplay(VehiclePackage i_VehiclePackage)
        {
            string ownerName;
            string ownerPhone;
            GarageEnums.eVehicleStatus vehicleStatus;
            StringBuilder stringBuilder = new StringBuilder();
            (ownerName, ownerPhone, vehicleStatus) = i_VehiclePackage.GetOwnerInfo();
            stringBuilder.Append(string.Format(
                @"
Client Info:
owner name: {0}
owner phone number: {1}
status: {2}",
                ownerName,
                ownerPhone,
                vehicleStatus));
            Vehicle vehicle = i_VehiclePackage.Vehicle;
            stringBuilder.Append(
                $@"
Vehicle Info:
Vehicle Type: {vehicle.GetType()}
Model name: {vehicle.ModelName}.
License number: {vehicle.LicenseNumber}.
Energy left: {vehicle.EnetgyLeft}.");
            stringBuilder.AppendLine();
            foreach (Wheel wheel in vehicle.Wheels)
            {
                stringBuilder.Append($"wheel info: {wheel.GetInfo()}");
                stringBuilder.AppendLine();
            }

            if (vehicle is IMotorized)
            {
                stringBuilder.Append($"Fuel Type: {(vehicle as IMotorized).FuelType}");
                stringBuilder.AppendLine();
                stringBuilder.Append($"Fuel current amount: {(vehicle as IMotorized).CurrentAmountOfFuel}");
                stringBuilder.AppendLine();
                stringBuilder.Append($"Fuel Max amount of fuel: {(vehicle as IMotorized).MaxAmountOfFuel}");
                stringBuilder.AppendLine();
            }

            if (vehicle is IElectrical)
            {
                stringBuilder.Append($"battery time left: {(vehicle as IElectrical).BatteryTimeLeft}");
                stringBuilder.AppendLine();
                stringBuilder.Append($"battery max time: {(vehicle as IElectrical).MaxBatteryTime}");
                stringBuilder.AppendLine();
            }

            if (vehicle is Car)
            {
                stringBuilder.Append($"Number of doors: {(vehicle as Car).NumberOfDoors}");
                stringBuilder.AppendLine();
                stringBuilder.Append($"Color: {(vehicle as Car).Color}");
                stringBuilder.AppendLine();
            }

            if (vehicle is Motorcycle)
            {
                stringBuilder.Append($"License Type: {(vehicle as Motorcycle).LicenseType}");
                stringBuilder.AppendLine();
                stringBuilder.Append($"Motor volume in cc: {(vehicle as Motorcycle).EngineVolumeCC}");
                stringBuilder.AppendLine();
            }

            if (vehicle is Truck)
            {
                stringBuilder.Append($"Is danger: {(vehicle as Truck).IsDanger}");
                stringBuilder.AppendLine();
                stringBuilder.Append($"Max weight to carry: {(vehicle as Truck).MaxWeight}");
                stringBuilder.AppendLine();
            }

            return stringBuilder.ToString();
        }
    }
}
