USE [master]
GO

/****** Object:  Database [IEPProjekatPS120511_db]    Script Date: 6/26/2015 9:23:22 AM ******/
CREATE DATABASE [IEPProjekatPS120511_db]
GO

ALTER DATABASE [IEPProjekatPS120511_db] SET COMPATIBILITY_LEVEL = 100
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [IEPProjekatPS120511_db].[dbo].[sp_fulltext_database] @action = 'disable'
end
GO

ALTER DATABASE [IEPProjekatPS120511_db] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [IEPProjekatPS120511_db] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [IEPProjekatPS120511_db] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [IEPProjekatPS120511_db] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [IEPProjekatPS120511_db] SET ARITHABORT OFF 
GO

ALTER DATABASE [IEPProjekatPS120511_db] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [IEPProjekatPS120511_db] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [IEPProjekatPS120511_db] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [IEPProjekatPS120511_db] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [IEPProjekatPS120511_db] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [IEPProjekatPS120511_db] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [IEPProjekatPS120511_db] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [IEPProjekatPS120511_db] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [IEPProjekatPS120511_db] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [IEPProjekatPS120511_db] SET  DISABLE_BROKER 
GO

ALTER DATABASE [IEPProjekatPS120511_db] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [IEPProjekatPS120511_db] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [IEPProjekatPS120511_db] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [IEPProjekatPS120511_db] SET ALLOW_SNAPSHOT_ISOLATION ON 
GO

ALTER DATABASE [IEPProjekatPS120511_db] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [IEPProjekatPS120511_db] SET READ_COMMITTED_SNAPSHOT ON 
GO

ALTER DATABASE [IEPProjekatPS120511_db] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [IEPProjekatPS120511_db] SET RECOVERY FULL 
GO

ALTER DATABASE [IEPProjekatPS120511_db] SET  MULTI_USER 
GO

ALTER DATABASE [IEPProjekatPS120511_db] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [IEPProjekatPS120511_db] SET DB_CHAINING OFF 
GO

ALTER DATABASE [IEPProjekatPS120511_db] SET  READ_WRITE 
GO
