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
    public class UserBLL
    {
        private static readonly IUserDAL dal = (IUserDAL)DALFactory.Create("UserDAL");

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddUser(UserModel model)
        {
            return dal.AddUser(model);
        }

        /// <summary>
        /// 更新Disabled
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Update(UserModel model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 获取用户信息 by OpenID
        /// </summary>
        /// <param name="opneid"></param>
        /// <returns></returns>
        public UserModel GetUserModelByID(string opneid)
        {
            return dal.GetUserModelByID(opneid);
        }
    }
}
