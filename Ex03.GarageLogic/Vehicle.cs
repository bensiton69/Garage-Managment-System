using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        private readonly string r_ModelName;
        public string ModelName => r_ModelName;

        private readonly string r_LicenseNumber;
        public string LicenseNumber => r_LicenseNumber;

        private readonly float r_EnergyLeft;

        public float EnetgyLeft => r_EnergyLeft;

        protected readonly List<Wheel> r_Wheels = new List<Wheel>();
        public List<Wheel> Wheels => r_Wheels;


        protected Vehicle(
            string i_ModelName,
            string i_LicenseNumber,
            float i_EnergyLeft,
            string i_WheelManufacturerName,
            float i_CurrentAirPressure,
            int i_NumberOfWheels,
            float i_MaxAirPressure)
        {
            if(!(0 <= i_EnergyLeft) || !(i_EnergyLeft <= 100))
            {
                throw new GarageExceptions.ValueOutOfRangeException(0, 100);
            }

            r_ModelName = i_ModelName;
            r_LicenseNumber = i_LicenseNumber;
            r_EnergyLeft = i_EnergyLeft;
            r_Wheels = new List<Wheel>();
            Wheel wheel = new Wheel(i_WheelManufacturerName, i_CurrentAirPressure, i_MaxAirPressure);
            for(int i = 0; i < i_NumberOfWheels; i++)
            {
                r_Wheels.Add(wheel);
            }
        }

        public bool FillAirToMax()
        {
            foreach(Wheel wheel in Wheels)
            {
                wheel.FillAirToMax();
            }

            return true;
        }
        protected Vehicle(
            string i_ModelName,
            string i_LicenseNumber,
            float i_EnergyLeft,
            string i_WheelManufacturerName,
            int i_NumberOfWheels,
            float i_MaxAirPressure)
        {
            if (!(0 <= i_EnergyLeft) || !(i_EnergyLeft <= 100))
            {
                throw new GarageExceptions.ValueOutOfRangeException(0, 100);
            }

            r_ModelName = i_ModelName;
            r_LicenseNumber = i_LicenseNumber;
            r_EnergyLeft = i_EnergyLeft;
            r_Wheels = new List<Wheel>();
            Wheel wheel = new Wheel(i_WheelManufacturerName, i_MaxAirPressure);
            for (int i = 0; i < i_NumberOfWheels; i++)
            {
                r_Wheels.Add(wheel);
            }
        }
    }

}
