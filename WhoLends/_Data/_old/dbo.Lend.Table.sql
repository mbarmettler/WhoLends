USE [WhoLends]
GO
/****** Object:  Table [dbo].[Lend]    Script Date: 16.11.2016 14:51:05 ******/
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
	[LRId] [int] NULL,
 CONSTRAINT [PK_Lend] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Lend] ON 

INSERT [dbo].[Lend] ([Id], [From], [To], [CreatedAt], [LendItemId], [UserId], [LenderUserId], [LRId]) VALUES (2002, CAST(N'2016-11-15 15:48:00.000' AS DateTime), CAST(N'2016-11-15 15:48:00.000' AS DateTime), CAST(N'2016-11-15 15:48:47.367' AS DateTime), 1, 1006, 1002, NULL)
INSERT [dbo].[Lend] ([Id], [From], [To], [CreatedAt], [LendItemId], [UserId], [LenderUserId], [LRId]) VALUES (2003, CAST(N'2016-11-15 15:56:00.000' AS DateTime), CAST(N'2016-11-16 13:32:03.140' AS DateTime), CAST(N'2016-11-15 15:56:32.080' AS DateTime), 4, 1006, 1002, 1002)
SET IDENTITY_INSERT [dbo].[Lend] OFF
/****** Object:  Index [IX_FK_LendItemLend]    Script Date: 16.11.2016 14:51:06 ******/
CREATE NONCLUSTERED INDEX [IX_FK_LendItemLend] ON [dbo].[Lend]
(
	[LendItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FK_LendLendReturn]    Script Date: 16.11.2016 14:51:06 ******/
CREATE NONCLUSTERED INDEX [IX_FK_LendLendReturn] ON [dbo].[Lend]
(
	[LRId] ASC,
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FK_LendUser]    Script Date: 16.11.2016 14:51:06 ******/
CREATE NONCLUSTERED INDEX [IX_FK_LendUser] ON [dbo].[Lend]
(
	[LenderUserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FK_UserLend]    Script Date: 16.11.2016 14:51:06 ******/
CREATE NONCLUSTERED INDEX [IX_FK_UserLend] ON [dbo].[Lend]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Lend]  WITH CHECK ADD  CONSTRAINT [FK_LendItemLend] FOREIGN KEY([LendItemId])
REFERENCES [dbo].[LendItem] ([Id])
GO
ALTER TABLE [dbo].[Lend] CHECK CONSTRAINT [FK_LendItemLend]
GO
ALTER TABLE [dbo].[Lend]  WITH CHECK ADD  CONSTRAINT [FK_LendLendReturn] FOREIGN KEY([LRId], [Id])
REFERENCES [dbo].[LendReturn] ([Id], [LendId])
GO
ALTER TABLE [dbo].[Lend] CHECK CONSTRAINT [FK_LendLendReturn]
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
