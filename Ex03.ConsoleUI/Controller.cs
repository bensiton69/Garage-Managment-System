using System;
using System.Linq;
using System.Text;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class Controller
    {
        private const int k_MinLengthForNames = 2;
        private const int k_MaxLengthForNames = 25;
        private const int k_MinLengthForLicenseNumber = 6;
        private const int k_MaxLengthForLicenseNumber = 12;
        private const int k_MinLengthForPhoneNumber = 6;
        private const int k_MaxLengthForPhoneNumber = 10;
        private const int k_MinMotorcycleVolume = 50;
        private const int k_MaxnMotorcycleVolume = 1890;
        private const float k_MinIsFloatZero = 0;
        private const float k_MaxValForTruckCarry = 99999;
        private readonly GarageManagement r_GarageModel;
        private readonly UserInterface r_UserInterfaceView;

        public Controller()
        {
            r_GarageModel = new GarageManagement();
            r_UserInterfaceView = new UserInterface();
/*            r_GarageModel.AddNewVehicle(
                "benjhbs",
                "11111111",
                new MotorizedMotorcycle("Honda motor", "111111", 11, "111111"));
            r_GarageModel.AddNewVehicle(
                "benjhbs",
                "11111111",
                new ElectricalMotorcycle("Honda motor", "222222", 11, "111111"));*/
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
                    r_GarageModel.GarageValidation();
                    displayLicenseNumberByStatus();
                    break;
                case GarageEnums.eUserAction.ChangeVehicleState:
                    r_GarageModel.GarageValidation();
                    changeVehicleState();
                    break;
                case GarageEnums.eUserAction.FillAirToMax:
                    r_GarageModel.GarageValidation();
                    fillAirToMax();
                    break;
                case GarageEnums.eUserAction.RefuelingMotorized:
                    r_GarageModel.GarageValidation();
                    refuelingMotorized();
                    break;
                case GarageEnums.eUserAction.ChargeElectrical:
                    r_GarageModel.GarageValidation(); 
                    chargeElectrical();
                    break;
                case GarageEnums.eUserAction.DisplayFullInfo:
                    r_GarageModel.GarageValidation();
                    displayFullInfo();
                    break;
            }
        }

        private void addNewVehicleToGarage()
        {
            r_UserInterfaceView.DisplayMessage("Add new vehicle");
            string licenseNumber = getLicenseNumber();
            VehiclePackage vehiclePackage = null;
            if (r_GarageModel.IsLicenseNumberMatch(licenseNumber, ref vehiclePackage) == false)
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
            r_GarageModel.VehicleValidation(licenseNumber, ref vehiclePackage);
            GarageEnums.eVehicleStatus vehicleStatus = 0;
            vehicleStatus = buildChoiceMenu(vehicleStatus);
            r_GarageModel.ChangeVehicleStatus(vehiclePackage, vehicleStatus);
        }

        private void fillAirToMax()
        {
            r_UserInterfaceView.DisplayMessage("FillAirToMax");
            string licenseNumber = getLicenseNumber();
            VehiclePackage vehiclePackage = null;
            r_GarageModel.VehicleValidation(licenseNumber, ref vehiclePackage);
            r_GarageModel.FillAirToMax(vehiclePackage.Vehicle);
        }

        private void refuelingMotorized()
        {
            r_UserInterfaceView.DisplayMessage("RefuelingMotorized");
            string licenseNumber = getLicenseNumber();
            float amountOfFuelToRefuel = 0;
            VehiclePackage vehiclePackage = null;
            r_GarageModel.VehicleValidation(licenseNumber, ref vehiclePackage);
            Vehicle vehicle = vehiclePackage.Vehicle;
            r_GarageModel.MotorizedValidation(vehicle);
            float currentAmountOfFuel = (vehicle as IMotorized).CurrentAmountOfFuel;
            float maxAmountOfFuel = (vehicle as IMotorized).MaxAmountOfFuel;
            r_GarageModel.IsEqualsToMax(currentAmountOfFuel, maxAmountOfFuel);
            GarageEnums.eFuelType fuelType = 0;
            r_UserInterfaceView.DisplayMessage("insert the amount of fuel to refuel:");
            float maxToFuel = r_GarageModel.SubTwoFloats(maxAmountOfFuel, currentAmountOfFuel);
            r_UserInterfaceView.GetVariable(ref amountOfFuelToRefuel, k_MinIsFloatZero, maxToFuel);
            fuelType = buildChoiceMenu(fuelType);
            r_GarageModel.FillFuelForMotorized(vehicle, fuelType, amountOfFuelToRefuel);
        }

        private void chargeElectrical()
        {
            r_UserInterfaceView.DisplayMessage("ChargeElectrical");
            float amountOfTimeToCharge = 0;
            string licenseNumber = getLicenseNumber();
            VehiclePackage vehiclePackage = null;
            r_GarageModel.VehicleValidation(licenseNumber, ref vehiclePackage);
            Vehicle vehicle = vehiclePackage.Vehicle;
            r_GarageModel.ElectricalValidation(vehicle);
            float maxBatteryTime = (vehicle as IElectrical).MaxBatteryTime;
            float currentBatteryTime = (vehicle as IElectrical).BatteryTimeLeft;
            r_GarageModel.IsEqualsToMax(currentBatteryTime, maxBatteryTime);
            r_UserInterfaceView.DisplayMessage("insert the amount of Time to charge:");
            float maxToCharge = r_GarageModel.SubTwoFloats(maxBatteryTime, currentBatteryTime);
            r_UserInterfaceView.GetVariable(ref amountOfTimeToCharge, k_MinIsFloatZero, maxToCharge);
            r_GarageModel.ChargeElectricVehicle(vehicle, amountOfTimeToCharge);
        }

        private void displayFullInfo()
        {
            string licenseNumber = getLicenseNumber();
            VehiclePackage vehiclePackage = null;
            r_GarageModel.VehicleValidation(licenseNumber, ref vehiclePackage);
            string vehicleInfo = r_GarageModel.VehiclePackageToDisplay(vehiclePackage);
            r_UserInterfaceView.DisplayMessage(vehicleInfo);
        }
        
        private void displayVehicle(VehiclePackage i_VehiclePackage)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("This vehicle belong to: ");
            stringBuilder.AppendLine();
            stringBuilder.Append(string.Format("Name: {0}", i_VehiclePackage.OwnerFullName));
            stringBuilder.AppendLine();
            stringBuilder.Append(string.Format("Status: {0}", i_VehiclePackage.Status));
            r_UserInterfaceView.DisplayMessage(stringBuilder.ToString());
        }

        private void createVehicle(string i_LicenseNumber)
        {
        VehicleFactory.eVehicleType vehicleType = 0;
        vehicleType = buildChoiceMenu(vehicleType);
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

            r_GarageModel.AddNewVehicle(costumerName, costumerPhone, vehicleToAdd);
        }

        private void setMotorizedFields(Vehicle io_VehicleToAdd)
        {
            r_UserInterfaceView.DisplayMessage("What is The current amount of fuel in your vehicle?");
            float currentAmountOfFuel = 0;
            float maxAmountOfFuel = (io_VehicleToAdd as IMotorized).MaxAmountOfFuel;
            r_UserInterfaceView.GetVariable(ref currentAmountOfFuel, k_MinIsFloatZero, maxAmountOfFuel);
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
            r_UserInterfaceView.GetVariable(ref maxWeight, k_MinIsFloatZero, k_MaxValForTruckCarry);
            (io_VehicleToAdd as Truck)?.AddTruckFields(isDanger, maxWeight);
        }

        private void setWheelCurrentAirPressure(Vehicle io_VehicleToAdd)
        {
            float currentWheelPressure = 0;
            float maxAirPressure = io_VehicleToAdd.Wheels.FirstOrDefault().MaxAirPressure;
            r_UserInterfaceView.DisplayMessage("Please insert the current air pressure your of wheels: ");
            r_UserInterfaceView.GetVariable(ref currentWheelPressure, k_MinIsFloatZero, maxAirPressure);
            io_VehicleToAdd.SetWheelCurrentAirPressure(currentWheelPressure);
        }

        private void setElectricalFields(Vehicle io_VehicleToAdd)
        {
            float batteryTimeLeft = 0;
            r_UserInterfaceView.DisplayMessage("how many battery time left for electrical vehicle?");
            float maxTime = (io_VehicleToAdd as IElectrical).MaxBatteryTime;
            r_UserInterfaceView.GetVariable(ref batteryTimeLeft, k_MinIsFloatZero, maxTime);
            (io_VehicleToAdd as IElectrical)?.AddElectricalFields(batteryTimeLeft);
        }

        private void setMotorcycleFields(Vehicle io_VehicleToAdd)
        {
            GarageEnums.eLicenseType licenseType = 0;
            r_UserInterfaceView.DisplayMessage("What is the licenseType of your motorcycle?");
            licenseType = buildChoiceMenu(licenseType);
            r_UserInterfaceView.DisplayMessage("What is the volume of your motorcycle in CC?");
            int motorcycleVolume = 0;
            r_UserInterfaceView.GetVariable(ref motorcycleVolume, k_MinMotorcycleVolume, k_MaxnMotorcycleVolume);
            (io_VehicleToAdd as Motorcycle)?.AddMotorcycleFields(licenseType, motorcycleVolume);
        }

        private void setCarFields(Vehicle io_VehicleToAdd)
        {
            GarageEnums.eColor color = 0;
            GarageEnums.eNumberOfDoor numberOfDoor = 0;
            r_UserInterfaceView.DisplayMessage("What is the  color of your car?");
            color = buildChoiceMenu(color);
            r_UserInterfaceView.DisplayMessage("How many doors for your car?");
            numberOfDoor = buildChoiceMenu(numberOfDoor);
            (io_VehicleToAdd as Car)?.AddCarFields(color, numberOfDoor);
        }

        private string getLicenseNumber()
        {
            r_UserInterfaceView.DisplayMessage("Please insert your license Number:");
            string licenseNumber = null;
            r_UserInterfaceView.GetVariable(ref licenseNumber, k_MinLengthForLicenseNumber, k_MaxLengthForLicenseNumber);
            return licenseNumber;
        }

        private (string, string) getCostumerDetails()
        {
            string fullName = null;
            string phoneNumber = null;
            r_UserInterfaceView.DisplayMessage("Please insert Your full name: ");
            r_UserInterfaceView.GetName(ref fullName, k_MinLengthForNames, k_MaxLengthForNames);
            r_UserInterfaceView.DisplayMessage("Please insert Your phone number: ");
            r_UserInterfaceView.GetNumbers(ref phoneNumber, k_MinLengthForPhoneNumber, k_MaxLengthForPhoneNumber);
            return (fullName, phoneNumber);
        }

        private (string modelName, float energyLeft, string wheelManufacturerName) getGeneralInfoForVehicle()
        {
            string modelName = null;
            string wheelManufacturerName = null;
            float energyLeft = 0;
            r_UserInterfaceView.DisplayMessage("Please insert the Model Name for your vehicle: ");
            r_UserInterfaceView.GetVariable(ref modelName, k_MinLengthForNames, k_MaxLengthForNames);
            r_UserInterfaceView.DisplayMessage("Please insert the energy left for your vehicle: ");
            r_UserInterfaceView.GetVariable(ref energyLeft, 0, 100);
            r_UserInterfaceView.DisplayMessage("Please insert the Manufacturer Name your of wheels: ");
            r_UserInterfaceView.GetVariable(ref wheelManufacturerName, k_MinLengthForNames, k_MaxLengthForNames);
            return (modelName, energyLeft, wheelManufacturerName);
        }

        private T buildChoiceMenu<T>(T i_Param)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("Choose the option number from choice menu: ");
            stringBuilder.AppendLine();
            foreach (Enum value in Enum.GetValues(i_Param.GetType()))
            {
                stringBuilder.Append(
                    string.Format("[{0}] for {1}", (int)Enum.Parse(value.GetType(), value.ToString()), value));
                stringBuilder.AppendLine();
            }

            r_UserInterfaceView.DisplayMessage(stringBuilder.ToString());
            int userInput = 0;
            int maxVal = Enum.GetValues(typeof(T)).Cast<int>().Max();
            int minVal = Enum.GetValues(typeof(T)).Cast<int>().Min();
            r_UserInterfaceView.GetVariable(ref userInput, minVal, maxVal);
            return (T)Enum.ToObject(typeof(T), userInput);
        }
    }
}
