using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class StudentGroup
    {
        //ліст
        private List<Student> _students = new();

        public string GroupName { get; set; }
        public string Specialty { get; set; }
        public int Course { get; set; }
        //властивості
        public int GroupSize => _students.Count;
        //властивість для середнього балу групи
        public double AverageGroupGrade =>
            _students.Count == 0 ? 0 : Math.Round(_students.Average(s => s.averageGrade), 2);

        //додавання та видалення студентів
        public void AddStudent(Student s)
        {
            if (_students.Any(x => x.recordBookNumber == s.recordBookNumber))
                throw new Exception("Студент з таким номером вже існує!");

            _students.Add(s);
        }
        public void RemoveStudent(string recordBookNumber)
        {
            _students.RemoveAll(s => s.recordBookNumber == recordBookNumber);
        }

        //пошук студентів за ПІБ та номером залікової
        public List<Student> FindStudent(string query)
        {
            return _students
                .Where(s => s.fullName.Contains(query, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public Student FindStudent(int recordBookNumber)
        {
            return _students.FirstOrDefault(s => s.recordBookNumber == recordBookNumber.ToString());
        }
        //отримання списку відмінників та студентів за статусом
        public List<Student> GetExcellentStudents() =>
            _students.Where(s => s.IsExcellent()).ToList();

        public List<Student> GetStudentsByStatus(StudentStatus status) =>
            _students.Where(s => s.Status == status).ToList();
        //збереження та завантаження групи у файл
        public void SaveToFile(string path)
        {
            var data = new GroupDto
            {
                GroupName = GroupName,
                Specialty = Specialty,
                Course = Course,
                Students = _students
            };

            File.WriteAllText(path, JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true }));
            Console.WriteLine("Групу збережено у файл.");
        }
        public void LoadFromFile(string path)
        {
            if (!File.Exists(path)) return;

            var data = JsonSerializer.Deserialize<GroupDto>(File.ReadAllText(path));

            GroupName = data.GroupName;
            Specialty = data.Specialty;
            Course = data.Course;
            _students = data.Students ?? new();
            Console.WriteLine("Групу завантажено з файлу.");
        }
        //внутрішній клас для серіалізації
        private class GroupDto
        {
            public string GroupName { get; set; }
            public string Specialty { get; set; }
            public int Course { get; set; }
            public List<Student> Students { get; set; }
        }
    }
}