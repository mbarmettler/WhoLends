USE [WhoLends]
GO
/****** Object:  Table [dbo].[LendItem]    Script Date: 16.11.2016 14:51:05 ******/
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
	[FileId] [int] NOT NULL,
 CONSTRAINT [PK_LendItem] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[LendItem] ON 

INSERT [dbo].[LendItem] ([Id], [Name], [Description], [CustomerId], [Quantity], [Condition], [CreatedAt], [UserId], [FileId]) VALUES (1, N'Werkzeugkiste normal', N'asdfasdf', N'234234234234', 2, 0, CAST(N'2016-11-11 09:48:40.667' AS DateTime), 1002, 0)
INSERT [dbo].[LendItem] ([Id], [Name], [Description], [CustomerId], [Quantity], [Condition], [CreatedAt], [UserId], [FileId]) VALUES (2, N'Test Gegenstand with image', N'asdfasdf', N'234234234234', 2, 0, CAST(N'2016-11-11 09:51:13.340' AS DateTime), 1002, 2)
INSERT [dbo].[LendItem] ([Id], [Name], [Description], [CustomerId], [Quantity], [Condition], [CreatedAt], [UserId], [FileId]) VALUES (3, N'Werkzeugkiste normal', N'afasdf', N'1232kdk', 2, 0, CAST(N'2016-11-11 11:32:16.147' AS DateTime), 1002, 3)
INSERT [dbo].[LendItem] ([Id], [Name], [Description], [CustomerId], [Quantity], [Condition], [CreatedAt], [UserId], [FileId]) VALUES (4, N'Dienstfahrzeug', N'Audi S4, rot', N'xx3234234234', 1, 0, CAST(N'2016-11-11 14:19:52.907' AS DateTime), 1006, 4)
SET IDENTITY_INSERT [dbo].[LendItem] OFF
/****** Object:  Index [IX_FK_UserLendItem]    Script Date: 16.11.2016 14:51:06 ******/
CREATE NONCLUSTERED INDEX [IX_FK_UserLendItem] ON [dbo].[LendItem]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[LendItem]  WITH CHECK ADD  CONSTRAINT [FK_UserLendItem] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[LendItem] CHECK CONSTRAINT [FK_UserLendItem]
GO
