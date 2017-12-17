
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;



namespace DuPont.ApiConsole
{
    class Program
    {
        public delegate string MethodCaller(string date);//定义个代理 
        static void Main6(string[] args)
        {
            string workdir = Directory.GetCurrentDirectory();//应用程序当前工作目录           
            if (1 == 1)
            {
                try
                {
                    System.Console.Write("请输入要测试的方法，1为异步，2为同步:\r\n");
                    string e =System.Console.ReadLine().ToString();
                    //日期路径
                    //var cornurl = @"D:\DuPontWeb\ApiPresentation\FileJson";
                    //DirectoryInfo theFolder = new DirectoryInfo(cornurl);
                    ////DirectoryInfo[] dirInfo = theFolder.GetDirectories();
                    ////遍历文件夹
                    ////foreach (DirectoryInfo NextFolder in dirInfo)
                    ////{                       
                    //  FileInfo[] fileInfo = theFolder.GetFiles();
                    //    foreach (FileInfo NextFile in fileInfo)  //遍历文件
                    //    {
                    //        string filename=NextFile.Name;
                    //        System.Console.WriteLine(filename.Substring(0,filename.IndexOf('.')));
                    //    }
                    ////}
                    Stopwatch sw = new Stopwatch();
                    sw.Start();
                    if (e == "1")
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            //string dt = Convert.ToDateTime("2017年04月13日").ToString("yyyy-MM-dd");
                            MethodCaller mc = new MethodCaller(TransfeDate);//实例化委托
                            string name = "2017-04-14,2017-06-23";//输入参数 
                            //mc += TransfeDate;
                            //AsyncCallback asyncballback = t => System.Console.WriteLine("这里是AsyncCallback回调{0},当前线程id{1}", t.AsyncState, Thread.CurrentThread.ManagedThreadId);
                            //IAsyncResult result = mc.BeginInvoke(name, asyncballback, ansycback("w"));
                            IAsyncResult result = mc.BeginInvoke(name, null, null);
                            //mc.Invoke(name);
                            // mc(name);
                            //if(!result.IsCompleted)
                            //{
                            System.Console.WriteLine("{0},当前线程id{1}", "程序正在执行...", Thread.CurrentThread.ManagedThreadId);
                           // }
                            //string myname = mc.EndInvoke(result);//用于接收返回值 ,會耗時
                        }
                    }
                    else if(e=="2")
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            string myname = TransfeDate("2017-04-14,2017-06-23");
                            System.Console.WriteLine("{0},当前线程id{1}", "程序正在执行...", Thread.CurrentThread.ManagedThreadId);
                        }
                    }
                    else if(e=="6") {
                       int s= AnsycbackForCache("w");                       
                      System.Console.WriteLine("{0}", s);                      
                       
                    }
                    //string myname = TransfeDate("2017-04-14,2017-06-23,2017-06-23");
                    //string date = TransfeDate("2017年04月13日、2017年04月14日、");
                    sw.Stop();
                    TimeSpan ProGramts = sw.Elapsed;
                    System.Console.Write("运行时间：" + ProGramts.Seconds + "秒" + ProGramts.Milliseconds + "\r\n");
                    System.Console.ReadLine();
                }
                catch (Exception ex)
                {
                    string logErrstring = DateTime.Now.ToString("\r\n---------MM/dd/yyyy HH:mm:ss,fff---------\r\n") + ex.Message;
                    throw ;                 
                }
            }
            System.Console.ReadLine();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ExpectedDate"></param>
        /// <returns></returns>
        private static string TransfeDate(string ExpectedDate)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            System.Console.WriteLine("-{0},当前线程id{1}", ExpectedDate, Thread.CurrentThread.ManagedThreadId);              
            string resultdate = null;
            //干活日期 
            if (!string.IsNullOrWhiteSpace(ExpectedDate))
            {
                //Thread.Sleep(100);
                for (int i = 0; i < 9999; i++)
                {
                    string tempdate = ExpectedDate;
                    if (tempdate.Contains(',') || tempdate.Contains('-'))
                    {
                        string[] tempdates = tempdate.Split(',');
                        foreach (var item in tempdates)
                        {
                            string idate = Convert.ToDateTime(item).ToString("yyyy年MM月dd日");
                            resultdate += idate + "、";
                        }
                        resultdate = resultdate.TrimEnd('、');
                    }
                    else if (tempdate.Contains("、"))
                    {
                        if (tempdate.EndsWith("、"))
                        {
                            tempdate = tempdate.TrimEnd('、');
                        }
                        resultdate = tempdate;
                    }
                }
                sw.Stop();
                TimeSpan ProGramts = sw.Elapsed;
                System.Console.WriteLine("-{0},当前线程id{1},耗时：{2}", ExpectedDate, Thread.CurrentThread.ManagedThreadId, +ProGramts.Seconds + "秒" + ProGramts.Milliseconds + "\r\n");
                return resultdate;
            }
            else
            {
                return ExpectedDate;
            }
        }
       
        private static string ansycback(string o)
        {
            myclass6 c6 = new myclass6();
            c6.method();
            System.Console.WriteLine("s{0},当前线程id{1}","tart", Thread.CurrentThread.ManagedThreadId);
            MyClass m = new MyClass();
            Task<string> async = new Task<string>(() =>m.returns());
            async.Start();
            //完成时延续的任务
            async.ContinueWith(task => TransfeDate("2017-04-14,2017-06-23"));
            string s = async.Result;
            RedisHelper redis =new RedisHelper();
            var rediscache=redis.Get<string>("Mfristkey1");
            if (rediscache != null)
            {
                redis.Remove("Secondkey2");
                var secondkey = redis.Get<string>("Secondkey2");
            }
            if (async.IsCompleted)
            {         
                System.Console.WriteLine("s{0},当前线程id{1}", s, Thread.CurrentThread.ManagedThreadId);
                return s;
            }
            else
            {
                System.Console.WriteLine("0:{0},当前线程id{1}", o, Thread.CurrentThread.ManagedThreadId);
                return o;
            }

        }

        private static int AnsycbackForCache(string o)
        {
            int s = 1 , c = 1;
            List<int> list = new List<int>();
            System.Web.Caching.Cache cache = System.Web.HttpRuntime.Cache;
            if (cache.Get("list") != null)
            {
                s = (int)cache.Get("list");
            }
            else
            {
                //787459714  99999999
                //771310710  111111111
                //887459713   99999999+1
                try
                {
                    //for (int i = 0; i < 67108864; i++)
                    //{
                    //    s = s + i;
                    //    //list.Add(i);
                    //}
                    //for (int i = 0; i < 99999999+10; i++)
                    //{
                    //    c = c + i;
                    //    list.Add(i);
                    //}
                    Thread.Sleep(2000);
                }
                catch(Exception ex)
                {
                    throw;
                }
            }
            return c;
        }
        class  MyClass
        {
            public int Age { get; set; }
            public string Name { get; set; }
            public MyClass()
            {
                Age = 20;
                Name = "张三";
            }
            public string returns()
            {
                System.Console.WriteLine("name:{0},当前线程id{1}", Name, Thread.CurrentThread.ManagedThreadId);
                return Name;
            }
        }
        class myclass6
        {
            public int age { get; set; } = 2;
            public void method()
            {
                string s = "1";                
                string s1 = $"{s},{age}";
                
                
            }
        }

    }
}
