using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace ConsoleApp1.порти
{
    public class PortLogger
    {
        private readonly StringBuilder logBuilder = new();
        public PortLogger()
        {
            logBuilder = new StringBuilder();
        }
        public void LogOperation(string operation, int portNumber, string details)
        {
            logBuilder.AppendLine(
                $"[{DateTime.Now: HH:mm:ss}] " +
                $"{operation} | " +
                $"Port: {portNumber} | " +
                $"{details}"
            );
        }
        public string GetFullLog()
        {
            return logBuilder.ToString();
        }
        public void SaveLogToFile(string path = "port_log.txt")
        {
            File.WriteAllText("port_log.txt", logBuilder.ToString());
        }
    }
}
