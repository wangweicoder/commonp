using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApplication
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private delegate string MethodCaller(string date);
        public MainWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 异步计算
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < 5; i++)
            {                
                MethodCaller mc = TransfeDate;
                string name = "2017-04-14,2017-06-23";//输入参数 
                AsyncCallback asyncballback = t => Console.WriteLine("这里是AsyncCallback回调{0},当前线程id{1}", t.AsyncState, Thread.CurrentThread.ManagedThreadId);
                IAsyncResult result = mc.BeginInvoke(name, asyncballback, "w");               
                Console.WriteLine("{0},当前线程id{1}", "异步程序正在执行...", Thread.CurrentThread.ManagedThreadId);                
            }
            sw.Stop();
            TimeSpan ProGramts = sw.Elapsed;
            Console.WriteLine("-{0},当前线程id{1},耗时：{2}", "异步", Thread.CurrentThread.ManagedThreadId, +ProGramts.Seconds + "秒" + ProGramts.Milliseconds + "\r\n");

        }
        /// <summary>
        /// 同步计算
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Click(object sender, RoutedEventArgs e)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < 5; i++)
            {
                string myname = TransfeDate("2017-04-14,2017-06-23");
                Console.WriteLine("{0},当前线程id{1}", "同步程序正在执行...", Thread.CurrentThread.ManagedThreadId);
            }
            sw.Stop();
            TimeSpan ProGramts = sw.Elapsed;
            Console.WriteLine("-{0},当前线程id{1},耗时：{2}", "同步", Thread.CurrentThread.ManagedThreadId, +ProGramts.Seconds + "秒" + ProGramts.Milliseconds + "\r\n");

        }
        private static string TransfeDate(string ExpectedDate)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            Console.WriteLine("-{0},当前线程id{1}", ExpectedDate, Thread.CurrentThread.ManagedThreadId);
            string resultdate = null;
            //干活日期 
            if (!string.IsNullOrWhiteSpace(ExpectedDate))
            {
                Thread.Sleep(100);
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
                Console.WriteLine("-{0},当前线程id{1},耗时：{2}", ExpectedDate, Thread.CurrentThread.ManagedThreadId, +ProGramts.Seconds + "秒" + ProGramts.Milliseconds + "\r\n");
                return resultdate;
            }
            else
            {
                return ExpectedDate;
            }
        }
    }
}
