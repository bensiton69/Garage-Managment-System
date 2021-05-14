using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    class UserInterface
    {
        private const string k_ForamtError = "This is the wrong format! ";
        private const string k_ArgumenttError = "This is the wrong Argument! ";
        private const string k_ExitMassage = "Goodbye! ";
        private readonly string r_SeparateLine = new string('~',15);

        public string GetLicenseNumber()
        {
            ////TODO: read from controller
            string msgToPrint = "Please insert your license Number:";
            DisplayMassage(msgToPrint);
            return Console.ReadLine();
        }
        
        public void DisplayMassage(string i_Msg)
        {
            Console.WriteLine(r_SeparateLine);
            Console.WriteLine(i_Msg);
        }

        public void VehiclePrint(Vehicle i_Vehicle)
        {
            ////TODO: move to conteroller and sent by string?
            //// TODO: string builder?
            
            string msg = String.Format(@"
Print Vehicle:
{0} is the name of model.
{1} is the license number.
{2} is the energy left.",
                i_Vehicle.ModelName,
                i_Vehicle.LicenseNumber,
                i_Vehicle.EnetgyLeft);
           DisplayMassage(msg);
            foreach (Wheel wheel in i_Vehicle.Wheels)
            {
                Console.WriteLine($"wheel info:\n {wheel.GetInfo()}");
            }

            if (i_Vehicle is IMotorized)
            {
                Console.WriteLine($"Fuel Type: {(i_Vehicle as IMotorized).FuelType}");
                Console.WriteLine($"Fuel current amount: {(i_Vehicle as IMotorized).CurrentAmountOfFuel}");
                Console.WriteLine($"Fuel Max amount of fuel: {(i_Vehicle as IMotorized).MaxAmountOfFuel}");
            }

            if (i_Vehicle is IElectrical)
            {
                Console.WriteLine($"battery time left: {(i_Vehicle as IElectrical).BatteryTimeLeft}");
                Console.WriteLine($"battery max time: {(i_Vehicle as IElectrical).MaxBatteryTime}");
            }

            if (i_Vehicle is Car)
            {
                Console.WriteLine($"Number of doors: {(i_Vehicle as Car).NumberOfDoors}");
                Console.WriteLine($"Color: {(i_Vehicle as Car).Color}");
            }

            if (i_Vehicle is Motorcycle)
            {
                Console.WriteLine($"License Type: {(i_Vehicle as Motorcycle).LicenseType}");
                Console.WriteLine($"Motor volume in cc: {(i_Vehicle as Motorcycle).EngineVolumeCC}");
            }

        }

        public string GetVar()
        {
            return Console.ReadLine();
        }

        public void DisplayFormatError()
        { 
           Console.WriteLine(r_SeparateLine);
           Console.WriteLine(k_ForamtError);
        }
        public void DisplayArgumentError()
        {
            Console.WriteLine(r_SeparateLine);
            Console.WriteLine(k_ArgumenttError);
        }
    }
}
