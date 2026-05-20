using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.практична_9
{
    // Делегат для операцій над студентом
    public delegate void StudentOperation(Student student);

    // Делегат для операцій над групою
    public delegate void GroupOperation(StudentGroup group);
}
