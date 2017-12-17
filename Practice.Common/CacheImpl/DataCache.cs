using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice.Common
{
   public class DataCache
    {
        private static IDataCache _instance = null;
        private static IDataCache _rinstance = null;
        private static object locko = new object();
        public DataCache() { }
        /// <summary>  
        /// 线程安全的单例模式
        /// </summary>  
        public static IDataCache Instance()
        {
            if (_instance == null)
            {
                lock (locko)
                {
                    if(_instance==null)
                    _instance = new HttpRuntimeCache();//这里可以改变缓存类型  
                }
            }
            return _instance;

        }
        /// <summary>  
        /// 静态实例，外部可直接调用  
        /// </summary>  
        public static IDataCache RInstance
        {
            get {
                if (_rinstance == null)
                    _rinstance = new RedisHelper();//这里可以改变缓存类型  
                return _rinstance;
            }
        
        }
    }
}
