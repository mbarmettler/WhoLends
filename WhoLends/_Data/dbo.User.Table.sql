USE [WhoLends]
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([Id], [Email], [EmailConfirmed], [PasswordHash], [LockoutEndDateUtc], [LockoutEnabled], [UserName], [RoleId]) VALUES (1002, N'test7@gmail.com', 0, N'AGeRdL2lWXk29ggbE10rYI8rzDRU4HuWLCvqsF0muB2KRcmQo0YvxbSKISzaAEI/tQ==', NULL, 0, N'TestUser', 2)
INSERT [dbo].[User] ([Id], [Email], [EmailConfirmed], [PasswordHash], [LockoutEndDateUtc], [LockoutEnabled], [UserName], [RoleId]) VALUES (1003, N'test8@gmail.com', 0, N'AISptQU+QDdDERLWrXOZNJ7mYVB6g59z13i5if04nIAUxlriBiEBM80X+3hpf+6dCA==', NULL, 0, N'TestUser8', 2)
INSERT [dbo].[User] ([Id], [Email], [EmailConfirmed], [PasswordHash], [LockoutEndDateUtc], [LockoutEnabled], [UserName], [RoleId]) VALUES (1006, N'greenlion1010@gmail.com', 1, N'AJW05uf0url0abwCkXM+YWUeOi3ZInKhrxoWnMTqHux1sjhJI4ZXwvrDU1i6iIlmLA==', NULL, 0, N'Mike', 1)
SET IDENTITY_INSERT [dbo].[User] OFF
