using Helios.Common;
using Read.IDAL;
using Read.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Read.BLL
{
    public class LoginLogBLL
    {
        private static readonly ILoginLogDAL dal = (ILoginLogDAL)DALFactory.Create("LoginLogDAL");

        /// <summary>
        /// 添加登录日志
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddLoginLog(LoginLogModel model)
        {
            return dal.AddLoginLog(model);
        }
    }
}
