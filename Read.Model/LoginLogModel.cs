using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Read.Model
{
    public class LoginLogModel
    {
        #region
        private Guid _id;
        private string _openid;
        private string _nickname;
        private string _logintime;
        #endregion

        #region
        public LoginLogModel(){}

        public LoginLogModel(Guid id, string openid, string nickname, string logintime)
        {
            this._id = id;
            this._openid = openid;
            this._nickname = nickname;
            this._logintime = logintime;
        }

        #endregion

        #region
        /// <summary>
        /// 主键
        /// </summary>
        public Guid ID
        {
            get { return _id; }
            set { _id=value; }
        }

        /// <summary>
        /// 唯一标识
        /// </summary>
        public string OpenID
        {
            get { return _openid; }
            set { _openid=value; }
        }

        /// <summary>
        /// 微信昵称
        /// </summary>
        public string NickName
        {
            get { return _nickname; }
            set { _nickname=value; }
        }

        /// <summary>
        /// 登陆时间
        /// </summary>
        public string LoginTime
        {
            get { return _logintime; }
            set { _logintime=value; }
        }
        #endregion
    }
}
