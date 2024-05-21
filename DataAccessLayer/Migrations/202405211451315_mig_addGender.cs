namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig_addGender : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Writers", "WriterGender", c => c.String(maxLength: 6));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Writers", "WriterGender");
        }
    }
}
