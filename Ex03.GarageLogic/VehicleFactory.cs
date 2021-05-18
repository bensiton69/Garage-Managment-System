namespace Ex03.GarageLogic
{
    public static class VehicleFactory
    {
        public enum eVehicleType
        {
            ElectricalMotorcycle = 1,
            MotorizedMotorcycle = 2,
            ElectricalCar = 3,
            MotorizedCar = 4,
            Truck = 5
        }

        public static Vehicle CreateVehicle(
            eVehicleType i_VehicleType,
            string i_LicenseNumber,
            (string modelName, float energyLeft, string) i_GetGeneralInfoForVehicle)
        {
            Vehicle vehicle = null;
            string modelName;
            float energyLeft;
            string wheelManufacturerName;
            (modelName, energyLeft, wheelManufacturerName) = i_GetGeneralInfoForVehicle;
            switch (i_VehicleType)
            {
                case eVehicleType.ElectricalMotorcycle:
                    {
                        vehicle = new ElectricalMotorcycle(
                            modelName,
                            i_LicenseNumber,
                            energyLeft,
                            wheelManufacturerName);
                        break;
                    }

                case eVehicleType.MotorizedMotorcycle:
                    {
                        vehicle = new MotorizedMotorcycle(
                            modelName,
                            i_LicenseNumber,
                            energyLeft,
                            wheelManufacturerName);
                        break;
                    }
                
                case eVehicleType.ElectricalCar:
                    {
                        vehicle = new ElectricCar(
                            modelName,
                            i_LicenseNumber,
                            energyLeft,
                            wheelManufacturerName);
                        break;
                    }

                case eVehicleType.MotorizedCar:
                    {
                        vehicle = new MotorizedCar(
                            modelName,
                            i_LicenseNumber,
                            energyLeft,
                            wheelManufacturerName);
                        break;
                    }

                case eVehicleType.Truck:
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
