using Helios.Common;
using Read.IDAL;
using Read.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Read.BLL
{
    public class MoodBLL
    {
        private static readonly IMoodDAL dal = (IMoodDAL)DALFactory.Create("MoodDAL");
        /// <summary>
        /// 添加心情
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddMood(MoodModl model)
        {
            return dal.AddMood(model);
        }

        /// <summary>
        /// 删除心情
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool DeleteMood(MoodModl model)
        {
            return dal.DeleteMood(model);
        }

        /// <summary>
        /// 修改心情
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateMood(MoodModl model)
        {
            return dal.UpdateMood(model);
        }

        /// <summary>
        /// 获取心情信息 by OpenID
        /// </summary>
        /// <param name="opneid"></param>
        /// <returns></returns>
        public DataTable GetMoodToTable(string openid)
        {
            return dal.GetMoodToTable(openid);
        }
    }
}
