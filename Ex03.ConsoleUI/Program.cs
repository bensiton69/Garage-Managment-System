﻿using System;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    class Program
    {
        public static void Main()
        {
            float? ben = null;
            DoSomething(ref ben);
            Console.WriteLine(ben);
            Controller controller = new Controller();
            //controller.Entry();
            /*            GarageManagement garageManagement = new GarageManagement();
                        Vehicle vehicle = new ElectricCar(
                            "Tesla 2020",
                            "8932",
                            90,
                            "Tesla",
                            25);
                        (vehicle as IElectrical).BatteryTimeLeft = 2;
                        (vehicle as Car).AddCarFields(GarageEnums.eColor.Black, GarageEnums.eNumberOfDoor.Three);
                        garageManagement.AddNewCar("Elon Mask", "050-333-2127", vehicle);
                        TypeValiation(vehicle, typeof(IMotorized));*/
        }

        private static void DoSomething(ref float? i_Ben)
        {
            
        }

        /*        private static void TypeValiation<T>(Vehicle i_VehiclePackageVehicle, T i_Param)
        {
            
            if (i_VehiclePackageVehicle is i_Param)
            {

            }
        }*/
        /*public static void ExampleVehicle(ref GarageManagement i_GarageManagement)
        {
            Vehicle vehicle = new ElectricCar(
                "Tesla 2020",
                "8932",
                90,
                "Tesla",
                25);
            (vehicle as 
        ).BatteryTimeLeft = 2;
            (vehicle as Car).AddCarFields(GarageEnums.eColor.Black, GarageEnums.eNumberOfDoor.Three);
            i_GarageManagement.AddNewCar("Elon Mask", "050-333-2127", vehicle);

            vehicle = new MotorizedMotorcycle(
               "Honda CB590",
               "1234",
               37,
               "SUZUKI",
               22);
            (vehicle as IMotorized).CurrentAmountOfFuel = 2;
            (vehicle as Motorcycle).AddMotorcycleFields(GarageEnums.eLicenseType.AA, 1200);
            i_GarageManagement.AddNewCar("Ben Israel Siton", "050-241-0020", vehicle);
            i_GarageManagement.ChangeVehicleStatus("1234", GarageEnums.eVehicleStatus.Repaired);

            vehicle = new MotorizedCar(
                "seatIbiza fr 2014",
                "6570",
                18,
                "seat",
                28);
            (vehicle as IMotorized).CurrentAmountOfFuel = 3.1f;
            (vehicle as Car).AddCarFields(GarageEnums.eColor.White, GarageEnums.eNumberOfDoor.Three);
            i_GarageManagement.AddNewCar("MOTI dhan", "050-111-7777", vehicle);
        }*/
    }
}
