using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    class UserInterface
    {
        private const string k_ForamtError = "This is the wrong format! ";
        private const string k_ArgumenttError = "This is the wrong Argument! ";
        private const string k_ExitMassage = "Goodbye! ";
        private readonly string r_SeparateLine = new string('~',15);

        public string GetLicenseNumber()
        {
            ////TODO: read from controller
            string msgToPrint = "Please insert your license Number:";
            DisplayMassage(msgToPrint);
            return Console.ReadLine();
        }
        
        public void DisplayMassage(string i_Msg)
        {
            Console.Clear();
            Console.WriteLine(r_SeparateLine);
            Console.WriteLine(i_Msg);
        }

        

        public string GetVar()
        {
            return Console.ReadLine();
        }

        public void DisplayFormatError()
        { 
           Console.WriteLine(r_SeparateLine);
           Console.WriteLine(k_ForamtError);
        }
        public void DisplayArgumentError()
        {
            Console.WriteLine(r_SeparateLine);
            Console.WriteLine(k_ArgumenttError);
        }
    }
}
