using Practice.Common;
using Practice.Interface;
using Practice.Models.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private  IUser _user;
        private IDataCache cache = DataCache.Instance();
        public HomeController(IUser user)
        {
            _user = user;
        }
        public ActionResult Index()
        {            
            Stopwatch sw = new Stopwatch();
            sw.Start();
            //AnsycbackForCache();
            //var list = AnsycbackForCache();
            //var list =Task.Run(() => AnsycbackForCache()).Result;
            var count=Task.Run(()=> taskinsertdata(10)).Result;
            sw.Stop();
            TimeSpan ProGramts = sw.Elapsed;
            ViewBag.time="运行时间：" + ProGramts.Seconds + "秒" + ProGramts.Milliseconds + "毫秒"+count;
            return View();
        }
        /// <summary>
        /// 异步多线程
        /// </summary> 
        private async  Task<int> taskinsertdata(int nums)
        {
            IList<T_User> IEnumerable = new List<T_User>();           
            for (int i = 0; i < nums; i++)
            {
                IEnumerable.Add(new T_User()
                {
                    PhoneNumber="1316120389"+i,
                    Password = "7",
                    NickName = "22" + i,
                    LoginToken=Guid.NewGuid().ToString().ToLower(),                                    
                    LastLoginTime = DateTime.Now,                   
                });
            } 
           return await _user.InsertAsync(IEnumerable);

        }

        /// <summary>
        /// 获得数据
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        private IList<T_User> AnsycbackForCache()
        {
            IList<T_User> list = new List<T_User>();
            //IDataCache cache = DataCache.RInstance;
            var Mfristkey1 = cache.Get<string>("Mfristkey1");
            if (Mfristkey1 != null)
            {              
                cache.Delete("Mfristkey1");
            }
            if (cache.Get<IList<T_User>>("list") != null)
            {
                list = cache.Get<IList<T_User>>("list");
            }
            else
            {
                list = _user.GetAll(x => x.IsDeleted == false);
                cache.Set<IList<T_User>>("list", list,DateTime.Now.AddMinutes(2));
            }
            return list;
        }
        public ActionResult About()
        {           
            Stopwatch sw = new Stopwatch();
            sw.Start();
            //int s = AnsycbackForCache("w");           
            int s = insertdata(10);
            sw.Stop();
            TimeSpan ProGramts = sw.Elapsed;
            ViewBag.time = "运行时间：" + ProGramts.Seconds + "秒" + ProGramts.Milliseconds + "毫秒" + s;
            return View();
        }
        private int insertdata(int nums)
        {
            IList<T_User> IEnumerable = new List<T_User>();           
            for (int i = 0; i < nums; i++)
            {
                IEnumerable.Add(new T_User()
                {
                    PhoneNumber = "1316120389" + i,
                    Password = "112",
                    NickName = "0427" + i,
                    LoginToken = Guid.NewGuid().ToString().ToLower(),                    
                    LastLoginTime = DateTime.Now,
                });
            }           
            if (cache.Get<IList<T_User>>("list") != null)
            {
                cache.Delete("list");
            }
            return _user.Insert(IEnumerable);

        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}