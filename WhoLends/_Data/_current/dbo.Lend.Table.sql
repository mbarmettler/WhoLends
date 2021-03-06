USE [WhoLends]
GO
/****** Object:  Table [dbo].[Lend]    Script Date: 01.12.2016 17:56:43 ******/
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
	[LendReturnId] [int] NULL,
 CONSTRAINT [PK_Lend] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Lend] ON 

INSERT [dbo].[Lend] ([Id], [From], [To], [CreatedAt], [LendItemId], [UserId], [LenderUserId], [LendReturnId]) VALUES (6, CAST(N'2016-12-01 17:46:00.000' AS DateTime), NULL, CAST(N'2016-12-01 17:46:56.717' AS DateTime), 1, 1006, 1002, NULL)
INSERT [dbo].[Lend] ([Id], [From], [To], [CreatedAt], [LendItemId], [UserId], [LenderUserId], [LendReturnId]) VALUES (7, CAST(N'2016-12-01 17:48:00.000' AS DateTime), NULL, CAST(N'2016-12-01 17:48:45.533' AS DateTime), 1, 1006, 1002, NULL)
SET IDENTITY_INSERT [dbo].[Lend] OFF
ALTER TABLE [dbo].[Lend]  WITH CHECK ADD  CONSTRAINT [FK_LenderUser] FOREIGN KEY([LenderUserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Lend] CHECK CONSTRAINT [FK_LenderUser]
GO
ALTER TABLE [dbo].[Lend]  WITH CHECK ADD  CONSTRAINT [FK_LendItemLend] FOREIGN KEY([LendItemId])
REFERENCES [dbo].[LendItem] ([Id])
GO
ALTER TABLE [dbo].[Lend] CHECK CONSTRAINT [FK_LendItemLend]
GO
ALTER TABLE [dbo].[Lend]  WITH CHECK ADD  CONSTRAINT [FK_LendReturn] FOREIGN KEY([LendReturnId])
REFERENCES [dbo].[LendReturn] ([Id])
GO
ALTER TABLE [dbo].[Lend] CHECK CONSTRAINT [FK_LendReturn]
GO
ALTER TABLE [dbo].[Lend]  WITH CHECK ADD  CONSTRAINT [FK_UserLend] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Lend] CHECK CONSTRAINT [FK_UserLend]
GO
