using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_test
{
    class Model
    {
        private int m_Sum = 0;
        public int Sum
        {
            get
            {
                return m_Sum;
            }
        }
        public void Calc(int i_FirstNum, int i_SecondNum)
        {
            m_Sum = i_FirstNum + i_SecondNum;
        }
    }
}
