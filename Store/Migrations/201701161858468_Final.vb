Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class Final
        Inherits DbMigration
    
        Public Overrides Sub Up()
            DropForeignKey("dbo.ShoppingBasketItems", "Product_ID", "dbo.Products")
            DropForeignKey("dbo.ShoppingBasketItems", "ShoppingBasket_ID", "dbo.ShoppingBaskets")
            DropIndex("dbo.ShoppingBasketItems", New String() { "Product_ID" })
            DropIndex("dbo.ShoppingBasketItems", New String() { "ShoppingBasket_ID" })
            DropTable("dbo.ShoppingBasketItems")
        End Sub
        
        Public Overrides Sub Down()
            CreateTable(
                "dbo.ShoppingBasketItems",
                Function(c) New With
                    {
                        .ID = c.Int(nullable := False, identity := True),
                        .Amount = c.Int(nullable := False),
                        .Product_ID = c.Int(),
                        .ShoppingBasket_ID = c.Int()
                    }) _
                .PrimaryKey(Function(t) t.ID)
            
            CreateIndex("dbo.ShoppingBasketItems", "ShoppingBasket_ID")
            CreateIndex("dbo.ShoppingBasketItems", "Product_ID")
            AddForeignKey("dbo.ShoppingBasketItems", "ShoppingBasket_ID", "dbo.ShoppingBaskets", "ID")
            AddForeignKey("dbo.ShoppingBasketItems", "Product_ID", "dbo.Products", "ID")
        End Sub
    End Class
End Namespace
