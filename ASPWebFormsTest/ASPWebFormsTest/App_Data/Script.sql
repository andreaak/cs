﻿CREATE TABLE [dbo].[Categories] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [CategoryName] NVARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

SET IDENTITY_INSERT [dbo].[Categories] ON
INSERT INTO [dbo].[Categories] ([Id], [CategoryName]) VALUES (1, N'Продукты питания')
INSERT INTO [dbo].[Categories] ([Id], [CategoryName]) VALUES (2, N'Техника')
INSERT INTO [dbo].[Categories] ([Id], [CategoryName]) VALUES (3, N'Книги')
SET IDENTITY_INSERT [dbo].[Categories] OFF
GO

CREATE TABLE [dbo].[Products] (
    [Id]         INT           IDENTITY (1, 1) NOT NULL,
    [Name]       NVARCHAR (50) NULL,
    [Price]      MONEY         NULL,
    [CategoryId] INT           NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

SET IDENTITY_INSERT [dbo].[Products] ON
INSERT INTO [dbo].[Products] ([Id], [Name], [Price], [CategoryId]) VALUES (1, N'Яблоко', CAST(2.0000 AS Money), 1)
INSERT INTO [dbo].[Products] ([Id], [Name], [Price], [CategoryId]) VALUES (2, N'Груша', CAST(3.0000 AS Money), 1)
INSERT INTO [dbo].[Products] ([Id], [Name], [Price], [CategoryId]) VALUES (3, N'Хлеб', CAST(10.0000 AS Money), 1)
INSERT INTO [dbo].[Products] ([Id], [Name], [Price], [CategoryId]) VALUES (4, N'Телефон', CAST(1000.0000 AS Money), 2)
INSERT INTO [dbo].[Products] ([Id], [Name], [Price], [CategoryId]) VALUES (5, N'Ноутбук', CAST(15000.0000 AS Money), 2)
INSERT INTO [dbo].[Products] ([Id], [Name], [Price], [CategoryId]) VALUES (6, N'ASP.NET для начинающих', CAST(300.0000 AS Money), 3)
INSERT INTO [dbo].[Products] ([Id], [Name], [Price], [CategoryId]) VALUES (7, N'ASP.NET для профессионалов', CAST(400.0000 AS Money), 3)
INSERT INTO [dbo].[Products] ([Id], [Name], [Price], [CategoryId]) VALUES (8, N'CLR via C#', CAST(450.0000 AS Money), 3)
SET IDENTITY_INSERT [dbo].[Products] OFF
GO

CREATE TABLE [dbo].[Users] (
    [ID]       INT           IDENTITY (1, 1) NOT NULL,
    [Login]    NVARCHAR (50) NULL,
    [Password] NVARCHAR (50) NULL,
    [Email]    NVARCHAR (50) NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([ID] ASC)
);
GO

SET IDENTITY_INSERT [dbo].[Users] ON
INSERT INTO [dbo].[Users] ([ID], [Login], [Password], [Email]) VALUES (1, N'admin', N'123', N'admin@test.com')
INSERT INTO [dbo].[Users] ([ID], [Login], [Password], [Email]) VALUES (2, N'user', N'456', N'user@test.com')
INSERT INTO [dbo].[Users] ([ID], [Login], [Password], [Email]) VALUES (3, N'testuser', N'erty', N'fsdfs')
SET IDENTITY_INSERT [dbo].[Users] OFF

CREATE TABLE [dbo].[Images] (
    [ImageId] INT   IDENTITY (1, 1) NOT NULL,
    [Image]   IMAGE NOT NULL,
    CONSTRAINT [PK_Images] PRIMARY KEY CLUSTERED ([ImageId] ASC)
);
GO

SET IDENTITY_INSERT [dbo].[Images] ON
INSERT INTO [dbo].[Images] ([ImageId], [Image]) VALUES (1, 0xFFD8FFE000104A46494600010201006000600000FFE1363D4578696600004D4D002A00000008000D010E00020000002C000000AA011200030000000100010000011A000500000001000000D6011B000500000001000000DE012800030000000100020000013100020000001C000000E6013200020000001400000102013B0002)
INSERT INTO [dbo].[Images] ([ImageId], [Image]) VALUES (2, 0xFFD8FFE000104A46494600010201004800480000FFE11CAE4578696600004D4D002A000000080007011200030000000100010000011A00050000000100000062011B0005000000010000006A012800030000000100020000013100020000001C0000007201320002000000140000008E8769000400000001000000A4000000D0)
INSERT INTO [dbo].[Images] ([ImageId], [Image]) VALUES (3, 0xFFD8FFE000104A46494600010201000100010000FFE1132A4578696600004D4D002A000000080007011200030000000100010000011A00050000000100000062011B0005000000010000006A012800030000000100020000013100020000001C0000007201320002000000140000008E8769000400000001000000A4000000D0)
SET IDENTITY_INSERT [dbo].[Images] OFF
GO

CREATE TABLE [dbo].[Messages] (
    [Id]           INT            NOT NULL,
    [MessageTitle] NVARCHAR (MAX) NULL,
    [MessageBody]  NVARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

INSERT INTO [dbo].[Messages] ([Id], [MessageTitle], [MessageBody]) VALUES (1, N'Test Title (SQL Database)', N'Test Message (SQL Database)')
INSERT INTO [dbo].[Messages] ([Id], [MessageTitle], [MessageBody]) VALUES (2, N'Test Title 2', N'Test Message Body2')
GO