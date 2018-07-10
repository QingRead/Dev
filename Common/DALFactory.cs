using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Helios.Common
{
    public class DALFactory
    {
        public static object Create(string cName)
        {
            try
            {
                // 这里可以查看 DAL 接口类。
                string path = System.Configuration.ConfigurationSettings.AppSettings["ReadDAL"].ToString();
                string className = path + "." + cName;

                // 用配置文件指定的类组合
                return Assembly.Load(path).CreateInstance(className);
            }
            catch (Exception err)
            {
                return null;
                throw err;
            }
        }
    }
}
