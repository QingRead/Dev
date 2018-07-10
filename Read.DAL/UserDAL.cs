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
    public class UserDAL:IUserDAL
    {
        public UserDAL() { }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddUser(UserModel model)
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@OpenID", model.OpenID);
            param[1] = new SqlParameter("@NickName", model.NickName);
            try
            {
                int i = SQLHelper.ExecuteNonQuery(CommandType.Text, @"INSERT INTO dbo.[User]
	                                                                    ( ID ,
	                                                                      OpenID ,
	                                                                      NickName ,
	                                                                      Createtime ,
	                                                                      Modifytime ,
	                                                                      Disabled
	                                                                    )
	                                                            VALUES  ( NEWID() , -- ID - uniqueidentifier
	                                                                      @OpenID , -- OpenID - nvarchar(100)
	                                                                      @NickName , -- NickName - nvarchar(50)
	                                                                      GETDATE() , -- Createtime - nvarchar(50)
	                                                                      GETDATE() , -- Modifytime - nvarchar(50)
	                                                                      0  -- Disabled - int
	                                                                    )", param);
                return i > 0;
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
        }

        /// <summary>
        /// 更新Disabled
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Update(UserModel model)
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@OpenID", model.OpenID);
            try
            {
                int i = SQLHelper.ExecuteNonQuery(CommandType.Text, "Update [dbo].[User] set Disabled=1,Modifytime=getdate() where OpenID=@OpenID AND Disabled=0", param);
                return i > 0;
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
        }

        /// <summary>
        /// 获取用户信息 by OpenID
        /// </summary>
        /// <param name="opneid"></param>
        /// <returns></returns>
        public UserModel GetUserModelByID(string opneid)
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@OpenID", opneid);
            UserModel model = null;
            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(CommandType.Text, "SELECT * FROM [dbo].[User]  WHERE OpenID = @OpenID AND Disabled=0", param))
                {
                    while (dr.Read())
                    {
                        model = GetUserModel(dr);
                    }
                    return model;
                }
            }
            catch { return null; }
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private UserModel GetUserModel(SqlDataReader dr)
        {
            UserModel model = new UserModel();
            model.ID = SQLDataHelper.GetGuid(dr, "ID");
            model.OpenID = SQLDataHelper.GetString(dr, "OpenID");
            model.NickName = SQLDataHelper.GetString(dr, "NickName");
            model.Createtime = SQLDataHelper.GetString(dr, "Createtime");
            model.Modifytime = SQLDataHelper.GetString(dr, "Modifytime");
            model.Disabled = SQLDataHelper.GetInt(dr, "Disabled");
            return model;
        }
    }
}
