CREATE TABLE [dbo].[Categories] (
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