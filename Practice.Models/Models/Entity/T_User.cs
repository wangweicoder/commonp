using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice.Models.Models
{
   public class T_User
    {
        public T_User()
        {           
            CreateTime = DateTime.Now;
            IsDeleted = false;
            Type = 0;
        }
        public long Id { get; set; }
        /// <summary>
        /// 用户昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 用户类型
        /// </summary>
        public int Type { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string LoginToken { get; set; }       
        public string UserName { get; set; }       
        public System.DateTime CreateTime { get; set; }      
        public Nullable<System.DateTime> DeletedTime { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<System.DateTime> ModifiedTime { get; set; }     

        /// <summary>
        /// 最近一次登录的时间
        /// </summary>
        public DateTime? LastLoginTime { get; set; }    
      
    }
}
