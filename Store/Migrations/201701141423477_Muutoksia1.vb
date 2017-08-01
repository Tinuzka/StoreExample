Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class Muutoksia1
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "dbo.ShoppingBaskets",
                Function(c) New With
                    {
                        .ID = c.Int(nullable := False, identity := True)
                    }) _
                .PrimaryKey(Function(t) t.ID)
            
            CreateTable(
                "dbo.ShoppingBasketItems",
                Function(c) New With
                    {
                        .ID = c.Int(nullable := False, identity := True),
                        .Amount = c.Int(nullable := False),
                        .Product_ID = c.Int(),
                        .ShoppingBasket_ID = c.Int()
                    }) _
                .PrimaryKey(Function(t) t.ID) _
                .ForeignKey("dbo.Products", Function(t) t.Product_ID) _
                .ForeignKey("dbo.ShoppingBaskets", Function(t) t.ShoppingBasket_ID) _
                .Index(Function(t) t.Product_ID) _
                .Index(Function(t) t.ShoppingBasket_ID)
            
        End Sub
        
        Public Overrides Sub Down()
            DropForeignKey("dbo.ShoppingBasketItems", "ShoppingBasket_ID", "dbo.ShoppingBaskets")
            DropForeignKey("dbo.ShoppingBasketItems", "Product_ID", "dbo.Products")
            DropIndex("dbo.ShoppingBasketItems", New String() { "ShoppingBasket_ID" })
            DropIndex("dbo.ShoppingBasketItems", New String() { "Product_ID" })
            DropTable("dbo.ShoppingBasketItems")
            DropTable("dbo.ShoppingBaskets")
        End Sub
    End Class
End Namespace
