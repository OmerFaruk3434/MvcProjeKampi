namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WriterPhoneNumber : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Writers", "WriterPhoneNumber", c => c.String(maxLength: 20));
            AlterColumn("dbo.Writers", "WriterName", c => c.String(maxLength: 100));
            AlterColumn("dbo.Writers", "WriterSurName", c => c.String(maxLength: 100));
            AlterColumn("dbo.Writers", "WriterPassword", c => c.String(maxLength: 500));
            AlterColumn("dbo.Writers", "WriterTitle", c => c.String(maxLength: 1000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Writers", "WriterTitle", c => c.String(maxLength: 50));
            AlterColumn("dbo.Writers", "WriterPassword", c => c.String(maxLength: 200));
            AlterColumn("dbo.Writers", "WriterSurName", c => c.String(maxLength: 50));
            AlterColumn("dbo.Writers", "WriterName", c => c.String(maxLength: 50));
            DropColumn("dbo.Writers", "WriterPhoneNumber");
        }
    }
}
