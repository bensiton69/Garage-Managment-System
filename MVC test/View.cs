using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_test
{
    class View
    {
        private string m_FirstNum;
        private string m_SecondNum;
        private string m_Lable;
        private string m_CalaSol;

        public int FirstNum
        {
            get
            {
                return int.Parse(m_FirstNum);
            }
        }
        public int SecondNum
        {
            get
            {
                return int.Parse(m_SecondNum);
            }
        }
        public string Lable
        {
            get
            {
                return m_Lable;
            }
        }
        public int CalcSol
        {
            get
            {
                return int.Parse(m_CalaSol);
            }
            set
            {
                m_CalaSol = value.ToString();
            }
        }
    }
}
