using System;

namespace Ex03.GarageLogic
{
    public class GarageExceptions
    {
        public class ValueOutOfRangeException : Exception
        {
            private readonly float r_MinValue;
            private readonly float r_MaxValue;

            public ValueOutOfRangeException(float i_MinValue, float i_MaxValue)
                : base(string.Format("You only can insert value between {0} - {1}.", i_MinValue, i_MaxValue))
            {
                r_MaxValue = i_MaxValue;
                r_MinValue = i_MinValue;
            }

            public ValueOutOfRangeException(string i_Message, float i_MinValue, float i_MaxValue)
                : base(string.Format(i_Message + "{0} - {1}.", i_MinValue, i_MaxValue))
            {
                r_MaxValue = i_MaxValue;
                r_MinValue = i_MinValue;
            }
        }

        public class VehicleDoNotExist : Exception
        {
            public VehicleDoNotExist(string i_LicenseNumber)
                : base(string.Format("The Vehicle with license number: '{0}' do not exist!", i_LicenseNumber))
            {
            }

            public VehicleDoNotExist()
                : base("The Vehicle with the matching conditions do not exist!")
            {
            }
        }

        public class GarageIsEmpty : Exception
        {
            public GarageIsEmpty()
                : base("The garage is empty!")
            {
            }
        }

        public class VehicleIsMax : Exception
        {
            public VehicleIsMax()
                : base("This action is not available, Its already on Max!")
            {
            }
        }
    }
}
