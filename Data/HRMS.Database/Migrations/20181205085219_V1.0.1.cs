using Microsoft.EntityFrameworkCore.Migrations;

namespace HRMS.Database.Migrations
{
    public partial class V101 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("set IDENTITY_INSERT dbo.Permission ON;");

            migrationBuilder.Sql(@"INSERT [dbo].[Permission] ([Id], [ModifyTime], [Modifier], [CreateTime], [Creator], [Name], [Control], [Action], [OrderIndex], [ParentID], [Remarks]) VALUES (1, NULL, NULL, CAST(0x070000000000083F0B AS DateTime2), N'System', N'用户管理', N'System', N'UserIndex', 0, NULL, NULL)
GO
INSERT [dbo].[Permission] ([Id], [ModifyTime], [Modifier], [CreateTime], [Creator], [Name], [Control], [Action], [OrderIndex], [ParentID], [Remarks]) VALUES (2, NULL, NULL, CAST(0x070000000000083F0B AS DateTime2), N'System', N'用户管理-查询', N'System', N'UserIndexData', 1, 1, NULL)
GO
INSERT [dbo].[Permission] ([Id], [ModifyTime], [Modifier], [CreateTime], [Creator], [Name], [Control], [Action], [OrderIndex], [ParentID], [Remarks]) VALUES (3, NULL, NULL, CAST(0x070000000000083F0B AS DateTime2), N'System', N'用户管理-添加', N'System', N'UserIndexCreate', 2, 1, NULL)
GO
INSERT [dbo].[Permission] ([Id], [ModifyTime], [Modifier], [CreateTime], [Creator], [Name], [Control], [Action], [OrderIndex], [ParentID], [Remarks]) VALUES (4, NULL, NULL, CAST(0x070000000000083F0B AS DateTime2), N'System', N'用户管理-更新', N'System', N'UserIndexUpdate', 3, 1, NULL)
GO
INSERT [dbo].[Permission] ([Id], [ModifyTime], [Modifier], [CreateTime], [Creator], [Name], [Control], [Action], [OrderIndex], [ParentID], [Remarks]) VALUES (5, NULL, NULL, CAST(0x070000000000083F0B AS DateTime2), N'System', N'用户管理-删除', N'System', N'UserIndexDelete', 4, 1, NULL)
GO
INSERT [dbo].[Permission] ([Id], [ModifyTime], [Modifier], [CreateTime], [Creator], [Name], [Control], [Action], [OrderIndex], [ParentID], [Remarks]) VALUES (6, NULL, NULL, CAST(0x070000000000083F0B AS DateTime2), N'System', N'角色管理', N'System', N'RoleIndex', 0, NULL, NULL)
GO
INSERT [dbo].[Permission] ([Id], [ModifyTime], [Modifier], [CreateTime], [Creator], [Name], [Control], [Action], [OrderIndex], [ParentID], [Remarks]) VALUES (7, NULL, NULL, CAST(0x070000000000083F0B AS DateTime2), N'System', N'角色管理-查询', N'System', N'RoleIndexData', 1, 6, NULL)
GO
INSERT [dbo].[Permission] ([Id], [ModifyTime], [Modifier], [CreateTime], [Creator], [Name], [Control], [Action], [OrderIndex], [ParentID], [Remarks]) VALUES (8, NULL, NULL, CAST(0x070000000000083F0B AS DateTime2), N'System', N'角色管理-添加', N'System', N'RoleIndexCreate', 2, 6, NULL)
GO
INSERT [dbo].[Permission] ([Id], [ModifyTime], [Modifier], [CreateTime], [Creator], [Name], [Control], [Action], [OrderIndex], [ParentID], [Remarks]) VALUES (9, NULL, NULL, CAST(0x070000000000083F0B AS DateTime2), N'System', N'角色管理-更新', N'System', N'RoleIndexUpdate', 3, 6, NULL)
GO
INSERT [dbo].[Permission] ([Id], [ModifyTime], [Modifier], [CreateTime], [Creator], [Name], [Control], [Action], [OrderIndex], [ParentID], [Remarks]) VALUES (11, NULL, NULL, CAST(0x070000000000083F0B AS DateTime2), N'System', N'角色管理-删除', N'System', N'RoleIndexDelete', 3, 6, NULL)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
