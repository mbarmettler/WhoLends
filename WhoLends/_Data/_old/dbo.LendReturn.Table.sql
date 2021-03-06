USE [WhoLends]
GO
/****** Object:  Table [dbo].[LendReturn]    Script Date: 16.11.2016 14:51:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LendReturn](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LendId] [int] NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[UserId] [int] NOT NULL,
	[SetComplete] [bit] NOT NULL,
	[FileId] [int] NOT NULL,
 CONSTRAINT [PK_LendReturn] PRIMARY KEY CLUSTERED 
(
	[Id] ASC,
	[LendId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[LendReturn] ON 

INSERT [dbo].[LendReturn] ([Id], [LendId], [Description], [CreatedAt], [UserId], [SetComplete], [FileId]) VALUES (1002, 2003, N'Return Description blablablablablablaba', CAST(N'2016-11-16 13:31:21.000' AS DateTime), 1006, 1, 0)
SET IDENTITY_INSERT [dbo].[LendReturn] OFF
/****** Object:  Index [IX_FK_UserLendReturn]    Script Date: 16.11.2016 14:51:06 ******/
CREATE NONCLUSTERED INDEX [IX_FK_UserLendReturn] ON [dbo].[LendReturn]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[LendReturn]  WITH CHECK ADD  CONSTRAINT [FK_UserLendReturn] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[LendReturn] CHECK CONSTRAINT [FK_UserLendReturn]
GO
