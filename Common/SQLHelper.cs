using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace Helios.Common
{
    public class SQLHelper
    {
        //取得数据库连接web.config 中配置 
        public static readonly string ConnectString = ConfigurationSettings.AppSettings["DBString"].ToString();

        /// <summary>
        /// 无事务，数据查询
        /// </summary>
        /// <param name="cmdType">存储过程或Sql语句</param>
        /// <param name="cmdText">存储过程名或Sql语句内容</param>
        /// <param name="CommandParams">参数列表</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(CommandType cmdType, string cmdText, params SqlParameter[] CommandParams)
        {
            SqlCommand cmd = new SqlCommand();

            SqlConnection conn = new SqlConnection(ConnectString);

            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, CommandParams);
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// 有事务，数据操作类
        /// </summary>
        /// <param name="trans">事务</param>
        /// <param name="cmdType">操作类别 (stored procedure,sql)</param>
        /// <param name="cmdText">存储过程名或Sql语句</param>
        /// <param name="CommandParams">参数</param>
        /// <returns>返回影响的数据行数</returns>
        public static int ExecuteNonQuery(SqlTransaction trans, CommandType cmdType, string cmdText, params SqlParameter[] CommandParams)
        {

            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, CommandParams);
            /*if (cmdType == CommandType.StoredProcedure)
            {
            cmd.Parameters.Add("@RETURN_VALUE", "").Direction = ParameterDirection.ReturnValue;
            cmd.ExecuteNonQuery();

            val = (int)cmd.Parameters["@RETURN_VALUE"].Value;
            }
            else
            if (cmdType==CommandType.Text)
            {
            val = cmd.ExecuteNonQuery();
            }*/
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();

            return val;
        }

        /// <summary>
        /// 返回数据集 DataReader
        /// </summary>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="CommandParams"></param>
        /// <returns></returns>
        public static SqlDataReader ExecuteReader(CommandType cmdType, string cmdText, params SqlParameter[] CommandParams)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection(ConnectString);

            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, CommandParams);
                SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch (Exception ex)
            {
                conn.Close();
                //throw new Exception("操作失败！");
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 有事务的取数据
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="CommandParams"></param>
        /// <returns></returns>
        public static SqlDataReader ExecuteReader(SqlTransaction trans, CommandType cmdType, string cmdText, params SqlParameter[] CommandParams)
        {
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, CommandParams);
            SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            cmd.Parameters.Clear();
            return rdr;
        }

        public static object ExecuteScalar(CommandType cmdType, string cmdText, params SqlParameter[] CommandParams)
        {
            SqlCommand cmd = new SqlCommand();

            SqlConnection conn = new SqlConnection(ConnectString);

            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, CommandParams);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

        }

        public static object ExecuteScalar(SqlTransaction trans, CommandType cmdType, string cmdText, params SqlParameter[] CommandParams)
        {
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, CommandParams);
            object val = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return val;

        }

        /// <summary>
        /// 根据Sql语句取得表
        /// </summary>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="CommandParams"></param>
        /// <returns></returns>
        public static DataTable ExecuteTable(CommandType cmdType, string cmdText, params SqlParameter[] CommandParams)
        {
            DataTable temptable = new DataTable();

            SqlCommand cmd = new SqlCommand();

            SqlConnection conn = new SqlConnection(ConnectString);

            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, CommandParams);

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                SqlCommandBuilder scb = new SqlCommandBuilder(da);

                da.Fill(temptable);
            }
            catch(Exception ex)
            {
                //throw ex;
            }
            finally
            {
                conn.Close();
            }

            return temptable;
        }


        public static DataTable ExecuteTable(SqlTransaction trans, CommandType cmdType, string cmdText, params SqlParameter[] CommandParams)
        {
            DataTable temptable = new DataTable();
            SqlCommand cmd = new SqlCommand();

            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, CommandParams);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            SqlCommandBuilder scb = new SqlCommandBuilder(da);
            da.Fill(temptable);
            cmd.Parameters.Clear();

            return temptable;

        }


        /// <summary>
        /// 根据Sql语句或存储过程取得数据
        /// </summary>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="CommandParams"></param>
        /// <returns></returns>
        public static DataSet ExecuteDataSet(CommandType cmdType, string cmdText, params SqlParameter[] CommandParams)
        {
            SqlConnection conn = new SqlConnection(ConnectString);

            SqlCommand cmd = new SqlCommand();

            DataSet TempDataSet = new DataSet();

            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, CommandParams);
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(TempDataSet);
                cmd.Parameters.Clear();
                return TempDataSet;

            }
            finally
            {
                conn.Close();
            }

        }


        public static DataSet ExecuteDataSet(SqlTransaction trans, CommandType cmdType, string cmdText, params SqlParameter[] CommandParams)
        {
            SqlCommand cmd = new SqlCommand();
            DataSet TempDataSet = new DataSet();
            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, CommandParams);
            SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
            sqlAdapter.Fill(TempDataSet);
            cmd.Parameters.Clear();
            return TempDataSet;
        }

        /// <summary>
        /// 生成Sql语句或准备
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="conn"></param>
        /// <param name="trans"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="cmdParms"></param>
        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
        {

            if (conn.State != ConnectionState.Open)
                conn.Open();

            cmd.Connection = conn;
            cmd.CommandText = cmdText;

            if (trans != null)
                cmd.Transaction = trans;

            cmd.CommandType = cmdType;

            if (cmdParms != null)
            {
                foreach (SqlParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }

        public static object ToDBValue(object value)
        {
            return value == null ? DBNull.Value : value;
        }

        public static object FromDBValue(object dbValue)
        {
            return dbValue == DBNull.Value ? null : dbValue;
        }
    }
}
