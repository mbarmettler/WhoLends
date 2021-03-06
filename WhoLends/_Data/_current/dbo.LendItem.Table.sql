USE [WhoLends]
GO
/****** Object:  Table [dbo].[LendItem]    Script Date: 01.12.2016 17:56:43 ******/
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
	[Avialable] [smallint] NOT NULL,
	[Condition] [int] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[UserId] [int] NOT NULL,
	[FileId] [int] NULL,
 CONSTRAINT [PK_LendItem] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[LendItem] ON 

INSERT [dbo].[LendItem] ([Id], [Name], [Description], [CustomerId], [Quantity], [Avialable], [Condition], [CreatedAt], [UserId], [FileId]) VALUES (1, N'Dienstfahrzeug', N'Audi S3, rot', N'NW 234234', 2, 0, 0, CAST(N'2016-12-01 17:11:57.373' AS DateTime), 1006, 2008)
INSERT [dbo].[LendItem] ([Id], [Name], [Description], [CustomerId], [Quantity], [Avialable], [Condition], [CreatedAt], [UserId], [FileId]) VALUES (2, N'Werkzeugkiste normal', N'1x Hammer
1 x Schraubenzieher
1 x Rohrzange
1 x Schnüffelstück', N'234234234', 10, 10, 0, CAST(N'2016-12-01 17:55:08.623' AS DateTime), 1006, 2009)
SET IDENTITY_INSERT [dbo].[LendItem] OFF
ALTER TABLE [dbo].[LendItem]  WITH CHECK ADD  CONSTRAINT [FK_LendItemFile] FOREIGN KEY([FileId])
REFERENCES [dbo].[File] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[LendItem] CHECK CONSTRAINT [FK_LendItemFile]
GO
ALTER TABLE [dbo].[LendItem]  WITH CHECK ADD  CONSTRAINT [FK_UserLendItem] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[LendItem] CHECK CONSTRAINT [FK_UserLendItem]
GO
