namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class gender_data_Editbool : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Writers", "WriterGender", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Writers", "WriterGender", c => c.String(maxLength: 6));
        }
    }
}
