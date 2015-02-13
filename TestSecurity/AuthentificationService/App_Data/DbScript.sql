IF EXISTS(SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES
      WHERE TABLE_NAME = 'UsersInRoles')
   DROP TABLE [UsersInRoles]

IF EXISTS(SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES
      WHERE TABLE_NAME = 'Users')
   DROP TABLE [Users]

CREATE TABLE [Users] (
    [UserId]           int IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [ApplicationId]    NVARCHAR (256)   NOT NULL,
    [UserName]         NVARCHAR (256)   NOT NULL,
    [Password]         NVARCHAR (128)   NOT NULL,
);

IF EXISTS(SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES
      WHERE TABLE_NAME = 'Roles')
   DROP TABLE [Roles]
   
CREATE TABLE [Roles] (
    [RoleId]          int IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [ApplicationId]   NVARCHAR (256)   NOT NULL,
    [RoleName]        NVARCHAR (256)   NOT NULL,
);

CREATE TABLE [UsersInRoles] (
    [UserId] int NOT NULL,
    [RoleId] int NOT NULL,
    PRIMARY KEY ([UserId], [RoleId]),
    FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([UserId]),
    FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Roles] ([RoleId])
);
GO