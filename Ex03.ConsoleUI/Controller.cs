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
        private const int k_MinLengthForNames = 2;
        private const int k_MaxLengthForNames = 25;
        private const int k_MinLengthForLicenseNumber = 6;
        private const int k_MaxLengthForLicenseNumber = 12;
        private const int k_MinLengthForPhoneNumber = 6;
        private const int k_MaxLengthForPhoneNumber = 10;
        private const float k_MinIsFloatZero = 0;
        private const float k_MaxValForTruckCarry = 99999;

        public Controller()
        {
            r_GarageModel = new GarageManagement();
            r_UserInterfaceView = new UserInterface();
        }
        public void Entry()
        {
            while(true)
            {
                try
                {
                    GarageEnums.eUserAction userInput = 0;
                    userInput = buildChoiceMenu(userInput);
                    r_UserInterfaceView.ClearScreen();
                    startChoiceMenu(userInput);
                    r_UserInterfaceView.DisplaySuccess();
                }
                catch (Exception ex)
                {
                    r_UserInterfaceView.DisplayErrorMessage(ex.Message);
                }
            }
        }
        private void exitProgram()
        {
            r_UserInterfaceView.DisplayExitMessage();
            Environment.Exit(0);
        }
        private void startChoiceMenu(GarageEnums.eUserAction i_UserInput)
        {
            switch (i_UserInput)
            {
                case GarageEnums.eUserAction.Exit:
                    exitProgram();
                    break;
                case GarageEnums.eUserAction.AddNewVehicle:
                    addNewVehicleToGarage();
                    break;
                case GarageEnums.eUserAction.DisplayAllLicenseNumbersInGarage:
                    garageValidation();
                    displayLicenseNumberByStatus();
                    break;
                case GarageEnums.eUserAction.ChangeVehicleState:
                    garageValidation();
                    changeVehicleState();
                    break;
                case GarageEnums.eUserAction.FillAirToMax:
                    garageValidation();
                    fillAirToMax();
                    break;
                case GarageEnums.eUserAction.RefuelingMotorized:
                    garageValidation();
                    refuelingMotorized();
                    break;
                case GarageEnums.eUserAction.ChargeElectrical:
                    garageValidation(); 
                    chargeElectrical();
                    break;
                case GarageEnums.eUserAction.DisplayFullInfo:
                    garageValidation();
                    displayFullInfo();
                    break;
            }
        }


        private void addNewVehicleToGarage()
        {
            r_UserInterfaceView.DisplayMessage("Add new vehicle");
            string licenseNumber = getLicenseNumber();
            VehiclePackage vehiclePackage = null;
            if (r_GarageModel.IsLicenseNumberMatch(licenseNumber,ref vehiclePackage) == false)
            {
                createVehicle(licenseNumber);
            }
            else
            {
                displayVehicle(vehiclePackage);
            }

        }
        private void displayLicenseNumberByStatus()
        {
            GarageEnums.eChoice sortChoice = 0;
            r_UserInterfaceView.DisplayMessage("Would you Display by status?");
            sortChoice = buildChoiceMenu(sortChoice);
            string allLicenseToPrint;

            if (sortChoice == GarageEnums.eChoice.Yes)
            {
                GarageEnums.eVehicleStatus sortStatus = 0;
                allLicenseToPrint = r_GarageModel.GetLicenseNumbersByStatus(buildChoiceMenu(sortStatus));
            }
            else
            {
                allLicenseToPrint = r_GarageModel.GetLicenseNumbers();
            }
            r_UserInterfaceView.DisplayMessage(allLicenseToPrint);
        }
        private void changeVehicleState()
        {
            r_UserInterfaceView.DisplayMessage("ChangeVehicleState");
            string licenseNumber = getLicenseNumber();
            VehiclePackage vehiclePackage = null;
            vehicleValidation(licenseNumber, ref vehiclePackage);
            GarageEnums.eVehicleStatus vehicleStatus = 0;
            vehicleStatus = buildChoiceMenu(vehicleStatus);
            r_GarageModel.ChangeVehicleStatus(vehiclePackage, vehicleStatus);
        }
        private void fillAirToMax()
        {
            r_UserInterfaceView.DisplayMessage(("FillAirToMax"));
            string licenseNumber = getLicenseNumber();
            VehiclePackage vehiclePackage = null;
            vehicleValidation(licenseNumber,ref vehiclePackage);
            r_GarageModel.FillAirToMax(vehiclePackage.Vehicle);

        }
        private void refuelingMotorized()
        {
            r_UserInterfaceView.DisplayMessage("RefuelingMotorized");
            string licenseNumber = getLicenseNumber();
            VehiclePackage vehiclePackage = null;
            vehicleValidation(licenseNumber, ref vehiclePackage);
            Vehicle vehicle = vehiclePackage.Vehicle;
            GarageEnums.eFuelType fuelType = 0;
            fuelType = (GarageEnums.eFuelType)buildChoiceMenu(fuelType);
            float amountOfFuelToRefuel = 0;
            r_UserInterfaceView.DisplayMessage("insert the amount of fuel to refuel:");
            getVariable(ref amountOfFuelToRefuel);
            r_GarageModel.FillFuelForMotorized(vehicle, fuelType, amountOfFuelToRefuel);
        }
        private void chargeElectrical()
        {
            r_UserInterfaceView.DisplayMessage("ChargeElectrical");
            string licenseNumber = getLicenseNumber();
            VehiclePackage vehiclePackage = null;
            vehicleValidation(licenseNumber, ref vehiclePackage);
            Vehicle vehicle = vehiclePackage.Vehicle;
            float amountOfTimeToCharge = 0;
            r_UserInterfaceView.DisplayMessage("insert the amount of Time to charge:");
            getVariable(ref amountOfTimeToCharge);
            r_GarageModel.ChargeElectricVehicle(vehicle, amountOfTimeToCharge);

        }
        private void displayFullInfo()
        {
            string licenseNumber = getLicenseNumber();
            VehiclePackage vehiclePackage = null;
            vehicleValidation(licenseNumber, ref vehiclePackage);
            string vehicleInfo = r_GarageModel.VehiclePackageToDisplay(vehiclePackage);
            r_UserInterfaceView.DisplayMessage(vehicleInfo);
        }



        private void displayVehicle(VehiclePackage i_VehiclePackage)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("This vehicle belong to: ");
            stringBuilder.AppendLine();
            stringBuilder.Append($"Name: {i_VehiclePackage.OwnerFullName}");
            stringBuilder.AppendLine();
            stringBuilder.Append($"Status: {i_VehiclePackage.Status}");
            r_UserInterfaceView.DisplayMessage(stringBuilder.ToString());
        }
        private void createVehicle(string i_LicenseNumber)
        {
        GarageEnums.eVehicleType vehicleType = 0;
        vehicleType = (GarageEnums.eVehicleType)buildChoiceMenu(vehicleType);
        Vehicle vehicleToAdd = VehicleFactory.CreateVehicle(vehicleType, i_LicenseNumber, getGeneralInfoForVehicle());
        setWheelCurrentAirPressure(vehicleToAdd);
        (string costumerName, string costumerPhone) = getCostumerDetails();
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
            if(vehicleToAdd is Truck)
            {
                setTruckFields(vehicleToAdd);
            }
            r_GarageModel.AddNewCar(costumerName, costumerPhone, vehicleToAdd);
        }


        private void setMotorizedFields(Vehicle io_VehicleToAdd)
        {
            r_UserInterfaceView.DisplayMessage(($"What is The current amount of fuel in your vehicle?"));
            float currentAmountOfFuel = 0;
            float maxAmountOfFuel = (io_VehicleToAdd as IMotorized).MaxAmountOfFuel;
            getVariable(ref currentAmountOfFuel, k_MinIsFloatZero, maxAmountOfFuel);
            (io_VehicleToAdd as IMotorized)?.AddMotorizedFields(currentAmountOfFuel);
        }
        private void setTruckFields(Vehicle io_VehicleToAdd)
        {
            GarageEnums.eChoice isDangerChoice = 0;
            float maxWeight = 0;
            bool isDanger = false;
            r_UserInterfaceView.DisplayMessage("Does your truck carry any dangerous materials?");
            isDangerChoice = buildChoiceMenu(isDangerChoice);
            if (isDangerChoice == GarageEnums.eChoice.Yes)
            {
                isDanger = true;
            }
            r_UserInterfaceView.DisplayMessage("What is the max weight to carry?");
            getVariable(ref maxWeight, k_MinIsFloatZero,k_MaxValForTruckCarry);
            (io_VehicleToAdd as Truck)?.AddTruckFields(isDanger,maxWeight);
        }
        private void setWheelCurrentAirPressure(Vehicle io_VehicleToAdd)
        {
            float currentWheelPressure = 0;
            float maxAirPressure = io_VehicleToAdd.Wheels.FirstOrDefault().MaxAirPressure;
            r_UserInterfaceView.DisplayMessage("Please insert the current air pressure your of wheels: ");
            getVariable(ref currentWheelPressure, k_MinIsFloatZero, maxAirPressure);
            io_VehicleToAdd.SetWheelCurrentAirPressure(currentWheelPressure);
        }
        private void setElectricalFields(Vehicle io_VehicleToAdd)
        {
            float batteryTimeLeft = 0;
            r_UserInterfaceView.DisplayMessage("how many battery time left for electrical vehicle?");
            float maxTime = (io_VehicleToAdd as IElectrical).MaxBatteryTime;
            getVariable(ref batteryTimeLeft, k_MinIsFloatZero, maxTime);
            (io_VehicleToAdd as IElectrical)?.AddElectricalFields(batteryTimeLeft);
        }
        private void setMotorcycleFields(Vehicle io_VehicleToAdd)
        {
            GarageEnums.eLicenseType licenseType = 0;
            r_UserInterfaceView.DisplayMessage("What is the licenseType of your motorcycle?");
            licenseType = (GarageEnums.eLicenseType)buildChoiceMenu(licenseType);
            r_UserInterfaceView.DisplayMessage("What is the volume of your motorcycle in CC?");
            int motorcycleVolume = 0;
            getVariable(ref motorcycleVolume);
            (io_VehicleToAdd as Motorcycle)?.AddMotorcycleFields(licenseType, motorcycleVolume);
        }
        private void setCarFields(Vehicle io_VehicleToAdd)
        {
            GarageEnums.eColor color = 0;
            GarageEnums.eNumberOfDoor numberOfDoor = 0;
            r_UserInterfaceView.DisplayMessage(("What is the  color of your car?"));
            color = (GarageEnums.eColor)buildChoiceMenu(color);
            r_UserInterfaceView.DisplayMessage("How many doors for your car?");
            numberOfDoor = (GarageEnums.eNumberOfDoor)buildChoiceMenu(numberOfDoor);
            (io_VehicleToAdd as Car)?.AddCarFields(color, numberOfDoor);
        }



        
        private void getVariable<T>(ref T io_Param)
        {
            bool isValid = false;
            while(isValid == false)
            {
                try
                {
                    io_Param = (T)Convert.ChangeType(r_UserInterfaceView.GetVar(), typeof(T));
                    isValid = true;
                }
                catch(FormatException)
                {
                    r_UserInterfaceView.DisplayFormatError();
                }
            }
        }
        private void getVariable(ref string io_StringToInitialize,int i_MinLength, int i_MaxLength)
        {
            bool isValid = false;
            while(isValid == false)
            {
                getVariable(ref io_StringToInitialize);
                int lengthOfString = io_StringToInitialize.Length;
                if(i_MinLength <= lengthOfString && lengthOfString <= i_MaxLength)
                {
                    isValid = true;
                }
                else
                {
                    //// input is not valid
                    GarageExceptions.ValueOutOfRangeException voore =
                        new GarageExceptions.ValueOutOfRangeException(i_MinLength, i_MaxLength);
                    r_UserInterfaceView.DisplayMessage(voore.Message);
                }
            }
        }
        private void getVariable(ref float io_FloatToInitialize, float i_MinVal, float i_MaxVal)
        {
            bool isValid = false;
            while (isValid == false)
            {
                getVariable(ref io_FloatToInitialize);
                if(i_MinVal <= io_FloatToInitialize && io_FloatToInitialize <= i_MaxVal)
                {
                    isValid = true;
                }
                else
                {
                    //// input is not valid
                    GarageExceptions.ValueOutOfRangeException voore =
                        new GarageExceptions.ValueOutOfRangeException(i_MinVal, i_MaxVal);
                    r_UserInterfaceView.DisplayMessage(voore.Message);
                }
            }
        }
        private void getVariable(ref int io_IntToInitialize, int i_MinVal, int i_MaxVal)
        {
            bool isValid = false;
            while (isValid == false)
            {
                getVariable(ref io_IntToInitialize);
                if (i_MinVal <= io_IntToInitialize && io_IntToInitialize <= i_MaxVal)
                {
                    isValid = true;
                }
                else
                {
                    GarageExceptions.ValueOutOfRangeException voore =
                        new GarageExceptions.ValueOutOfRangeException(i_MinVal, i_MaxVal);
                    r_UserInterfaceView.DisplayMessage(voore.Message);
                }
            }
        }


        private string getLicenseNumber()
        {
            r_UserInterfaceView.DisplayMessage("Please insert your license Number:");
            string licenseNumber = null;
            getVariable(ref licenseNumber, k_MinLengthForLicenseNumber, k_MaxLengthForLicenseNumber);
            return licenseNumber;
        }
        private (string, string) getCostumerDetails()
        {
            string fullName = null;
            string phoneNumber = null;
            r_UserInterfaceView.DisplayMessage("Please insert Your full name: ");
            getVariable(ref fullName, k_MinLengthForNames, k_MaxLengthForNames);
            r_GarageModel.LettersValidation(fullName);
            r_UserInterfaceView.DisplayMessage("Please insert Your phone number: ");
            getVariable(ref phoneNumber, k_MinLengthForPhoneNumber, k_MaxLengthForPhoneNumber);
            return (fullName, phoneNumber);
        }
        private (string modelName, float energyLeft, string wheelManufacturerName) getGeneralInfoForVehicle()
        {
            string modelName = null;
            string wheelManufacturerName = null;
            float energyLeft = 0;
            r_UserInterfaceView.DisplayMessage("Please insert the Model Name for your vehicle: ");
            getVariable(ref modelName, k_MinLengthForNames, k_MaxLengthForNames);
            r_UserInterfaceView.DisplayMessage("Please insert the energy left for your vehicle: ");
            getVariable(ref energyLeft, 0, 100);
            r_UserInterfaceView.DisplayMessage("Please insert the Manufacturer Name your of wheels: ");
            getVariable(ref wheelManufacturerName, k_MinLengthForNames, k_MaxLengthForNames);
            return (modelName, energyLeft, wheelManufacturerName);
        }



        private T buildChoiceMenu<T>(T i_Param)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("Choose the option number from choice menu: ");
            stringBuilder.AppendLine();
            foreach (Enum value in Enum.GetValues(i_Param.GetType()))
            {
                stringBuilder.Append($"[{(int)Enum.Parse(value.GetType(), value.ToString())}] for {value}");
                stringBuilder.AppendLine();
            }
            r_UserInterfaceView.DisplayMessage(stringBuilder.ToString());
            int userInput = 0;
            int maxVal = Enum.GetValues(typeof(T)).Cast<int>().Max();
            int minVal = Enum.GetValues(typeof(T)).Cast<int>().Min();
            getVariable(ref userInput,minVal,maxVal);
            return (T)Enum.ToObject(typeof(T), userInput);
        }
        private void garageValidation()
        {
            if(r_GarageModel.IsEmpty() == true)
            {
                throw new GarageExceptions.GarageIsEmpty();
            }
        }
        private void  vehicleValidation(string i_LicenseNumber, ref VehiclePackage i_VehiclePackage)
        {
            if(r_GarageModel.IsLicenseNumberMatch(i_LicenseNumber, ref i_VehiclePackage) == false)
            {
                throw new GarageExceptions.VehicleDoNotExist(i_LicenseNumber);
            }
        }

    }
}
