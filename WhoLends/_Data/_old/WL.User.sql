USE [WhoLends]
GO
/****** Object:  User [WL]    Script Date: 16.11.2016 14:51:04 ******/
CREATE USER [WL] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [WL]
GO
