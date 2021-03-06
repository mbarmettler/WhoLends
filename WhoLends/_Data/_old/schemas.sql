USE [WhoLends]
GO
/****** Object:  Table [dbo].[Lend]    Script Date: 19.10.2016 16:20:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lend](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[From] [datetime] NOT NULL,
	[To] [datetime] NULL,
	[CreatedAt] [datetime] NOT NULL,
	[LendItemId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[LenderUserId] [int] NOT NULL,
 CONSTRAINT [PK_Lend] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LendItem]    Script Date: 19.10.2016 16:20:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LendItem](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[CustomerId] [nvarchar](max) NOT NULL,
	[Quantity] [smallint] NOT NULL,
	[Condition] [int] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_LendItem] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Role]    Script Date: 19.10.2016 16:20:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[User]    Script Date: 19.10.2016 16:20:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
	[RoleId] [int] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[Lend]  WITH CHECK ADD  CONSTRAINT [FK_LendItemLend] FOREIGN KEY([LendItemId])
REFERENCES [dbo].[LendItem] ([Id])
GO
ALTER TABLE [dbo].[Lend] CHECK CONSTRAINT [FK_LendItemLend]
GO
ALTER TABLE [dbo].[Lend]  WITH CHECK ADD  CONSTRAINT [FK_LendUser] FOREIGN KEY([LenderUserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Lend] CHECK CONSTRAINT [FK_LendUser]
GO
ALTER TABLE [dbo].[Lend]  WITH CHECK ADD  CONSTRAINT [FK_UserLend] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Lend] CHECK CONSTRAINT [FK_UserLend]
GO
ALTER TABLE [dbo].[LendItem]  WITH CHECK ADD  CONSTRAINT [FK_UserLendItem] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[LendItem] CHECK CONSTRAINT [FK_UserLendItem]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_RoleUser] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_RoleUser]
GO
