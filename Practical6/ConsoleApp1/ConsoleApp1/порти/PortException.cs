using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.порти
{
    public class PortException : Exception
    {
        public PortException(string message) : base(message)
        {

        }
    }
}
