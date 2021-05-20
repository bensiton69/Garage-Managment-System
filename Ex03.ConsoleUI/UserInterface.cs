using System;
using System.Linq;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class UserInterface
    {
        private const string k_FormatError = "This is the wrong format! ";
        private const string k_ExitMessage = "Goodbye! ";
        private const string k_OnlyNumbersValidMessage = "You can only insert numbers here!";
        private const string k_OnlyLettersValidMessage = "You can only insert letters and spaces here!";
        private readonly string r_SeparateLine = new string('~', 15);

        public void DisplayMessage(string i_Msg)
        {
            Console.WriteLine(r_SeparateLine);
            Console.WriteLine(i_Msg);
        }

        public void DisplayExitMessage()
        {
            DisplayMessage(k_ExitMessage);
        }

        public string GetVar()
        {
            return Console.ReadLine();
        }

        public void DisplayFormatError()
        {
            Console.WriteLine(r_SeparateLine);
            Console.WriteLine(k_FormatError);
        }

        public void ClearScreen()
        {
            Console.Clear();
        }

        public void DisplaySuccess()
        {
            DisplayMessage("Success!");
        }

        public void DisplayErrorMessage(string i_ExMessage)
        {
            string errorMsg = string.Format("Error: {0}", i_ExMessage);
            DisplayMessage(errorMsg);
        }

        public void GetVariable<T>(ref T io_Param)
        {
            bool isValid = false;
            while(isValid == false)
            {
                try
                {
                    io_Param = (T)Convert.ChangeType(GetVar(), typeof(T));
                    isValid = true;
                }
                catch(FormatException)
                {
                    DisplayFormatError();
                }
            }
        }

        public void GetVariable(ref string io_StringToInitialize, int i_MinLength, int i_MaxLength)
        {
            bool isValid = false;
            while(isValid == false)
            {
                GetVariable(ref io_StringToInitialize);
                int lengthOfString = io_StringToInitialize.Length;
                if(i_MinLength <= lengthOfString && lengthOfString <= i_MaxLength)
                {
                    isValid = true;
                }
                else
                {
                    //// input is not valid
                    GarageExceptions.ValueOutOfRangeException voore =
                        new GarageExceptions.ValueOutOfRangeException("The length of input should be between ", i_MinLength, i_MaxLength);
                    DisplayMessage(voore.Message);
                }
            }
        }

        public void GetVariable(ref float io_FloatToInitialize, float i_MinVal, float i_MaxVal)
        {
            bool isValid = false;
            while(isValid == false)
            {
                GetVariable(ref io_FloatToInitialize);
                if(i_MinVal <= io_FloatToInitialize && io_FloatToInitialize <= i_MaxVal)
                {
                    isValid = true;
                }
                else
                {
                    //// input is not valid
                    GarageExceptions.ValueOutOfRangeException voore =
                        new GarageExceptions.ValueOutOfRangeException(i_MinVal, i_MaxVal);
                    DisplayMessage(voore.Message);
                }
            }
        }

        public void GetVariable(ref int io_IntToInitialize, int i_MinVal, int i_MaxVal)
        {
            bool isValid = false;
            while(isValid == false)
            {
                GetVariable(ref io_IntToInitialize);
                if(i_MinVal <= io_IntToInitialize && io_IntToInitialize <= i_MaxVal)
                {
                    isValid = true;
                }
                else
                {
                    GarageExceptions.ValueOutOfRangeException voore =
                        new GarageExceptions.ValueOutOfRangeException(i_MinVal, i_MaxVal);
                    DisplayMessage(voore.Message);
                }
            }
        }

        public void GetNumbers(ref string io_StringToInitialize, int i_MinLength, int i_MaxLength)
        {
            bool isValid = false;
            while(isValid == false)
            {
                GetVariable(ref io_StringToInitialize, i_MinLength, i_MaxLength);
                if(io_StringToInitialize.All(char.IsDigit))
                {
                    isValid = true;
                }
                else
                {
                    ArgumentException ae = new ArgumentException(k_OnlyNumbersValidMessage);
                    DisplayErrorMessage(ae.Message);
                }
            }
        }

        public void GetName(ref string i_FullName, int i_MinLengthForNames, int i_MaxLengthForNames)
        {
            bool isValid = false;
            while(isValid == false)
            {
                GetVariable(ref i_FullName, i_MinLengthForNames, i_MaxLengthForNames);

                if(i_FullName.All(i_CharToCompare => char.IsWhiteSpace(i_CharToCompare) || char.IsLetter(i_CharToCompare)))
                {
                    isValid = true;
                }
                else
                {
                    ArgumentException ae = new ArgumentException(k_OnlyLettersValidMessage);
                    DisplayErrorMessage(ae.Message);
                }
            }
        }
    }
}
