using StudentManagement.ConsoleApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.ConsoleApp.Presentation
{
    public interface IStudentsRepo
    {

        void Save(Student student);
        List<Student> GetAllStudent();
        Student GetStudentByID(int id);
        void DeleteStudentByID(int id);
        void UpdateStudent(Student student);
    }

}
