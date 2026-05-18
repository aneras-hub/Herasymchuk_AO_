using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class PortMatrix
    {
        private Port[,] ports;
        private const int Size = 16;
        public PortMatrix()
        {
            ports = new Port[Size, Size];
            InitializeMatrix();
        }
        private void InitializeMatrix()
        {
            int portNumber = 1;
            for (int row = 0; row < 16; row++)
            {
                for (int col = 0; col < 16; col++)
                {
                    ports[row, col] = new Port(portNumber, $"Device-{portNumber}");
                    portNumber++;
                }
            }
        }
        public void OpenPort(int row, int col)
        {
            ValidateIndexes(row, col);
            ports[row, col].Open();
        }
        public void ClosePort(int row, int col)
        {
            ValidateIndexes(row, col);
            ports[row, col].Close();
        }
        public void WriteToPort(int row, int col, byte[] data)
        {
            ValidateIndexes(row, col);
            ports[row, col].WriteData(data);
        }
        public byte[] ReadFromPort(int row, int col)
        {
            ValidateIndexes(row, col);
            return ports[row, col].ReadData();
        }
        public string ScanMatrix()
        {
            StringBuilder portM = new StringBuilder();

            for (int row = 0; row < 16; row++)
            {
                for (int col = 0; col < 16; col++)
                {
                    Port port = ports[row, col];
                    portM.Append($"[{port.PortNumber}");
                    if (port.IsOpen)
                    {
                        portM.Append(":Відкрито");
                    }
                    else
                    {
                        portM.Append(":Закрито");
                    }
                    portM.Append("] ");
                }
                portM.AppendLine();
            }
            return portM.ToString();
        }

        public Port GetPort(int row, int col)
        {
            ValidateIndexes(row, col);
            return ports[row, col];
        }
        private void ValidateIndexes(int row, int col)
        {
            if (row < 0 || row >= 16 || col < 0 || col >= 16)
            {
                throw new IndexOutOfRangeException("Невірні координати порту!");
            }
        }
        public List<Port> GetOpenPortsByDevice(string deviceName)
        {
            List<Port> result = new List<Port>();

            for (int row = 0; row < Size; row++)
            {
                for (int col = 0; col < Size; col++)
                {
                    Port port = ports[row, col];

                    if (port.IsOpen &&
                        port.DeviceName.Contains(deviceName,
                        StringComparison.OrdinalIgnoreCase))
                    {
                        result.Add(port);
                    }
                }
            }

            return result;
        }
    }
}
