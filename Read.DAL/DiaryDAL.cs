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
    public class DiaryDAL:IDiaryDAL
    {
        public DiaryDAL() { }

        /// <summary>
        /// 添加日记
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddDiary(DiaryModel model)
        {
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@OpenID", model.OpenID);
            param[1] = new SqlParameter("@NickName", model.NickName);
            param[2] = new SqlParameter("@DiaryContent", model.DiaryContent);
            param[3] = new SqlParameter("@City", model.City);
            param[4] = new SqlParameter("@Weather", model.Weather);
            param[5] = new SqlParameter("@Createtime", model.Createtime);
            param[6] = new SqlParameter("@Modifytime", model.Modifytime);
            param[7] = new SqlParameter("@IsPublic", model.IsPublic);
            try
            {
                int i = SQLHelper.ExecuteNonQuery(CommandType.Text, @"INSERT INTO dbo.Diary
                                                            ( ID ,
                                                              OpenID ,
                                                              NickName ,
                                                              DiaryContent ,
                                                              City,
                                                              Weather,
                                                              Createtime ,
                                                              Modifytime ,
                                                              Disabled,
                                                              IsPublic
                                                            )
                                                    VALUES  ( NEWID() , -- ID - uniqueidentifier
                                                              @OpenID , -- OpenID - nvarchar(100)
                                                              @NickName , -- NickName - nvarchar(50)
                                                              @DiaryContent , -- DiaryContent - text
                                                              @City,
                                                              @Weather,
                                                              @Createtime , -- Createtime - nvarchar(50)
                                                              @Modifytime , -- Modifytime - nvarchar(50)
                                                              0, -- Disabled - int
                                                              @IsPublic
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
        /// 删除日记
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool DeleteDiary(DiaryModel model)
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@ID", model.ID);
            param[1] = new SqlParameter("@Modifytime", model.Modifytime);
            try
            {
                int i = SQLHelper.ExecuteNonQuery(CommandType.Text, "Update dbo.Diary set Disabled =1,Modifytime=@Modifytime where ID=@ID AND Disabled=0", param);
                return i > 0;
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
        }

        /// <summary>
        /// 修改日记
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateDiary(DiaryModel model)
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@ID", model.ID);
            param[1] = new SqlParameter("@DiaryContent", model.DiaryContent);
            param[2] = new SqlParameter("@Modifytime", model.Modifytime);
            try
            {
                int i = SQLHelper.ExecuteNonQuery(CommandType.Text, "Update dbo.Diary set DiaryContent =@DiaryContent,Modifytime=@Modifytime where ID=@ID AND Disabled=0", param);
                return i > 0;
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
        }

        /// <summary>
        /// 获取日记信息 by OpenID
        /// </summary>
        /// <param name="opneid"></param>
        /// <returns></returns>
        public DataTable GetDiaryToTable(string openid)
        {
            try
            {
                SqlParameter[] parm = new SqlParameter[1];
                parm[0] = new SqlParameter("@OpenID", openid);

                return SQLHelper.ExecuteTable(CommandType.Text, @"SELECT * FROM dbo.Diary WHERE Disabled=0 AND OpenID=@OpenID ORDER BY Createtime DESC", parm);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// 获取日记信息
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private DiaryModel GetDiaryModel(SqlDataReader dr)
        {
            DiaryModel model = new DiaryModel();
            model.ID = SQLDataHelper.GetGuid(dr, "ID");
            model.OpenID = SQLDataHelper.GetString(dr, "OpenID");
            model.NickName = SQLDataHelper.GetString(dr, "NickName");
            model.DiaryContent = SQLDataHelper.GetString(dr, "DiaryContent");
            model.City = SQLDataHelper.GetString(dr, "City");
            model.Weather = SQLDataHelper.GetString(dr, "Weather");
            model.Createtime = SQLDataHelper.GetString(dr, "Createtime");
            model.Modifytime = SQLDataHelper.GetString(dr, "Modifytime");
            model.Disabled = SQLDataHelper.GetInt(dr, "Disabled");
            model.SortID = SQLDataHelper.GetInt(dr, "SortID");
            model.IsPublic = SQLDataHelper.GetInt(dr, "IsPublic");
            return model;
        }
    }
}
