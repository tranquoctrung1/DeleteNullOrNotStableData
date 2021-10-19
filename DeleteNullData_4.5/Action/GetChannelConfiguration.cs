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
    public class GetChannelConfiguration
    {
        public List<t_Channel_Configuration> GetChananelConfigurationAction()
        {
            List<t_Channel_Configuration> list = new List<t_Channel_Configuration>();
            try
            {
                try
                {
                    Connect.ConnectToDataBase();
                    SqlDataReader reader = Connect.Select("Select ChannelId, LoggerId from t_Channel_Configurations ");
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            t_Channel_Configuration t = new t_Channel_Configuration();
                            try
                            {
                                t.ChannelID = reader["ChannelId"].ToString();
                            }
                            catch (SqlException sqlException)
                            {
                                t.ChannelID = "";
                            }
                            try
                            {
                                t.LoggerID = reader["LoggerId"].ToString();
                            }
                            catch (SqlException sqlException1)
                            {
                                t.LoggerID = "";
                            }
                            list.Add(t);
                        }
                    }
                }
                catch (SqlException sqlException2)
                {
                    throw sqlException2;
                }
            }
            finally
            {
                Connect.DisconnectToDataBase();
            }
            return list;
        }
    }
}
