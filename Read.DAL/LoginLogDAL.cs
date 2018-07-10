using Helios.Common;
using Read.IDAL;
using Read.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Read.DAL
{
    public class LoginLogDAL: ILoginLogDAL
    {
        /// <summary>
        /// 添加登录日志
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddLoginLog(LoginLogModel model)
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@OpenID", model.OpenID);
            param[1] = new SqlParameter("@NickName", model.NickName);
            try
            {
                int i = SQLHelper.ExecuteNonQuery(CommandType.Text, @"INSERT INTO dbo.LoginLog
                                                ( ID, 
                                                  OpenID, 
                                                  NickName, 
                                                  LoginTime )
                                        VALUES  ( NEWID(), -- ID - uniqueidentifier
                                                  @OpenID, -- OpenID - nvarchar(100)
                                                  @NickName, -- NickName - nvarchar(50)
                                                  GETDATE()  -- LoginTime - nvarchar(50)
                                                  )", param);
                return i > 0;
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
        }
    }
}
