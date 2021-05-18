namespace Ex03.GarageLogic
{
    public interface IMotorized
    {
        GarageEnums.eFuelType FuelType
        {
            get;
            set;
        }

        float CurrentAmountOfFuel
        {
            get;
            set;
        }

        float MaxAmountOfFuel
        {
            get;
            set;
        }

        bool FillFuel(GarageEnums.eFuelType i_FuelType, float i_AmountOfFuelToRefuel);

        void AddMotorizedFields(float i_CurrentAmountOfFuel);
    }
}
