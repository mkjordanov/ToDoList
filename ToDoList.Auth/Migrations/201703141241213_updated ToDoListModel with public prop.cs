namespace ToDoList.Auth.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedToDoListModelwithpublicprop : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ToDoListModels", "IsPublic", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ToDoListModels", "IsPublic");
        }
    }
}
