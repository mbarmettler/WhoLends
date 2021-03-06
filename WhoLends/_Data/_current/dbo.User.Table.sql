USE [WhoLends]
GO
/****** Object:  Table [dbo].[User]    Script Date: 01.12.2016 17:56:43 ******/
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

INSERT [dbo].[User] ([Id], [Email], [EmailConfirmed], [PasswordHash], [LockoutEndDateUtc], [LockoutEnabled], [UserName], [RoleId]) VALUES (1002, N'test7@gmail.com', 0, N'AGeRdL2lWXk29ggbE10rYI8rzDRU4HuWLCvqsF0muB2KRcmQo0YvxbSKISzaAEI/tQ==', NULL, 0, N'TestUser', 2)
INSERT [dbo].[User] ([Id], [Email], [EmailConfirmed], [PasswordHash], [LockoutEndDateUtc], [LockoutEnabled], [UserName], [RoleId]) VALUES (1003, N'test8@gmail.com', 0, N'AISptQU+QDdDERLWrXOZNJ7mYVB6g59z13i5if04nIAUxlriBiEBM80X+3hpf+6dCA==', NULL, 0, N'TestUser8', 2)
INSERT [dbo].[User] ([Id], [Email], [EmailConfirmed], [PasswordHash], [LockoutEndDateUtc], [LockoutEnabled], [UserName], [RoleId]) VALUES (1006, N'greenlion1010@gmail.com', 1, N'AJW05uf0url0abwCkXM+YWUeOi3ZInKhrxoWnMTqHux1sjhJI4ZXwvrDU1i6iIlmLA==', NULL, 0, N'Mike', 1)
INSERT [dbo].[User] ([Id], [Email], [EmailConfirmed], [PasswordHash], [LockoutEndDateUtc], [LockoutEnabled], [UserName], [RoleId]) VALUES (1007, N'mike.barmettler@chainiq.com', 0, N'AEvvMynRayGIIPtE40K/RWEZd7PgWzT6qyn3KihqWnwth1Gvay/nfWvlBdthyVXEAg==', NULL, 0, N'Tester', 2)
SET IDENTITY_INSERT [dbo].[User] OFF
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_Role]
GO
