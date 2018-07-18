using Helios.Common;
using Read.BLL;
using Read.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace QingRead.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        #region 用户
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="openID"></param>
        /// <param name="nickname"></param>
        /// <returns></returns>
        public JsonResult GetUserInfo(string code, string nickname)
        {
            try
            {
                string openid = GetOpenidByCode(code);
                UserModel model = new UserModel();
                UserBLL bll = new UserBLL();
                model = bll.GetUserModelByID(openid);
                if (model != null)
                {
                    return Json(new { OpenID=model.OpenID }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    model = new UserModel();
                    model.OpenID = openid;
                    model.NickName = nickname;
                    model.Createtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    model.Modifytime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    bll.AddUser(model);
                    return Json(new { OpenID=openid }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="openID"></param>
        /// <returns></returns>
        public void DeleteUser(string openid)
        {
            try
            {
                UserModel model = new UserModel();
                UserBLL bll = new UserBLL();
                model.OpenID = openid;
                model.Modifytime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                bll.Update(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 日记
        /// <summary>
        /// 获取日记信息
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        public JsonResult GetDiaryInfo(string openid)
        {
            try
            {
                DiaryBLL bll = new DiaryBLL();
                DataTable dt = bll.GetDiaryToTable(openid);

                if (dt != null && dt.Rows.Count > 0)
                {
                    return Json(new { Model = TableToList(dt), IsExist = true }, JsonRequestBehavior.AllowGet);
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 添加日记(类型:1.日记，2.心情，3.手账)
        /// </summary>
        /// <param name="openid"></param>
        /// <param name="nickname"></param>
        /// <param name="content"></param>
        public void AddDiary(string openid, string nickname, string content)
        {
            try
            {
                string[] arrStr = content.Replace("\"","").Replace("}","").Split(new char[2] { ',',':'});
                if (arrStr[9] == "1")
                {
                    DiaryBLL bll = new DiaryBLL();
                    DiaryModel model = new DiaryModel();
                    model.OpenID = openid;
                    model.NickName = nickname;
                    model.Weather = arrStr[3];
                    model.City = arrStr[5];
                    model.DiaryContent = arrStr[7];
                    model.IsPublic = int.Parse(arrStr[11]);
                    model.Createtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    model.Modifytime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    bll.AddDiary(model);
                }
                else if (arrStr[9] == "2")
                {
                    MoodBLL bll = new MoodBLL();
                    MoodModl model = new MoodModl();
                    model.OpenID = openid;
                    model.NickName = nickname;
                    model.Weather = arrStr[3];
                    model.City = arrStr[5];
                    model.Mood = arrStr[7];
                    model.IsPublic = int.Parse(arrStr[11]);
                    model.Createtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    model.Modifytime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    bll.AddMood(model);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 删除日记
        /// </summary>
        /// <param name="openid"></param>
        public void DeleteDiary(string id)
        {
            try
            {
                DiaryBLL bll = new DiaryBLL();
                DiaryModel model = new DiaryModel();
                model.ID = Guid.Parse(id);
                model.Modifytime= DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                bll.DeleteDiary(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 修改日记
        /// </summary>
        /// <param name="openid"></param>
        public void UpdateDiary(string id,string content)
        {
            try
            {
                DiaryBLL bll = new DiaryBLL();
                DiaryModel model = new DiaryModel();
                model.ID = Guid.Parse(id);
                model.DiaryContent = content;
                model.Modifytime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                bll.UpdateDiary(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 日记放入list
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<DiaryModel> TableToList(DataTable dt)
        {
            try
            {
                List<DiaryModel> list = new List<DiaryModel>();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    list.Add(new DiaryModel()
                    {
                        ID = Guid.Parse(dt.Rows[i]["ID"].ToString()),
                        OpenID= dt.Rows[i]["OpenID"].ToString(),
                        NickName = dt.Rows[i]["NickName"].ToString(),
                        DiaryContent = dt.Rows[i]["DiaryContent"].ToString(),
                        City = dt.Rows[i]["City"].ToString(),
                        Weather = dt.Rows[i]["Weather"].ToString(),
                        Createtime = DateTime.Parse(dt.Rows[i]["Createtime"].ToString()).ToString("yyyy-MM-dd HH:mm"),
                        Modifytime = DateTime.Parse(dt.Rows[i]["Modifytime"].ToString()).ToString("yyyy-MM-dd HH:mm"),
                        Disabled = int.Parse(dt.Rows[i]["Disabled"].ToString()),
                        SortID = int.Parse(dt.Rows[i]["SortID"].ToString()),
                        IsPublic = int.Parse(dt.Rows[i]["IsPublic"].ToString())
                    });
                }

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 心情

        /// <summary>
        /// 获取心情信息
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        public JsonResult GetMoodInfo(string openid)
        {
            try
            {
                MoodBLL bll = new MoodBLL();
                DataTable dt = bll.GetMoodToTable(openid);

                if (dt != null && dt.Rows.Count > 0)
                {
                    return Json(new { Model = MoodTableToList(dt), IsExist = true }, JsonRequestBehavior.AllowGet);
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 删除心情
        /// </summary>
        /// <param name="openid"></param>
        public void DeleteMood(string id)
        {
            try
            {
                MoodBLL bll = new MoodBLL();
                MoodModl model = new MoodModl();
                model.ID = Guid.Parse(id);
                model.Modifytime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                bll.DeleteMood(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 修改心情
        /// </summary>
        /// <param name="openid"></param>
        public void UpdateMood(string id, string content)
        {
            try
            {
                MoodBLL bll = new MoodBLL();
                MoodModl model = new MoodModl();
                model.ID = Guid.Parse(id);
                model.Mood = content;
                model.Modifytime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                bll.UpdateMood(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 心情放入list
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<MoodModl> MoodTableToList(DataTable dt)
        {
            try
            {
                List<MoodModl> list = new List<MoodModl>();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    list.Add(new MoodModl()
                    {
                        ID = Guid.Parse(dt.Rows[i]["ID"].ToString()),
                        OpenID= dt.Rows[i]["OpenID"].ToString(),
                        NickName = dt.Rows[i]["NickName"].ToString(),
                        Mood = dt.Rows[i]["Mood"].ToString(),
                        City = dt.Rows[i]["City"].ToString(),
                        Weather = dt.Rows[i]["Weather"].ToString(),
                        Createtime = DateTime.Parse(dt.Rows[i]["Createtime"].ToString()).ToString("yyyy-MM-dd HH:mm"),
                        Modifytime = DateTime.Parse(dt.Rows[i]["Modifytime"].ToString()).ToString("yyyy-MM-dd HH:mm"),
                        Disabled = int.Parse(dt.Rows[i]["Disabled"].ToString()),
                        IsPublic = int.Parse(dt.Rows[i]["IsPublic"].ToString())
                    });
                }

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 日志
        /// <summary>
        /// 添加登录日志
        /// </summary>
        /// <param name="openid"></param>
        /// <param name="nickname"></param>
        public void AddLoginLog(string code, string nickname)
        {
            try
            {
                string openid = GetOpenidByCode(code);
                LoginLogBLL bll = new LoginLogBLL();
                LoginLogModel model = new LoginLogModel();
                model.OpenID = openid;
                model.NickName = nickname;
                model.LoginTime= DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                bll.AddLoginLog(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 获取用户信息(根据code)

        public string GetOpenidByCode(string code)
        {
            string url = "https://api.weixin.qq.com/sns/jscode2session?appid="+ DESEncrypt.UnAesStr("v4j1VcTNJ6jrA6WjHpZqDWO/TC68FF3qBV4cDTcCfHY=") + "&secret="+ DESEncrypt.UnAesStr("iPz+iDqI1UE2sI95IuqEcETR+1PWEL9NT8hbQJ25KAkL67//XePTfaGIUqOLKh0q") + "&js_code=" + code +"&grant_type=authorization_code";
            string html = string.Empty;
            try
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                request.Method = "GET";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream ioStream = response.GetResponseStream();
                StreamReader sr = new StreamReader(ioStream, Encoding.Default);
                html = sr.ReadToEnd();
                sr.Close();
                ioStream.Close();
                response.Close();
                if (!html.Contains("errmsg") && !html.Contains("errcode"))
                {
                    string[] arrStr = html.Replace("\"","").Replace("}","").Split(new char[] { ',',':'});
                    return arrStr[3];
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
        #endregion
}