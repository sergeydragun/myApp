using Microsoft.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace myApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {

            string numberOfProblem = "6"; // args[0];
            string connection = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Сергей\source\repos\myApp\myApp\MyAppDataBase.mdf;Integrated Security=True"; 
            Table table = new Table();

            switch (numberOfProblem)
            {
                case "1":
                    await table.Create(connection);
                    break;
                case "2":
                    await table.Insert(args, connection);
                    break;
                case "3":
                    await table.Select(connection);
                    break;
                case "4":
                    await table.InsertOneMillionPeople(connection);
                    break;
                case "5":
                    await table.SecondSelect(connection);
                    break;
                case "6":
                    await table.Delete(connection);
                    break;

            }
            

            //await table.Delete(connection);
            
            //await table.Insert(args, connection);
            //await table.Select(connection);
        }
    }
}