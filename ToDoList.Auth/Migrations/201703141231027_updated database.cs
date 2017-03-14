namespace ToDoList.Auth.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateddatabase : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ToDoListModels", "Name", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.ToDoListModels", "Category", c => c.Int(nullable: false));
            AddColumn("dbo.ToDoListTasks", "IsDone", c => c.Boolean(nullable: false));
            AlterColumn("dbo.ToDoListTasks", "Task", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ToDoListTasks", "Task", c => c.String());
            DropColumn("dbo.ToDoListTasks", "IsDone");
            DropColumn("dbo.ToDoListModels", "Category");
            DropColumn("dbo.ToDoListModels", "Name");
        }
    }
}
