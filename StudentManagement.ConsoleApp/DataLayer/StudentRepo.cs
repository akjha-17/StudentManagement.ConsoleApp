using StudentManagement.ConsoleApp.Entities;
using StudentManagement.ConsoleApp.Presentation;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.ConsoleApp.DataLayer
{
    public class StudentRepo : IStudentsRepo
    {
        //SqlConnection conn = null;    //*----

        IDbConnection conn = null;
        public StudentRepo() {

            //conn = new SqlConnection();     //*----

            //conn.ConnectionString = ConfigurationManager.ConnectionStrings["defaultConnection"].ConnectionString;


            // if we want a flexible db connecter
            // using factory

            string provider= ConfigurationManager.ConnectionStrings["defaultConnection"].ProviderName;
            DbProviderFactory factory = DbProviderFactories.GetFactory(provider);

            conn= factory.CreateConnection();


            conn.ConnectionString = ConfigurationManager.ConnectionStrings["defaultConnection"].ConnectionString;

            // conn.Open();
            // Console.WriteLine("Connected...");
            /*
             string sqlInsert = $"insert into students values('{student.FirstName}','{student.LastName}','{student.Dob}','{student.Dob}','{student.Mobile}','{student.Course}')";
             SqlCommand cmd = new SqlCommand(sqlInsert, conn);
             cmd.ExecuteNonQuery();
             conn.Close();
            */

        }

        public Student GetStudentByID(int id)
        {
            Student student= new Student();
            //string sqlQuery = $"SELECT * FROM students where RollNO={id}";
            //string sqlQry = $"SELECT * FROM students where RollNO=@id";
            string sqlQry = "sp_Select_Student";
            conn.Open();
            //SqlCommand command = new SqlCommand(sqlQuery, conn);
            IDbCommand command = conn.CreateCommand();
            command.CommandText = sqlQry;
            command.CommandType = CommandType.StoredProcedure;
            command.Connection = conn;
            //SqlParameter p1 = new SqlParameter();
            //command.Parameters.AddWithValue("@id", student.RolNo);
            IDbDataParameter p1 = command.CreateParameter();
            p1.ParameterName = "@id";
            p1.Value = id;
            command.Parameters.Add(p1);
            //SqlDataReader reader = command.ExecuteReader();
            IDataReader reader = command.ExecuteReader();
            //if (reader.HasRows)

                while (reader.Read())
                {
                    student.RollNo=id;
                    student.FirstName = reader.GetString(1);
                    student.LastName= reader.GetString(2);
                    student.Dob=reader.GetDateTime(3);
                    student.Email= reader.GetString(4);
                    student.Mobile= reader.GetString(5);
                    student.Course= reader.GetString(6);
                }
            reader.Close();
            conn.Close();
            return student;

        }

        public List<Student> GetAllStudent()
        {
            List<Student> students = new List<Student>();
            //string sqlQuery = $"SELECT * FROM students";
            string sqlQuery = "sp_Select_All_Students";
            conn.Open();
            IDbCommand command = conn.CreateCommand();
            //SqlCommand command = new SqlCommand(sqlQuery, conn);
            command.CommandText= sqlQuery;
            command.CommandType = CommandType.StoredProcedure;
            command.Connection = conn;
            //SqlDataReader reader = command.ExecuteReader();
            IDataReader reader =command.ExecuteReader();
            //if (reader.HasRows)
            
                while (reader.Read())
                {
                    Student student = new Student();
                    student.RollNo = reader.GetInt32(0); 
                    student.FirstName = reader.GetString(1);
                    student.LastName = reader.GetString(2);
                    student.Dob = reader.GetDateTime(3);
                    student.Email = reader.GetString(4);
                    student.Mobile = reader.GetString(5);
                    student.Course = reader.GetString(6);
                    students.Add(student);
                }
            
            reader.Close();
            conn.Close();
            return students;
        }

        public void DeleteStudentByID(int id)
        {
            //string sqlQuery = $"DELETE FROM students where RollNo=@sid";
            string sqlQuery = "sp_Delete_Student";
            conn.Open();
            //SqlCommand command = new SqlCommand(sqlQuery, conn);
            IDbCommand command= conn.CreateCommand();
            command.CommandText= sqlQuery;
            command.Connection = conn;
            command.CommandType = CommandType.StoredProcedure;
            IDataParameter p1 = command.CreateParameter();
            p1.ParameterName = "sid";
            p1.Value = id;
            command.Parameters.Add(p1);
            command.ExecuteNonQuery();
            conn.Close();

        }

        public void Save(Student student)
        {

            // string sqlInsert = $"insert into students values ('{student.FirstName}','{student.LastName}','{student.Dob}','{student.Email}','{student.Mobile}','{student.Course}')";
            //string sqlInsert = $"insert into students values (@fname,@lname,@dob,@email,@mobile,@course)";
           // stored procedure
            string sqlInsert = "sp_Insert_Student";
            //conn.Open();
            IDbCommand cmd= conn.CreateCommand();
            cmd.CommandText= sqlInsert;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;

            IDbDataParameter p1 = cmd.CreateParameter();
            p1.ParameterName = "@fname";
            p1.Value = student.FirstName;
            cmd.Parameters.Add(p1);

            //cmd.Parameters.AddWithValue("@lname", student.LastName);
            IDbDataParameter p2 = cmd.CreateParameter();
            p2.ParameterName = "@lname";
            p2.Value = student.LastName;
            cmd.Parameters.Add(p2);

            //SqlParameter p3 = new SqlParameter();
            IDbDataParameter p3 = cmd.CreateParameter();
            p3.ParameterName = "@dob";
            p3.Value = student.Dob;
            cmd.Parameters.Add(p3);

            //SqlParameter p4 = new SqlParameter();
            IDbDataParameter p4 = cmd.CreateParameter();
            p4.ParameterName = "@email";
            p4.Value = student.Email;
            cmd.Parameters.Add(p4);

            //SqlParameter p5 = new SqlParameter();
            IDbDataParameter p5 = cmd.CreateParameter();
            p5.ParameterName = "@mobile";
            p5.Value = student.Mobile;
            cmd.Parameters.Add(p5);

            //SqlParameter p6 = new SqlParameter();
            IDbDataParameter p6 = cmd.CreateParameter();
            p6.ParameterName = "@course";
            p6.Value = student.Course;
            cmd.Parameters.Add(p6);


            //SqlCommand sqlCommand = new SqlCommand(sqlInsert, conn);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void UpdateStudent(Student student)
        {
            //string sqlUpdate = $"update students set FirstName=@fname,LastName=@lname,Dob=@dob,Email=@email,Mobile=@mobile,Course=@course WHERE RollNO=@sid";
            string sqlUpdate = "sp_Update_Student";
            conn.Open();
            IDbCommand cmd = conn.CreateCommand();
            cmd.CommandText = sqlUpdate;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;


            IDbDataParameter p = cmd.CreateParameter();
            p.ParameterName = "@sid";
            p.Value = student.RollNo;
            cmd.Parameters.Add(p);

            IDbDataParameter p1 = cmd.CreateParameter();
            p1.ParameterName = "@fname";
            p1.Value = student.FirstName;
            cmd.Parameters.Add(p1);

            //cmd.Parameters.AddWithValue("@lname", student.LastName);
            IDbDataParameter p2 = cmd.CreateParameter();
            p2.ParameterName = "@lname";
            p2.Value = student.LastName;
            cmd.Parameters.Add(p2);

            //SqlParameter p3 = new SqlParameter();
            IDbDataParameter p3 = cmd.CreateParameter();
            p3.ParameterName = "@dob";
            p3.Value = student.Dob;
            cmd.Parameters.Add(p3);

            //SqlParameter p4 = new SqlParameter();
            IDbDataParameter p4 = cmd.CreateParameter();
            p4.ParameterName = "@email";
            p4.Value = student.Email;
            cmd.Parameters.Add(p4);

            //SqlParameter p5 = new SqlParameter();
            IDbDataParameter p5 = cmd.CreateParameter();
            p5.ParameterName = "@mobile";
            p5.Value = student.Mobile;
            cmd.Parameters.Add(p5);

            //SqlParameter p6 = new SqlParameter();
            IDbDataParameter p6 = cmd.CreateParameter();
            p6.ParameterName = "@course";
            p6.Value = student.Course;
            cmd.Parameters.Add(p6);


            //SqlCommand sqlCommand = new SqlCommand(sqlInsert, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}
