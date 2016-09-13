
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 09/13/2016 17:21:19
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

IF OBJECT_ID(N'[dbo].[FK_LendReturnLend]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Lend] DROP CONSTRAINT [FK_LendReturnLend];
GO
IF OBJECT_ID(N'[dbo].[FK_UserLendReturn]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[LendReturn] DROP CONSTRAINT [FK_UserLendReturn];
GO
IF OBJECT_ID(N'[dbo].[FK_UserLend]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Lend] DROP CONSTRAINT [FK_UserLend];
GO
IF OBJECT_ID(N'[dbo].[FK_RoleUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[User] DROP CONSTRAINT [FK_RoleUser];
GO
IF OBJECT_ID(N'[dbo].[FK_UserLendItem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[LendItem] DROP CONSTRAINT [FK_UserLendItem];
GO
IF OBJECT_ID(N'[dbo].[FK_LendItemLend]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Lend] DROP CONSTRAINT [FK_LendItemLend];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Role]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Role];
GO
IF OBJECT_ID(N'[dbo].[User]', 'U') IS NOT NULL
    DROP TABLE [dbo].[User];
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
    [Email] nvarchar(256)  NULL,
    [EmailConfirmed] bit  NOT NULL,
    [PasswordHash] nvarchar(max)  NULL,
    [LockoutEndDateUtc] datetime  NULL,
    [LockoutEnabled] bit  NOT NULL,
    [UserName] nvarchar(256)  NOT NULL,
    [Role_Id] int  NOT NULL
);
GO

-- Creating table 'Lend'
CREATE TABLE [dbo].[Lend] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [LendItemId] int  NOT NULL,
    [CreatedByUserId] nvarchar(max)  NOT NULL,
    [LendReturnId] nvarchar(max)  NOT NULL,
    [From] datetime  NOT NULL,
    [To] datetime  NULL,
    [CreatedAt] datetime  NOT NULL,
    [LendReturn_Id] int  NULL,
    [LendReturn_LendId] nvarchar(max)  NULL,
    [LendReturn_CreatedByUserId] nvarchar(max)  NULL,
    [User_Id] int  NOT NULL,
    [LendItem_Id] int  NOT NULL,
    [LendItem_CreatedByUserId] int  NOT NULL
);
GO

-- Creating table 'LendItem'
CREATE TABLE [dbo].[LendItem] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CreatedByUserId] int  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [CustomerId] nvarchar(max)  NOT NULL,
    [Quantity] smallint  NOT NULL,
    [Condition] int  NOT NULL,
    [CreatedAt] datetime  NOT NULL
);
GO

-- Creating table 'LendReturn'
CREATE TABLE [dbo].[LendReturn] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [LendId] nvarchar(max)  NOT NULL,
    [CreatedByUserId] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [User_Id] int  NOT NULL
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

-- Creating primary key on [Id] in table 'User'
ALTER TABLE [dbo].[User]
ADD CONSTRAINT [PK_User]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id], [CreatedByUserId], [LendReturnId], [LendItemId] in table 'Lend'
ALTER TABLE [dbo].[Lend]
ADD CONSTRAINT [PK_Lend]
    PRIMARY KEY CLUSTERED ([Id], [CreatedByUserId], [LendReturnId], [LendItemId] ASC);
GO

-- Creating primary key on [Id], [CreatedByUserId] in table 'LendItem'
ALTER TABLE [dbo].[LendItem]
ADD CONSTRAINT [PK_LendItem]
    PRIMARY KEY CLUSTERED ([Id], [CreatedByUserId] ASC);
GO

-- Creating primary key on [Id], [LendId], [CreatedByUserId] in table 'LendReturn'
ALTER TABLE [dbo].[LendReturn]
ADD CONSTRAINT [PK_LendReturn]
    PRIMARY KEY CLUSTERED ([Id], [LendId], [CreatedByUserId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [LendReturn_Id], [LendReturn_LendId], [LendReturn_CreatedByUserId] in table 'Lend'
ALTER TABLE [dbo].[Lend]
ADD CONSTRAINT [FK_LendReturnLend]
    FOREIGN KEY ([LendReturn_Id], [LendReturn_LendId], [LendReturn_CreatedByUserId])
    REFERENCES [dbo].[LendReturn]
        ([Id], [LendId], [CreatedByUserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LendReturnLend'
CREATE INDEX [IX_FK_LendReturnLend]
ON [dbo].[Lend]
    ([LendReturn_Id], [LendReturn_LendId], [LendReturn_CreatedByUserId]);
GO

-- Creating foreign key on [User_Id] in table 'LendReturn'
ALTER TABLE [dbo].[LendReturn]
ADD CONSTRAINT [FK_UserLendReturn]
    FOREIGN KEY ([User_Id])
    REFERENCES [dbo].[User]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserLendReturn'
CREATE INDEX [IX_FK_UserLendReturn]
ON [dbo].[LendReturn]
    ([User_Id]);
GO

-- Creating foreign key on [User_Id] in table 'Lend'
ALTER TABLE [dbo].[Lend]
ADD CONSTRAINT [FK_UserLend]
    FOREIGN KEY ([User_Id])
    REFERENCES [dbo].[User]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserLend'
CREATE INDEX [IX_FK_UserLend]
ON [dbo].[Lend]
    ([User_Id]);
GO

-- Creating foreign key on [Role_Id] in table 'User'
ALTER TABLE [dbo].[User]
ADD CONSTRAINT [FK_RoleUser]
    FOREIGN KEY ([Role_Id])
    REFERENCES [dbo].[Role]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RoleUser'
CREATE INDEX [IX_FK_RoleUser]
ON [dbo].[User]
    ([Role_Id]);
GO

-- Creating foreign key on [CreatedByUserId] in table 'LendItem'
ALTER TABLE [dbo].[LendItem]
ADD CONSTRAINT [FK_UserLendItem]
    FOREIGN KEY ([CreatedByUserId])
    REFERENCES [dbo].[User]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserLendItem'
CREATE INDEX [IX_FK_UserLendItem]
ON [dbo].[LendItem]
    ([CreatedByUserId]);
GO

-- Creating foreign key on [LendItem_Id], [LendItem_CreatedByUserId] in table 'Lend'
ALTER TABLE [dbo].[Lend]
ADD CONSTRAINT [FK_LendItemLend]
    FOREIGN KEY ([LendItem_Id], [LendItem_CreatedByUserId])
    REFERENCES [dbo].[LendItem]
        ([Id], [CreatedByUserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LendItemLend'
CREATE INDEX [IX_FK_LendItemLend]
ON [dbo].[Lend]
    ([LendItem_Id], [LendItem_CreatedByUserId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------