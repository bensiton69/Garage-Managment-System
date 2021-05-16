namespace Ex03.GarageLogic
{
    public class Wheel
    {
        string m_ManufacturerName;
        private float m_CurrentAirPressure;
        private float m_MaxAirPressure;

        public float CurrentAirPressure
        {
            get => m_CurrentAirPressure;
            private set
            {
                //// TODO: the getvariable methot allready validate the input
                if(value <= MaxAirPressure)
                {
                    m_CurrentAirPressure = value;
                }
                else
                {
                    throw new GarageExceptions.ValueOutOfRangeException(0, MaxAirPressure);
                }
            }
        }

        public float MaxAirPressure => m_MaxAirPressure;

        public Wheel(string i_ManufacturerName, float i_CurrentAirPressure, float i_MaxAirPressure)
        {
            m_ManufacturerName = i_ManufacturerName;
            m_CurrentAirPressure = i_CurrentAirPressure;
            m_MaxAirPressure = i_MaxAirPressure;
        }

        public Wheel(string i_WheelManufacturerName, float i_MaxAirPressure)
        {
            m_ManufacturerName = i_WheelManufacturerName;
            m_MaxAirPressure = i_MaxAirPressure;
        }

        public (string m_ManufacturerName, float m_CurrentAirPressure, float m_MaxAirPressure) GetInfo()
        {
            return (m_ManufacturerName, m_CurrentAirPressure, m_MaxAirPressure);
        }

        public bool FillAirToMax()
        {
            CurrentAirPressure = MaxAirPressure;
            return true;
        }
    }
}
