﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Read.Model
{
    [Serializable]
    public class MoodModl
    {
        #region
        private Guid _id;
        private string _openid;
        private string _nickname;
        private string _mood;
        private string _city;
        private string _weather;
        private string _createtime;
        private string _modifytime;
        private int _disabled;
        private int _ispublic;
        #endregion

        #region
        public MoodModl()
        {
            Disabled = 0;
            IsPublic = 0;
        }

        public MoodModl(Guid id, string openid, string nickname, string createtime, string modifytime, int disabled, string mood, string city, string weather, int ispublic)
        {
            this._id = id;
            this._openid = openid;
            this._nickname = nickname;
            this._createtime = createtime;
            this._modifytime = modifytime;
            this._disabled = disabled;
            this._mood = mood;
            this._weather = weather;
            this._city = city;
            this._ispublic = ispublic;
        }

        #endregion

        #region
        /// <summary>
        /// 主键
        /// </summary>
        public Guid ID
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// 唯一标识
        /// </summary>
        public string OpenID
        {
            get { return _openid; }
            set { _openid = value; }
        }

        /// <summary>
        /// 微信昵称
        /// </summary>
        public string NickName
        {
            get { return _nickname; }
            set { _nickname = value; }
        }

        /// <summary>
        /// 日记内容
        /// </summary>
        public string Mood
        {
            get { return _mood; }
            set { _mood = value; }
        }

        /// <summary>
        /// 城市
        /// </summary>
        public string City
        {
            get { return _city; }
            set { _city = value; }
        }

        /// <summary>
        /// 天气
        /// </summary>
        public string Weather
        {
            get { return _weather; }
            set { _weather = value; }
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string Createtime
        {
            get { return _createtime; }
            set { _createtime = value; }
        }

        /// <summary>
        /// 修改时间
        /// </summary>
        public string Modifytime
        {
            get { return _modifytime; }
            set { _modifytime = value; }
        }

        /// <summary>
        /// 删除
        /// </summary>
        public int Disabled
        {
            get { return _disabled; }
            set { _disabled = value; }
        }

        /// <summary>
        /// 是否公开
        /// </summary>
        public int IsPublic
        {
            get { return _ispublic; }
            set { _ispublic = value; }
        }
        #endregion
    }
}
