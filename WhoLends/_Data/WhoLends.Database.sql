USE [master]
GO
/****** Object:  Database [WhoLends]    Script Date: 16.11.2016 14:51:04 ******/
CREATE DATABASE [WhoLends]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'WhoLends', FILENAME = N'C:\Users\mike.barmettler\WhoLends.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'WhoLends_log', FILENAME = N'C:\Users\mike.barmettler\WhoLends_log.ldf' , SIZE = 1280KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [WhoLends] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [WhoLends].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [WhoLends] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [WhoLends] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [WhoLends] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [WhoLends] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [WhoLends] SET ARITHABORT OFF 
GO
ALTER DATABASE [WhoLends] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [WhoLends] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [WhoLends] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [WhoLends] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [WhoLends] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [WhoLends] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [WhoLends] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [WhoLends] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [WhoLends] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [WhoLends] SET  DISABLE_BROKER 
GO
ALTER DATABASE [WhoLends] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [WhoLends] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [WhoLends] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [WhoLends] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [WhoLends] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [WhoLends] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [WhoLends] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [WhoLends] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [WhoLends] SET  MULTI_USER 
GO
ALTER DATABASE [WhoLends] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [WhoLends] SET DB_CHAINING OFF 
GO
ALTER DATABASE [WhoLends] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [WhoLends] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [WhoLends] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [WhoLends] SET  READ_WRITE 
GO
