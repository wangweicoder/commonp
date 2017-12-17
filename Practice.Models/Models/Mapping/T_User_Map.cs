using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice.Models.Models
{
   public class T_User_Map:EntityTypeConfiguration<T_User>
    {
        public T_User_Map()
        {
            this.HasKey(t => t.Id);
            // Properties
            this.Property(t => t.PhoneNumber)
                .HasColumnType("varchar")
                .IsOptional()
                .HasMaxLength(11);

            this.Property(t => t.Password)
                .HasColumnType("varchar")
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.LoginToken)
                .HasColumnType("varchar")
                .HasMaxLength(200);
            this.Property(t => t.UserName)
               .HasMaxLength(50);
            this.Property(t => t.NickName)
              .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("T_User");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.PhoneNumber).HasColumnName("PhoneNumber");
            this.Property(t => t.Password).HasColumnName("Password");
            this.Property(t => t.LoginToken).HasColumnName("LoginToken");          
            this.Property(t => t.UserName).HasColumnName("UserName");           
        }
    }
}
