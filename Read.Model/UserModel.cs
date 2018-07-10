using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Read.Model
{
    public class UserModel
    {
        #region
        private Guid _id;
        private string _openid;
        private string _nickname;
        private string _createtime;
        private string _modifytime;
        private int _disabled;
        #endregion

        #region
        public UserModel()
        {
            Disabled = 0;
        }

        public UserModel(Guid id,string openid,string nickname,string createtime,string modifytime,int disabled)
        {
            this._id = id;
            this._openid = openid;
            this._nickname = nickname;
            this._createtime = createtime;
            this._modifytime = modifytime;
            this._disabled = disabled;
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
        /// 添加时间
        /// </summary>
        public string Createtime
        {
            get { return _createtime; }
            set {  _createtime=value; }
        }

        /// <summary>
        /// 修改时间
        /// </summary>
        public string Modifytime
        {
            get { return _modifytime; }
            set { _modifytime=value; }
        }

        /// <summary>
        /// 注销，取消关注
        /// </summary>
        public int Disabled
        {
            get { return _disabled; }
            set {  _disabled=value; }
        }
        #endregion
    }
}
