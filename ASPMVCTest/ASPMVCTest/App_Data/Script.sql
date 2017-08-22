CREATE TABLE [dbo].[Products] (
    [Id]    INT             IDENTITY (1, 1) NOT NULL,
    [Name]  NVARCHAR (MAX)  NULL,
    [Price] DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_dbo.Products] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

SET IDENTITY_INSERT [dbo].[Products] ON
INSERT INTO [dbo].[Products] ([Id], [Name], [Price]) VALUES (1, N'Product 1', CAST(10.00 AS Decimal(18, 2)))
INSERT INTO [dbo].[Products] ([Id], [Name], [Price]) VALUES (2, N'Product 2', CAST(112.00 AS Decimal(18, 2)))
SET IDENTITY_INSERT [dbo].[Products] OFF
GO