USE [WhoLends]
GO
SET IDENTITY_INSERT [dbo].[LendItem] ON 

INSERT [dbo].[LendItem] ([Id], [Name], [Description], [CustomerId], [Quantity], [Condition], [CreatedAt], [UserId], [FileId]) VALUES (1, N'Werkzeugkiste normal', N'asdfasdf', N'234234234234', 2, 0, CAST(N'2016-11-11 09:48:40.667' AS DateTime), 1002, 0)
INSERT [dbo].[LendItem] ([Id], [Name], [Description], [CustomerId], [Quantity], [Condition], [CreatedAt], [UserId], [FileId]) VALUES (2, N'Test Gegenstand with image', N'asdfasdf', N'234234234234', 2, 0, CAST(N'2016-11-11 09:51:13.340' AS DateTime), 1002, 2)
INSERT [dbo].[LendItem] ([Id], [Name], [Description], [CustomerId], [Quantity], [Condition], [CreatedAt], [UserId], [FileId]) VALUES (3, N'Werkzeugkiste normal', N'afasdf', N'1232kdk', 2, 0, CAST(N'2016-11-11 11:32:16.147' AS DateTime), 1002, 3)
INSERT [dbo].[LendItem] ([Id], [Name], [Description], [CustomerId], [Quantity], [Condition], [CreatedAt], [UserId], [FileId]) VALUES (4, N'Dienstfahrzeug', N'Audi S4, rot', N'xx3234234234', 1, 0, CAST(N'2016-11-11 14:19:52.907' AS DateTime), 1006, 4)
SET IDENTITY_INSERT [dbo].[LendItem] OFF
