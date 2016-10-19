USE [WhoLends]
GO
/****** Object:  Table [dbo].[LendItem]    Script Date: 19.10.2016 16:30:39 ******/
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
SET IDENTITY_INSERT [dbo].[LendItem] ON 

INSERT [dbo].[LendItem] ([Id], [Name], [Description], [CustomerId], [Quantity], [Condition], [CreatedAt], [UserId]) VALUES (2, N'Test Gegenstand', N'asdfasdf', N'asdfasdf234234', 1, 0, CAST(N'2016-09-21 16:04:12.367' AS DateTime), 2)
INSERT [dbo].[LendItem] ([Id], [Name], [Description], [CustomerId], [Quantity], [Condition], [CreatedAt], [UserId]) VALUES (1001, N'Werkzeugkiste normal', N'werkzeugkasten verschiedene sachen drin', N'asdjflasdfjjalsdf2323234', 10, 0, CAST(N'2016-10-06 17:58:03.277' AS DateTime), 3)
INSERT [dbo].[LendItem] ([Id], [Name], [Description], [CustomerId], [Quantity], [Condition], [CreatedAt], [UserId]) VALUES (2001, N'Dienstfahrzeug', N'Ford Fiesta,
blau,
NW xxxxx', N'1234123123123', 1, 1, CAST(N'2016-10-19 09:41:59.760' AS DateTime), 1002)
SET IDENTITY_INSERT [dbo].[LendItem] OFF
ALTER TABLE [dbo].[LendItem]  WITH CHECK ADD  CONSTRAINT [FK_UserLendItem] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[LendItem] CHECK CONSTRAINT [FK_UserLendItem]
GO
