USE [WhoLends]
GO
/****** Object:  Table [dbo].[LendReturn]    Script Date: 13.12.2016 18:39:58 ******/
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
	[SetComplete] [bit] NULL,
	[FileId] [int] NULL,
 CONSTRAINT [PK_LendReturn] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[LendReturn] ON 

INSERT [dbo].[LendReturn] ([Id], [LendId], [Description], [CreatedAt], [UserId], [SetComplete], [FileId]) VALUES (2007, 6, N'totalschaden !', CAST(N'2016-12-01 17:56:59.423' AS DateTime), 1006, 0, 10)
INSERT [dbo].[LendReturn] ([Id], [LendId], [Description], [CreatedAt], [UserId], [SetComplete], [FileId]) VALUES (3007, 7, N'asdfasdfasdfasdf', CAST(N'2016-12-02 14:10:34.477' AS DateTime), 1006, 0, 5)
INSERT [dbo].[LendReturn] ([Id], [LendId], [Description], [CreatedAt], [UserId], [SetComplete], [FileId]) VALUES (3010, 1003, N'kuuuuujjkj', CAST(N'2016-12-02 14:48:50.283' AS DateTime), 1006, 0, 3008)
INSERT [dbo].[LendReturn] ([Id], [LendId], [Description], [CreatedAt], [UserId], [SetComplete], [FileId]) VALUES (3011, 1004, N'iioioijjk', CAST(N'2016-12-02 14:51:07.483' AS DateTime), 1006, 0, NULL)
INSERT [dbo].[LendReturn] ([Id], [LendId], [Description], [CreatedAt], [UserId], [SetComplete], [FileId]) VALUES (3012, 1005, N'eswfwerfasdf', CAST(N'2016-12-02 14:59:41.723' AS DateTime), 1006, 0, NULL)
SET IDENTITY_INSERT [dbo].[LendReturn] OFF
ALTER TABLE [dbo].[LendReturn]  WITH CHECK ADD  CONSTRAINT [FK_LendReturnFile] FOREIGN KEY([FileId])
REFERENCES [dbo].[File] ([Id])
GO
ALTER TABLE [dbo].[LendReturn] CHECK CONSTRAINT [FK_LendReturnFile]
GO
ALTER TABLE [dbo].[LendReturn]  WITH CHECK ADD  CONSTRAINT [FK_LendReturnUser] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[LendReturn] CHECK CONSTRAINT [FK_LendReturnUser]
GO
