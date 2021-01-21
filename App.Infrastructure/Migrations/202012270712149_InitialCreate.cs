namespace App.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Permission",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Type = c.String(maxLength: 256),
                        Value = c.String(),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                        CreatedByID = c.Long(),
                        ModifiedByID = c.Long(),
                        IsActive = c.Boolean(nullable: false),
                        IsDefault = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.Type, unique: true);
            
            CreateTable(
                "dbo.RolePermission",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        RoleID = c.Long(nullable: false),
                        PermissionID = c.Long(nullable: false),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                        CreatedByID = c.Long(),
                        ModifiedByID = c.Long(),
                        IsActive = c.Boolean(nullable: false),
                        IsDefault = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Permission", t => t.PermissionID, cascadeDelete: true)
                .ForeignKey("dbo.Role", t => t.RoleID, cascadeDelete: true)
                .Index(t => t.RoleID)
                .Index(t => t.PermissionID);
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 256),
                        ConcurrencyStamp = c.String(),
                        TenantID = c.Long(nullable: false),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                        CreatedByID = c.Long(),
                        ModifiedByID = c.Long(),
                        IsActive = c.Boolean(nullable: false),
                        IsDefault = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Tenant", t => t.TenantID, cascadeDelete: true)
                .Index(t => t.TenantID);
            
            CreateTable(
                "dbo.Tenant",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 256),
                        Url = c.String(),
                        Website = c.String(),
                        Address = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                        Copyright = c.String(),
                        Logo = c.Binary(),
                        Favicon = c.Binary(),
                        FaviconUrl = c.String(),
                        Code = c.String(maxLength: 256),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                        CreatedByID = c.Long(),
                        ModifiedByID = c.Long(),
                        IsActive = c.Boolean(nullable: false),
                        IsDefault = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.Code, unique: true);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        UserName = c.String(maxLength: 256),
                        Email = c.String(maxLength: 256),
                        PhoneNumber = c.String(),
                        CreatorUserID = c.Long(),
                        LastName = c.String(),
                        FirstName = c.String(),
                        OtherName = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        ConcurrencyStamp = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        IsPasswordRequired = c.Boolean(nullable: false),
                        LockoutEnd = c.DateTimeOffset(precision: 7),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        IsSuperAdmin = c.Boolean(nullable: false),
                        IsDefault = c.Boolean(nullable: false),
                        Passport = c.Binary(),
                        TenantID = c.Long(nullable: false),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                        CreatedByID = c.Long(),
                        ModifiedByID = c.Long(),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Tenant", t => t.TenantID, cascadeDelete: true)
                .Index(t => t.TenantID);
            
            CreateTable(
                "dbo.UserPermission",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        UserID = c.Long(nullable: false),
                        PermissionID = c.Long(nullable: false),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                        CreatedByID = c.Long(),
                        ModifiedByID = c.Long(),
                        IsActive = c.Boolean(nullable: false),
                        IsDefault = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Permission", t => t.PermissionID, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.PermissionID);
            
            CreateTable(
                "dbo.UserRole",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        UserID = c.Long(nullable: false),
                        RoleID = c.Long(nullable: false),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                        CreatedByID = c.Long(),
                        ModifiedByID = c.Long(),
                        IsActive = c.Boolean(nullable: false),
                        IsDefault = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.User", t => t.UserID)
                .ForeignKey("dbo.Role", t => t.RoleID)
                .Index(t => t.UserID)
                .Index(t => t.RoleID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRole", "RoleID", "dbo.Role");
            DropForeignKey("dbo.UserRole", "UserID", "dbo.User");
            DropForeignKey("dbo.UserPermission", "UserID", "dbo.User");
            DropForeignKey("dbo.UserPermission", "PermissionID", "dbo.Permission");
            DropForeignKey("dbo.User", "TenantID", "dbo.Tenant");
            DropForeignKey("dbo.Role", "TenantID", "dbo.Tenant");
            DropForeignKey("dbo.RolePermission", "RoleID", "dbo.Role");
            DropForeignKey("dbo.RolePermission", "PermissionID", "dbo.Permission");
            DropIndex("dbo.UserRole", new[] { "RoleID" });
            DropIndex("dbo.UserRole", new[] { "UserID" });
            DropIndex("dbo.UserPermission", new[] { "PermissionID" });
            DropIndex("dbo.UserPermission", new[] { "UserID" });
            DropIndex("dbo.User", new[] { "TenantID" });
            DropIndex("dbo.Tenant", new[] { "Code" });
            DropIndex("dbo.Role", new[] { "TenantID" });
            DropIndex("dbo.RolePermission", new[] { "PermissionID" });
            DropIndex("dbo.RolePermission", new[] { "RoleID" });
            DropIndex("dbo.Permission", new[] { "Type" });
            DropTable("dbo.UserRole");
            DropTable("dbo.UserPermission");
            DropTable("dbo.User");
            DropTable("dbo.Tenant");
            DropTable("dbo.Role");
            DropTable("dbo.RolePermission");
            DropTable("dbo.Permission");
        }
    }
}
