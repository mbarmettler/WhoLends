
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/02/2016 09:19:58
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

IF OBJECT_ID(N'[dbo].[FK_LendItemLend]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Lend] DROP CONSTRAINT [FK_LendItemLend];
GO
IF OBJECT_ID(N'[dbo].[FK_UserLendItem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[LendItem] DROP CONSTRAINT [FK_UserLendItem];
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
IF OBJECT_ID(N'[dbo].[FK_LendUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Lend] DROP CONSTRAINT [FK_LendUser];
GO
IF OBJECT_ID(N'[dbo].[FK_LendLR]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Lend] DROP CONSTRAINT [FK_LendLR];
GO
IF OBJECT_ID(N'[dbo].[FK_LendReturnLR]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[LendReturn] DROP CONSTRAINT [FK_LendReturnLR];
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
IF OBJECT_ID(N'[dbo].[LRSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LRSet];
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
    [RoleId] int  NOT NULL
);
GO

-- Creating table 'Lend'
CREATE TABLE [dbo].[Lend] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [From] datetime  NOT NULL,
    [To] datetime  NULL,
    [CreatedAt] datetime  NOT NULL,
    [LendItemId] int  NOT NULL,
    [UserId] int  NOT NULL,
    [LenderUserId] int  NOT NULL,
    [LRId] int  NULL
);
GO

-- Creating table 'LendItem'
CREATE TABLE [dbo].[LendItem] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [CustomerId] nvarchar(max)  NOT NULL,
    [Quantity] smallint  NOT NULL,
    [Condition] int  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [UserId] int  NOT NULL
);
GO

-- Creating table 'LendReturn'
CREATE TABLE [dbo].[LendReturn] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [LendId] int  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [UserId] int  NOT NULL,
    [LRId] int  NULL,
    [SetComplete] bit  NOT NULL
);
GO

-- Creating table 'LRSet'
CREATE TABLE [dbo].[LRSet] (
    [Id] int IDENTITY(1,1) NOT NULL
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

-- Creating primary key on [Id] in table 'Lend'
ALTER TABLE [dbo].[Lend]
ADD CONSTRAINT [PK_Lend]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'LendItem'
ALTER TABLE [dbo].[LendItem]
ADD CONSTRAINT [PK_LendItem]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id], [LendId] in table 'LendReturn'
ALTER TABLE [dbo].[LendReturn]
ADD CONSTRAINT [PK_LendReturn]
    PRIMARY KEY CLUSTERED ([Id], [LendId] ASC);
GO

-- Creating primary key on [Id] in table 'LRSet'
ALTER TABLE [dbo].[LRSet]
ADD CONSTRAINT [PK_LRSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [LendItemId] in table 'Lend'
ALTER TABLE [dbo].[Lend]
ADD CONSTRAINT [FK_LendItemLend]
    FOREIGN KEY ([LendItemId])
    REFERENCES [dbo].[LendItem]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LendItemLend'
CREATE INDEX [IX_FK_LendItemLend]
ON [dbo].[Lend]
    ([LendItemId]);
GO

-- Creating foreign key on [UserId] in table 'LendItem'
ALTER TABLE [dbo].[LendItem]
ADD CONSTRAINT [FK_UserLendItem]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[User]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserLendItem'
CREATE INDEX [IX_FK_UserLendItem]
ON [dbo].[LendItem]
    ([UserId]);
GO

-- Creating foreign key on [UserId] in table 'LendReturn'
ALTER TABLE [dbo].[LendReturn]
ADD CONSTRAINT [FK_UserLendReturn]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[User]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserLendReturn'
CREATE INDEX [IX_FK_UserLendReturn]
ON [dbo].[LendReturn]
    ([UserId]);
GO

-- Creating foreign key on [UserId] in table 'Lend'
ALTER TABLE [dbo].[Lend]
ADD CONSTRAINT [FK_UserLend]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[User]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserLend'
CREATE INDEX [IX_FK_UserLend]
ON [dbo].[Lend]
    ([UserId]);
GO

-- Creating foreign key on [RoleId] in table 'User'
ALTER TABLE [dbo].[User]
ADD CONSTRAINT [FK_RoleUser]
    FOREIGN KEY ([RoleId])
    REFERENCES [dbo].[Role]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RoleUser'
CREATE INDEX [IX_FK_RoleUser]
ON [dbo].[User]
    ([RoleId]);
GO

-- Creating foreign key on [LenderUserId] in table 'Lend'
ALTER TABLE [dbo].[Lend]
ADD CONSTRAINT [FK_LendUser]
    FOREIGN KEY ([LenderUserId])
    REFERENCES [dbo].[User]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LendUser'
CREATE INDEX [IX_FK_LendUser]
ON [dbo].[Lend]
    ([LenderUserId]);
GO

-- Creating foreign key on [LRId] in table 'Lend'
ALTER TABLE [dbo].[Lend]
ADD CONSTRAINT [FK_LendLR]
    FOREIGN KEY ([LRId])
    REFERENCES [dbo].[LRSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LendLR'
CREATE INDEX [IX_FK_LendLR]
ON [dbo].[Lend]
    ([LRId]);
GO

-- Creating foreign key on [LRId] in table 'LendReturn'
ALTER TABLE [dbo].[LendReturn]
ADD CONSTRAINT [FK_LendReturnLR]
    FOREIGN KEY ([LRId])
    REFERENCES [dbo].[LRSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LendReturnLR'
CREATE INDEX [IX_FK_LendReturnLR]
ON [dbo].[LendReturn]
    ([LRId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------