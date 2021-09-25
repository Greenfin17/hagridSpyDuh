USE [master]
GO
/****** Object:  Database [SpyDuh]    Script Date: 9/25/2021 11:42:51 AM ******/
CREATE DATABASE [SpyDuh]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SpyDuh', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\SpyDuh.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'SpyDuh_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\SpyDuh_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [SpyDuh] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SpyDuh].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SpyDuh] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SpyDuh] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SpyDuh] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SpyDuh] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SpyDuh] SET ARITHABORT OFF 
GO
ALTER DATABASE [SpyDuh] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SpyDuh] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SpyDuh] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SpyDuh] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SpyDuh] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SpyDuh] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SpyDuh] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SpyDuh] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SpyDuh] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SpyDuh] SET  DISABLE_BROKER 
GO
ALTER DATABASE [SpyDuh] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SpyDuh] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SpyDuh] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SpyDuh] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SpyDuh] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SpyDuh] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SpyDuh] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SpyDuh] SET RECOVERY FULL 
GO
ALTER DATABASE [SpyDuh] SET  MULTI_USER 
GO
ALTER DATABASE [SpyDuh] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SpyDuh] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SpyDuh] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SpyDuh] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [SpyDuh] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'SpyDuh', N'ON'
GO
ALTER DATABASE [SpyDuh] SET QUERY_STORE = OFF
GO
USE [SpyDuh]
GO
/****** Object:  Table [dbo].[Handler]    Script Date: 9/25/2021 11:42:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Handler](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[AgencyName] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HandlerSpyRelationship]    Script Date: 9/25/2021 11:42:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HandlerSpyRelationship](
	[Id] [uniqueidentifier] NOT NULL,
	[HandlerId] [uniqueidentifier] NOT NULL,
	[SpyId] [uniqueidentifier] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Spy]    Script Date: 9/25/2021 11:42:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Spy](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SpyEnemiesRelationship]    Script Date: 9/25/2021 11:42:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SpyEnemiesRelationship](
	[Id] [uniqueidentifier] NOT NULL,
	[SpyId] [uniqueidentifier] NOT NULL,
	[SpyEnemyId] [uniqueidentifier] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SpyFriendRelationship]    Script Date: 9/25/2021 11:42:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SpyFriendRelationship](
	[Id] [uniqueidentifier] NOT NULL,
	[SpyId] [uniqueidentifier] NOT NULL,
	[SpyFriendId] [uniqueidentifier] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SpyServices]    Script Date: 9/25/2021 11:42:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SpyServices](
	[Id] [uniqueidentifier] NOT NULL,
	[Description] [varchar](50) NOT NULL,
	[ENUM] [int] IDENTITY(0,1) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SpyServicesRelationship]    Script Date: 9/25/2021 11:42:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SpyServicesRelationship](
	[Id] [uniqueidentifier] NOT NULL,
	[SpyId] [uniqueidentifier] NOT NULL,
	[ServiceId] [uniqueidentifier] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SpySkillRelationship]    Script Date: 9/25/2021 11:42:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SpySkillRelationship](
	[Id] [uniqueidentifier] NOT NULL,
	[SpyId] [uniqueidentifier] NOT NULL,
	[SkillId] [uniqueidentifier] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SpySkills]    Script Date: 9/25/2021 11:42:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SpySkills](
	[Id] [uniqueidentifier] NOT NULL,
	[Description] [varchar](50) NOT NULL,
	[ENUM] [int] IDENTITY(0,1) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Handler] ADD  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[HandlerSpyRelationship] ADD  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[Spy] ADD  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[SpyEnemiesRelationship] ADD  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[SpyFriendRelationship] ADD  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[SpyServices] ADD  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[SpyServicesRelationship] ADD  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[SpySkillRelationship] ADD  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[SpySkills] ADD  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[HandlerSpyRelationship]  WITH CHECK ADD  CONSTRAINT [FK_HandlerSpy_Handler] FOREIGN KEY([HandlerId])
REFERENCES [dbo].[Handler] ([Id])
GO
ALTER TABLE [dbo].[HandlerSpyRelationship] CHECK CONSTRAINT [FK_HandlerSpy_Handler]
GO
ALTER TABLE [dbo].[HandlerSpyRelationship]  WITH CHECK ADD  CONSTRAINT [FK_HandlerSpy_Spy] FOREIGN KEY([SpyId])
REFERENCES [dbo].[Spy] ([Id])
GO
ALTER TABLE [dbo].[HandlerSpyRelationship] CHECK CONSTRAINT [FK_HandlerSpy_Spy]
GO
ALTER TABLE [dbo].[SpyEnemiesRelationship]  WITH CHECK ADD  CONSTRAINT [FK_SpyEnemy_SpyEnemies] FOREIGN KEY([SpyEnemyId])
REFERENCES [dbo].[Spy] ([Id])
GO
ALTER TABLE [dbo].[SpyEnemiesRelationship] CHECK CONSTRAINT [FK_SpyEnemy_SpyEnemies]
GO
ALTER TABLE [dbo].[SpyEnemiesRelationship]  WITH CHECK ADD  CONSTRAINT [FK_SpyId_SpyEnemies] FOREIGN KEY([SpyId])
REFERENCES [dbo].[Spy] ([Id])
GO
ALTER TABLE [dbo].[SpyEnemiesRelationship] CHECK CONSTRAINT [FK_SpyId_SpyEnemies]
GO
ALTER TABLE [dbo].[SpyFriendRelationship]  WITH CHECK ADD  CONSTRAINT [FK_SpyFriend_SpyFriends] FOREIGN KEY([SpyFriendId])
REFERENCES [dbo].[Spy] ([Id])
GO
ALTER TABLE [dbo].[SpyFriendRelationship] CHECK CONSTRAINT [FK_SpyFriend_SpyFriends]
GO
ALTER TABLE [dbo].[SpyFriendRelationship]  WITH CHECK ADD  CONSTRAINT [FK_SpyId_SpyFriends] FOREIGN KEY([SpyId])
REFERENCES [dbo].[Spy] ([Id])
GO
ALTER TABLE [dbo].[SpyFriendRelationship] CHECK CONSTRAINT [FK_SpyId_SpyFriends]
GO
ALTER TABLE [dbo].[SpyServicesRelationship]  WITH CHECK ADD  CONSTRAINT [FK_ServiceId_SpyServices] FOREIGN KEY([ServiceId])
REFERENCES [dbo].[SpyServices] ([Id])
GO
ALTER TABLE [dbo].[SpyServicesRelationship] CHECK CONSTRAINT [FK_ServiceId_SpyServices]
GO
ALTER TABLE [dbo].[SpyServicesRelationship]  WITH CHECK ADD  CONSTRAINT [FK_SpyId_SpyServices] FOREIGN KEY([SpyId])
REFERENCES [dbo].[Spy] ([Id])
GO
ALTER TABLE [dbo].[SpyServicesRelationship] CHECK CONSTRAINT [FK_SpyId_SpyServices]
GO
ALTER TABLE [dbo].[SpySkillRelationship]  WITH CHECK ADD  CONSTRAINT [FK_SkillId] FOREIGN KEY([SkillId])
REFERENCES [dbo].[SpySkills] ([Id])
GO
ALTER TABLE [dbo].[SpySkillRelationship] CHECK CONSTRAINT [FK_SkillId]
GO
ALTER TABLE [dbo].[SpySkillRelationship]  WITH CHECK ADD  CONSTRAINT [FK_SpyId] FOREIGN KEY([SpyId])
REFERENCES [dbo].[Spy] ([Id])
GO
ALTER TABLE [dbo].[SpySkillRelationship] CHECK CONSTRAINT [FK_SpyId]
GO
USE [master]
GO
ALTER DATABASE [SpyDuh] SET  READ_WRITE 
GO
