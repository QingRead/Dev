using Read.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Read.IDAL
{
    public interface IDiaryDAL
    {
        /// <summary>
        /// 添加日记
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool AddDiary(DiaryModel model);

        /// <summary>
        /// 删除日记
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool DeleteDiary(DiaryModel model);

        /// <summary>
        /// 修改日记
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool UpdateDiary(DiaryModel model);

        /// <summary>
        /// 获取日记信息 by OpenID
        /// </summary>
        /// <param name="opneid"></param>
        /// <returns></returns>
        DataTable GetDiaryToTable(string openid);
    }
}
