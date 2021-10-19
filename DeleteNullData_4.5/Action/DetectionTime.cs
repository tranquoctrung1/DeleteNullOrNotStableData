using DeleteNullData_4._5.ConnectDB;
using DeleteNullData_4._5.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeleteNullData_4._5.Action
{
    public class DetectionTime
    {
        public DateTimeDetection GetDateTimeDetection(string channelId, DateTime start, DateTime end)
        {
            DateTimeDetection detection = new DateTimeDetection()
            {
                StartDate = new DateTime(1970, 1, 1),
                EndDate = new DateTime(1970, 1, 1)
            };
            try
            {
                try
                {
                    Connect.ConnectToDataBase();
                    string cmdText = string.Format("select top 1 TimeStamp from  t_Data_Logger_{0} where (Value is null or Value >= 8888) and TimeStamp between convert(nvarchar, '{1}', 120) and convert(nvarchar, '{2}', 120)", channelId, start, end);
                    SqlDataReader reader = Connect.Select(cmdText);
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            try
                            {
                                detection.StartDate = DateTime.Parse(reader["TimeStamp"].ToString());
                            }
                            catch (Exception exception)
                            {
                                Console.WriteLine(exception.ToString());
                                detection.StartDate = new DateTime(1970, 1, 1);
                            }
                        }
                    }
                }
                catch (SqlException sqlException)
                {
                    throw sqlException;
                }
            }
            finally
            {
                Connect.ConnectToDataBase();
            }
            return detection;
        }
    }
}
