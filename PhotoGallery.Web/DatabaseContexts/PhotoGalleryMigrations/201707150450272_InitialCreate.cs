namespace PhotoGallery.Web.DatabaseContexts.PhotoGalleryMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Photos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        FileName = c.String(nullable: false),
                        FileExtension = c.String(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Photo_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Photos", t => t.Photo_Id)
                .Index(t => t.Photo_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tags", "Photo_Id", "dbo.Photos");
            DropIndex("dbo.Tags", new[] { "Photo_Id" });
            DropTable("dbo.Tags");
            DropTable("dbo.Photos");
        }
    }
}
