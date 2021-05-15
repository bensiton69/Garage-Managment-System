using System;

namespace Ex03.GarageLogic
{
    public class GarageExceptions
    {
        public class ValueOutOfRangeException : Exception
        {
            public ValueOutOfRangeException(float i_MinValue, float i_MaxValue)
                : base($"You only can insert value between {i_MinValue} - {i_MaxValue}.")
            {
                maxValue = i_MaxValue;
                m_MinValue = i_MinValue;
            }

            private float m_MinValue;
            private float maxValue;
        }

        public class VehicleDoNotExist : Exception
        {
            private readonly string r_LicenseNumber;

            public VehicleDoNotExist(string i_LicenseNumber)
                : base($"The Vehicle with license number: '{i_LicenseNumber}' do not exist!")
            {
                r_LicenseNumber = i_LicenseNumber;
            }
        }
    }
}
