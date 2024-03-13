using StudentManagement.ConsoleApp.DataLayer;
using StudentManagement.ConsoleApp.Entities;
using StudentManagement.ConsoleApp.Presentation;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.ConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {

            while (true)
            {
                Console.WriteLine("Student Manager");
                Console.WriteLine("=====================");
                Console.WriteLine("1. Create Student");
                Console.WriteLine("2. Get All Student");
                Console.WriteLine("3. Get Student By ID");
                Console.WriteLine("4. Edit Student");
                Console.WriteLine("5. Delete Student");
                Console.WriteLine("6. Exit");
                Console.WriteLine("----------------------");
                Console.Write("Enter your choice [1-6] :");
                int choice = int.Parse(Console.ReadLine());
                IStudentsRepo repository = new StudentRepo();
                switch (choice)
                {
                    case 1: CreateStudent(repository); break;
                    case 2: GetAllStudent(repository); break;
                    case 3: GetStudentById(repository); break;
                    case 4: EditStudent(repository); break;
                    case 5: DeleteStudent(repository); break;
                    case 6: Environment.Exit(0); break;
                    default: Console.WriteLine("Invalid option"); break;
                }

            }
        }






        // collect student data from user and store in db
        /*Student student = new Student
        {
            FirstName = "Aman",
            LastName = "Jha",
            Dob = DateTime.Parse("01/02/2007"),
            Email = "amankumarj17@gmail.com",
            Mobile = "9110228386",
            Course = "CSE",

        };*/


        // in DAL
        // DB Server :SQL Server
        // Database: StudentDB2024
        // Table: Student
        // Fraamework: ADO.Net
        // Approach:Connected Approach

        private static void DeleteStudent(IStudentsRepo repository)
        {
            Console.Write("Student ID to be deleted:");
            int id = int.Parse(Console.ReadLine());

            repository.DeleteStudentByID(id);
            Console.WriteLine("Contact deleted successfully");
        }
        private static void EditStudent(IStudentsRepo repository)
        {
            Student student = new Student();
            Console.Write("Contact ID to be updated:");
            int id = int.Parse(Console.ReadLine());
            student.RollNo = id;
            Console.Write("New Student's first Name :");
            student.FirstName = Console.ReadLine();
            Console.Write("New Student's last Name :");
            student.LastName = Console.ReadLine();
            Console.Write("New DOB:");
            student.Dob = DateTime.Parse(Console.ReadLine());
            Console.Write("New Email ID:");
            student.Email = Console.ReadLine();
            Console.Write("New Mobile No :");
            student.Mobile = Console.ReadLine();
            Console.Write("New Course :");
            student.Course = Console.ReadLine();
            repository.UpdateStudent(student);
            Console.WriteLine("Contact updated successfully");
        }

        private static void GetStudentById(IStudentsRepo repository)
        {
            Console.WriteLine("Enter ID for which u want details");
            int userInput = int.Parse(Console.ReadLine());
            Student s = repository.GetStudentByID(userInput);
            Console.WriteLine("Student ID: " + s.RollNo);
            Console.WriteLine("Student First Name: " + s.FirstName);
            Console.WriteLine("Student First Name: " + s.LastName);
            Console.WriteLine("Student Dob: " + s.Dob);
            Console.WriteLine("Email ID: " + s.Email);
            Console.WriteLine("Mobile No: " + s.Mobile);
            Console.WriteLine("Course: " + s.Course);
            Console.WriteLine("---------------------");
        }

        private static void GetAllStudent(IStudentsRepo repository)
        {
            List<Student> students = new List<Student>();
            students = repository.GetAllStudent();
            Console.WriteLine("Students are:");
            foreach (Student s in students)
            {
                Console.WriteLine("Student ID: " + s.RollNo);
                Console.WriteLine("Student First Name: " + s.FirstName);
                Console.WriteLine("Student First Name: " + s.LastName);
                Console.WriteLine("Student Dob: " + s.Dob);
                Console.WriteLine("Email ID: " + s.Email);
                Console.WriteLine("Mobile No: " + s.Mobile);
                Console.WriteLine("Couse: " + s.Course);
                Console.WriteLine("---------------------");
            }
        }

        private static void CreateStudent(IStudentsRepo repository)
        {
            Student student = new Student();
            Console.Write("New Student's first Name :");
            student.FirstName = Console.ReadLine();
            Console.Write("New Student's last Name :");
            student.LastName = Console.ReadLine();
            Console.Write("New DOB:");
            student.Dob = DateTime.Parse(Console.ReadLine());
            Console.Write("New Email ID:");
            student.Email = Console.ReadLine();
            Console.Write("New Mobile No :");
            student.Mobile = Console.ReadLine();
            Console.Write("New Course :");
            student.Course = Console.ReadLine();
            repository.Save(student);
            Console.WriteLine("Contact saved successfully");
        }



    }
}






























        // step1: connect to db
/*        SqlConnection conn = new SqlConnection();
            conn.ConnectionString = @"Server=(localdb)\mssqllocaldb;Database=StudentDB2024;Integrated Security=true";
            conn.Open();
            Console.WriteLine("Connected...");*/


            // step2: send sql commands to db
            /*string sqlInsert=$"insert into students values('{student.FirstName}','{student.LastName}','{student.Dob}','{student.Dob}','{student.Mobile}','{student.Course}')";
            SqlCommand cmd = new SqlCommand(sqlInsert,conn);
            cmd.ExecuteNonQuery();*/

            //step3:  process the returned data

            // step4: close db
            /*conn.Close();*/
