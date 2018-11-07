namespace ToDoList.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Entry",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TaskName = c.String(),
                        Description = c.String(),
                        DateAdded = c.DateTime(nullable: false),
                        IsCompleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Entry");
        }
    }
}
