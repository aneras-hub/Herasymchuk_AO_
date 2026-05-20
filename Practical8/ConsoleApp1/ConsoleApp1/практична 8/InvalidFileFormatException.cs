using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.практична_8
{
    public class InvalidFileFormatException : Exception
    {
        public InvalidFileFormatException()
        {
        }

        public InvalidFileFormatException(string message)
            : base(message)
        {
        }

        public InvalidFileFormatException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}