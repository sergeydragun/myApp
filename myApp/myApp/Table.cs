using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace myApp
{
    internal class Table
    {
        public async Task Create(string connection)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connection))
            {
                await sqlConnection.OpenAsync();

                SqlCommand command = new SqlCommand();
                command.CommandText = "CREATE TABLE [Table_Pesons]" +
                    "\r\n(\r\n\t[Id] INT NOT NULL PRIMARY KEY IDENTITY," +
                    "\r\n\t[SurName] NVARCHAR(20)," +
                    "\r\n\t[Name] NVARCHAR(20)," +
                    "\r\n\t[Patronymic] NVARCHAR(20)," +
                    "\r\n\t[DateBirthday] DATE," +
                    "\r\n\t[Sex] NVARCHAR(20)\r\n)";
                command.Connection = sqlConnection;
                await command.ExecuteNonQueryAsync();
                Console.WriteLine("БД создана");
            }       
        }

        public async Task Delete(string connection)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connection))
            {
                await sqlConnection.OpenAsync();

                SqlCommand command = new SqlCommand();
                command.CommandText = "DROP TABLE [Table_Pesons]";
                command.Connection = sqlConnection;
                await command.ExecuteNonQueryAsync();
                Console.WriteLine("БД удалена");
            }
        }

        public async Task Insert(string[] args, string connection)
        {           
            using (SqlConnection sqlConnection = new SqlConnection(connection))
            {
                await sqlConnection.OpenAsync();
                SqlCommand command = new SqlCommand();
                string exp = "INSERT INTO [Table_Pesons] ([SurName], [Name], [Patronymic], [DateBirthday], [Sex]) VALUES (";

                for(int i = 1; i < args.Length; i++)
                {
                    exp += "'";
                    exp += args[i];
                    exp += "',";
                    exp += " ";                   
                }
                exp = exp.Substring(0, exp.Length - 2);
                exp += ")";
                Console.WriteLine(exp);
                command.CommandText = exp;
                command.Connection = sqlConnection;
                await command.ExecuteNonQueryAsync();
                Console.WriteLine("Данные записаны");
            }
        }

        public async Task Select(string connection)
        {
            string exp = "SELECT DISTINCT [SurName], [Name], [Patronymic], [DateBirthday], [Sex]," +
                " DATEDIFF(YY, [DateBirthday], GETDATE()) AS Age" +
                " FROM [Table_Pesons]" +
                " ORDER BY [SurName], [Name], [Patronymic]";
            using (SqlConnection sqlConnection = new SqlConnection(connection))
            {
                await sqlConnection.OpenAsync();

                SqlCommand command = new SqlCommand(exp, sqlConnection);
                SqlDataReader reader = await command.ExecuteReaderAsync();
                command.Connection = sqlConnection;
                
                if(reader.HasRows)
                {
                    
                    string columnName1 = reader.GetName(0);
                    string columnName2 = reader.GetName(1);
                    string columnName3 = reader.GetName(2);
                    string columnName4 = reader.GetName(3);
                    string columnName5 = reader.GetName(4);
                    string columnName6 = reader.GetName(5);

                    Console.WriteLine($"{columnName1} | {columnName2} | {columnName3} | {columnName4} | {columnName5} {columnName6}");
                    

                    while(await reader.ReadAsync())
                    {
                        object columnName11 = reader.GetValue(0);
                        object columnName21 = reader.GetValue(1);
                        object columnName31 = reader.GetValue(2);
                        object columnName41 = reader.GetValue(3);
                        object columnName51 = reader.GetValue(4);
                        object columnName61 = reader.GetValue(5);

                        Console.WriteLine($"{columnName11}| {columnName21} | {columnName31} | {columnName41} | {columnName51} | {columnName61}");
                    }
                }

                await reader.CloseAsync();
                Console.WriteLine("Данные получены");
            }
           
        }

        public async Task InsertOneMillionPeople(string connection)
        {
            Generate generate= new Generate();
            string ex = generate.AllWhatWeNeed();
            Console.WriteLine(ex);
            using(SqlConnection sqlConnection = new SqlConnection(connection))
            {
                await sqlConnection.OpenAsync();
                SqlCommand command = new SqlCommand();
                command.CommandText = ex;
                command.Connection= sqlConnection;
                await command.ExecuteNonQueryAsync();
            }

            Console.WriteLine("Новые данные записаны");
        }

        public async Task SecondSelect(string connection)
        {
            string exp = "SELECT DISTINCT [SurName], [Name], [Patronymic], [DateBirthday], [Sex]," +
                " DATEDIFF(YY, [DateBirthday], GETDATE()) AS Age" +
                " FROM [Table_Pesons]" +
                " WHERE [Sex] = 'man' AND [SurName] LIKE 'F%'" +
                " ORDER BY [SurName], [Name], [Patronymic]";

            var sw = new Stopwatch();
            sw.Start();
            using (SqlConnection sqlConnection = new SqlConnection(connection))
            {
                await sqlConnection.OpenAsync();

                SqlCommand command = new SqlCommand(exp, sqlConnection);
                SqlDataReader reader = await command.ExecuteReaderAsync();
                command.Connection = sqlConnection;

                if (reader.HasRows)
                {

                    string columnName1 = reader.GetName(0);
                    string columnName2 = reader.GetName(1);
                    string columnName3 = reader.GetName(2);
                    string columnName4 = reader.GetName(3);
                    string columnName5 = reader.GetName(4);
                    string columnName6 = reader.GetName(5);

                    Console.WriteLine($"{columnName1} | {columnName2} | {columnName3} | {columnName4} | {columnName5} {columnName6}");


                    while (await reader.ReadAsync())
                    {
                        object columnName11 = reader.GetValue(0);
                        object columnName21 = reader.GetValue(1);
                        object columnName31 = reader.GetValue(2);
                        object columnName41 = reader.GetValue(3);
                        object columnName51 = reader.GetValue(4);
                        object columnName61 = reader.GetValue(5);

                        Console.WriteLine($"{columnName11}| {columnName21} | {columnName31} | {columnName41} | {columnName51} | {columnName61}");
                    }
                }

                await reader.CloseAsync();
                sw.Stop();

                Console.WriteLine($"Данные получены. Затраченное время: {sw.ElapsedMilliseconds}") ;
            }

        }
    }
}
