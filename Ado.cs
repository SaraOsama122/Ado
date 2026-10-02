using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace ConsoleApp1
{
    internal class Ado
    {
        static void Main(string[] args)
        {
            string str = @"Server=sara\MSSQLSERVER01;Database=Ado;Trusted_Connection=True;TrustServerCertificate=True;";
            
            SqlConnection con = new SqlConnection(str);
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Employee", con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);

            DataTable dt = new DataTable();
            da.Fill(dt);

            dt.PrimaryKey = new DataColumn[] { dt.Columns["ID"] };

            int choice = 0;

            while (choice != 4)
            {
                Console.Clear();
                Console.WriteLine("1. Add Employee");
                Console.WriteLine("2. Display Employees");
                Console.WriteLine("3. Delete Employee");
                Console.WriteLine("4. Exit");
                Console.Write("Enter choice: ");

                choice = int.Parse(Console.ReadLine());

                if (choice == 1)
                {
                    DataRow dr = dt.NewRow();

                    Console.Write("ID: ");
                    dr["ID"] = int.Parse(Console.ReadLine());

                    Console.Write("Name: ");
                    dr["Name"] = Console.ReadLine();

                    Console.Write("Age: ");
                    dr["Age"] = int.Parse(Console.ReadLine());

                    dt.Rows.Add(dr);
                    da.Update(dt);

                    Console.WriteLine("Added!");
                    Console.ReadKey();
                }
                else if (choice == 2)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        if (row.RowState != DataRowState.Deleted)
                        {
                            Console.WriteLine(row["ID"] + " - " + row["Name"] + " - " + row["Age"]);
                        }
                    }
                    Console.ReadKey();
                }
                else if (choice == 3)
                {
                    Console.Write("Enter ID to delete: ");
                    int id = int.Parse(Console.ReadLine());

                    DataRow row = dt.Rows.Find(id);

                    if (row != null)
                    {
                        row.Delete();
                        da.Update(dt);
                        Console.WriteLine("Deleted!");
                    }
                    else
                    {
                        Console.WriteLine("Not found!");
                    }
                    Console.ReadKey();
                }
            }
        }
    }
}
