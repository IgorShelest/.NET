USE [master]
GO
/****** Object:  Database [WebAPIApplicationDataBase]    Script Date: 21.02.2020 14:41:07 ******/
CREATE DATABASE [WebAPIApplicationDataBase]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'WebAPIApplicationDataBase', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER01\MSSQL\DATA\WebAPIApplicationDataBase.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'WebAPIApplicationDataBase_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER01\MSSQL\DATA\WebAPIApplicationDataBase_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [WebAPIApplicationDataBase] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [WebAPIApplicationDataBase].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [WebAPIApplicationDataBase] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [WebAPIApplicationDataBase] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [WebAPIApplicationDataBase] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [WebAPIApplicationDataBase] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [WebAPIApplicationDataBase] SET ARITHABORT OFF 
GO
ALTER DATABASE [WebAPIApplicationDataBase] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [WebAPIApplicationDataBase] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [WebAPIApplicationDataBase] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [WebAPIApplicationDataBase] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [WebAPIApplicationDataBase] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [WebAPIApplicationDataBase] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [WebAPIApplicationDataBase] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [WebAPIApplicationDataBase] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [WebAPIApplicationDataBase] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [WebAPIApplicationDataBase] SET  ENABLE_BROKER 
GO
ALTER DATABASE [WebAPIApplicationDataBase] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [WebAPIApplicationDataBase] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [WebAPIApplicationDataBase] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [WebAPIApplicationDataBase] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [WebAPIApplicationDataBase] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [WebAPIApplicationDataBase] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [WebAPIApplicationDataBase] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [WebAPIApplicationDataBase] SET RECOVERY FULL 
GO
ALTER DATABASE [WebAPIApplicationDataBase] SET  MULTI_USER 
GO
ALTER DATABASE [WebAPIApplicationDataBase] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [WebAPIApplicationDataBase] SET DB_CHAINING OFF 
GO
ALTER DATABASE [WebAPIApplicationDataBase] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [WebAPIApplicationDataBase] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [WebAPIApplicationDataBase] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'WebAPIApplicationDataBase', N'ON'
GO
ALTER DATABASE [WebAPIApplicationDataBase] SET QUERY_STORE = OFF
GO
USE [WebAPIApplicationDataBase]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 21.02.2020 14:41:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomerModels]    Script Date: 21.02.2020 14:41:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerModels](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[MobileNumber] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.CustomerModels] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TransactionModels]    Script Date: 21.02.2020 14:41:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TransactionModels](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetime] NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[Currency] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[CustomerId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.TransactionModels] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Index [IX_CustomerId]    Script Date: 21.02.2020 14:41:07 ******/
CREATE NONCLUSTERED INDEX [IX_CustomerId] ON [dbo].[TransactionModels]
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[TransactionModels]  WITH CHECK ADD  CONSTRAINT [FK_dbo.TransactionModels_dbo.CustomerModels_CustomerId] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[CustomerModels] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TransactionModels] CHECK CONSTRAINT [FK_dbo.TransactionModels_dbo.CustomerModels_CustomerId]
GO
USE [master]
GO
ALTER DATABASE [WebAPIApplicationDataBase] SET  READ_WRITE 
GO
