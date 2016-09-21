USE [WhoLends]
GO
/****** Object:  Table [dbo].[Lend]    Script Date: 21.09.2016 09:54:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lend](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LendItemId] [int] NOT NULL,
	[CreatedByUserId] [nvarchar](max) NOT NULL,
	[LendReturnId] [nvarchar](max) NOT NULL,
	[From] [datetime] NOT NULL,
	[To] [datetime] NULL,
	[CreatedAt] [datetime] NOT NULL,
	[LendReturn_Id] [int] NULL,
	[LendReturn_LendId] [nvarchar](max) NULL,
	[LendReturn_CreatedByUserId] [nvarchar](max) NULL,
	[User_Id] [int] NOT NULL,
	[LendItem_Id] [int] NOT NULL,
	[LendItem_CreatedByUserId] [int] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LendItem]    Script Date: 21.09.2016 09:54:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LendItem](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedByUserId] [int] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[CustomerId] [nvarchar](max) NOT NULL,
	[Quantity] [smallint] NOT NULL,
	[Condition] [int] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
 CONSTRAINT [PK_LendItem] PRIMARY KEY CLUSTERED 
(
	[Id] ASC,
	[CreatedByUserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LendReturn]    Script Date: 21.09.2016 09:54:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LendReturn](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LendId] [nvarchar](max) NOT NULL,
	[CreatedByUserId] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[User_Id] [int] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Role]    Script Date: 21.09.2016 09:54:00 ******/
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
/****** Object:  Table [dbo].[User]    Script Date: 21.09.2016 09:54:00 ******/
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
	[Role_Id] [int] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[LendItem] ON 

INSERT [dbo].[LendItem] ([Id], [CreatedByUserId], [Name], [Description], [CustomerId], [Quantity], [Condition], [CreatedAt]) VALUES (1, 1, N'Werkzeugkiste normal', N'werkzeugkasten verschiedene sachen drin', N'asdfasdf+', 5, 0, CAST(N'2016-09-13 17:22:40.947' AS DateTime))
SET IDENTITY_INSERT [dbo].[LendItem] OFF
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([Id], [Name]) VALUES (1, N'admin')
SET IDENTITY_INSERT [dbo].[Role] OFF
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([Id], [Email], [EmailConfirmed], [PasswordHash], [LockoutEndDateUtc], [LockoutEnabled], [UserName], [Role_Id]) VALUES (1, N'greenlion1010@gmail.com', 1, N'hash', CAST(N'2016-02-09 15:36:27.430' AS DateTime), 0, N'Mike', 1)
SET IDENTITY_INSERT [dbo].[User] OFF
ALTER TABLE [dbo].[Lend]  WITH CHECK ADD  CONSTRAINT [FK_LendItemLend] FOREIGN KEY([LendItem_Id], [LendItem_CreatedByUserId])
REFERENCES [dbo].[LendItem] ([Id], [CreatedByUserId])
GO
ALTER TABLE [dbo].[Lend] CHECK CONSTRAINT [FK_LendItemLend]
GO
ALTER TABLE [dbo].[Lend]  WITH CHECK ADD  CONSTRAINT [FK_UserLend] FOREIGN KEY([User_Id])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Lend] CHECK CONSTRAINT [FK_UserLend]
GO
ALTER TABLE [dbo].[LendItem]  WITH CHECK ADD  CONSTRAINT [FK_UserLendItem] FOREIGN KEY([CreatedByUserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[LendItem] CHECK CONSTRAINT [FK_UserLendItem]
GO
ALTER TABLE [dbo].[LendReturn]  WITH CHECK ADD  CONSTRAINT [FK_UserLendReturn] FOREIGN KEY([User_Id])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[LendReturn] CHECK CONSTRAINT [FK_UserLendReturn]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_RoleUser] FOREIGN KEY([Role_Id])
REFERENCES [dbo].[Role] ([Id])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_RoleUser]
GO
