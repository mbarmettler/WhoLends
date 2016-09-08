
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 09/07/2016 14:55:06
-- Generated from EDMX file: D:\PRV\WhoLends\WhoLends\WhoLends.Data\WLModel.edmx
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

IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserClaims] DROP CONSTRAINT [FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserLogins] DROP CONSTRAINT [FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId];
GO
IF OBJECT_ID(N'[dbo].[FK_AspNetUserRoles_AspNetRoles]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_AspNetUserRoles_AspNetRoles];
GO
IF OBJECT_ID(N'[dbo].[FK_AspNetUserRoles_AspNetUsers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_AspNetUserRoles_AspNetUsers];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[C__MigrationHistory]', 'U') IS NOT NULL
    DROP TABLE [dbo].[C__MigrationHistory];
GO
IF OBJECT_ID(N'[dbo].[AspNetRoles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetRoles];
GO
IF OBJECT_ID(N'[dbo].[AspNetUserClaims]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUserClaims];
GO
IF OBJECT_ID(N'[dbo].[AspNetUserLogins]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUserLogins];
GO
IF OBJECT_ID(N'[dbo].[AspNetUsers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUsers];
GO
IF OBJECT_ID(N'[dbo].[LendSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LendSet];
GO
IF OBJECT_ID(N'[dbo].[LendItemSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LendItemSet];
GO
IF OBJECT_ID(N'[dbo].[AspNetUserRoles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUserRoles];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'C__MigrationHistory'
CREATE TABLE [dbo].[C__MigrationHistory] (
    [MigrationId] nvarchar(150)  NOT NULL,
    [ContextKey] nvarchar(300)  NOT NULL,
    [Model] varbinary(max)  NOT NULL,
    [ProductVersion] nvarchar(32)  NOT NULL
);
GO

-- Creating table 'AspNetRoles'
CREATE TABLE [dbo].[AspNetRoles] (
    [Id] nvarchar(128)  NOT NULL,
    [Name] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'AspNetUserClaims'
CREATE TABLE [dbo].[AspNetUserClaims] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserId] nvarchar(128)  NOT NULL,
    [ClaimType] nvarchar(max)  NULL,
    [ClaimValue] nvarchar(max)  NULL
);
GO

-- Creating table 'AspNetUserLogins'
CREATE TABLE [dbo].[AspNetUserLogins] (
    [LoginProvider] nvarchar(128)  NOT NULL,
    [ProviderKey] nvarchar(128)  NOT NULL,
    [UserId] nvarchar(128)  NOT NULL
);
GO

-- Creating table 'AspNetUsers'
CREATE TABLE [dbo].[AspNetUsers] (
    [Id] nvarchar(128)  NOT NULL,
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
    [UserName] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'LendSet'
CREATE TABLE [dbo].[LendSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [LendItemId] nvarchar(max)  NOT NULL,
    [From] datetime  NOT NULL,
    [To] datetime  NOT NULL,
    [LendReturn_Id] int  NOT NULL
);
GO

-- Creating table 'LendItemSet'
CREATE TABLE [dbo].[LendItemSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [Quantity] smallint  NOT NULL,
    [Condition] int  NOT NULL,
    [Lend_Id] int  NOT NULL
);
GO

-- Creating table 'LendReturnSet'
CREATE TABLE [dbo].[LendReturnSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [CreationDate] datetime  NOT NULL
);
GO

-- Creating table 'AspNetUserRoles'
CREATE TABLE [dbo].[AspNetUserRoles] (
    [AspNetRoles_Id] nvarchar(128)  NOT NULL,
    [AspNetUsers_Id] nvarchar(128)  NOT NULL
);
GO

-- Creating table 'LendAspNetUsers'
CREATE TABLE [dbo].[LendAspNetUsers] (
    [Lend_Id] int  NOT NULL,
    [AspNetUsers_Id] nvarchar(128)  NOT NULL
);
GO

-- Creating table 'LendReturnAspNetUsers'
CREATE TABLE [dbo].[LendReturnAspNetUsers] (
    [LendReturn_Id] int  NOT NULL,
    [AspNetUsers_Id] nvarchar(128)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [MigrationId], [ContextKey] in table 'C__MigrationHistory'
ALTER TABLE [dbo].[C__MigrationHistory]
ADD CONSTRAINT [PK_C__MigrationHistory]
    PRIMARY KEY CLUSTERED ([MigrationId], [ContextKey] ASC);
GO

-- Creating primary key on [Id] in table 'AspNetRoles'
ALTER TABLE [dbo].[AspNetRoles]
ADD CONSTRAINT [PK_AspNetRoles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AspNetUserClaims'
ALTER TABLE [dbo].[AspNetUserClaims]
ADD CONSTRAINT [PK_AspNetUserClaims]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [LoginProvider], [ProviderKey], [UserId] in table 'AspNetUserLogins'
ALTER TABLE [dbo].[AspNetUserLogins]
ADD CONSTRAINT [PK_AspNetUserLogins]
    PRIMARY KEY CLUSTERED ([LoginProvider], [ProviderKey], [UserId] ASC);
GO

-- Creating primary key on [Id] in table 'AspNetUsers'
ALTER TABLE [dbo].[AspNetUsers]
ADD CONSTRAINT [PK_AspNetUsers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'LendSet'
ALTER TABLE [dbo].[LendSet]
ADD CONSTRAINT [PK_LendSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'LendItemSet'
ALTER TABLE [dbo].[LendItemSet]
ADD CONSTRAINT [PK_LendItemSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'LendReturnSet'
ALTER TABLE [dbo].[LendReturnSet]
ADD CONSTRAINT [PK_LendReturnSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [AspNetRoles_Id], [AspNetUsers_Id] in table 'AspNetUserRoles'
ALTER TABLE [dbo].[AspNetUserRoles]
ADD CONSTRAINT [PK_AspNetUserRoles]
    PRIMARY KEY CLUSTERED ([AspNetRoles_Id], [AspNetUsers_Id] ASC);
GO

-- Creating primary key on [Lend_Id], [AspNetUsers_Id] in table 'LendAspNetUsers'
ALTER TABLE [dbo].[LendAspNetUsers]
ADD CONSTRAINT [PK_LendAspNetUsers]
    PRIMARY KEY CLUSTERED ([Lend_Id], [AspNetUsers_Id] ASC);
GO

-- Creating primary key on [LendReturn_Id], [AspNetUsers_Id] in table 'LendReturnAspNetUsers'
ALTER TABLE [dbo].[LendReturnAspNetUsers]
ADD CONSTRAINT [PK_LendReturnAspNetUsers]
    PRIMARY KEY CLUSTERED ([LendReturn_Id], [AspNetUsers_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UserId] in table 'AspNetUserClaims'
ALTER TABLE [dbo].[AspNetUserClaims]
ADD CONSTRAINT [FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId'
CREATE INDEX [IX_FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId]
ON [dbo].[AspNetUserClaims]
    ([UserId]);
GO

-- Creating foreign key on [UserId] in table 'AspNetUserLogins'
ALTER TABLE [dbo].[AspNetUserLogins]
ADD CONSTRAINT [FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId'
CREATE INDEX [IX_FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]
ON [dbo].[AspNetUserLogins]
    ([UserId]);
GO

-- Creating foreign key on [AspNetRoles_Id] in table 'AspNetUserRoles'
ALTER TABLE [dbo].[AspNetUserRoles]
ADD CONSTRAINT [FK_AspNetUserRoles_AspNetRoles]
    FOREIGN KEY ([AspNetRoles_Id])
    REFERENCES [dbo].[AspNetRoles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [AspNetUsers_Id] in table 'AspNetUserRoles'
ALTER TABLE [dbo].[AspNetUserRoles]
ADD CONSTRAINT [FK_AspNetUserRoles_AspNetUsers]
    FOREIGN KEY ([AspNetUsers_Id])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AspNetUserRoles_AspNetUsers'
CREATE INDEX [IX_FK_AspNetUserRoles_AspNetUsers]
ON [dbo].[AspNetUserRoles]
    ([AspNetUsers_Id]);
GO

-- Creating foreign key on [Lend_Id] in table 'LendAspNetUsers'
ALTER TABLE [dbo].[LendAspNetUsers]
ADD CONSTRAINT [FK_LendAspNetUsers_Lend]
    FOREIGN KEY ([Lend_Id])
    REFERENCES [dbo].[LendSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [AspNetUsers_Id] in table 'LendAspNetUsers'
ALTER TABLE [dbo].[LendAspNetUsers]
ADD CONSTRAINT [FK_LendAspNetUsers_AspNetUsers]
    FOREIGN KEY ([AspNetUsers_Id])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LendAspNetUsers_AspNetUsers'
CREATE INDEX [IX_FK_LendAspNetUsers_AspNetUsers]
ON [dbo].[LendAspNetUsers]
    ([AspNetUsers_Id]);
GO

-- Creating foreign key on [Lend_Id] in table 'LendItemSet'
ALTER TABLE [dbo].[LendItemSet]
ADD CONSTRAINT [FK_LendLendItem]
    FOREIGN KEY ([Lend_Id])
    REFERENCES [dbo].[LendSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LendLendItem'
CREATE INDEX [IX_FK_LendLendItem]
ON [dbo].[LendItemSet]
    ([Lend_Id]);
GO

-- Creating foreign key on [LendReturn_Id] in table 'LendSet'
ALTER TABLE [dbo].[LendSet]
ADD CONSTRAINT [FK_LendLendReturn]
    FOREIGN KEY ([LendReturn_Id])
    REFERENCES [dbo].[LendReturnSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LendLendReturn'
CREATE INDEX [IX_FK_LendLendReturn]
ON [dbo].[LendSet]
    ([LendReturn_Id]);
GO

-- Creating foreign key on [LendReturn_Id] in table 'LendReturnAspNetUsers'
ALTER TABLE [dbo].[LendReturnAspNetUsers]
ADD CONSTRAINT [FK_LendReturnAspNetUsers_LendReturn]
    FOREIGN KEY ([LendReturn_Id])
    REFERENCES [dbo].[LendReturnSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [AspNetUsers_Id] in table 'LendReturnAspNetUsers'
ALTER TABLE [dbo].[LendReturnAspNetUsers]
ADD CONSTRAINT [FK_LendReturnAspNetUsers_AspNetUsers]
    FOREIGN KEY ([AspNetUsers_Id])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LendReturnAspNetUsers_AspNetUsers'
CREATE INDEX [IX_FK_LendReturnAspNetUsers_AspNetUsers]
ON [dbo].[LendReturnAspNetUsers]
    ([AspNetUsers_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------