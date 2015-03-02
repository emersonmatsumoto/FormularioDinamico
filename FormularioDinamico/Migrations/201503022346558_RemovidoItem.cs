namespace FormularioDinamico.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovidoItem : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Item", "CampoId", "dbo.Campo");
            DropIndex("dbo.Item", new[] { "CampoId" });
            AddColumn("dbo.Campo", "Lista", c => c.String());
            DropTable("dbo.Item");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Item",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CampoId = c.Int(nullable: false),
                        Texto = c.String(nullable: false, maxLength: 60),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.Campo", "Lista");
            CreateIndex("dbo.Item", "CampoId");
            AddForeignKey("dbo.Item", "CampoId", "dbo.Campo", "Id", cascadeDelete: true);
        }
    }
}
