using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeleteNullData_4._5.ConnectDB
{
    public static class Connect
    {
        private static string connectionString;

        private static SqlConnection sqlConnection;

        static Connect()
        {
            Connect.connectionString = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
            Connect.sqlConnection = new SqlConnection(Connect.connectionString);
        }

        public static void ConnectToDataBase()
        {
            if ((Connect.sqlConnection == null ? false : Connect.sqlConnection.State == ConnectionState.Closed))
            {
                Connect.sqlConnection.Open();
            }
        }

        public static void DisconnectToDataBase()
        {
            if ((Connect.sqlConnection == null ? false : Connect.sqlConnection.State == ConnectionState.Open))
            {
                Connect.sqlConnection.Close();
            }
        }

        public static int ExcuteNonQuery(string sqlquery)
        {
            return (new SqlCommand(sqlquery, Connect.sqlConnection)).ExecuteNonQuery();
        }

        public static SqlDataReader Select(string sqlQuery)
        {
            SqlCommand command = new SqlCommand(sqlQuery, Connect.sqlConnection);
            SqlDataReader reader = command.ExecuteReader();
            command.CommandTimeout = 0;
            command.Dispose();
            return reader;
        }
    }
}
