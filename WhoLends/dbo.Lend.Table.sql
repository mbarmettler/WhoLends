USE [WhoLends]
GO
/****** Object:  Table [dbo].[Lend]    Script Date: 19.10.2016 16:30:39 ******/
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
SET IDENTITY_INSERT [dbo].[Lend] ON 

INSERT [dbo].[Lend] ([Id], [From], [To], [CreatedAt], [LendItemId], [UserId], [LenderUserId]) VALUES (1005, CAST(N'2016-09-13 17:22:40.947' AS DateTime), CAST(N'2016-09-13 17:22:40.947' AS DateTime), CAST(N'2016-09-13 17:22:40.947' AS DateTime), 2, 2, 2)
INSERT [dbo].[Lend] ([Id], [From], [To], [CreatedAt], [LendItemId], [UserId], [LenderUserId]) VALUES (2007, CAST(N'1999-01-01 00:00:00.000' AS DateTime), NULL, CAST(N'2016-10-06 18:07:46.037' AS DateTime), 1001, 2, 3)
INSERT [dbo].[Lend] ([Id], [From], [To], [CreatedAt], [LendItemId], [UserId], [LenderUserId]) VALUES (3003, CAST(N'2001-01-01 00:00:00.000' AS DateTime), NULL, CAST(N'2016-10-19 08:57:30.943' AS DateTime), 1001, 1002, 1003)
SET IDENTITY_INSERT [dbo].[Lend] OFF
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
