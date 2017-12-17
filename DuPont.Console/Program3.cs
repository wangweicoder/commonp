using Practice.Interface;
using Practice.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DuPont.ApiConsole
{
    class Program3
    {
        static void Main654(string[] args)
        {
            Console.WriteLine("我是主线程，线程ID：{0}", Thread.CurrentThread.ManagedThreadId);
            insert();
            Console.ReadLine();
        }
        static async Task insert()
        {
            Console.WriteLine("调用taskinsertdata()之前，线程ID：{0}", Thread.CurrentThread.ManagedThreadId);
            var s= taskinsertdata(10);            
            Console.WriteLine("调用taskinsertdata()之后，线程ID：{0}。当前时间：{1}", Thread.CurrentThread.ManagedThreadId, DateTime.Now.ToString("yyyy-MM-dd hh:MM:ss"));
            Console.WriteLine("得到taskinsertdata()方法的结果：{0}。线程ID：{1}。当前时间：{2}", await s, Thread.CurrentThread.ManagedThreadId, DateTime.Now.ToString("yyyy-MM-dd hh:MM:ss"));

        }
        /// <summary>
        /// 异步多线程
        /// </summary> 
        private static async Task<int> taskinsertdata(int nums)
        {
            IList<T_User> IEnumerable = new List<T_User>();
            Console.WriteLine("调用taskinsertdata()方法内，线程ID：{0}", Thread.CurrentThread.ManagedThreadId);
            IUser _user = new Practice.Repository.UserImpl();
            for (int i = 0; i < nums; i++)
            {
                IEnumerable.Add(new T_User()
                {
                    PhoneNumber = "1316120389" + i,
                    Password = "19",
                    NickName = "19" + i,
                    LoginToken = Guid.NewGuid().ToString().ToLower(),
                    LastLoginTime = DateTime.Now,
                });
            }
            return await _user.InsertAsync(IEnumerable);
            //用async方法，就要用await来修饰
        }
    }
}
