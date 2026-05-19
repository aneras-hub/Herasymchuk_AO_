using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Port : ICloneable
    {
        public int PortNumber { get; }
        public byte[] DataBuffer { get; }
        public bool IsOpen { get; private set; }
        public string DeviceName { get; }
        private int dataLength = 0;
        public Port(int portNumber, string deviceName)
        {
            PortNumber = portNumber;
            DeviceName = deviceName;
            DataBuffer = new byte[64];
            IsOpen = false;
        }

        public void Open()
        {
            IsOpen = true;
        }
        public void Close()
        {
            IsOpen = false;
        }
        public void WriteData(byte[] data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            if (!IsOpen)
                throw new PortException("Порт закритий!");

            dataLength = Math.Min(data.Length, DataBuffer.Length);
            Array.Clear(DataBuffer, 0, DataBuffer.Length);
            Array.Copy(data, DataBuffer, dataLength);
        }
        public byte[] ReadData()
        {
            if (!IsOpen)
            {
                throw new Exception("Порт закритий!");
            }
            return DataBuffer.Take(dataLength).ToArray();
        }
        public string GetPortInfo()
        {
            StringBuilder port = new StringBuilder();

            port.AppendLine($"Порт №: {PortNumber}");
            port.AppendLine($"Пристрій: {DeviceName}");
            port.AppendLine($"Стан: {(IsOpen ? "Відкритий" : "Закритий")}");

            return port.ToString();
        }
        public object Clone()
        {
            Port copy = new Port(this.PortNumber, this.DeviceName);

            if (this.IsOpen)
            {
                copy.Open();
            }

            Array.Copy(this.DataBuffer, copy.DataBuffer, this.DataBuffer.Length);

            return copy;
        }
    }
}