namespace Ex03.GarageLogic
{
    public class Wheel
    {
        private const float k_MinAirPressure = 0;
        private readonly float r_MaxAirPressure;
        private readonly string r_ManufacturerName;
        private float m_CurrentAirPressure;

        public float CurrentAirPressure
        {
            get => m_CurrentAirPressure;
            set => m_CurrentAirPressure = value;
        }

        public float MaxAirPressure => r_MaxAirPressure;

        public Wheel(string i_WheelManufacturerName, float i_MaxAirPressure)
        {
            CurrentAirPressure = k_MinAirPressure;
            r_ManufacturerName = i_WheelManufacturerName;
            r_MaxAirPressure = i_MaxAirPressure;
        }

        public (string m_ManufacturerName, float m_CurrentAirPressure, float m_MaxAirPressure) GetInfo()
        {
            return (r_ManufacturerName, m_CurrentAirPressure, r_MaxAirPressure);
        }

        public bool FillAirToMax()
        {
            CurrentAirPressure = MaxAirPressure;
            return true;
        }
    }
}
