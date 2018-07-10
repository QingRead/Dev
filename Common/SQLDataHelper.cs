using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helios.Common
{
    public class SQLDataHelper
    {

        public static string GetString(SqlDataReader dr,string key)
        {
            return dr[key].ToString();
        }

        public static DateTime GetDateTime(SqlDataReader dr, string key)
        {
            try
            {
                return Convert.ToDateTime(dr[key].ToString());
            }
            catch { return new DateTime(); }
        }


        public static decimal GetDecimal(SqlDataReader dr, string key)
        {
            try
            {
                return Convert.ToDecimal(dr[key].ToString());
            }
            catch { return new decimal(); }
            
        }


        public static int GetInt(SqlDataReader dr, string key)
        {
            try
            {
                return Convert.ToInt32(dr[key].ToString());
            }
            catch { return 0; }
           
        }

        public static Guid GetGuid(SqlDataReader dr, string key)
        {
            try
            {
                return Guid.Parse(dr[key].ToString());
            }
            catch { return new Guid(); }
           
        }

        public static double GetDouble(SqlDataReader dr, string key)
        {
            try
            {
                return Convert.ToDouble(dr[key].ToString());
            }
            catch { return 0; }
           
        }

        public static bool GetBoll(SqlDataReader dr, string key)
        {
            try
            {
                return Convert.ToBoolean(dr[key].ToString());
            }
            catch { return false; }

        }


    }
}
