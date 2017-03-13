namespace ToDoList.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ToDoListTasks",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Category = c.Int(nullable: false),
                        ExpirationDate = c.DateTime(nullable: false),
                        Priority = c.Int(nullable: false),
                        Task = c.String(),
                        ToDoList_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ToDoListModels", t => t.ToDoList_Id)
                .Index(t => t.ToDoList_Id);
            
            CreateTable(
                "dbo.ToDoListModels",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ToDoListTasks", "ToDoList_Id", "dbo.ToDoListModels");
            DropIndex("dbo.ToDoListTasks", new[] { "ToDoList_Id" });
            DropTable("dbo.ToDoListModels");
            DropTable("dbo.ToDoListTasks");
        }
    }
}
