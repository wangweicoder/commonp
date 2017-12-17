namespace Practice.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.T_User",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        NickName = c.String(maxLength: 500),
                        Type = c.Int(nullable: false),
                        PhoneNumber = c.String(maxLength: 11, unicode: false),
                        Password = c.String(nullable: false, maxLength: 100, unicode: false),
                        LoginToken = c.String(maxLength: 200, unicode: false),
                        UserName = c.String(maxLength: 50),
                        CreateTime = c.DateTime(nullable: false),
                        DeletedTime = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        ModifiedTime = c.DateTime(),
                        LastLoginTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.T_User");
        }
    }
}
