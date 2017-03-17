namespace ToDoList.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedautogenertedidsforallmodels : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.ToDoListTasks");
            AlterColumn("dbo.ToDoListTasks", "Id", c => c.Guid(nullable: false, identity: true));
            AddPrimaryKey("dbo.ToDoListTasks", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.ToDoListTasks");
            AlterColumn("dbo.ToDoListTasks", "Id", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.ToDoListTasks", "Id");
        }
    }
}
