using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Sql;
using System.Data.SqlClient;

namespace SampleReflection
{
    public class MiddleWare
    {
        public static EcApphDetail GetEcApphDetail(long customerNo, DateTime currentDate)
        {
            EcApphDetail ecApphDetail = new EcApphDetail();


            string constring = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\IncomerDefault.mdf;Integrated Security=True;Connect Timeout=30";
            string Query = "CONV.EC_APPH_DETAIL";
            SqlConnection con = new SqlConnection(constring);
            
            SqlCommand cmd = new SqlCommand(Query, con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
             
            cmd.Parameters.Add(new SqlParameter("@CUSTOMER_NUMBER", System.Data.SqlDbType.BigInt, 20, "1234"));
            cmd.Parameters.Add(new SqlParameter("@DATE", System.Data.SqlDbType.DateTime, 20, DateTime.Now.ToLongDateString()));

             
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    foreach (var prop in ecApphDetail.GetType().GetProperties())
                    {
                        
                        if (prop.PropertyType == typeof(Int32))
                        {
                            prop.SetValue(ecApphDetail, Convert.ToInt32(reader[prop.Name]), null);
                        }
                        if (prop.PropertyType == typeof(Int64))
                        {
                            prop.SetValue(ecApphDetail, Convert.ToInt64(reader[prop.Name]), null);
                        }
                        if (prop.PropertyType == typeof(DateTime))
                        {
                            prop.SetValue(ecApphDetail, Convert.ToDateTime(reader[prop.Name]), null);
                        }
                        if (prop.PropertyType == typeof(String))
                        {
                            prop.SetValue(ecApphDetail, Convert.ToString(reader[prop.Name]), null);
                        }
                      
                    }

                }
            }


            return ecApphDetail;
        }
    }
}