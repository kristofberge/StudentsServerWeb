using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace StudentsServerWeb
{
    class StudentsDatabase
    {

        private static StudentsDatabase _instance;
        
        private Random r = new Random();

        private readonly string connectionString;

        private StudentsDatabase(){
            connectionString = "Data Source=(LocalDB)\\v11.0;AttachDbFilename=C:\\prog\\C#\\StudentsServerWeb\\App_Data\\Students.mdf;Integrated Security=True;";
        }

        private bool OpenConnection(SqlConnection connection)
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (SqlException e)
            {
                switch (e.Number)
                {
                    case 0:
                        //MessageBox.Show("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        //MessageBox.Show("Invalid username/password, please try again");
                        break;

                    default:
                        //MessageBox.Show("An error has occured.");
                        break;
                }
                return false;
            }
        }

        

        public static StudentsDatabase GetInstance()
        {
            if (_instance == null)
                _instance = new StudentsDatabase();
            return (StudentsDatabase)_instance;
        }


        public DataTableReader ExecuteSelect(String sql) {

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        OpenConnection(connection);
                        var dt = new DataTable();
                        dt.Load(cmd.ExecuteReader());
                        return dt.CreateDataReader();
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public bool ExecuteInsertUpdateDelete(string sql) {

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        OpenConnection(connection);
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (SqlException)
            {
                throw;
                return false;
            }
        }
    }
}
