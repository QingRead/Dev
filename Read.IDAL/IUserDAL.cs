using Read.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Read.IDAL
{
    public interface  IUserDAL
    {
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool AddUser(UserModel model);

        /// <summary>
        /// 更新Disabled
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool Update(UserModel model);

        /// <summary>
        /// 获取用户信息 by OpenID
        /// </summary>
        /// <param name="opneid"></param>
        /// <returns></returns>
        UserModel GetUserModelByID(string opneid);
    }
}
