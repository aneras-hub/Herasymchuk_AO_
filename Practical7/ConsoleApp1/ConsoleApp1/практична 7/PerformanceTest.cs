using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.практична_7
{
    public class PerformanceTest
    {
        private const int Count = 100000;

        public string Run()
        {
            Stopwatch stopwatch = new Stopwatch();

            long memoryBeforeStruct;
            long memoryAfterStruct;
            long memoryBeforeClass;
            long memoryAfterClass;

            long fillStructTime;
            long sortStructTime;
            long searchStructTime;

            long fillClassTime;
            long sortClassTime;
            long searchClassTime;

            StudentRecord[] studentRecords;
            Student[] students;

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            memoryBeforeStruct = GC.GetTotalMemory(true);

            stopwatch.Start();
            studentRecords = GenerateStudentRecords();
            stopwatch.Stop();
            fillStructTime = stopwatch.ElapsedMilliseconds;

            memoryAfterStruct = GC.GetTotalMemory(true);

            stopwatch.Restart();
            Array.Sort(studentRecords, (a, b) => a.AverageGrade.CompareTo(b.AverageGrade));
            stopwatch.Stop();
            sortStructTime = stopwatch.ElapsedMilliseconds;

            stopwatch.Restart();
            StudentRecord foundRecord = studentRecords.FirstOrDefault(s => s.RecordBookNumber == "50000");
            stopwatch.Stop();
            searchStructTime = stopwatch.ElapsedMilliseconds;

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            memoryBeforeClass = GC.GetTotalMemory(true);

            stopwatch.Restart();
            students = GenerateStudents();
            stopwatch.Stop();
            fillClassTime = stopwatch.ElapsedMilliseconds;

            memoryAfterClass = GC.GetTotalMemory(true);

            stopwatch.Restart();
            Array.Sort(students, (a, b) => a.averageGrade.CompareTo(b.averageGrade));
            stopwatch.Stop();
            sortClassTime = stopwatch.ElapsedMilliseconds;

            stopwatch.Restart();
            Student foundStudent = students.FirstOrDefault(s => s.RecordBookNumber == "50000");
            stopwatch.Stop();
            searchClassTime = stopwatch.ElapsedMilliseconds;

            long structMemory = memoryAfterStruct - memoryBeforeStruct;
            long classMemory = memoryAfterClass - memoryBeforeClass;

            return
                "=== ПОРІВНЯННЯ ПРОДУКТИВНОСТІ STRUCT VS CLASS ===\n\n" +
                $"Кількість елементів: {Count}\n\n" +
                $"{"Операція",-25} {"Struct",-15} {"Class",-15}\n" +
                $"{new string('-', 55)}\n" +
                $"{"Заповнення",-25} {fillStructTime + " ms",-15} {fillClassTime + " ms",-15}\n" +
                $"{"Сортування",-25} {sortStructTime + " ms",-15} {sortClassTime + " ms",-15}\n" +
                $"{"Пошук",-25} {searchStructTime + " ms",-15} {searchClassTime + " ms",-15}\n" +
                $"{"Пам'ять",-25} {structMemory + " bytes",-15} {classMemory + " bytes",-15}\n";
        }

        private StudentRecord[] GenerateStudentRecords()
        {
            StudentRecord[] records = new StudentRecord[Count];

            for (int i = 0; i < Count; i++)
            {
                records[i] = new StudentRecord(
                    $"Студент {i}",
                    i.ToString(),
                    i % 100,
                    i % 101
                );
            }

            return records;
        }

        private Student[] GenerateStudents()
        {
            Student[] students = new Student[Count];

            for (int i = 0; i < Count; i++)
            {
                Student student = new Student(
                    $"Студент {i} Тестовий",
                    DateTime.Now.AddYears(-18),
                    $"student{i}@test.com",
                    DateTime.Now,
                    i.ToString()
                );

                student.CourseProgress = i % 101;
                student.Journal.SetGrade("Тест", i % 100);

                students[i] = student;
            }

            return students;
        }
    }
}