using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounts
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Disconnect();
            /*try
            {
                TransferMoney(111, 222, 1000);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Server error");
              
            }*/
        }

        public static void Disconnect()
        {
            SqlConnection sqlConnection = new SqlConnection("Server=(localdb)\\mssqllocaldb;Database=StudentDB2024;Integrated Security=true");
            string selectSql = $"select * from students";
            SqlDataAdapter da = new SqlDataAdapter(selectSql, sqlConnection);
            DataSet ds=new DataSet();
            //SqlConnection.Open();
            da.Fill(ds,"students");
            //SqlConnection.Close);
            foreach(DataRow row in ds.Tables["students"].Rows)
            {
                Console.WriteLine(row["firstName"]);
                if (row["rollNo"] == "1002")
                {
                    row["mobile"] = "999999999"; // update
                    //row.Delete();// delete
                   

                }
                
            }
            // add a new row
            DataRow newRow = ds.Tables["students"].NewRow();
            newRow[0] = 1234;
            newRow[1] = "Kamlesh";
            newRow[2] = "Babu";
            newRow[3] = "02/03/2004";
            newRow[4] = "aaa@aa.aa";
            newRow[5] = "0909090909";
            newRow[6] = "BBA";
            ds.Tables["students"].Rows.Add(newRow);
            Console.WriteLine("lll");
            SqlCommandBuilder sqlbuilder = new SqlCommandBuilder(da);
            da.Update(ds, "students");

        }





        public static void TransferMoney(int fromAcc,int toAccNo,int amount)
        {
            SqlConnection sqlConnection = new SqlConnection("Server=(localdb)\\mssqllocaldb;Database=StudentDB2024;Integrated Security=true");
            
            string update1 = $"update accounts set balance=balance-{amount} where accno={fromAcc}";
            string update2 = $"update accounts set balance=balance+{amount} where accno={toAccNo}";
            SqlCommand cmd1 = new SqlCommand(update1, sqlConnection);
            SqlCommand cmd2 = new SqlCommand(update2, sqlConnection);
            sqlConnection.Open();
            SqlTransaction tx = sqlConnection.BeginTransaction();
            cmd1.Transaction = tx;
            cmd2.Transaction = tx;


            using (sqlConnection)
            {
                try
                {
                    
                    cmd1.ExecuteNonQuery();
                    //
                    //throw new Exception();
                    cmd2.ExecuteNonQuery();
                    tx.Commit();
                } catch (Exception ex)
                {
                    tx.Rollback();
                    throw ex;
                }
            }


        }
    }

}
