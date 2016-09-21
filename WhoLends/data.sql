USE [WhoLends]
GO
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([Id], [Name]) VALUES (1, N'admin')
SET IDENTITY_INSERT [dbo].[Role] OFF
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([Id], [Email], [EmailConfirmed], [PasswordHash], [LockoutEndDateUtc], [LockoutEnabled], [UserName], [RoleId]) VALUES (1, N'greenlion1010@gmail.com', 1, N'<hash>', CAST(N'2016-09-13 17:22:40.947' AS DateTime), 0, N'Mike', 1)
SET IDENTITY_INSERT [dbo].[User] OFF
SET IDENTITY_INSERT [dbo].[LendItem] ON 

INSERT [dbo].[LendItem] ([Id], [Name], [Description], [CustomerId], [Quantity], [Condition], [CreatedAt], [UserId]) VALUES (1, N'Werkzeugkiste normal', N'werkzeugkasten verschiedene sachen drin', N'asdfasdf3333', 10, 0, CAST(N'2016-09-21 11:18:26.923' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[LendItem] OFF
SET IDENTITY_INSERT [dbo].[Lend] ON 

INSERT [dbo].[Lend] ([Id], [From], [To], [CreatedAt], [LendItemId], [UserId]) VALUES (3, CAST(N'1999-01-01 00:00:00.000' AS DateTime), CAST(N'1999-01-02 00:00:00.000' AS DateTime), CAST(N'2016-09-21 11:31:18.750' AS DateTime), 1, 1)
SET IDENTITY_INSERT [dbo].[Lend] OFF
