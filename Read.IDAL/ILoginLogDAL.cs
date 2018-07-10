using Read.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Read.IDAL
{
    public interface ILoginLogDAL
    {
        /// <summary>
        /// 添加登录日志
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool AddLoginLog(LoginLogModel model);
    }
}
