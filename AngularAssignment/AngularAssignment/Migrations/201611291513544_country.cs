namespace AngularAssignment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class country : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.People", "country", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.People", "country", c => c.Int());
        }
    }
}
