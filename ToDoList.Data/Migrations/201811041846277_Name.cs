namespace ToDoList.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Name : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Entry", newName: "Entries");
            CreateTable(
                "dbo.Steps",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        EntryId = c.Guid(nullable: false),
                        Data = c.String(),
                    })
                .PrimaryKey(t => new { t.Id, t.EntryId })
                .ForeignKey("dbo.Entries", t => t.EntryId, cascadeDelete: true)
                .Index(t => t.EntryId);
            
            AddColumn("dbo.Entries", "Name", c => c.String());
            AddColumn("dbo.Entries", "DueDate", c => c.DateTime());
            AddColumn("dbo.Entries", "Category", c => c.String());
            DropColumn("dbo.Entries", "TaskName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Entries", "TaskName", c => c.String());
            DropForeignKey("dbo.Steps", "EntryId", "dbo.Entries");
            DropIndex("dbo.Steps", new[] { "EntryId" });
            DropColumn("dbo.Entries", "Category");
            DropColumn("dbo.Entries", "DueDate");
            DropColumn("dbo.Entries", "Name");
            DropTable("dbo.Steps");
            RenameTable(name: "dbo.Entries", newName: "Entry");
        }
    }
}
