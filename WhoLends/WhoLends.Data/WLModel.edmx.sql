
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 09/08/2016 16:58:58
-- Generated from EDMX file: D:\PRV\GitHub\WhoLends\WhoLends\WhoLends.Data\WLModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [WhoLends];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_LendUsers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Lend] DROP CONSTRAINT [FK_LendUsers];
GO
IF OBJECT_ID(N'[dbo].[FK_LendLendItem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[LendItem] DROP CONSTRAINT [FK_LendLendItem];
GO
IF OBJECT_ID(N'[dbo].[FK_LendLendReturn]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Lend] DROP CONSTRAINT [FK_LendLendReturn];
GO
IF OBJECT_ID(N'[dbo].[FK_LendReturnAspNetUsers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[LendReturn] DROP CONSTRAINT [FK_LendReturnAspNetUsers];
GO
IF OBJECT_ID(N'[dbo].[FK_UsersRoles]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Users] DROP CONSTRAINT [FK_UsersRoles];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Roles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Roles];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[Lend]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Lend];
GO
IF OBJECT_ID(N'[dbo].[LendItem]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LendItem];
GO
IF OBJECT_ID(N'[dbo].[LendReturn]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LendReturn];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Role'
CREATE TABLE [dbo].[Role] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'User'
CREATE TABLE [dbo].[User] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [RoleId] nvarchar(max)  NOT NULL,
    [Email] nvarchar(256)  NULL,
    [EmailConfirmed] bit  NOT NULL,
    [PasswordHash] nvarchar(max)  NULL,
    [SecurityStamp] nvarchar(max)  NULL,
    [PhoneNumber] nvarchar(max)  NULL,
    [PhoneNumberConfirmed] bit  NOT NULL,
    [TwoFactorEnabled] bit  NOT NULL,
    [LockoutEndDateUtc] datetime  NULL,
    [LockoutEnabled] bit  NOT NULL,
    [AccessFailedCount] int  NOT NULL,
    [UserName] nvarchar(256)  NOT NULL,
    [Roles_Id] int  NOT NULL
);
GO

-- Creating table 'Lend'
CREATE TABLE [dbo].[Lend] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [LendItemId] nvarchar(max)  NOT NULL,
    [CreatedByUserId] nvarchar(max)  NOT NULL,
    [LendReturnId] nvarchar(max)  NOT NULL,
    [From] datetime  NOT NULL,
    [To] datetime  NULL,
    [CreatedAt] datetime  NOT NULL,
    [Users_Id] int  NOT NULL,
    [Users_RoleId] nvarchar(max)  NOT NULL,
    [LendReturn_Id] int  NOT NULL,
    [LendReturn_LendId] nvarchar(max)  NOT NULL,
    [LendReturn_CreatedByUserId] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'LendItem'
CREATE TABLE [dbo].[LendItem] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [Quantity] smallint  NOT NULL,
    [Condition] int  NOT NULL,
    [Lend_Id] int  NOT NULL,
    [Lend_CreatedByUserId] nvarchar(max)  NOT NULL,
    [Lend_LendItemId] nvarchar(max)  NOT NULL,
    [Lend_LendReturnId] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'LendReturn'
CREATE TABLE [dbo].[LendReturn] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [LendId] nvarchar(max)  NOT NULL,
    [CreatedByUserId] nvarchar(max)  NOT NULL,
    [CustomerId] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [Users_Id] int  NOT NULL,
    [Users_RoleId] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Role'
ALTER TABLE [dbo].[Role]
ADD CONSTRAINT [PK_Role]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id], [RoleId] in table 'User'
ALTER TABLE [dbo].[User]
ADD CONSTRAINT [PK_User]
    PRIMARY KEY CLUSTERED ([Id], [RoleId] ASC);
GO

-- Creating primary key on [Id], [CreatedByUserId], [LendItemId], [LendReturnId] in table 'Lend'
ALTER TABLE [dbo].[Lend]
ADD CONSTRAINT [PK_Lend]
    PRIMARY KEY CLUSTERED ([Id], [CreatedByUserId], [LendItemId], [LendReturnId] ASC);
GO

-- Creating primary key on [Id] in table 'LendItem'
ALTER TABLE [dbo].[LendItem]
ADD CONSTRAINT [PK_LendItem]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id], [LendId], [CreatedByUserId] in table 'LendReturn'
ALTER TABLE [dbo].[LendReturn]
ADD CONSTRAINT [PK_LendReturn]
    PRIMARY KEY CLUSTERED ([Id], [LendId], [CreatedByUserId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Users_Id], [Users_RoleId] in table 'Lend'
ALTER TABLE [dbo].[Lend]
ADD CONSTRAINT [FK_LendUsers]
    FOREIGN KEY ([Users_Id], [Users_RoleId])
    REFERENCES [dbo].[User]
        ([Id], [RoleId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LendUsers'
CREATE INDEX [IX_FK_LendUsers]
ON [dbo].[Lend]
    ([Users_Id], [Users_RoleId]);
GO

-- Creating foreign key on [Lend_Id], [Lend_CreatedByUserId], [Lend_LendItemId], [Lend_LendReturnId] in table 'LendItem'
ALTER TABLE [dbo].[LendItem]
ADD CONSTRAINT [FK_LendLendItem]
    FOREIGN KEY ([Lend_Id], [Lend_CreatedByUserId], [Lend_LendItemId], [Lend_LendReturnId])
    REFERENCES [dbo].[Lend]
        ([Id], [CreatedByUserId], [LendItemId], [LendReturnId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LendLendItem'
CREATE INDEX [IX_FK_LendLendItem]
ON [dbo].[LendItem]
    ([Lend_Id], [Lend_CreatedByUserId], [Lend_LendItemId], [Lend_LendReturnId]);
GO

-- Creating foreign key on [LendReturn_Id], [LendReturn_LendId], [LendReturn_CreatedByUserId] in table 'Lend'
ALTER TABLE [dbo].[Lend]
ADD CONSTRAINT [FK_LendLendReturn]
    FOREIGN KEY ([LendReturn_Id], [LendReturn_LendId], [LendReturn_CreatedByUserId])
    REFERENCES [dbo].[LendReturn]
        ([Id], [LendId], [CreatedByUserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LendLendReturn'
CREATE INDEX [IX_FK_LendLendReturn]
ON [dbo].[Lend]
    ([LendReturn_Id], [LendReturn_LendId], [LendReturn_CreatedByUserId]);
GO

-- Creating foreign key on [Users_Id], [Users_RoleId] in table 'LendReturn'
ALTER TABLE [dbo].[LendReturn]
ADD CONSTRAINT [FK_LendReturnAspNetUsers]
    FOREIGN KEY ([Users_Id], [Users_RoleId])
    REFERENCES [dbo].[User]
        ([Id], [RoleId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LendReturnAspNetUsers'
CREATE INDEX [IX_FK_LendReturnAspNetUsers]
ON [dbo].[LendReturn]
    ([Users_Id], [Users_RoleId]);
GO

-- Creating foreign key on [Roles_Id] in table 'User'
ALTER TABLE [dbo].[User]
ADD CONSTRAINT [FK_UsersRoles]
    FOREIGN KEY ([Roles_Id])
    REFERENCES [dbo].[Role]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UsersRoles'
CREATE INDEX [IX_FK_UsersRoles]
ON [dbo].[User]
    ([Roles_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------