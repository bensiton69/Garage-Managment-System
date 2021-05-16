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
        private const float k_MinIsFloatZero = 0;
        private const float k_MinAmountOfFuel = 0;
        private const float k_MaxValForTruckCarry = 99999;
        private const float k_MinValForTruckCarry = 0;
        private const float k_MinValForAirPressure = 0;
        private const float k_MaxValForMotorcycleAirPressure = 30;
        private const float k_MaxValForCarAirPressure = 32;
        private const float k_MaxValForTruckAirPressure = 32;


        public Controller()
        {
            r_GarageModel = new GarageManagement();
            r_UserInterfaceView = new UserInterface();
        }

        public void Entry()
        {
            while(true)
            {
                GarageEnums.eUserAction userInput = 0;
                userInput = (GarageEnums.eUserAction)buildChoiceMenu(userInput);
                startChoiceMenu(userInput);
            }
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

        private void exitProgram()
        {
            r_UserInterfaceView.DisplayExitMessage();
            Environment.Exit(0);
        }

        private void addNewVehicleToGarage()
        {
            r_UserInterfaceView.DisplayMessage("Add new vehicle");
            string licenseNumber = r_UserInterfaceView.GetLicenseNumber();
            VehiclePackage vehiclePackage = null;
            if (r_GarageModel.IsLicenseNumberMatch(licenseNumber,ref vehiclePackage) == false)
            {
                addNewVehicleToGarage(licenseNumber);
            }
            else
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("This vehicle belong to: ");
                stringBuilder.AppendLine();
                stringBuilder.Append($"Name: {vehiclePackage.OwnerFullName}");
                stringBuilder.AppendLine();
                stringBuilder.Append($"Status: {vehiclePackage.Status}");
                r_UserInterfaceView.DisplayMessage(stringBuilder.ToString());
            }

        }

        private void displayLicenseNumberByStatus()
        {
            GarageEnums.eChoice sortChoice = 0;
            r_UserInterfaceView.DisplayMessage("Would you Display by status?");
            int userChoice = buildChoiceMenu(sortChoice);
            sortChoice = (GarageEnums.eChoice)userChoice;
            List<VehiclePackage> vehiclePackageToPrint;
            StringBuilder stringBuilder = new StringBuilder();
            //// TODO: switch with enumvalidation?
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
            r_UserInterfaceView.DisplayMessage(stringBuilder.ToString());

        }
        private void changeVehicleState()
        {
            r_UserInterfaceView.DisplayMessage("ChangeVehicleState");
            string licenseNumber = r_UserInterfaceView.GetLicenseNumber();
            GarageEnums.eVehicleStatus vehicleStatus = 0;
            int userChoice = buildChoiceMenu(vehicleStatus);
            vehicleStatus = (GarageEnums.eVehicleStatus)userChoice;
            r_UserInterfaceView.DisplayMessage(($"{vehicleStatus}"));
            try
            {
                r_GarageModel.ChangeVehicleStatus(licenseNumber, vehicleStatus);
            }
            catch(GarageExceptions.VehicleDoNotExist vdne)
            {
                r_UserInterfaceView.DisplayMessage(vdne.Message);
            }
        }
        private void fillAirToMax()
        {
            r_UserInterfaceView.DisplayMessage(("FillAirToMax"));
            string licenseNumber = r_UserInterfaceView.GetLicenseNumber();
            r_GarageModel.FillAirToMax(licenseNumber);
        }
        private void refuelingMotorized()
        {
            r_UserInterfaceView.DisplayMessage("RefuelingMotorized");
            string licenseNumber = r_UserInterfaceView.GetLicenseNumber();
            GarageEnums.eFuelType fuelType = 0;
            fuelType = (GarageEnums.eFuelType)buildChoiceMenu(fuelType);
            float amountOfFuelToRefuel = 0;
            r_UserInterfaceView.DisplayMessage("insert the amount of fuel to refuel:");
            getVariable(ref amountOfFuelToRefuel);

            try
            {
                r_GarageModel.FillFuelForMotorized(licenseNumber, fuelType, amountOfFuelToRefuel);
            }
            catch(GarageExceptions.ValueOutOfRangeException voore)
            {
                r_UserInterfaceView.DisplayMessage(voore.Message);
            }
            catch(ArgumentException ae)
            {
                r_UserInterfaceView.DisplayMessage(ae.Message);
            }
            catch(GarageExceptions.VehicleDoNotExist vdne)
            {
                r_UserInterfaceView.DisplayMessage(vdne.Message);
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
                r_UserInterfaceView.DisplayMessage(msgToPrint);
                VehiclePrint(vehiclePackage.Vehicle);
            }
        }

        private void chargeElectrical()
        {
            r_UserInterfaceView.DisplayMessage("ChargeElectrical");
            
            string licenseNumber = r_UserInterfaceView.GetLicenseNumber();
            float amountOfTimeToCharge = 0;
            r_UserInterfaceView.DisplayMessage("insert the amount of Time to charge:");
            getVariable(ref amountOfTimeToCharge);
            try
            {
                r_GarageModel.ChargeElectricVehicle(licenseNumber, amountOfTimeToCharge);
            }
            catch (GarageExceptions.ValueOutOfRangeException voore)
            {
                r_UserInterfaceView.DisplayMessage(voore.Message);
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
                r_UserInterfaceView.DisplayMessage($"The type of Vehicle is {vehicleToAdd.GetType()}");
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
                valToReturn = true;
            }
            return valToReturn;
        }
        private void setMotorizedFields(Vehicle i_VehicleToAdd)
        {
            r_UserInterfaceView.DisplayMessage(($"What is The current amount of fuel in your vehicle?"));
            float currentAmountOfFuel = 0;
            float maxAmountOfFuel = (i_VehicleToAdd as IMotorized).MaxAmountOfFuel;
            getVariable(ref currentAmountOfFuel, k_MinAmountOfFuel, maxAmountOfFuel);
            (i_VehicleToAdd as IMotorized)?.AddMotorizedFields(currentAmountOfFuel);
        }

        private void setTruckFields(Vehicle i_VehicleToAdd)
        {
            GarageEnums.eChoice isDangerChoice = 0;
            float maxWeight = 0;
            bool isDanger = false;
            r_UserInterfaceView.DisplayMessage("Does your truck carry any dangerous materials?");
            int userChoice = buildChoiceMenu(isDangerChoice);
            isDangerChoice = (GarageEnums.eChoice)userChoice;
            if(isDangerChoice == GarageEnums.eChoice.Yes)
            {
                isDanger = true;
            }
            r_UserInterfaceView.DisplayMessage("What is the max weight to carry?");
            getVariable(ref maxWeight, k_MinValForTruckCarry,k_MaxValForTruckCarry);
            (i_VehicleToAdd as Truck).AddTruckFields(isDanger,maxWeight);
        }
        public (string modelName, float energyLeft, string wheelManufacturerName, float currentWheelPressure) GetGeneralInfoForVehicle()
        {
            string modelName = null;
            string wheelManufacturerName = null;
            float energyLeft = 0;
            float currentWheelPressure = 0;
            r_UserInterfaceView.DisplayMessage("Please insert the Model Name for your vehicle: ");
            getVariable(ref modelName, k_MinLengthForFullName, k_MaxLengthForFullName);
            r_UserInterfaceView.DisplayMessage("Please insert the energy left for your vehicle: ");
            getVariable(ref energyLeft, 0 ,100);
            r_UserInterfaceView.DisplayMessage("Please insert the Manufacturer Name your of wheels: ");
            getVariable(ref wheelManufacturerName, k_MinLengthForFullName, k_MaxLengthForFullName);
            r_UserInterfaceView.DisplayMessage("Please insert the current air pressure your of wheels: ");
            getVariable(ref currentWheelPressure);
            return (modelName, energyLeft, wheelManufacturerName, currentWheelPressure);
        }

        public (string, string) GetCostumerDetails()
        {
            r_UserInterfaceView.DisplayMessage("Please insert Your full name: ");
            string fullName = null;
            getVariable(ref fullName, k_MinLengthForFullName, k_MaxLengthForFullName);
            r_UserInterfaceView.DisplayMessage("Please insert Your phone number: ");
            string phoneNumber = null;
            getVariable(ref phoneNumber, k_MinLengthForPhoneNumber, k_MaxLengthForPhoneNumber);
            return (fullName, phoneNumber);
        }
        private void setElectricalFields(Vehicle i_VehicleToAdd)
        {
            //// TODO: instead of value 0, try nullable
            float batteryTimeLeft = 0;
            r_UserInterfaceView.DisplayMessage("how many battery time left for electrical vehicle?");
            float maxTime = (i_VehicleToAdd as IElectrical).MaxBatteryTime;
            getVariable(ref batteryTimeLeft, k_MinIsFloatZero, maxTime);
            (i_VehicleToAdd as IElectrical)?.AddElectricalFields(batteryTimeLeft);
        }

        private void setMotorcycleFields(Vehicle i_VehicleToAdd)
        {
            GarageEnums.eLicenseType licenseType = 0;
            r_UserInterfaceView.DisplayMessage("What is the licenseType of your motorcycle?");
            licenseType = (GarageEnums.eLicenseType)buildChoiceMenu(licenseType);
            r_UserInterfaceView.DisplayMessage("What is the volume of your motorcycle in CC?");
            int motorcycleVolume = 0;
            getVariable(ref motorcycleVolume);
            (i_VehicleToAdd as Motorcycle)?.AddMotorcycleFields(licenseType, motorcycleVolume);
        }

        private void setCarFields(Vehicle i_VehicleToAdd)
        {
            GarageEnums.eColor color = 0;
            GarageEnums.eNumberOfDoor numberOfDoor = 0;
            r_UserInterfaceView.DisplayMessage(("What is the  color of your car?"));
            color = (GarageEnums.eColor)buildChoiceMenu(color);
            r_UserInterfaceView.DisplayMessage("How many doors for your car?");
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
        private void getVariable(ref string i_StringToInitialize,int i_MinLength, int i_MaxLength)
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
                        new GarageExceptions.ValueOutOfRangeException(i_MinLength, i_MaxLength);
                    r_UserInterfaceView.DisplayMessage(voore.Message);
                }
            }
        }

        private void getVariable(ref float i_FloatToInitialize, float i_MinVal, float i_MaxVal)
        {
            bool isValid = false;
            while (isValid == false)
            {
                getVariable(ref i_FloatToInitialize);
                if(i_MinVal <= i_FloatToInitialize && i_FloatToInitialize <= i_MaxVal)
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
        private int buildChoiceMenu<T>(T i_Param)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (Enum value in Enum.GetValues(i_Param.GetType()))
            {
                stringBuilder.Append($"[{(int)Enum.Parse(value.GetType(), value.ToString())}] for {value}");
                stringBuilder.AppendLine();
            }
            r_UserInterfaceView.DisplayMessage(stringBuilder.ToString());
            //// TODO: Code duplication!!
            bool isInputValid = false;
            string userInput = null;
            while (isInputValid == false)
            {
                getVariable(ref userInput);
                isInputValid = genericEnumValidation(typeof(T), userInput);
            }
            return int.Parse(userInput);
        }
        public void VehiclePrint(Vehicle i_Vehicle)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(
                $@"
Vehicle Info:
Model name: {i_Vehicle.ModelName}.
License number: {i_Vehicle.LicenseNumber}.
Energy left: {i_Vehicle.EnetgyLeft}.");
            stringBuilder.AppendLine();
            foreach (Wheel wheel in i_Vehicle.Wheels)
            {
                stringBuilder.Append($"wheel info: {wheel.GetInfo()}");
                stringBuilder.AppendLine();
            }

            if (i_Vehicle is IMotorized)
            {
                stringBuilder.Append($"Fuel Type: {(i_Vehicle as IMotorized).FuelType}");
            stringBuilder.AppendLine();
                stringBuilder.Append($"Fuel current amount: {(i_Vehicle as IMotorized).CurrentAmountOfFuel}");
            stringBuilder.AppendLine();
                stringBuilder.Append($"Fuel Max amount of fuel: {(i_Vehicle as IMotorized).MaxAmountOfFuel}");
            stringBuilder.AppendLine();
            }

            if (i_Vehicle is IElectrical)
            {
                stringBuilder.Append($"battery time left: {(i_Vehicle as IElectrical).BatteryTimeLeft}");
            stringBuilder.AppendLine();
                stringBuilder.Append($"battery max time: {(i_Vehicle as IElectrical).MaxBatteryTime}");
            stringBuilder.AppendLine();
            }

            if (i_Vehicle is Car)
            {
                stringBuilder.Append($"Number of doors: {(i_Vehicle as Car).NumberOfDoors}");
            stringBuilder.AppendLine();
                stringBuilder.Append($"Color: {(i_Vehicle as Car).Color}");
            stringBuilder.AppendLine();
            }

            if (i_Vehicle is Motorcycle)
            {
                stringBuilder.Append($"License Type: {(i_Vehicle as Motorcycle).LicenseType}");
            stringBuilder.AppendLine();
                stringBuilder.Append($"Motor volume in cc: {(i_Vehicle as Motorcycle).EngineVolumeCC}");
            stringBuilder.AppendLine();
            }

            if (i_Vehicle is Truck)
            {
                stringBuilder.Append($"Is danger: {(i_Vehicle as Truck).IsDanger}");
            stringBuilder.AppendLine();
                stringBuilder.Append($"Max weight to carry: {(i_Vehicle as Truck).MaxWeight}");
            stringBuilder.AppendLine();
            }
            r_UserInterfaceView.DisplayMessage(stringBuilder.ToString());
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

        private void GetWheel(GarageEnums.eVehicleType i_VehicleType)
        {
            string wheelManufacturerName = null;
            float currentWheelPressure = 0;
            float MaxValOfAirPressure;
            int numOfWheels;
            switch(i_VehicleType)
            {
                case GarageEnums.eVehicleType.ElectricalCar:
                    MaxValOfAirPressure = 32;
                    numOfWheels = 4;
                    break;
                case GarageEnums.eVehicleType.MotorizedCar:
                    MaxValOfAirPressure = 32;
                    numOfWheels = 4;
                    break;
                case GarageEnums.eVehicleType.ElectricalMotorcycle:
                    MaxValOfAirPressure = 30;
                    numOfWheels = 2;
                    break;
                case GarageEnums.eVehicleType.MotorizedMotorcycle:
                    MaxValOfAirPressure = 30;
                    numOfWheels = 2;
                    break;
                case GarageEnums.eVehicleType.Truck:
                    MaxValOfAirPressure = 26;
                    numOfWheels = 16;
                    break;
            }
            r_UserInterfaceView.DisplayMessage("Please insert the Model Name for your vehicle: ");
            getVariable(ref wheelManufacturerName, k_MinLengthForFullName, k_MaxLengthForFullName);
            r_UserInterfaceView.DisplayMessage("Please insert the current air pressure your of wheels: ");
            getVariable(ref currentWheelPressure);
        }
    }
}
