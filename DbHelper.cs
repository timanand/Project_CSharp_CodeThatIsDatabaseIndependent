// In Solution Explorer, Add References in: System.Configuration


using System;
using System.Configuration;
using System.Data; //IDbConnection IDbCommand


// Code for SQL Server
using System.Data.SqlClient; //SqlConnection SqlCommand

// Code for OLEDB 
//using System.Data.OleDb;




namespace DB_IndependentCodeUsingSQLServer
{
    public static class DbHelper
    {

        // Read Connection String from App.Config
        public static string GetConnectionString()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["CSharpADONET"].ConnectionString;
            return connectionString;
        }



        // Code for SQL Server - BEGIN
        public static IDbConnection CreateConnection()
        {
            string connectionString = GetConnectionString();
            return new SqlConnection(connectionString);
        }

        public static IDbCommand CreateCommand(string cmdText, IDbConnection con)
        {
            return new SqlCommand(cmdText, (SqlConnection)con);
        }
        // for SQL Server - END



        // BEGIN

        public static void AddParameter(this IDbCommand command, string name, object value)
        {
            var parameter = command.CreateParameter();
            parameter.ParameterName = name;
            parameter.Value = value;
            command.Parameters.Add(parameter);
        }



        // Code for OleDB
        //public static IDbConnection CreateConnection()
        //{
        //    string connectionString = GetConnectionString();
        //    return new OleDbConnection(connectionString);
        //}

        //public static IDbCommand CreateCommand(string cmdText, IDbConnection con)
        //{
        //    return new OleDbCommand(cmdText, (OleDbConnection)con);
        //}


    }
}
