using Practice.Models.Migrations;
using Practice.Models.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice.Models
{
    public class PracticeContext : DbContext
    {
        static PracticeContext()
        {
            Database.SetInitializer<PracticeContext>(new CreateDatabaseIfNotExists<PracticeContext>());
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<PracticeContext, Configuration>());
        }
        public PracticeContext() : base("name=PracticeEntities")
            {
            this.Configuration.ProxyCreationEnabled = true;
        }
        public DbSet<T_User> User { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new T_User_Map());
        }
    }

}
