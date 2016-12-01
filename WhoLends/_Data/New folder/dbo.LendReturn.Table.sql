USE [WhoLends]
GO
/****** Object:  Table [dbo].[LendReturn]    Script Date: 01.12.2016 17:56:43 ******/
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
