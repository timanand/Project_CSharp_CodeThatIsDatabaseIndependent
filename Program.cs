using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data; // only fetch interface namespace





namespace DB_IndependentCodeUsingSQLServer
{
    class Demo1
    {
        static void Main()
        {

            ReadAllRecordsFromTable_Category();
            ReadDataFromTable_Employee(employeeID : 10);

            AddRecordToTable_Category("Test5");
            AddRecordToTable_Category_viaStoredProc("Test6");

            UpdateTable_Categories(12, "Sweets");

            Delete_CategoryRecord(10);

        }


        static void ReadAllRecordsFromTable_Category()
        {

            // Read all records from Category table

            IDbConnection con = null;

            try
            {

                con = DbHelper.CreateConnection();
                con.Open();

                IDbCommand cmd = DbHelper.CreateCommand("SELECT * FROM Category", con);

                IDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine(reader["CategoryId"] + " " + reader["CategoryName"]);
                }

            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }


            finally
            {
                con.Close();
                con.Dispose();
            }
        }


        static void ReadDataFromTable_Employee(int employeeID)
        {

            // Read data from Employee table by executing function, 'ReadEmployeeRecordByEmployeeId' and looking up EmployeeId

            // Need to run below SQL Script on NorthwindSlim database

            //CREATE FUNCTION[dbo].[ReadEmployeeRecordByEmployeeId](@Id INT)
            //RETURNS TABLE
            //AS
            //RETURN
            //SELECT* FROM Employee WHERE EmployeeId = @Id;


            IDbConnection con = null;


            try
            {
                
                con = DbHelper.CreateConnection();
                con.Open();

                IDbCommand cmd = DbHelper.CreateCommand("SELECT * FROM dbo.ReadEmployeeRecordByEmployeeId(" + employeeID.ToString() + ")", con);

                IDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine(reader["EmployeeId"] + " " + reader["FirstName"] + " " + reader["LastName"]);
                }

            }


            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }


            finally
            {
                con.Close();
                con.Dispose();
            }

        }



        static void AddRecordToTable_Category(string categoryName)
        {

            // Add record to Category table using passed parameter, categoryname

            IDbConnection con = null;


            try
            {
            
                con = DbHelper.CreateConnection();
                con.Open();

                IDbCommand cmd = DbHelper.CreateCommand("INSERT INTO Category(CategoryName) VALUES(@CategoryName)", con);

                //creating sql parametrers using the cmd object
                DbHelper.AddParameter(cmd, "@CategoryName", categoryName);

                cmd.ExecuteNonQuery();

                Console.WriteLine("Data inserted");

            }


            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }


            finally
            {
                con.Close();
                con.Dispose();
            }


        }



        static void AddRecordToTable_Category_viaStoredProc(string categoryName)
        {
            // Add record to Category table using passed parameter, categoryname (this uses Stored Procedure)

            // Need to run below SQL Script on NorthwindSlim database

            //CREATE PROC[dbo].[spDA_Category_Save]

            //    (
            //        @CategoryName NVARCHAR(15)
            //    )

            //AS

            //INSERT INTO Category
            //    (
            //        CategoryName

            //    )


            //VALUES
            //    (
            //        @CategoryName
            //    )


            IDbConnection con = null;


            try
            {

                con = DbHelper.CreateConnection();
                con.Open();

                IDbCommand cmd = DbHelper.CreateCommand("spDA_Category_Save", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                //creating sql parametrers using the cmd object
                DbHelper.AddParameter(cmd, "@CategoryName", categoryName);

                cmd.ExecuteNonQuery();

                Console.WriteLine("Data inserted");

            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }


            finally
            {
                con.Close();
                con.Dispose();
            }
            
        }



        static void UpdateTable_Categories(int categoryID, string updatedCategoryName)
        {

            // Update Categories table with parameter, updatedCategoryName on passed parameter, categoryID (key)

            IDbConnection con = null;

            try
            {

                con = DbHelper.CreateConnection();
                con.Open();

                IDbCommand cmd = DbHelper.CreateCommand("UPDATE Category SET CategoryName = '" + updatedCategoryName + "' WHERE CategoryId=" + categoryID.ToString(), con);

                cmd.ExecuteNonQuery();
            }


            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }


            finally
            {
                con.Close();
                con.Dispose();
            }

        }



        static void Delete_CategoryRecord(int categoryID)
        {

            // Delete Category record using parameter value, categoryID (key)

            IDbConnection con = null;


            try
            {

                con = DbHelper.CreateConnection();
                con.Open();

                IDbCommand cmd = DbHelper.CreateCommand("DELETE FROM Category WHERE CategoryId=" + categoryID.ToString(), con);

                cmd.ExecuteNonQuery();

            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }


            finally
            {
                con.Close();
                con.Dispose();
            }


        }

    }
}
