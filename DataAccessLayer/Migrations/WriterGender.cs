namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WriterGender : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Writers", "WriterGender", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Writers", "WriterGender", c => c.Boolean(nullable: false));
        }
    }
}
