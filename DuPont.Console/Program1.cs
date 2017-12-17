using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuPont.ApiConsole
{
    class Program1
    {
        static void Main5(string[] args)
        {
            Console.WriteLine("简单工厂模式：");
            AbstractCar productA = SimpleFactory.Create(ProductEnum.ConcreateProductA);
            //cara carA = new cara();
            //carA.GetInfo();
            var person = new { name = "ww", arg = 30 };
            var arg=person.arg;
            var cara = new ConcreateCarA { name = "a" };
            cara.run();
            var lists = new List<int> { 1, 2, 3, 4, 5 };
            var s=lists.Where(x => x > 3).Sum();
            var u = from v in lists where v > 3 select v;
            u.Sum();
            productA.GetInfo();
            Console.ReadLine();
        }
        static void Main1(string[] ages)
        {
            //Console.WriteLine("反射工厂模式：");
            //AbstractCar pa = ReflectFactory.Create("DuPont.ApiConsole.ConcreateCarA");
            //pa.GetInfo();           
            ////
            //Console.WriteLine("工厂方法模式：");
            //IFactoryMethod factoryB = new ConcreateFactoryA();
            //var productB = factoryB.Create();
            //productB.GetInfo();          
            //
            Console.WriteLine("抽象工厂模式：");
            IAbstractFactory bmwFactory = new BMWFactory();
            bmwFactory.CreateCar().GetInfo();
            IAbstractFactory bydFactory = new BYDFactory();
            bydFactory.CreateCar().run();          
            Console.ReadLine();
        }
    }
    /// <summary>
    /// 线程安全的懒汉式
    /// </summary>
    public class Singleton
    {
        //volatile 不稳定的
        private volatile static Singleton _instance = null;
        private static readonly object lockHelper = new object();
        private Singleton() { }
        public static Singleton CreateInstance()
        {
            if (_instance == null)
            {
                lock (lockHelper)
                {
                    if (_instance == null)
                        _instance = new Singleton();
                }
            }
            return _instance;
        }
    }
    /// <summary>
    /// 饿汉式单例
    /// </summary>
    public class EagerSingleton
    {
        private static EagerSingleton instance = new EagerSingleton();

        private EagerSingleton() { }

        public static EagerSingleton GetInstance()
        {
            return instance;
        }
    }

    /// <summary>
    /// C#特有的写法
    /// </summary>
    public class Singleton1
    {
        private Singleton1() { }
        public static readonly Singleton1 instance = new Singleton1();
    }
    /// <summary>
    /// 产品枚举
    /// </summary>
    public enum ProductEnum
    {
        ConcreateProductA,
        ConcreateProductB
    }
    /// <summary>
    /// 简单工厂模式：
    /// 简单工厂模式的工厂类一般是使用静态方法，通过接收的参数的不同来返回不同的对象实例。
    /// 不修改代码的话，是无法扩展的。（如果增加新的产品，需要增加工厂的Swith分支）
    /// 不符合【开放封闭原则】
    /// </summary>
    public static class SimpleFactory
    {
        public static AbstractCar Create(ProductEnum productType)
        {
            switch (productType)
            {
                case ProductEnum.ConcreateProductA: return new ConcreateCarA();
                case ProductEnum.ConcreateProductB: return new ConcreateCarB();
                default: return null;
            }
        }
    }
    /// <summary>
    /// 反射工厂
    ///  <param name="names">子类</param>
    /// </summary>
    public static class  ReflectFactory
    {
        public static AbstractCar Create(string names)
        {
            Type type = Type.GetType(names, null, null);
            var instance = type.Assembly.CreateInstance(names) as AbstractCar;
            return instance;
        }

    }
    /// <summary>
    /// 工厂方法模式：
    /// 工厂方法是针对每一种产品提供一个工厂类。通过不同的工厂实例来创建不同的产品实例。
    /// 在同一等级结构中，支持增加任意产品。
    /// 符合【开放封闭原则】，但随着产品类的增加，对应的工厂也会随之增多
    /// </summary>
    public interface IFactoryMethod
    {
        AbstractCar Create();
    }
    public class ConcreateFactoryA : IFactoryMethod
    {
        public AbstractCar Create()
        {
            return new ConcreateCarA();

        }
    }

    public class ConcreateFactoryB : IFactoryMethod
    {
        public AbstractCar Create()
        {
            return new ConcreateCarB();
        }
    }

    /// <summary>
    /// 抽象工厂模式：
    /// 抽象工厂是应对产品族概念的，比如说，每个汽车公司可能要同时生产轿车，货车，客车，那么每一个工厂都要有创建轿车，货车和客车的方法。
    /// 应对产品族概念而生，增加新的产品线很容易，但是无法增加新的产品。
    /// </summary>
    public interface IAbstractFactory
    {
        AbstractCar CreateCar();
        AbstractBus CreateBus();
    }   
    /// <summary>
    /// 宝马工厂
     /// </summary>
    public class BMWFactory : IAbstractFactory
    {
        public AbstractCar CreateCar()
        {
            return new ConcreateCarA();
        }
        public AbstractBus CreateBus()
        {
            return new ConcreateBusA();
        }
      
    }    /// <summary>
         /// 比亚迪工厂
         /// </summary>
    public class BYDFactory : IAbstractFactory
    {
        public AbstractCar CreateCar()
        {
            return new ConcreateCarB();
        }
        public AbstractBus CreateBus()
        {
            return new ConcreateBusA();
        }
    }   
    /// <summary>
    /// 抽象父类轿车
    /// </summary>
    public abstract class AbstractCar
    {
        public string name { get; set; } = "parentcar";

        public virtual void Run()
        {
            System.Console.WriteLine("走");
        }
        public abstract string run();
        public virtual string GetInfo()
        {
            System.Console.WriteLine(name + "info");
            return name + "info";
        }
    }
    public class ConcreateCarA : AbstractCar
    {
        public override void Run()
        {
            System.Console.WriteLine("carA走");
        }
        public override string GetInfo()
        {
            System.Console.WriteLine("Ainfo");
            return "Ainfo";
        }
        public override string run()
        {
            System.Console.WriteLine("Arun");
            return "Arun";
        }
    }
    public class ConcreateCarB : AbstractCar
    {
        public override void Run()
        {
            System.Console.WriteLine("carB走");
        }
        public override string run()
        {
            System.Console.WriteLine("Brun");
            return "Brun";
        }
    }
    /// <summary>
    /// 抽象父类公交车
    /// </summary>
    public abstract class AbstractBus
    {
        public string name { get; set; } = "parentBus";

        public virtual void Run()
        {
            System.Console.WriteLine("bus走");
        }
        public abstract string run();
        public virtual string GetInfo()
        {
            System.Console.WriteLine(name + "businfo");
            return name + "businfo";
        }
    }
    public class ConcreateBusA : AbstractBus
    {
        public override void Run()
        {
            System.Console.WriteLine("carA走");
        }
        public override string GetInfo()
        {
            System.Console.WriteLine("Ainfo");
            return "Ainfo";
        }
        public override string run()
        {
            System.Console.WriteLine("Arun");
            return "Arun";
        }
    }
    public class ConcreateBusB : AbstractBus
    {
        public override void Run()
        {
            System.Console.WriteLine("carB走");
        }
        public override string run()
        {
            System.Console.WriteLine("Brun");
            return "Brun";
        }
    }
   
    #region 普通类
    public class car
    {
        public string GetInfo()
        {
            System.Console.WriteLine("info");
            return "info";
        }
    }
    public class cara : car
    {
        public new string GetInfo()
        {
            System.Console.WriteLine("Ainfo");
            return "Ainfo";
        }
    }
    #endregion
}
