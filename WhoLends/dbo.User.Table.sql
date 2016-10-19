USE [WhoLends]
GO
/****** Object:  Table [dbo].[User]    Script Date: 19.10.2016 16:30:39 ******/
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
	[RoleId] [int] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([Id], [Email], [EmailConfirmed], [PasswordHash], [LockoutEndDateUtc], [LockoutEnabled], [UserName], [RoleId]) VALUES (2, N'greenlion1010@gmail.com', 1, N'<hash>', CAST(N'2016-09-13 17:22:40.947' AS DateTime), 0, N'Mike', 1)
INSERT [dbo].[User] ([Id], [Email], [EmailConfirmed], [PasswordHash], [LockoutEndDateUtc], [LockoutEnabled], [UserName], [RoleId]) VALUES (3, N'test@gmail.com', 1, N'<hash>', CAST(N'2016-09-13 17:22:40.947' AS DateTime), 0, N'Adam', 2)
INSERT [dbo].[User] ([Id], [Email], [EmailConfirmed], [PasswordHash], [LockoutEndDateUtc], [LockoutEnabled], [UserName], [RoleId]) VALUES (1002, N'test7@gmail.com', 0, N'AGeRdL2lWXk29ggbE10rYI8rzDRU4HuWLCvqsF0muB2KRcmQo0YvxbSKISzaAEI/tQ==', NULL, 0, N'TestUser', 2)
INSERT [dbo].[User] ([Id], [Email], [EmailConfirmed], [PasswordHash], [LockoutEndDateUtc], [LockoutEnabled], [UserName], [RoleId]) VALUES (1003, N'test8@gmail.com', 0, N'AISptQU+QDdDERLWrXOZNJ7mYVB6g59z13i5if04nIAUxlriBiEBM80X+3hpf+6dCA==', NULL, 0, N'TestUser8', 2)
SET IDENTITY_INSERT [dbo].[User] OFF
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_RoleUser] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_RoleUser]
GO
