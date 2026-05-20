using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.практична_9
{
    public class GroupReportEventArgs : EventArgs
    {
        public StudentGroup Group { get; }

        public string ReportText { get; }

        public DateTime GeneratedAt { get; }

        public GroupReportEventArgs(
            StudentGroup group,
            string reportText)
        {
            Group = group;
            ReportText = reportText;
            GeneratedAt = DateTime.Now;
        }
    }
}