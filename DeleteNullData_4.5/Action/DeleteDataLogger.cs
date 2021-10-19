using DeleteNullData_4._5.ConnectDB;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeleteNullData_4._5.Action
{
    public class DeleteDataLogger
    {
        public int DeleteDataLoggerHistory(string channelID, DateTime start, DateTime end)
        {
            int check = 0;
            try
            {
                try
                {
                    Connect.ConnectToDataBase();
                    string cmdText = string.Format("delete from t_Data_Logger_{0} where  TimeStamp between convert(nvarchar, '{1}', 120) and convert(nvarchar, '{2}', 120)", channelID, start, end);
                    check = Connect.ExcuteNonQuery(cmdText);
                }
                catch (SqlException sqlException)
                {
                    throw sqlException;
                }
            }
            finally
            {
                Connect.DisconnectToDataBase();
            }
            return check;
        }
    }
}
