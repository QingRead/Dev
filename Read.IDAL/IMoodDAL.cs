using Read.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Read.IDAL
{
    public interface IMoodDAL
    {
        /// <summary>
        /// 添加心情
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool AddMood(MoodModl model);

        /// <summary>
        /// 删除心情
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool DeleteMood(MoodModl model);

        /// <summary>
        /// 修改心情
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool UpdateMood(MoodModl model);

        /// <summary>
        /// 获取心情信息 by OpenID
        /// </summary>
        /// <param name="opneid"></param>
        /// <returns></returns>
        DataTable GetMoodToTable(string openid);
    }
}
