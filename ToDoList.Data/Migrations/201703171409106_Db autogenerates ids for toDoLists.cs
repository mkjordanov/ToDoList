namespace ToDoList.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DbautogeneratesidsfortoDoLists : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ToDoListTasks", "ToDoList_Id", "dbo.ToDoListModels");
            DropPrimaryKey("dbo.ToDoListModels");
            AlterColumn("dbo.ToDoListModels", "Id", c => c.Guid(nullable: false, identity: true));
            AddPrimaryKey("dbo.ToDoListModels", "Id");
            AddForeignKey("dbo.ToDoListTasks", "ToDoList_Id", "dbo.ToDoListModels", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ToDoListTasks", "ToDoList_Id", "dbo.ToDoListModels");
            DropPrimaryKey("dbo.ToDoListModels");
            AlterColumn("dbo.ToDoListModels", "Id", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.ToDoListModels", "Id");
            AddForeignKey("dbo.ToDoListTasks", "ToDoList_Id", "dbo.ToDoListModels", "Id");
        }
    }
}
