using System;

namespace Ex03.ConsoleUI
{
    class UserInterface
    {
        private const string k_ForamtError = "This is the wrong format! ";
        private const string k_ArgumenttError = "This is the wrong Argument! ";
        private const string k_ExitMessage = "Goodbye! ";
        private readonly string r_SeparateLine = new string('~',15);

        
        public void DisplayMessage(string i_Msg)
        {
            //Console.Clear();
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
           Console.WriteLine(k_ForamtError);
        }
        public void DisplayArgumentError()
        {
            Console.WriteLine(r_SeparateLine);
            Console.WriteLine(k_ArgumenttError);
        }

    }
}
