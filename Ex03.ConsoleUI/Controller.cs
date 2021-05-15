using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    class Controller
    {
        private readonly GarageManagement r_GarageModel;
        private readonly UserInterface r_UserInterfaceView;
        private const int k_MinLengthForFullName = 2;
        private const int k_MaxLengthForFullName = 18;
        private const int k_MinLengthForPhoneNumber = 6;
        private const int k_MaxLengthForPhoneNumber = 10;


        public Controller()
        {
            r_GarageModel = new GarageManagement();
            r_UserInterfaceView = new UserInterface();
        }

        public void Entry()
        {
            GarageEnums.eUserAction userInput = 0;
            userInput = (GarageEnums.eUserAction)buildChoiceMenu(userInput);
            startChoiceMenu(userInput);
        }

        private void startChoiceMenu(GarageEnums.eUserAction i_UserInput)
        {
            switch (i_UserInput)
            {
                case GarageEnums.eUserAction.AddNewVehicle:
                    addNewVehicleToGarage();
                    break;
                case GarageEnums.eUserAction.DisplayAllLicenseNumbersInGarage:
                    displayLicenseNumberByStatus();
                    break;
                case GarageEnums.eUserAction.ChangeVehicleState:
                    changeVehicleState();
                    break;
                case GarageEnums.eUserAction.FillAirToMax:
                    fillAirToMax();
                    break;
                case GarageEnums.eUserAction.RefuelingMotorized:
                    refuelingMotorized();
                    break;
                case GarageEnums.eUserAction.ChargeElectrical:
                    chargeElectrical();
                    break;
                case GarageEnums.eUserAction.DisplayFullInfo:
                    displayFullInfo();
                    break;

            }
        }

        private void addNewVehicleToGarage()
        {
            r_UserInterfaceView.DisplayMassage("Add new vehicle");
            string licenseNumber = r_UserInterfaceView.GetLicenseNumber();
            if (r_GarageModel.CheckIfLicenseNumberIsMatch(licenseNumber) == false)
            {
                addNewVehicleToGarage(licenseNumber);
            }

        }

        private void displayLicenseNumberByStatus()
        {
            GarageEnums.eChoice sortChoice = 0;
            r_UserInterfaceView.DisplayMassage("Would you Display by status?");
            int userChoice = buildChoiceMenu(sortChoice);
            sortChoice = (GarageEnums.eChoice)userChoice;
            List<VehiclePackage> vehiclePackageToPrint;
            StringBuilder stringBuilder = new StringBuilder();
            if (sortChoice == GarageEnums.eChoice.Yes)
            {
                vehiclePackageToPrint = r_GarageModel.GetVehicleByStatus();
            }
            else
            {
                vehiclePackageToPrint = r_GarageModel.Vehicles;
            }
            foreach (VehiclePackage vehiclePackage in vehiclePackageToPrint)
            {
                stringBuilder.Append(($"Status: {vehiclePackage.Status}, License number: {vehiclePackage.Vehicle.LicenseNumber}"));
                stringBuilder.AppendLine();
            }
            r_UserInterfaceView.DisplayMassage(stringBuilder.ToString());

        }
        private void changeVehicleState()
        {
            r_UserInterfaceView.DisplayMassage("ChangeVehicleState");
            string licenseNumber = r_UserInterfaceView.GetLicenseNumber();
            //eVehicleType vehicleType = GetVehicleType();
            GarageEnums.eVehicleStatus vehicleStatus = 0;
            int userChoice = buildChoiceMenu(vehicleStatus);
            vehicleStatus = (GarageEnums.eVehicleStatus)userChoice;
            r_UserInterfaceView.DisplayMassage(($"{vehicleStatus}"));
            r_GarageModel.ChangeVehicleStatus(licenseNumber, vehicleStatus);
        }
        private void fillAirToMax()
        {
            r_UserInterfaceView.DisplayMassage(("FillAirToMax"));
            string licenseNumber = r_UserInterfaceView.GetLicenseNumber();
            r_GarageModel.FillAirToMax(licenseNumber);
        }
        private void refuelingMotorized()
        {
            r_UserInterfaceView.DisplayMassage("RefuelingMotorized");
            string licenseNumber = r_UserInterfaceView.GetLicenseNumber();
            GarageEnums.eFuelType fuelType = 0;
            fuelType = (GarageEnums.eFuelType)buildChoiceMenu(fuelType);
            float amountOfFuelToRefuel = 0;
            r_UserInterfaceView.DisplayMassage("insert the amount of fuel to refuel:");
            getVariable(ref amountOfFuelToRefuel);

            try
            {
                r_GarageModel.FillFuelForMotorized(licenseNumber, fuelType, amountOfFuelToRefuel);
            }
            catch(GarageExceptions.ValueOutOfRangeException voore)
            {
                r_UserInterfaceView.DisplayMassage(voore.Message);
            }
            catch(ArgumentException ae)
            {
                r_UserInterfaceView.DisplayMassage(ae.Message);
            }
            catch(GarageExceptions.VehicleDoNotExist vdne)
            {
                r_UserInterfaceView.DisplayMassage(vdne.Message);
            }
            //r_GarageModel.FillFuelForMotorized(licenseNumber, fuelType, amountOfFuelToRefuel);
        }
        private void displayFullInfo()
        {
            string licenseNumber = r_UserInterfaceView.GetLicenseNumber();
            VehiclePackage vehiclePackage = null;
            if (r_GarageModel.IsLicenseNumberMatch(licenseNumber, ref vehiclePackage))
            {
                string ownerName;
                string ownerPhone;
                GarageEnums.eVehicleStatus vehicleStatus;
                (ownerName, ownerPhone, vehicleStatus) = vehiclePackage.GetOwnerInfo();
                string msgToPrint = string.Format(
                    @"
Client Info:
owner name: {0}
owner phone number: {1}
status: {2}",
                    ownerName,
                    ownerPhone,
                    vehicleStatus);
                r_UserInterfaceView.DisplayMassage(msgToPrint);
                VehiclePrint(vehiclePackage.Vehicle);
            }
        }

        private void chargeElectrical()
        {
            r_UserInterfaceView.DisplayMassage("ChargeElectrical");
            
            string licenseNumber = r_UserInterfaceView.GetLicenseNumber();
            float amountOfTimeToCharge = 0;
            r_UserInterfaceView.DisplayMassage("insert the amount of Time to charge:");
            getVariable(ref amountOfTimeToCharge);
            try
            {
                r_GarageModel.ChargeElectricVehicle(licenseNumber, amountOfTimeToCharge);
            }
            catch (GarageExceptions.ValueOutOfRangeException voore)
            {
                r_UserInterfaceView.DisplayMassage(voore.Message);
            }
        }
        private bool addNewVehicleToGarage(string i_LicenseNumber)
        {
            bool valToReturn = false;
            GarageEnums.eVehicleType vehicleType = 0;
            vehicleType = (GarageEnums.eVehicleType)buildChoiceMenu(vehicleType);
            Vehicle vehicleToAdd = VehicleFactory.CreateVehicle(vehicleType, i_LicenseNumber, GetGeneralInfoForVehicle());
            (string costumerName, string costumerPhone) = GetCostumerDetails();
            if (vehicleToAdd != null)
            {
                r_UserInterfaceView.DisplayMassage($"The type of Vehicle is {vehicleToAdd.GetType()}");
                if (vehicleToAdd is Car)
                {
                    setCarFields(vehicleToAdd);
                }
                if (vehicleToAdd is Motorcycle)
                {
                    setMotorcycleFields(vehicleToAdd);
                }
                if (vehicleToAdd is IElectrical)
                {
                    setElectricalFields(vehicleToAdd);
                }
                if (vehicleToAdd is IMotorized)
                {
                    setMotorizedFields(vehicleToAdd);
                }
                r_GarageModel.AddNewCar(costumerName, costumerPhone, vehicleToAdd);
                valToReturn = true;
            }
            return valToReturn;
        }
        private void setMotorizedFields(Vehicle i_VehicleToAdd)
        {
            //// TODO: input validation etc
            r_UserInterfaceView.DisplayMassage(($"What is The current amount of fuel in your vehicle? (max = {((IMotorized)i_VehicleToAdd).MaxAmountOfFuel})"));
            float currentAmountOfFuel = 0;
            getVariable(ref currentAmountOfFuel);
            (i_VehicleToAdd as IMotorized)?.AddMotorizedFields(currentAmountOfFuel);
        }
        public (string modelName, float energyLeft, string wheelManufacturerName, float currentWheelPressure) GetGeneralInfoForVehicle()
        {
            string modelName = null;
            string wheelManufacturerName = null;
            float energyLeft = 0;
            float currentWheelPressure = 0;
            r_UserInterfaceView.DisplayMassage("Please insert the Model Name for your vehicle: ");
            getVariable(ref modelName);
            r_UserInterfaceView.DisplayMassage("Please insert the energy left for your vehicle: ");
            getVariable(ref energyLeft);
            r_UserInterfaceView.DisplayMassage("Please insert the Manufacturer Name your of wheels: ");
            getVariable(ref wheelManufacturerName);
            r_UserInterfaceView.DisplayMassage("Please insert the current air pressure your of wheels: ");
            getVariable(ref currentWheelPressure);
            return (modelName, energyLeft, wheelManufacturerName, currentWheelPressure);
        }

        public (string, string) GetCostumerDetails()
        {
            r_UserInterfaceView.DisplayMassage("Please insert Your full name: ");
            string fullName = null;
            getVariable(ref fullName);
            r_UserInterfaceView.DisplayMassage("Please insert Your phone number: ");
            string phoneNumber = null;
            getVariable(ref phoneNumber);
            return (fullName, phoneNumber);
        }
        private void setElectricalFields(Vehicle i_VehicleToAdd)
        {
            //// TODO: input validation etc
            //// TODO: instead of value 0, try nullable
            float batteryTimeLeft = 0;
            r_UserInterfaceView.DisplayMassage("how many battery time left for electrical vehicle?");
            getVariable(ref batteryTimeLeft);
            (i_VehicleToAdd as IElectrical)?.AddElectricalFields(batteryTimeLeft);
        }

        private void setMotorcycleFields(Vehicle i_VehicleToAdd)
        {
            GarageEnums.eLicenseType licenseType = 0;
            r_UserInterfaceView.DisplayMassage("What is the licenseType of your motorcycle?");
            licenseType = (GarageEnums.eLicenseType)buildChoiceMenu(licenseType);
            r_UserInterfaceView.DisplayMassage("What is the volume of your motorcycle in CC?");
            //// TODO: input validation and exception
            int motorcycleVolume = 0;
            getVariable(ref motorcycleVolume);
            (i_VehicleToAdd as Motorcycle)?.AddMotorcycleFields(licenseType, motorcycleVolume);
        }

        private void setCarFields(Vehicle i_VehicleToAdd)
        {
            GarageEnums.eColor color = 0;
            GarageEnums.eNumberOfDoor numberOfDoor = 0;
            r_UserInterfaceView.DisplayMassage(("What is the  color of your car?"));
            color = (GarageEnums.eColor)buildChoiceMenu(color);
            r_UserInterfaceView.DisplayMassage("How many doors for your car?");
            numberOfDoor = (GarageEnums.eNumberOfDoor)buildChoiceMenu(numberOfDoor);
            (i_VehicleToAdd as Car)?.AddCarFields(color, numberOfDoor);
        }

        private void getVariable<T>(ref T i_Param)
        {
            bool isValid = false;
            while(isValid == false)
            {
                try
                {
                    i_Param = (T)Convert.ChangeType(r_UserInterfaceView.GetVar(), typeof(T));
                    isValid = true;
                }
                catch(FormatException)
                {
                    r_UserInterfaceView.DisplayFormatError();
                }
            }
        }
        private void getVariableas(ref string i_StringToInitialize,int i_MinLength, int i_MaxLength)
        {
            bool isValid = false;
            while(isValid == false)
            {
                getVariable(ref i_StringToInitialize);
                int lengthOfString = i_StringToInitialize.Length;
                if(i_MinLength <= lengthOfString && lengthOfString <= i_MaxLength)
                {
                    isValid = true;
                }
                else
                {
                    //// input is not valid
                    GarageExceptions.ValueOutOfRangeException voore =
                        new GarageExceptions.ValueOutOfRangeException(0, i_MaxLength);
                    r_UserInterfaceView.DisplayMassage(voore.Message);
                }
            }
        }

        private void getVariable(ref float i_FloatToInitialize, float i_MinVal, float i_MaxVal)
        {
            bool isValid = false;
            while (isValid == false)
            {
                getVariable(ref i_FloatToInitialize);
                if(i_MaxVal <= i_FloatToInitialize && i_FloatToInitialize <= i_MaxVal)
                {
                    isValid = true;
                }
                else
                {
                    //// input is not valid
                    GarageExceptions.ValueOutOfRangeException voore =
                        new GarageExceptions.ValueOutOfRangeException(i_MinVal, i_MaxVal);
                    r_UserInterfaceView.DisplayMassage(voore.Message);
                }
            }
        }
        private int buildChoiceMenu<T>(T i_Param)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (Enum value in Enum.GetValues(i_Param.GetType()))
            {
                stringBuilder.Append($"[{(int)Enum.Parse(value.GetType(), value.ToString())}] for {value}");
                stringBuilder.AppendLine();
            }
            r_UserInterfaceView.DisplayMassage(stringBuilder.ToString());
            //// TODO: Code duplication!!
            bool isInputValid = false;
            string userInput = null;
            while (isInputValid == false)
            {
                getVariable(ref userInput);
                //// TODO convert userinput inside enumv alidation?
                isInputValid = genericEnumValidation(typeof(T), userInput);
            }
            return int.Parse(userInput);
        }
        public void VehiclePrint(Vehicle i_Vehicle)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(
                $@"
Print Vehicle:
Model name: {i_Vehicle.ModelName}.
License number: {i_Vehicle.LicenseNumber}.
Energy left: {i_Vehicle.EnetgyLeft}.");

            foreach (Wheel wheel in i_Vehicle.Wheels)
            {
                stringBuilder.Append($"wheel info:\n {wheel.GetInfo()}");
            }

            if (i_Vehicle is IMotorized)
            {
                stringBuilder.Append($"Fuel Type: {(i_Vehicle as IMotorized).FuelType}");
                stringBuilder.Append($"Fuel current amount: {(i_Vehicle as IMotorized).CurrentAmountOfFuel}");
                stringBuilder.Append($"Fuel Max amount of fuel: {(i_Vehicle as IMotorized).MaxAmountOfFuel}");
            }

            if (i_Vehicle is IElectrical)
            {
                stringBuilder.Append($"battery time left: {(i_Vehicle as IElectrical).BatteryTimeLeft}");
                stringBuilder.Append($"battery max time: {(i_Vehicle as IElectrical).MaxBatteryTime}");
            }

            if (i_Vehicle is Car)
            {
                stringBuilder.Append($"Number of doors: {(i_Vehicle as Car).NumberOfDoors}");
                stringBuilder.Append($"Color: {(i_Vehicle as Car).Color}");
            }

            if (i_Vehicle is Motorcycle)
            {
                stringBuilder.Append($"License Type: {(i_Vehicle as Motorcycle).LicenseType}");
                stringBuilder.Append($"Motor volume in cc: {(i_Vehicle as Motorcycle).EngineVolumeCC}");
            }
            r_UserInterfaceView.DisplayMassage(stringBuilder.ToString());
        }
        private bool genericEnumValidation(Type i_Type, string i_UserInput)
        {
            bool isValid = false;
            try
            {
                int maxVal = Enum.GetValues(i_Type).Cast<int>().Max();
                int minVal = Enum.GetValues(i_Type).Cast<int>().Min();
                int enumIntVal = Int32.Parse(i_UserInput);
                isValid = minVal <= enumIntVal && enumIntVal <= maxVal;
            }
            catch(FormatException)
            {
                r_UserInterfaceView.DisplayFormatError();
            }
            return isValid;
        }
    }
}
