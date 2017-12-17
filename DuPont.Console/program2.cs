using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DuPont.APiConsole
{
    class program2
    {
        static void Main(string[] args)
        {
            Console.WriteLine("我是主线程，线程ID：{0}", Thread.CurrentThread.ManagedThreadId);
            TestAsync1();
            //TestAsync();
            Console.ReadLine();
        }

        static async Task TestAsync()
        {
            Console.WriteLine("调用GetReturnResult()之前，线程ID：{0}。当前时间：{1}", Thread.CurrentThread.ManagedThreadId, DateTime.Now.ToString("yyyy-MM-dd hh:MM:ss"));
            var name = GetReturnResult();
            Console.WriteLine("调用GetReturnResult()之后，线程ID：{0}。当前时间：{1}", Thread.CurrentThread.ManagedThreadId, DateTime.Now.ToString("yyyy-MM-dd hh:MM:ss"));
            Console.WriteLine("得到GetReturnResult()方法的结果：{0}。线程ID：{1}。当前时间：{2}", await name,Thread.CurrentThread.ManagedThreadId , DateTime.Now.ToString("yyyy-MM-dd hh:MM:ss"));
        }

        static void TestAsync1()
        {
            Console.WriteLine("调用asyncresult()之前，线程ID：{0}。当前时间：{1}", Thread.CurrentThread.ManagedThreadId, DateTime.Now.ToString("yyyy-MM-dd hh:MM:ss"));
            var name =  asyncresult();
            Console.WriteLine("调用asyncresult()之后，线程ID：{0}。当前时间：{1}", Thread.CurrentThread.ManagedThreadId, DateTime.Now.ToString("yyyy-MM-dd hh:MM:ss"));
            Console.WriteLine("得到asyncresult()方法的结果：{0}。线程ID：{1}。当前时间：{2}",  name, Thread.CurrentThread.ManagedThreadId, DateTime.Now.ToString("yyyy-MM-dd hh:MM:ss"));
        }
        /// <summary>
        /// 异步
        /// </summary>
        /// <returns></returns>
        static async Task<string> GetReturnResult()
        {
            Console.WriteLine("执行Task.Run之前, 线程ID：{0}", Thread.CurrentThread.ManagedThreadId);
            return await Task.Run(() =>
            {
                Thread.Sleep(3000);
                Console.WriteLine("GetReturnResult()方法里面线程ID: {0}", Thread.CurrentThread.ManagedThreadId);
                return "我是返回值";
            });
        }
        /// <summary>
        /// 普通多线程
        /// </summary>
        /// <returns></returns>
        static string asyncresult()
        {
            Console.WriteLine("asyncresult中执行Task.Run之前, 线程ID：{0}", Thread.CurrentThread.ManagedThreadId);
            string result =Task.Run(() => {

                Thread.Sleep(3000);
                Console.WriteLine("asyncresult()方法里面线程ID: {0}", Thread.CurrentThread.ManagedThreadId);
                return "asyncresult我是返回值";
            }).Result;
            return result;
        }
    }
}
