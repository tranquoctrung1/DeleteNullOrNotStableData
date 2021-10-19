using DeleteNullData_4._5.Action;
using DeleteNullData_4._5.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeleteNullData_4._5
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                DateTime currentDate = DateTime.Now;
                DateTime startDate = currentDate.AddDays(-7);
                List<t_Channel_Configuration> list = (new GetChannelConfiguration()).GetChananelConfigurationAction();
                if (list.Count == 0)
                {
                    Console.WriteLine("There are no channel to delete");
                }
                else
                {
                    foreach (t_Channel_Configuration item in list)
                    {
                        DetectionTime detectionTime = new DetectionTime();
                        DeleteComplexsData deleteComplexsData = new DeleteComplexsData();
                        DeleteDataIndex deleteDataIndex = new DeleteDataIndex();
                        DeleteDataLogger deleteDataLogger = new DeleteDataLogger();
                        Console.WriteLine(item.ChannelID.ToString());
                        try
                        {
                            DateTime timeOfNullValue = detectionTime.GetDateTimeDetection(item.ChannelID, startDate, currentDate).StartDate;
                            Console.WriteLine(timeOfNullValue.ToString());
                            if (timeOfNullValue != new DateTime(1970, 1, 1))
                            {
                                int nRowsDataLogger = deleteDataLogger.DeleteDataLoggerHistory(item.ChannelID, timeOfNullValue, currentDate);
                                int nRowsDataIndex = deleteDataIndex.DeleteDataLoggerIndexHistory(item.ChannelID, timeOfNullValue, currentDate);
                                int nRowDataComplex = deleteComplexsData.DeleteComlexData(item.LoggerID, timeOfNullValue, currentDate);
                                if (nRowDataComplex <= 0)
                                {
                                    Console.WriteLine(string.Concat("Nothing is deteted in t_Data_Complexes table with ", item.LoggerID, " "));
                                }
                                else
                                {
                                    Console.WriteLine(string.Format("Deleted {0} rows in t_Data_Complexes table with {1} from {2} to {3}", new object[] { nRowDataComplex, item.LoggerID, timeOfNullValue.ToString("dd/MM/yyyy hh:mm:ss"), currentDate.ToString("dd/MM/yyyy hh:mm:ss") }));
                                }
                                if (nRowsDataLogger <= 0)
                                {
                                    Console.WriteLine(string.Concat("Nothing is deleted in  t_Data_Logger_", item.ChannelID, " table"));
                                }
                                else
                                {
                                    Console.WriteLine(string.Format("Deleted {0} rows in t_Data_Logger_{1} table from {2} to {3}", new object[] { nRowsDataLogger, item.ChannelID, timeOfNullValue.ToString("dd/MM/yyyy hh:mm:ss"), currentDate.ToString("dd/MM/yyyy hh:mm:ss") }));
                                }
                                if (nRowsDataIndex <= 0)
                                {
                                    Console.WriteLine(string.Concat("Nothing is deleted in t_Data_Indexs_", item.ChannelID, " table"));
                                }
                                else
                                {
                                    Console.WriteLine(string.Format("Deleted {0} rows in t_Data_Indexs_{1} table from {2} to {3}", new object[] { nRowsDataIndex, item.ChannelID, timeOfNullValue.ToString("dd/MM/yyyy hh:mm:ss"), currentDate.ToString("dd/MM/yyyy hh:mm:ss") }));
                                }
                            }
                        }
                        catch (SqlException sqlException)
                        {
                            Console.WriteLine(sqlException.ToString());
                        }
                    }
                }
            }
            catch (SqlException sqlException1)
            {
                Console.WriteLine(sqlException1.ToString());
            }
        }
    }
}
