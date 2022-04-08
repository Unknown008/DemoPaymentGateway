USE [master]
GO
/****** Object:  Database [BankDemo]    Script Date: 08/04/2022 01:02:16 ******/
CREATE DATABASE [BankDemo]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'BankDemo', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\BankDemo.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'BankDemo_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\BankDemo_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [BankDemo] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BankDemo].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BankDemo] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BankDemo] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BankDemo] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BankDemo] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BankDemo] SET ARITHABORT OFF 
GO
ALTER DATABASE [BankDemo] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [BankDemo] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BankDemo] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BankDemo] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BankDemo] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BankDemo] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BankDemo] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BankDemo] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BankDemo] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BankDemo] SET  DISABLE_BROKER 
GO
ALTER DATABASE [BankDemo] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BankDemo] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BankDemo] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BankDemo] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BankDemo] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BankDemo] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [BankDemo] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BankDemo] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [BankDemo] SET  MULTI_USER 
GO
ALTER DATABASE [BankDemo] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BankDemo] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BankDemo] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BankDemo] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [BankDemo] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [BankDemo] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [BankDemo] SET QUERY_STORE = OFF
GO
USE [BankDemo]
GO
/****** Object:  User [demouser]    Script Date: 08/04/2022 01:02:16 ******/
CREATE USER [demouser] FOR LOGIN [demouser] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [demouser]
GO
/****** Object:  Table [dbo].[Accounts]    Script Date: 08/04/2022 01:02:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Accounts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AccountNumber] [nvarchar](55) NOT NULL,
	[CardNumber] [nvarchar](55) NOT NULL,
	[ExpiryMonth] [int] NOT NULL,
	[ExpiryYear] [int] NOT NULL,
	[Balance] [decimal](18, 2) NOT NULL,
	[Currency] [nvarchar](3) NOT NULL,
	[CVV] [nvarchar](10) NOT NULL,
	[CreatedBy] [nvarchar](55) NULL,
	[UpdatedBy] [nvarchar](55) NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedOn] [datetime] NULL,
 CONSTRAINT [PK_Accounts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Transactions]    Script Date: 08/04/2022 01:02:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transactions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AccountId] [int] NOT NULL,
	[Status] [nvarchar](55) NULL,
	[Amount] [decimal](18, 2) NULL,
	[Type] [nvarchar](55) NULL,
	[CreatedBy] [nvarchar](55) NULL,
	[UpdatedBy] [nvarchar](55) NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedOn] [datetime] NULL,
	[Currency] [nvarchar](55) NOT NULL,
	[RequesterId] [int] NOT NULL,
 CONSTRAINT [PK_Transactions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Accounts] ON 
GO
INSERT [dbo].[Accounts] ([Id], [AccountNumber], [CardNumber], [ExpiryMonth], [ExpiryYear], [Balance], [Currency], [CVV], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (1, N'1', N'4111111111111111', 12, 2022, CAST(956.00 AS Decimal(18, 2)), N'MUR', N'123', N'test', N'test', getdate(), getdate())
GO
INSERT [dbo].[Accounts] ([Id], [AccountNumber], [CardNumber], [ExpiryMonth], [ExpiryYear], [Balance], [Currency], [CVV], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (2, N'2', N'5111111111111111', 12, 2021, CAST(1000.00 AS Decimal(18, 2)), N'MUR', N'123', N'test', N'test', getdate(), getdate())
GO
SET IDENTITY_INSERT [dbo].[Accounts] OFF
GO
SET IDENTITY_INSERT [dbo].[Transactions] ON 
GO
INSERT [dbo].[Transactions] ([Id], [AccountId], [Status], [Amount], [Type], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn], [Currency], [RequesterId]) VALUES (1, 1, N'Complete', CAST(2.00 AS Decimal(18, 2)), N'Debit', N'test', N'test', getdate(), getdate(), N'MUR', 1)
SET IDENTITY_INSERT [dbo].[Transactions] OFF
GO
ALTER TABLE [dbo].[Transactions]  WITH CHECK ADD  CONSTRAINT [FK_Transactions_Accounts] FOREIGN KEY([AccountId])
REFERENCES [dbo].[Accounts] ([Id])
GO
ALTER TABLE [dbo].[Transactions] CHECK CONSTRAINT [FK_Transactions_Accounts]
GO
USE [master]
GO
ALTER DATABASE [BankDemo] SET  READ_WRITE 
GO
