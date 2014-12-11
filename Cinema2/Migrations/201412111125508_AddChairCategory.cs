namespace CinemaLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddChairCategory : DbMigration
    {
        public override void Up()
        {

            CreateTable(
                "dbo.ChairCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 4000),
                        Color = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.Id);
            Sql("Insert into ChairCategories(Name,Color) values('Standart','#78DBE2')");

            AddColumn("Chairs", "Category_Id", c => c.Int(defaultValue:1));
            AddForeignKey("Chairs", "Category_Id", "dbo.ChairCategories");
        }
        
        public override void Down()
        {
            
            DropForeignKey("dbo.Chairs", "Category_Id", "dbo.ChairCategories");
            DropIndex("dbo.Chairs", new[] { "Category_Id" });
            DropTable("dbo.ChairCategories");
            
        }
    }
}
