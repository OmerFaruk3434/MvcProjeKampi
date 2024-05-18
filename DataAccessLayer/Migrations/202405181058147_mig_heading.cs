namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig_heading : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Headings", "HeadingName", c => c.String(maxLength: 60));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Headings", "HeadingName", c => c.String(maxLength: 50));
        }
    }
}
