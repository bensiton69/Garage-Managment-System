using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMethodDesignPatternInCSharp
{
    class Program
    {
        public static void Main()
        {
            float? nullFloat = null;
            DoSomething(ref nullFloat);
            float myFloat = nullFloat?? new float();
            DoSomrthingWithFloat(myFloat);
        }

        private static void dosmothing()
        {
            throw new NotImplementedException();
        }

        public static void DoSomrthingWithFloat(float i_NullFloat)
        {
            Console.WriteLine(i_NullFloat.GetType());
        }

        public static void DoSomething(ref float? i_Float)
        {
            Console.WriteLine(i_Float.GetType());
        }
    }
}
