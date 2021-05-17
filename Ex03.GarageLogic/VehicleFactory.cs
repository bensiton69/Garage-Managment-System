namespace Ex03.GarageLogic
{
    public static class VehicleFactory
    {
        public static Vehicle CreateVehicle(
            GarageEnums.eVehicleType i_VehicleType,
            string i_LicenseNumber,
            (string modelName, float energyLeft, string) i_GetGeneralInfoForVehicle)
        {
            Vehicle vehicle = null;
            string modelName;
            float energyLeft;
            float currentAirPressure;
            string wheelManufacturerName;
            (modelName, energyLeft, wheelManufacturerName) = i_GetGeneralInfoForVehicle;
            switch (i_VehicleType)
            {
                case GarageEnums.eVehicleType.ElectricalMotorcycle:
                    {
                        vehicle = new ElectricalMotorcycle(
                            modelName,
                            i_LicenseNumber,
                            energyLeft,
                            wheelManufacturerName);
                        break;
                    }
                case GarageEnums.eVehicleType.MotorizedMotorcycle:
                    {
                        vehicle = new MotorizedMotorcycle(
                            modelName,
                            i_LicenseNumber,
                            energyLeft,
                            wheelManufacturerName);
                        break;
                    }
                case GarageEnums.eVehicleType.ElectricalCar:
                    {
                        vehicle = new ElectricCar(
                            modelName,
                            i_LicenseNumber,
                            energyLeft,
                            wheelManufacturerName);
                        break;
                    }
                case GarageEnums.eVehicleType.MotorizedCar:
                    {
                        vehicle = new MotorizedCar(
                            modelName,
                            i_LicenseNumber,
                            energyLeft,
                            wheelManufacturerName);
                        break;
                    }
                case GarageEnums.eVehicleType.Truck:
                    {
                        vehicle = new Truck(
                            modelName,
                            i_LicenseNumber,
                            energyLeft,
                            wheelManufacturerName);
                        break;
                    }
            }

            return vehicle;
        }
    }
}
