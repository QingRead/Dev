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
    public class DiaryBLL
    {
        private static readonly IDiaryDAL dal = (IDiaryDAL)DALFactory.Create("DiaryDAL");

        /// <summary>
        /// 添加日记
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddDiary(DiaryModel model)
        {
            return dal.AddDiary(model);
        }

        /// <summary>
        /// 删除日记
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool DeleteDiary(DiaryModel model)
        {
            return dal.DeleteDiary(model);
        }

        /// <summary>
        /// 修改日记
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateDiary(DiaryModel model)
        {
            return dal.UpdateDiary(model);
        }

        /// <summary>
        /// 获取日记信息 by OpenID
        /// </summary>
        /// <param name="opneid"></param>
        /// <returns></returns>
        public DataTable GetDiaryToTable(string openid)
        {
            return dal.GetDiaryToTable(openid);
        }
    }
}
