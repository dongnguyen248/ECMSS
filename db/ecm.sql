USE [master]
GO
/****** Object:  Database [ECM]    Script Date: 10/03/2022 08:47:51 ******/
CREATE DATABASE [ECM]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ECM', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\ECM.mdf' , SIZE = 10432KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'ECM_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\ECM_log.ldf' , SIZE = 39936KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ECM].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ECM] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ECM] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ECM] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ECM] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ECM] SET ARITHABORT OFF 
GO
ALTER DATABASE [ECM] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [ECM] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ECM] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ECM] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ECM] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ECM] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ECM] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ECM] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ECM] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ECM] SET  ENABLE_BROKER 
GO
ALTER DATABASE [ECM] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ECM] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ECM] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ECM] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ECM] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ECM] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ECM] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ECM] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ECM] SET  MULTI_USER 
GO
ALTER DATABASE [ECM] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ECM] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ECM] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ECM] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [ECM]
GO
/****** Object:  Table [dbo].[Department]    Script Date: 10/03/2022 08:47:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Department](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Directory]    Script Date: 10/03/2022 08:47:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Directory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[ParentId] [int] NULL,
	[DepartmentId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Employee]    Script Date: 10/03/2022 08:47:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Employee](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EpLiteId] [varchar](50) NOT NULL,
	[FirstName] [nvarchar](200) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[DepartmentId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[FileFavorite]    Script Date: 10/03/2022 08:47:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FileFavorite](
	[EmployeeId] [int] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[FileId] [uniqueidentifier] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FileHistory]    Script Date: 10/03/2022 08:47:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[FileHistory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[StatusId] [int] NOT NULL,
	[Size] [int] NOT NULL,
	[Version] [varchar](5) NOT NULL,
	[Modifier] [int] NOT NULL,
	[FileId] [uniqueidentifier] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[FileImportant]    Script Date: 10/03/2022 08:47:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FileImportant](
	[Id] [uniqueidentifier] NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[FileId] [uniqueidentifier] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FileInfo]    Script Date: 10/03/2022 08:47:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[FileInfo](
	[Name] [nvarchar](500) NOT NULL,
	[Owner] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[Tag] [nvarchar](500) NULL,
	[DirectoryId] [int] NOT NULL,
	[SecurityLevel] [varchar](20) NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[Extension] [varchar](20) NOT NULL,
 CONSTRAINT [Pk_FileInfo_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[FilePermission]    Script Date: 10/03/2022 08:47:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[FilePermission](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Permission] [varchar](10) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[FileShare]    Script Date: 10/03/2022 08:47:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FileShare](
	[EmployeeId] [int] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[Permission] [int] NOT NULL,
	[FileId] [uniqueidentifier] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FileStatus]    Script Date: 10/03/2022 08:47:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[FileStatus](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Status] [varchar](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 10/03/2022 08:47:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Roles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SpecialDepartment]    Script Date: 10/03/2022 08:47:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SpecialDepartment](
	[DepartmentId] [int] NOT NULL,
	[DirectoryId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[DepartmentId] ASC,
	[DirectoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TB_CONTENTS_INFO]    Script Date: 10/03/2022 08:47:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TB_CONTENTS_INFO](
	[Contents_Id] [int] NOT NULL,
	[Contents_Name] [char](100) NOT NULL,
	[Contents_Exp] [char](500) NULL,
	[Contents_Step] [int] NULL,
	[Contents_Parent_Id] [int] NULL,
 CONSTRAINT [PK_TB_CONTENTS_INFO] PRIMARY KEY CLUSTERED 
(
	[Contents_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TB_FILE_HISTORY]    Script Date: 10/03/2022 08:47:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TB_FILE_HISTORY](
	[Creation_Datetime] [char](15) NOT NULL,
	[File_Index_No] [char](15) NOT NULL,
	[Modify_Content] [nchar](1000) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TB_FILE_LIST]    Script Date: 10/03/2022 08:47:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TB_FILE_LIST](
	[File_Index_No] [char](15) NOT NULL,
	[File_Name] [char](300) NOT NULL,
	[File_Path] [char](300) NULL,
	[File_Size] [numeric](18, 0) NULL,
	[File_Security] [int] NULL,
	[File_Version] [float] NULL,
	[Modify_Date] [char](12) NULL,
	[Owner_ID] [char](30) NOT NULL,
	[Modifier_ID] [char](30) NULL,
	[File_Tag] [char](100) NULL,
	[Contents_Name] [char](100) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TB_SHARE_INFO]    Script Date: 10/03/2022 08:47:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TB_SHARE_INFO](
	[Owner_ID] [char](6) NULL,
	[Employee_No] [char](6) NULL,
	[File_Index_No] [char](6) NULL,
	[File_Authority] [int] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TB_USER_INFO]    Script Date: 10/03/2022 08:47:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TB_USER_INFO](
	[Employee_No] [char](6) NOT NULL,
	[EpLite_ID] [char](30) NOT NULL,
	[Employee_Name] [char](50) NOT NULL,
	[Employee_Dept] [char](50) NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Trash]    Script Date: 10/03/2022 08:47:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Trash](
	[Id] [uniqueidentifier] NOT NULL,
	[DeletedDate] [datetime] NULL,
	[FileId] [uniqueidentifier] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Department] ON 

INSERT [dbo].[Department] ([Id], [Name]) VALUES (60, N'Sales 1')
INSERT [dbo].[Department] ([Id], [Name]) VALUES (61, N'Sales 2')
INSERT [dbo].[Department] ([Id], [Name]) VALUES (62, N'Order Process')
INSERT [dbo].[Department] ([Id], [Name]) VALUES (63, N'Technology')
INSERT [dbo].[Department] ([Id], [Name]) VALUES (64, N'Maintenance')
INSERT [dbo].[Department] ([Id], [Name]) VALUES (65, N'Production 1')
INSERT [dbo].[Department] ([Id], [Name]) VALUES (66, N'Production 2')
INSERT [dbo].[Department] ([Id], [Name]) VALUES (67, N'Precision')
INSERT [dbo].[Department] ([Id], [Name]) VALUES (68, N'Safety/ Innovation')
INSERT [dbo].[Department] ([Id], [Name]) VALUES (69, N'Plan & Financing')
INSERT [dbo].[Department] ([Id], [Name]) VALUES (70, N'Management Support')
INSERT [dbo].[Department] ([Id], [Name]) VALUES (71, N'Purchase/ Im-Export')
INSERT [dbo].[Department] ([Id], [Name]) VALUES (72, N'ICT')
INSERT [dbo].[Department] ([Id], [Name]) VALUES (73, N'President Director')
INSERT [dbo].[Department] ([Id], [Name]) VALUES (74, N'Marketing Division')
INSERT [dbo].[Department] ([Id], [Name]) VALUES (75, N'Production Division')
INSERT [dbo].[Department] ([Id], [Name]) VALUES (76, N'Management Division')
SET IDENTITY_INSERT [dbo].[Department] OFF
SET IDENTITY_INSERT [dbo].[Directory] ON 

INSERT [dbo].[Directory] ([Id], [Name], [ParentId], [DepartmentId]) VALUES (2, N'ICT', NULL, 72)
INSERT [dbo].[Directory] ([Id], [Name], [ParentId], [DepartmentId]) VALUES (3, N'Maintenance', NULL, 64)
INSERT [dbo].[Directory] ([Id], [Name], [ParentId], [DepartmentId]) VALUES (4, N'Management Division', NULL, 76)
INSERT [dbo].[Directory] ([Id], [Name], [ParentId], [DepartmentId]) VALUES (5, N'Management Support', NULL, 70)
INSERT [dbo].[Directory] ([Id], [Name], [ParentId], [DepartmentId]) VALUES (6, N'Marketing Division', NULL, 74)
INSERT [dbo].[Directory] ([Id], [Name], [ParentId], [DepartmentId]) VALUES (7, N'Order Process', NULL, 62)
INSERT [dbo].[Directory] ([Id], [Name], [ParentId], [DepartmentId]) VALUES (8, N'Plan_Financing', NULL, 69)
INSERT [dbo].[Directory] ([Id], [Name], [ParentId], [DepartmentId]) VALUES (9, N'Precision', NULL, 67)
INSERT [dbo].[Directory] ([Id], [Name], [ParentId], [DepartmentId]) VALUES (10, N'President Director', NULL, 73)
INSERT [dbo].[Directory] ([Id], [Name], [ParentId], [DepartmentId]) VALUES (11, N'Production 1', NULL, 65)
INSERT [dbo].[Directory] ([Id], [Name], [ParentId], [DepartmentId]) VALUES (12, N'Production 2', NULL, 66)
INSERT [dbo].[Directory] ([Id], [Name], [ParentId], [DepartmentId]) VALUES (13, N'Production Division', NULL, 75)
INSERT [dbo].[Directory] ([Id], [Name], [ParentId], [DepartmentId]) VALUES (14, N'Purchase_Im-Export', NULL, 71)
INSERT [dbo].[Directory] ([Id], [Name], [ParentId], [DepartmentId]) VALUES (15, N'Safety_Innovation', NULL, 68)
INSERT [dbo].[Directory] ([Id], [Name], [ParentId], [DepartmentId]) VALUES (16, N'Sales 1', NULL, 60)
INSERT [dbo].[Directory] ([Id], [Name], [ParentId], [DepartmentId]) VALUES (17, N'Sales 2', NULL, 61)
INSERT [dbo].[Directory] ([Id], [Name], [ParentId], [DepartmentId]) VALUES (18, N'Technology', NULL, 63)
SET IDENTITY_INSERT [dbo].[Directory] OFF
SET IDENTITY_INSERT [dbo].[Employee] ON 

INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (1965, N'do.cuong', N'Cường', N'Đỗ Mạnh', 64, 1)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (1966, N'nh.tuan', N'Tuấn', N'Nguyễn Hữu', 64, 1)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (1967, N'vst050004', N'Nam', N'Vũ Văn', 64, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (1968, N'vst050008', N'Nam', N'Đỗ Thành', 64, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (1969, N'vst050010', N'Long', N'Nguyễn Đạt', 64, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (1970, N'vst050027', N'Tân', N'Đỗ Phước', 64, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (1971, N'vst070152', N'Thanh', N'Huỳnh Văn', 64, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (1972, N'vst080236', N'Trung', N'Vũ Đức', 64, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (1973, N'vst080307', N'Hoan', N'Đặng Bá', 64, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (1974, N'vst110533', N'Toản', N'Phạm Hồng', 64, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (1975, N'phan.hau', N'Hậu', N'Phan Đình', 64, 1)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (1976, N'tran.chinh', N'Chính', N'Trần Công', 64, 1)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (1977, N'vst110645', N'Mạnh', N'Phạm Quốc', 64, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (1978, N'vst110855', N'Hòa', N'Lê Trọng', 64, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (1979, N'vst110875', N'Hiệp', N'Nguyễn Đức', 64, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (1980, N'vst110934', N'Tiến', N'Huỳnh Minh', 64, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (1981, N'vst131201', N'Sáu', N'Nguyễn Bé', 64, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (1982, N'vst171531', N'Hải', N'Nguyễn Ngọc', 64, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (1983, N'vst171547', N'Dương', N'Nguyễn Công', 64, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (1984, N'cap.trung', N'Trung', N'Cáp Xuân', 64, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (1985, N'vst050016', N'Phương', N'Đoàn Thanh', 64, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (1986, N'vst050018', N'Hòa', N'Trần Đức', 64, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (1987, N'dinh.qua', N'Qua', N'Đinh', 64, 1)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (1988, N'van.dung', N'Dũng', N'Đoàn Văn', 64, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (1989, N'pham.huong', N'Hưởng', N'Phạm Văn', 64, 1)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (1990, N'vst080219', N'Hành', N'Võ Văn', 64, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (1991, N'vst080229', N'Ân', N'Phạm Ngọc', 64, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (1992, N'vst080261', N'Dương', N'Trương Hải', 64, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (1993, N'vst080310', N'Thọ', N'Đỗ Khắc', 64, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (1994, N'huynh.hung', N'Hưng', N'Huỳnh Phước', 64, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (1995, N'vst110694', N'Quỳnh', N'Tằng Văn', 64, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (1996, N'vst121000', N'Trọng', N'Nguyễn Đức', 64, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (1997, N'vst141334', N'Sương', N'Nguyễn Tú', 64, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (1998, N'nguyen.quang', N'Quãng', N'Nguyễn Văn', 64, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (1999, N'vst171506', N'Hùng', N'Võ Văn', 64, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2000, N'vst050026', N'Thơi', N'Vũ Tiến', 64, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2001, N'vst050053', N'Công', N'Nguyễn Đình', 64, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2002, N'vst060070', N'Thuận', N'Phạm Văn', 64, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2003, N'vst060077', N'Hải', N'Đỗ Văn', 64, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2004, N'ho.phuc', N'Phúc', N'Hồ Viết', 64, 1)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2005, N'vst070161', N'Bằng', N'Lê Việt', 64, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2006, N'vst070192', N'Thông', N'Mai Hoàng', 64, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2007, N'vst080238', N'Hùng', N'Nguyễn Ngọc', 64, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2008, N'vst080250', N'Trung', N'Chu Quốc', 64, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2009, N'vst080272', N'Hồng', N'Trần Viết', 64, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2010, N'vst080280', N'Đại', N'Phan Văn', 64, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2011, N'vst100478', N'Thao', N'Lê Xuân', 64, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2012, N'ly.thao', N'Thảo', N'Lý Giang', 64, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2013, N'mn.binh', N'Bình', N'Mai Ngọc', 64, 1)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2014, N'vst110314', N'Vinh', N'Ngô Trí', 64, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2015, N'vst110602', N'Quang', N'Lê Hữu', 64, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2016, N'vst110606', N'Cường', N'Đỗ Viết', 64, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2017, N'huynh.chau', N'Châu', N'Huỳnh Ngọc', 64, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2018, N'vst110619', N'Thanh', N'Đặng Thế', 64, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2019, N'nguyen.minh', N'Minh', N'Nguyễn Anh', 64, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2020, N'vst110657', N'Trực', N'Nguyễn Xuân', 64, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2021, N'vst110691', N'Luận', N'Trần Phúc', 64, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2022, N'vst110728', N'Hải', N'Nguyễn Minh', 64, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2023, N'vst110771', N'Sang', N'Trần Ngọc', 64, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2024, N'vst110866', N'Tuấn', N'Nguyễn Trí', 64, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2025, N'vo.nam', N'Năm', N'Võ Văn Bé', 64, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2026, N'vst110956', N'Thọ', N'Nguyễn Phước', 64, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2027, N'vst141312', N'Hưởng', N'Phạm Văn', 64, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2028, N'vst171525', N'Vũ', N'Phan Văn', 64, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2029, N'vst191650', N'Hiền', N'Nguyễn Thanh', 64, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2030, N'nguyen.hien', N'Hiền', N'Nguyễn Thị', 64, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2031, N'vst070154', N'Giàu', N'Lê Thanh', 64, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2032, N'hoang.trinh', N'Trình', N'Hoàng Văn', 64, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2033, N'dinh.phuong', N'Phương', N'Trần Đình', 64, 1)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2034, N'vothanhtung', N'Tùng', N'Võ Thanh', 64, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2035, N'vu.ngocbinh', N'Bình', N'Vũ Ngọc', 64, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2036, N'vo.trinh', N'Trinh', N'Võ Thị Tuyết', 70, 1)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2037, N'tran.trinh', N'Trinh', N'Trần Diễm', 70, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2038, N'cao.nguyet', N'Nguyệt', N'Cao Phương Thanh', 70, 1)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2039, N'pham.thithuy', N'Thủy', N'Phạm Thị', 70, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2040, N'pham.khoa', N'Khoa', N'Phạm Đăng', 70, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2041, N'vst070191', N'Cường', N'Hồ Văn', 70, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2042, N'vst070193', N'Nghĩa', N'Nguyễn Trọng', 70, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2043, N'vst080258', N'Toàn', N'Trương Đình', 70, 1)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2044, N'vst110623', N'Phương', N'Trần Văn', 70, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2045, N'vst110626', N'Chương', N'Nguyễn Quang', 70, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2046, N'vst110879', N'Duyên', N'Lê Đình', 70, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2047, N'vst110905', N'Anh', N'Võ Hùng', 70, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2048, N'vst121015', N'Thiện', N'Hồ Văn', 70, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2049, N'vst121053', N'Sáu', N'Dương Văn', 70, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2050, N'vst121113', N'Thắng', N'Trần Trung', 70, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2051, N'vst131139', N'Quý', N'Nguyễn Văn', 70, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2052, N'vst141311', N'Khánh', N'Nguyễn Văn', 70, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2053, N'vst181588', N'Tiệp', N'Huỳnh Nguyễn', 70, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2054, N'truong.shan', N'Sơn', N'Trương Thanh', 62, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2055, N'tran.thi', N'Thi', N'Trần Thế', 62, 1)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2056, N'nguyen.lan', N'Lân', N'Nguyễn Văn', 62, 1)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2057, N'vst110546', N'Hùng', N'Nguyễn Khắc', 62, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2058, N'tran.dang', N'Đặng', N'Trần Quang', 62, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2059, N'nguyen.nhung', N'Nhung', N'Nguyễn Thị Cẩm', 62, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2060, N'tran.linh', N'Linh', N'Trần Đoàn Phương', 62, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2061, N'tran.thigiang', N'Giang', N'Trần Thị', 62, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2062, N'vst201688', N'Dương', N'Nguyễn Đắc', 62, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2063, N'vst050029', N'Thuyên', N'Nguyễn Văn', 62, 2)
GO
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2064, N'vst050059', N'An', N'Đỗ Văn', 62, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2065, N'le.tam', N'Tám', N'Lê Văn', 62, 1)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2066, N'vst070140', N'Thanh', N'Nguyễn Chí', 62, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2067, N'vst070174', N'Toản', N'Vũ Trường', 62, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2068, N'vst080245', N'Sẽ', N'Nguyễn Đình', 62, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2069, N'vst080295', N'Vinh', N'Nguyễn Phong', 62, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2070, N'vst080300', N'Hùng', N'Phạm Văn', 62, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2071, N'vst090322', N'Dương', N'Nguyễn Văn', 62, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2072, N'vst090335', N'Thuyến', N'Phạm Văn', 62, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2073, N'vst100380', N'Tân', N'Phạm Ngọc', 62, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2074, N'vst121016', N'Đăng', N'Lê Đỗ Thảo', 62, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2075, N'vst121040', N'Hoàn', N'Nguyễn Hữu', 62, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2076, N'vst121041', N'Toàn', N'Lê Quang', 62, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2077, N'vst131129', N'Tuấn', N'Nguyễn Minh', 62, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2078, N'vst131140', N'Cường', N'Nguyễn Mạnh', 62, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2079, N'vst141221', N'Sĩ', N'Đỗ Văn', 62, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2080, N'vst141236', N'Trí', N'Nguyễn Quốc', 62, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2081, N'tran.son', N'Sơn', N'Trần Thế', 62, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2082, N'vst151364', N'Bừa', N'Phùng Văn', 62, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2083, N'vst151377', N'Long', N'Lê Văn', 62, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2084, N'vst161440', N'Đạt', N'Trần Đình', 62, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2085, N'vst161456', N'Nam', N'Trần Văn', 62, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2086, N'vst161457', N'Hòa', N'Hoàng Văn', 62, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2087, N'vst171514', N'Tòng', N'Võ Thanh', 62, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2088, N'vst171558', N'Đạt', N'Nguyễn Văn', 62, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2089, N'vst181600', N'Khét', N'Thạch', 62, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2090, N'vst191613', N'Mỹ', N'Phùng Văn', 62, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2091, N'vst191618', N'Nghĩa', N'Trịnh Xuân', 62, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2092, N'vst191622', N'Khánh', N'Nguyễn Hồng', 62, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2093, N'vst191646', N'Tiến', N'Nguyễn Minh', 62, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2094, N'vst201671', N'Đức', N'Tôn Trung', 62, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2095, N'vst201686', N'Sáng', N'Phạm Thanh', 62, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2096, N'vst201687', N'Sơn', N'Đặng Quốc', 62, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2097, N'vst201701', N'Thuận', N'Lâm Văn', 62, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2098, N'nmn.linh', N'Linh', N'Nguyễn Mai Ngọc', 69, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2099, N'truong.hung', N'Hùng', N'Trương Quang', 69, 1)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2100, N'nguyen.thanhgiang', N'Giang', N'Nguyễn Vũ Thanh', 69, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2101, N'nguyen.quynhanh', N'Anh', N'Nguyễn Quỳnh', 69, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2102, N'doan.trang', N'Trang', N'Đoàn Võ Thị Huyền', 69, 1)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2103, N'le.thuy', N'Thùy', N'Lê Thị', 69, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2104, N'tran.anhthu', N'Thư', N'Trần Ngọc Anh', 69, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2105, N'vst141229', N'Ân', N'Trần Hoài', 69, 1)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2106, N'nguyen.my', N'My', N'Nguyễn Thị Trà', 69, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2107, N'nguyen.baonhi', N'Nhi', N'Nguyễn Hoàng Bảo', 69, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2108, N'vst070169', N'Huyên', N'Bạch Đình', 67, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2109, N'vst100358', N'Quí', N'Huỳnh Ngọc', 67, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2110, N'vst100369', N'Bình', N'Nguyễn Văn', 67, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2111, N'vst100452', N'Nghĩa', N'Phạm Đại', 67, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2112, N'vst100466', N'Tiến', N'Đổng Minh', 67, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2113, N'vst110545', N'Hùng', N'Trần Thế', 67, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2114, N'vst110588', N'Vũ', N'Nguyễn Văn', 67, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2115, N'vst121030', N'Luân', N'Nguyễn Thành', 67, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2116, N'vst121031', N'Sơn', N'Nguyễn Hồng', 67, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2117, N'vst151407', N'Khôi', N'Dương Ngọc Thanh', 67, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2118, N'vst161476', N'Dũng', N'Nguyễn Tiến', 67, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2119, N'vst181596', N'Tâm', N'Hoàng Văn', 67, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2120, N'nguyen.huyquang', N'Quang', N'Nguyễn Huy', 67, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2121, N'vst191651', N'Hùng', N'Phan Văn', 67, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2122, N'vst050043', N'Long', N'Hoàng Văn', 67, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2123, N'vst070160', N'Thái', N'Hoàng Hồng', 67, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2124, N'vst070181', N'Chiến', N'Võ Văn', 67, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2125, N'vst110571', N'Hoài', N'Lê Văn', 67, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2126, N'vst121032', N'Tùng', N'Nguyễn Văn', 67, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2127, N'vst121033', N'Tuấn', N'Phạm Thanh', 67, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2128, N'vst121075', N'Đệ', N'Nguyễn Văn', 67, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2129, N'vst151369', N'Xuyên', N'Phạm Thanh', 67, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2130, N'vst161470', N'Trọng', N'Bùi Diệp', 67, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2131, N'vst181595', N'Tâm', N'Châu Nhật', 67, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2132, N'vst191625', N'Phát', N'Huỳnh Tiến', 67, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2133, N'nguyen.minhtam', N'Tâm', N'Nguyễn Minh', 67, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2134, N'vo.quyet', N'Quyết', N'Võ Xuân', 67, 1)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2135, N'nguyen.hon', N'Hơn', N'Nguyễn Tấn', 67, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2136, N'dang.thuong', N'Thương', N'Đặng Văn', 67, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2137, N'vst070164', N'Hạnh', N'Phạm Doãn', 67, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2138, N'vst080248', N'Dương', N'Hoàng Thế', 67, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2139, N'vst080249', N'Tửng', N'Trần Quốc', 67, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2140, N'vst100408', N'Trường', N'Huỳnh Nhật', 67, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2141, N'vst100409', N'Tiệp', N'Lương Ngọc', 67, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2142, N'vst100460', N'Cường', N'Nguyễn Chí', 67, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2143, N'vst110516', N'Thuận', N'Nguyễn Minh', 67, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2144, N'vst131148', N'Tài', N'Nguyễn Tấn', 67, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2145, N'vst131149', N'Huy', N'Tăng Văn', 67, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2146, N'vst131183', N'Bẩu', N'Vòng Trần', 67, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2147, N'vst131184', N'Dương', N'Phạm Minh', 67, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2148, N'vst161473', N'Hòa', N'Nguyễn Xuân', 67, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2149, N'vst171575', N'Nhâm', N'Lê Văn', 67, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2150, N'vst191652', N'Mạnh', N'Phan Văn', 67, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2151, N'vo.thien', N'Thiện', N'Võ Chí', 67, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2152, N'vst050052', N'Tân', N'Nguyễn Ngọc', 65, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2153, N'vst070156', N'Thông', N'Trần Duy', 65, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2154, N'vst070198', N'Đức', N'Nguyễn Hữu', 65, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2155, N'vst050044', N'Tiệp', N'Kiều Văn', 65, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2156, N'vst050047', N'Khải', N'Mai Xuân', 65, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2157, N'vst050051', N'Thiện', N'Đinh Đức', 65, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2158, N'vst060085', N'Đức', N'Trần Đình', 65, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2159, N'vst060089', N'Yên', N'Nguyễn Văn', 65, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2160, N'vst060099', N'Hải', N'Hồ Xuân', 65, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2161, N'vst060128', N'Quý', N'Phan Văn', 65, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2162, N'vst070173', N'Định', N'Nguyễn Đình', 65, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2163, N'vst080224', N'Phú', N'Lê Minh', 65, 2)
GO
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2164, N'vst080283', N'Kỷ', N'Đặng Văn', 65, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2165, N'vst080285', N'Tuân', N'Phạm Văn', 65, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2166, N'vst100348', N'Tài', N'Phạm Văn', 65, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2167, N'vst110500', N'Khánh', N'Phạm Quốc', 65, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2168, N'vst110544', N'Tuấn', N'Nguyễn Ngọc', 65, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2169, N'le.hoa', N'Hóa', N'Lê Xuân', 65, 1)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2170, N'vst110712', N'Linh', N'Nguyễn Hồng', 65, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2171, N'vst110732', N'Trường', N'Trần Văn', 65, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2172, N'vst110764', N'Dũng', N'Đỗ Huy', 65, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2173, N'vst121009', N'Thắng', N'Nguyễn Doãn', 65, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2174, N'vst131138', N'Ngọc', N'Lê Văn', 65, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2175, N'vst141272', N'Đà', N'Trần', 65, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2176, N'dang.soai', N'Soái', N'Đặng Đình', 65, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2177, N'vst161465', N'Quỳnh', N'Đậu Văn', 65, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2178, N'vst040001', N'Hướng', N'Nguyễn Văn', 65, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2179, N'vst060127', N'Hùng', N'Trần Văn', 65, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2180, N'vst060129', N'Duy', N'Phạm Văn', 65, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2181, N'vst060132', N'Tuyến', N'Nguyễn Minh', 65, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2182, N'vst060133', N'Dũng', N'Nguyễn Tiến', 65, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2183, N'vst070176', N'Phong', N'Phạm Tuấn', 65, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2184, N'vst080271', N'Phước', N'Hoàng Văn', 65, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2185, N'vst110547', N'Hùng', N'Lê Văn', 65, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2186, N'vst110681', N'Trung', N'Lê Thành', 65, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2187, N'vst110686', N'Bình', N'Phan Văn', 65, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2188, N'vst110736', N'Hùng', N'Nguyễn Thanh', 65, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2189, N'vst131161', N'Đức', N'Nguyễn Văn', 65, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2190, N'nguyen.khap', N'Khắp', N'Nguyễn Hoàng', 65, 1)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2191, N'vst131204', N'Khang', N'Ngô Văn', 65, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2192, N'vst141246', N'Phúc', N'Phan Thanh', 65, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2193, N'vst141282', N'Diệu', N'Đặng Sỹ', 65, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2194, N'nguyen.viet', N'Việt', N'Nguyễn Quốc', 65, 1)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2195, N'van.tuan', N'Tuấn', N'Nguyễn Văn', 65, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2196, N'vst060075', N'Bình', N'Thái Hữu', 65, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2197, N'vst060088', N'Học', N'Bùi Tá', 65, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2198, N'vst060098', N'Huy', N'Nguyễn Quốc', 65, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2199, N'vst070167', N'Thuận', N'Phạm Phước', 65, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2200, N'vst070183', N'Bình', N'Nguyễn Thanh', 65, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2201, N'vst070190', N'Phong', N'Tăng Bảo', 65, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2202, N'vst080226', N'Hoàng', N'Tăng Văn', 65, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2203, N'vst080239', N'Hương', N'Nguyễn Đình', 65, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2204, N'vst080240', N'Quý', N'Nguyễn Văn', 65, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2205, N'vst080242', N'Hồng', N'Đoàn Minh', 65, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2206, N'vst080308', N'Thịnh', N'Trần Xuân', 65, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2207, N'vst100347', N'Đài', N'Phan Bá', 65, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2208, N'vst100350', N'Hải', N'Nguyễn Đình', 65, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2209, N'vst100372', N'Ngộ', N'Lê Văn', 65, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2210, N'vst100383', N'Vở', N'Trần Văn', 65, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2211, N'vst100423', N'Du', N'Nguyễn Trọng', 65, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2212, N'vst100471', N'Nhân', N'Nguyễn Thành', 65, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2213, N'vst110505', N'Cường', N'Trần Đình', 65, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2214, N'vst110507', N'Bình', N'Sa Văn', 65, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2215, N'vst110946', N'Long', N'Đặng Thế', 65, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2216, N'vst110961', N'Quý', N'Lê Văn', 65, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2217, N'vst120997', N'Hàn', N'Phan Thanh', 65, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2218, N'vst131162', N'Ba', N'Trần Hữu', 65, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2219, N'dang.phuc', N'Phúc', N'Đặng Tấn', 65, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2220, N'vst161436', N'Công', N'Đỗ Tiến', 65, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2221, N'vst161460', N'Anh', N'Phan Cảnh', 65, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2222, N'vst161482', N'Hòa', N'Phạm Văn', 65, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2223, N'vst161492', N'Nam', N'Nguyễn Quang', 65, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2224, N'pham.trieutien', N'Tiên', N'Phạm Triều', 65, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2225, N'vst050036', N'Dũng', N'Trần Hùng', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2226, N'vst060108', N'Kiên', N'Nguyễn Trọng', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2227, N'vst070185', N'Vỹ', N'Vũ Trí', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2228, N'vst050005', N'Tăng', N'Nguyễn Anh', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2229, N'vst070171', N'Phúc', N'Đinh Mạnh', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2230, N'vst070175', N'Hận', N'Bùi Hoàng', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2231, N'vst080301', N'Bình', N'Lê Thanh', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2232, N'vst100363', N'Tuyến', N'Bùi Trọng', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2233, N'do.hanh', N'Hạnh', N'Đỗ Đức', 66, 1)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2234, N'vst110697', N'Thịnh', N'Thiều Quốc', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2235, N'vst110701', N'Phương', N'Nguyễn Duy', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2236, N'vst110727', N'Nghĩa', N'Nguyễn Phúc', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2237, N'vst110973', N'Khánh', N'Trần Bá', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2238, N'vst110977', N'Hùng', N'Lê Văn', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2239, N'vst121050', N'Duy', N'Nguyễn Văn', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2240, N'vst121060', N'Long', N'Hồ Hữu', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2241, N'vst121099', N'Phú', N'Mai Văn', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2242, N'vst131166', N'Quân', N'Lý Hùng', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2243, N'vst131167', N'Đồng', N'Nguyễn Văn', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2244, N'vst141339', N'Quân', N'Thân Minh', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2245, N'vst141343', N'Sáng', N'Nguyễn Văn', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2246, N'vst181584', N'Bình', N'Nguyễn Văn', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2247, N'vst201678', N'Cường', N'Lê Đức', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2248, N'nguyen.tiendat', N'Đạt', N'Nguyễn Tiến', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2249, N'vst060084', N'Hiện', N'Đào Văn', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2250, N'le.duong', N'Dương', N'Lê Văn', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2251, N'vst060118', N'Quân', N'Nguyễn Hồng', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2252, N'vst080233', N'Thông', N'Nguyễn Long', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2253, N'vst100457', N'Cường', N'Mạc Thế', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2254, N'vst110696', N'Thịnh', N'Trần Văn', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2255, N'vst110700', N'Hải', N'Nguyễn Văn', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2256, N'vst110761', N'Hòa', N'Nguyễn Đức', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2257, N'vst110776', N'Tiền', N'Từ Thanh', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2258, N'vst110786', N'Tiến', N'Phạm Văn', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2259, N'vst110806', N'Dũng', N'Huỳnh Tấn', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2260, N'vst110840', N'Lý', N'Phan Đình', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2261, N'vst110881', N'Sơn', N'Đinh Văn', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2262, N'vst110900', N'Dương', N'Đoàn Minh', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2263, N'vst110907', N'Vương', N'Trần Minh', 66, 2)
GO
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2264, N'vst131122', N'Thành', N'Trịnh Ngọc', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2265, N'vst131185', N'Thành', N'Đỗ Đình', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2266, N'vst131202', N'Mỹ', N'Nguyễn Xuân', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2267, N'vst131203', N'Đôn', N'Lê Dư', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2268, N'vst151386', N'Hải', N'Đặng Minh', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2269, N'vst161439', N'Thùy', N'Nguyễn Đức', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2270, N'vst171553', N'Việt', N'Phạm Văn', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2271, N'vst171555', N'Lợi', N'Phạm Văn', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2272, N'vst171561', N'Thịnh', N'Phạm Tấn', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2273, N'vst171571', N'Minh', N'Nguyễn Hoàng', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2274, N'vst181585', N'Ánh', N'Đào Văn', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2275, N'vst191639', N'Hùng', N'Trần Mạnh', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2276, N'vst201676', N'Cường', N'Đinh Trọng', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2277, N'mai.thang', N'Thắng', N'Mai Hoàng', 66, 1)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2278, N'vst050011', N'Bình', N'Phạm Văn', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2279, N'vst060096', N'Vinh', N'Trần Thanh', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2280, N'vst060097', N'Hưng', N'Dương Đình', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2281, N'vst080255', N'Tài', N'Huỳnh Trọng', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2282, N'pham.trinh', N'Trình', N'Phạm Công', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2283, N'vst100377', N'Kiều', N'Hoàng Mạnh', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2284, N'vst100412', N'Nam', N'Nguyễn Văn', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2285, N'vst110654', N'Trường', N'Trần Văn', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2286, N'vst110754', N'Quý', N'Trần Văn', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2287, N'vst110768', N'Quang', N'Phan Xuân', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2288, N'vst110812', N'Sang', N'Võ Hồng', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2289, N'vst110815', N'Ba', N'Lê Văn', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2290, N'vst110818', N'Khánh', N'Võ Văn', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2291, N'vst110832', N'Quân', N'Lê Văn', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2292, N'vst110846', N'Quân', N'Vương Huy', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2293, N'vst110847', N'Ân', N'Nguyễn Hoàng', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2294, N'vst110899', N'Nghĩa', N'Hồ Văn', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2295, N'vst110918', N'Quyền', N'Nguyễn Thanh', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2296, N'vst121069', N'Mạnh', N'Nguyễn Văn', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2297, N'vst121082', N'Dương', N'Đinh Văn', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2298, N'vst121091', N'Phương', N'Nguyễn Minh', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2299, N'vst131135', N'Thanh', N'Mai Tiến', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2300, N'vst131147', N'Danh', N'Nguyễn Thành', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2301, N'vst131159', N'Hải', N'Hoàng Đình', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2302, N'vst131160', N'Thắng', N'Mạnh Huỳnh', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2303, N'nguyen.tien', N'Tiến', N'Nguyễn Tân', 66, 1)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2304, N'vst131191', N'Chung', N'Ngô', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2305, N'vst141275', N'Lịch', N'Lê Thanh', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2306, N'vst141278', N'Cường', N'Phùng Ngọc', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2307, N'vst151382', N'Nam', N'Dương Hoài', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2308, N'vst151383', N'Hậu', N'Lê Nguyễn Phước', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2309, N'vst171518', N'Lĩnh', N'Phạm Đắc', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2310, N'vst171550', N'Mười', N'Đinh Hoàng', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2311, N'vst181591', N'Thi', N'Lê Đình', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2312, N'vst181592', N'Viên', N'Nguyễn Tường', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2313, N'vst191640', N'Quân', N'Nguyễn Văn', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2314, N'vst201690', N'Quân', N'Phạm Hồng', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2315, N'vst201699', N'Dương', N'Trần Đại', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2316, N'thach.thay', N'Thay', N'Thạch', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2317, N'vst060092', N'Tuấn', N'Phùng Văn', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2318, N'n.t.hoang', N'Hoàng', N'Nguyễn Thanh', 66, 1)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2319, N'vst100461', N'Lực', N'Nguyễn Tiến', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2320, N'vst110640', N'Trường', N'Võ Xuân', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2321, N'vst110715', N'Công', N'Võ Đình', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2322, N'vst110745', N'Lợi', N'Lê Văn', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2323, N'vst110801', N'Tiến', N'Lê Văn', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2324, N'vst110816', N'Hòa', N'Huỳnh Thanh', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2325, N'vst110842', N'Anh', N'Thái Hoàng', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2326, N'vst110949', N'Thịnh', N'Vũ Tiến', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2327, N'vst141356', N'Thành', N'Đào Trung', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2328, N'vst151392', N'Viễn', N'Trần Hoàng', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2329, N'vst191624', N'Đa', N'Trần Linh', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2330, N'vst191642', N'Hiệu', N'Trịnh Văn', 66, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2331, N'pham.tuyet', N'Tuyết', N'Phạm Thị Ngọc', 71, 1)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2332, N'vst100411', N'Tú', N'Lê Anh', 71, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2333, N'ho.phien', N'Phiên', N'Hồ Thị Phi', 71, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2334, N'bich.thu', N'Thu', N'Nguyễn Thị Bích', 71, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2335, N'vst141268', N'Năm', N'Đinh Xuân', 71, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2336, N'nguyen.thuvan', N'Vân', N'Nguyễn Thị Thu', 71, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2337, N'pham.bich', N'Bích', N'Phạm Thị Ngọc', 71, 1)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2338, N'mai.diep', N'Diệp', N'Mai Ngọc', 71, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2339, N'nguyen.truc', N'Trúc', N'Nguyễn Thanh', 71, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2340, N'nguyen.phuong', N'Phượng', N'Nguyễn Ngọc', 71, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2341, N'tran.binh', N'Bình', N'Trần Ngọc', 71, 1)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2342, N'tran.dung', N'Dũng', N'Trần', 71, 1)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2343, N'kim.thuy', N'Thủy', N'Nguyễn Kim', 71, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2344, N'nguyen.thu', N'Thu', N'Nguyễn Thị', 71, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2345, N'than.hang', N'Hằng', N'Thân Thị', 71, 1)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2346, N'ho.loan', N'Loan', N'Hồ Thị Thanh', 71, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2347, N'dang.trang', N'Trang', N'Đặng Thị Thùy', 71, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2348, N'huynh.tuan', N'Tuấn', N'Huỳnh Trọng', 71, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2349, N'bui.trang', N'Trang', N'Bùi Thị Thùy', 71, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2350, N'vst110692', N'Trí', N'Lê Hữu', 71, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2351, N'bui.thang', N'Thắng', N'Bùi Trọng', 71, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2352, N'vst151362', N'Sáng', N'Nguyễn Đình', 71, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2353, N'pham.hoai', N'Hoài', N'Phạm Thị Thu', 71, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2354, N'bui.duong', N'Dương', N'Bùi Ngọc', 68, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2355, N'hoang.dung', N'Dũng', N'Hoàng Ngọc', 68, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2356, N'nguyen.hiep', N'Hiệp', N'Nguyễn Văn Tấn', 68, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2357, N'vst050023', N'Bốn', N'Nguyễn Văn', 68, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2358, N'vu.hung', N'Hùng', N'Vũ Hữu', 68, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2359, N'vst110615', N'Đại', N'Lê Duy', 68, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2360, N'vst120983', N'Minh', N'Hồ Như', 68, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2361, N'vst121090', N'Tuấn', N'Nguyễn Thanh', 68, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2362, N'hoang.thuan', N'Thuận', N'Hoàng Quốc', 68, 1)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2363, N'phan.huu', N'Hữu', N'Phạm Đình', 60, 2)
GO
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2364, N'nguyen.duc', N'Đức', N'Nguyễn Anh', 60, 1)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2365, N'nguyen.lam', N'Lãm', N'Nguyễn Ngọc', 60, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2366, N'nguyen.giang', N'Giang', N'Nguyễn Hà', 60, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2367, N'pham.minhtung', N'Tùng', N'Phạm Minh', 60, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2368, N'trinh.ngoc', N'Ngọc', N'Trình Thị Mỹ', 60, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2369, N'ho.hoangmy', N'My', N'Hồ Thị Hoàng', 60, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2370, N'tran.tram', N'Trâm', N'Trần Thị Nhật', 60, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2371, N'nguyen.khoa', N'Khoa', N'Nguyễn Ngọc', 60, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2372, N'le.nguyet', N'Nguyệt', N'Lê Thị', 60, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2373, N'nguyen.thinu', N'Nụ', N'Nguyễn Thị', 60, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2374, N'tran.thai', N'Thái', N'Trần Quốc', 60, 1)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2375, N'nguyen.huong', N'Hường', N'Nguyễn Thị', 60, 1)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2376, N'tran.duchuy', N'Huy', N'Trần Đức', 60, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2377, N'pham.thuytien', N'Tiên', N'Phạm Hồng Thủy', 60, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2378, N'nguyen.trinh', N'Trinh', N'Nguyễn Thị Ngọc', 61, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2379, N'nguyen.thithanhngan', N'Ngân', N'Nguyễn Thị Thanh', 61, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2380, N'phan.vi', N'Vi', N'Phan Thị Tường', 61, 1)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2381, N'ho.ngoc', N'Ngọc', N'Hồ Thị Bích', 61, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2382, N'nguyen.minhthuy', N'Thúy', N'Nguyễn Thị Minh', 61, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2383, N'xuan.huong', N'Hương', N'Trần Thị Xuân', 61, 1)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2384, N'than.thuong', N'Thương', N'Thân Hoàng Xuân', 61, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2385, N'phamnhung', N'Nhung', N'Phạm Thị', 61, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2386, N'tran.tan', N'Tân', N'Trần Đức', 61, 1)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2387, N'nguyen.nhan', N'Nhân', N'Nguyễn Hữu', 63, 1)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2388, N'huynh.nhan', N'Nhân', N'Huỳnh Phúc', 63, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2389, N'nguyen.hoanghuy', N'Huy', N'Nguyễn Hoàng', 63, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2390, N'than.tien', N'Tiến', N'Thân Trọng', 63, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2391, N'pham.quoccuong', N'Cường', N'Phạm Quốc', 63, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2392, N'phan.dung', N'Dũng', N'Phan Sĩ', 63, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2393, N'vst060121', N'Ẩn', N'Đinh Văn', 63, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2394, N'nguyen.tu', N'Tú', N'Nguyễn Văn', 63, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2395, N'luong.phuoc', N'Phước', N'Lương Tấn', 63, 1)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2396, N'vst070155', N'Cường', N'Nguyễn Văn', 63, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2397, N'vst070170', N'Phương', N'Nguyễn Minh', 63, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2398, N'vst070189', N'Thanh', N'Trần Đình', 63, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2399, N'vst070207', N'Dũng', N'Trần Văn', 63, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2400, N'vst080234', N'Long', N'Trịnh Xuân', 63, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2401, N'vst080262', N'Phong', N'Nguyễn Văn', 63, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2402, N'vst090323', N'Hùng', N'Phạm Văn', 63, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2403, N'vst090325', N'Cường', N'Trần Minh', 63, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2404, N'vst100354', N'Quân', N'Hoàng Minh', 63, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2405, N'vst100375', N'Tài', N'Trần Hữu', 63, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2406, N'vst100399', N'Giờ', N'Đào Văn', 63, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2407, N'vst100431', N'Minh', N'Trương Tấn', 63, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2408, N'vst110852', N'Tú', N'Huỳnh Hoàng', 63, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2409, N'vst110854', N'Thiện', N'Thái Văn', 63, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2410, N'vst110942', N'Thi', N'Lê Anh', 63, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2411, N'vst120993', N'Hải', N'Hoàng Thế', 63, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2412, N'vst121004', N'Hòa', N'Phạm Văn', 63, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2413, N'vst121006', N'Thành', N'Hồ Văn', 63, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2414, N'vst121021', N'Tú', N'Phạm Văn', 63, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2415, N'vst121039', N'Nam', N'Nguyễn Xuân', 63, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2416, N'vst121067', N'Anh', N'Mai Quý', 63, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2417, N'vst121101', N'Công', N'Nguyễn Chính', 63, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2418, N'vst131123', N'Bang', N'Hoàng Đình', 63, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2419, N'vst131126', N'Bằng', N'Phạm Văn', 63, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2420, N'vst131130', N'Trung', N'Thái Hoàng', 63, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2421, N'vst141241', N'Vũ', N'Nguyễn Thanh', 63, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2422, N'vst141244', N'Tấn', N'Nguyễn Văn', 63, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2423, N'vst151378', N'Hận', N'Huỳnh Văn Trường', 63, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2424, N'vst151380', N'Nghiệp', N'Nguyễn Thành', 63, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2425, N'vst151390', N'Hóa', N'Đinh Văn', 63, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2426, N'vst151403', N'Chinh', N'Phan Trọng', 63, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2427, N'vst161469', N'Đậm', N'Nguyễn Hoàng', 63, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2428, N'vst161475', N'Hiếu', N'Huỳnh Anh', 63, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2429, N'vst171521', N'Triết', N'Nguyễn Minh', 63, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2430, N'vst181599', N'Kiệt', N'Lý Nhân', 63, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2431, N'doai.tuan', N'Tuấn', N'Đoái Hoàng', 63, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2432, N'vst191633', N'Hóa', N'Nguyễn Đình', 63, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2433, N'vst201673', N'Lưu', N'Đậu Văn', 63, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2434, N'vst201674', N'Thanh', N'Nguyễn Trung', 63, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2435, N'vst201696', N'Quy', N'Ngô Hoàng', 63, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2436, N'vst201698', N'Đông', N'Hồ Xuân', 63, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2437, N'vst201703', N'Long', N'Đặng Thành', 63, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2438, N'phan.trihao', N'Hảo', N'Phan Trí', 63, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2439, N'dinh.quangngoc', N'Ngọc', N'Đinh Quang', 63, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2440, N'vst060135', N'Cường', N'Nguyễn Phan', 63, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2441, N'bui.tuan', N'Tuấn', N'Bùi Đình', 63, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2442, N'vst090327', N'Triều', N'Trần Hồ', 63, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2443, N'vst100357', N'Thanh', N'Lê Hồng', 63, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2444, N'vst100385', N'Đông', N'Trần Thành', 63, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2445, N'vst110823', N'Tuấn', N'Đinh Trọng', 63, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2446, N'vst110867', N'Hiền', N'Bùi Thị Thu', 63, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2447, N'nguyen.van', N'Vân', N'Nguyễn Thị Cẩm', 63, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2448, N'vst110902', N'Dung', N'Đặng Thị Mỹ', 63, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2449, N'vst110903', N'Hằng', N'Phạm Thị Lệ', 63, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2450, N'vst121005', N'Bảo', N'Tô Trần Thái', 63, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2451, N'vst121052', N'Thành', N'Bùi Văn', 63, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2452, N'vst131193', N'Hoàng', N'Mã Văn', 63, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2453, N'nguyen.thoa', N'Thoa', N'Nguyễn Thị Minh', 63, 1)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2454, N'vst171538', N'Quang', N'Trần Đức', 63, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2455, N'vst171548', N'Bi', N'Nguyễn Văn', 63, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2456, N'vst191616', N'Toàn', N'Nguyễn Minh', 63, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2457, N'nguyen.cuong', N'Cường', N'Nguyễn Quốc', 63, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2458, N'vst201670', N'Phục', N'Nguyễn Quang', 63, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2459, N'tran.thanhsinh', N'Sinh', N'Trần Thanh', 63, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2460, N'vst201695', N'Long', N'Nguyễn Hoàng', 63, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2461, N'anhhuy.le', N'Huy', N'Lê Anh', 72, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2462, N'dacdong', N'Đông', N'Nguyễn Đắc', 72, 1)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2463, N'duylh', N'Duy', N'Lê Huỳnh', 72, 2)
GO
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2464, N'hwangmangi', N'Gi', N'Hwang Man', 72, 1)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2465, N'daekwanoh', N'Kwan', N'Oh Daek', 72, 1)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2466, N'parkduckbae', N'Bae', N'Park Duck', 64, 1)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2467, N'seobyungil', N'Il', N'Seo Byung', 62, 1)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2468, N'jy.lee', N'Yeob', N'Lee Jong', 73, 1)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2469, N'dj.jeong', N'Jung', N'Jeong Dae', 69, 1)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2470, N'sonminwoo', N'Woo', N'Son Min', 69, 1)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2471, N'ch.lee', N'Hoan', N'Lee Chang', 71, 1)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2472, N'mj.ko', N'Jeong', N'Ko Moon', 71, 1)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2473, N'yb.kang', N'Bin', N'Kang Yoo', 60, 1)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2474, N'minseok.park', N'Seok', N'Park Min', 60, 1)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2475, N'yoon.jwajin', N'Jin', N'Yoon Jwa', 61, 1)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2476, N'ey.park', N'Young', N'Park Eun', 63, 1)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2477, N'jujaewon', N'Won', N'Ju Jae', 63, 1)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2478, N'yoondongwoo', N'Woo', N'Yoon Dong', 76, 1)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2479, N'jung.js', N'Su', N'Jung Jin', 74, 1)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2480, N'hanmyunghoon', N'Hoon', N'Han Myung', 75, 1)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2481, N'kim.hk', N'Kab', N'Kim Hyoung', 75, 1)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2482, N'mangihwang', N'Gi', N'Hwang Man', 72, 1)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2483, N'nguyendlthuan', N'Thuần', N'Nguyễn Đình Lê', 72, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2484, N'duythai', N'Thái', N'Nguyễn Duy', 72, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2485, N'tiennguyen', N'Tiến', N'Nguyễn Đức', 72, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2486, N'quinguyen', N'Quí', N'Nguyễn Xuân', 72, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2487, N'le.thithanh', N'Thanh', N'Lê Thị', 69, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2488, N'do.lybang', N'Bằng', N'Đỗ Lý', 67, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2489, N'nguyen.ngoctuyet', N'Tuyết', N'Nguyễn Ngọc', 60, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2490, N'nguyen.maithuong', N'Thương', N'Nguyễn Mai', 62, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2491, N'nguyen.thientrang', N'Trang', N'Nguyễn Hoàng Thiên', 70, 2)
INSERT [dbo].[Employee] ([Id], [EpLiteId], [FirstName], [LastName], [DepartmentId], [RoleId]) VALUES (2492, N'nguyen.ngoctuan', N'Tuấn', N'Nguyễn Ngọc', 64, 2)
SET IDENTITY_INSERT [dbo].[Employee] OFF
SET IDENTITY_INSERT [dbo].[FilePermission] ON 

INSERT [dbo].[FilePermission] ([Id], [Permission]) VALUES (1, N'Read')
INSERT [dbo].[FilePermission] ([Id], [Permission]) VALUES (2, N'Edit')
SET IDENTITY_INSERT [dbo].[FilePermission] OFF
SET IDENTITY_INSERT [dbo].[FileStatus] ON 

INSERT [dbo].[FileStatus] ([Id], [Status]) VALUES (1, N'Created')
INSERT [dbo].[FileStatus] ([Id], [Status]) VALUES (2, N'Modified')
SET IDENTITY_INSERT [dbo].[FileStatus] OFF
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([Id], [Name]) VALUES (1, N'Manager')
INSERT [dbo].[Roles] ([Id], [Name]) VALUES (2, N'Staff')
SET IDENTITY_INSERT [dbo].[Roles] OFF
INSERT [dbo].[SpecialDepartment] ([DepartmentId], [DirectoryId]) VALUES (73, 2)
INSERT [dbo].[SpecialDepartment] ([DepartmentId], [DirectoryId]) VALUES (73, 3)
INSERT [dbo].[SpecialDepartment] ([DepartmentId], [DirectoryId]) VALUES (73, 4)
INSERT [dbo].[SpecialDepartment] ([DepartmentId], [DirectoryId]) VALUES (73, 5)
INSERT [dbo].[SpecialDepartment] ([DepartmentId], [DirectoryId]) VALUES (73, 6)
INSERT [dbo].[SpecialDepartment] ([DepartmentId], [DirectoryId]) VALUES (73, 7)
INSERT [dbo].[SpecialDepartment] ([DepartmentId], [DirectoryId]) VALUES (73, 8)
INSERT [dbo].[SpecialDepartment] ([DepartmentId], [DirectoryId]) VALUES (73, 9)
INSERT [dbo].[SpecialDepartment] ([DepartmentId], [DirectoryId]) VALUES (73, 10)
INSERT [dbo].[SpecialDepartment] ([DepartmentId], [DirectoryId]) VALUES (73, 11)
INSERT [dbo].[SpecialDepartment] ([DepartmentId], [DirectoryId]) VALUES (73, 12)
INSERT [dbo].[SpecialDepartment] ([DepartmentId], [DirectoryId]) VALUES (73, 13)
INSERT [dbo].[SpecialDepartment] ([DepartmentId], [DirectoryId]) VALUES (73, 14)
INSERT [dbo].[SpecialDepartment] ([DepartmentId], [DirectoryId]) VALUES (73, 15)
INSERT [dbo].[SpecialDepartment] ([DepartmentId], [DirectoryId]) VALUES (73, 16)
INSERT [dbo].[SpecialDepartment] ([DepartmentId], [DirectoryId]) VALUES (73, 17)
INSERT [dbo].[SpecialDepartment] ([DepartmentId], [DirectoryId]) VALUES (73, 18)
INSERT [dbo].[SpecialDepartment] ([DepartmentId], [DirectoryId]) VALUES (74, 7)
INSERT [dbo].[SpecialDepartment] ([DepartmentId], [DirectoryId]) VALUES (74, 16)
INSERT [dbo].[SpecialDepartment] ([DepartmentId], [DirectoryId]) VALUES (74, 17)
INSERT [dbo].[SpecialDepartment] ([DepartmentId], [DirectoryId]) VALUES (75, 3)
INSERT [dbo].[SpecialDepartment] ([DepartmentId], [DirectoryId]) VALUES (75, 9)
INSERT [dbo].[SpecialDepartment] ([DepartmentId], [DirectoryId]) VALUES (75, 11)
INSERT [dbo].[SpecialDepartment] ([DepartmentId], [DirectoryId]) VALUES (75, 12)
INSERT [dbo].[SpecialDepartment] ([DepartmentId], [DirectoryId]) VALUES (75, 15)
INSERT [dbo].[SpecialDepartment] ([DepartmentId], [DirectoryId]) VALUES (75, 18)
INSERT [dbo].[SpecialDepartment] ([DepartmentId], [DirectoryId]) VALUES (76, 5)
INSERT [dbo].[SpecialDepartment] ([DepartmentId], [DirectoryId]) VALUES (76, 8)
INSERT [dbo].[SpecialDepartment] ([DepartmentId], [DirectoryId]) VALUES (76, 14)
SET ANSI_PADDING ON

GO
/****** Object:  Index [UIdx_Directory_Name_ParentId]    Script Date: 10/03/2022 08:47:51 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UIdx_Directory_Name_ParentId] ON [dbo].[Directory]
(
	[Name] ASC,
	[ParentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UIdx_Employee_EpLiteId]    Script Date: 10/03/2022 08:47:51 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UIdx_Employee_EpLiteId] ON [dbo].[Employee]
(
	[EpLiteId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UIdx_FileHistory_FileId_Version]    Script Date: 10/03/2022 08:47:51 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UIdx_FileHistory_FileId_Version] ON [dbo].[FileHistory]
(
	[FileId] ASC,
	[Version] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UIdx_FileInfo_FileName_DirectoryId]    Script Date: 10/03/2022 08:47:51 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UIdx_FileInfo_FileName_DirectoryId] ON [dbo].[FileInfo]
(
	[Name] ASC,
	[DirectoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [UIdx_FileShare_FileId_EmployeeId]    Script Date: 10/03/2022 08:47:51 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UIdx_FileShare_FileId_EmployeeId] ON [dbo].[FileShare]
(
	[FileId] ASC,
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [UIdx_Trash_FileId]    Script Date: 10/03/2022 08:47:51 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UIdx_Trash_FileId] ON [dbo].[Trash]
(
	[FileId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[FileFavorite] ADD  CONSTRAINT [Df_FileFavorite_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[FileHistory] ADD  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [dbo].[FileHistory] ADD  DEFAULT ('0.1') FOR [Version]
GO
ALTER TABLE [dbo].[FileImportant] ADD  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[FileShare] ADD  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[Trash] ADD  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[Trash] ADD  DEFAULT (getdate()) FOR [DeletedDate]
GO
ALTER TABLE [dbo].[Directory]  WITH CHECK ADD  CONSTRAINT [Fk_Directory_Department] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Department] ([Id])
GO
ALTER TABLE [dbo].[Directory] CHECK CONSTRAINT [Fk_Directory_Department]
GO
ALTER TABLE [dbo].[Directory]  WITH CHECK ADD  CONSTRAINT [Fk_Directory_Seft] FOREIGN KEY([ParentId])
REFERENCES [dbo].[Directory] ([Id])
GO
ALTER TABLE [dbo].[Directory] CHECK CONSTRAINT [Fk_Directory_Seft]
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [Fk_Employee_Department] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Department] ([Id])
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [Fk_Employee_Department]
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [Fk_Employee_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([Id])
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [Fk_Employee_Roles]
GO
ALTER TABLE [dbo].[FileFavorite]  WITH CHECK ADD  CONSTRAINT [Fk_FileFavorite_Employee] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employee] ([Id])
GO
ALTER TABLE [dbo].[FileFavorite] CHECK CONSTRAINT [Fk_FileFavorite_Employee]
GO
ALTER TABLE [dbo].[FileFavorite]  WITH CHECK ADD  CONSTRAINT [Fk_FileFavorite_FileInfo] FOREIGN KEY([FileId])
REFERENCES [dbo].[FileInfo] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[FileFavorite] CHECK CONSTRAINT [Fk_FileFavorite_FileInfo]
GO
ALTER TABLE [dbo].[FileHistory]  WITH CHECK ADD  CONSTRAINT [Fk_FileHistory_Employee] FOREIGN KEY([Modifier])
REFERENCES [dbo].[Employee] ([Id])
GO
ALTER TABLE [dbo].[FileHistory] CHECK CONSTRAINT [Fk_FileHistory_Employee]
GO
ALTER TABLE [dbo].[FileHistory]  WITH CHECK ADD  CONSTRAINT [Fk_FileHistory_FileInfo] FOREIGN KEY([FileId])
REFERENCES [dbo].[FileInfo] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[FileHistory] CHECK CONSTRAINT [Fk_FileHistory_FileInfo]
GO
ALTER TABLE [dbo].[FileHistory]  WITH CHECK ADD  CONSTRAINT [Fk_FileHistory_FileStatus] FOREIGN KEY([StatusId])
REFERENCES [dbo].[FileStatus] ([Id])
GO
ALTER TABLE [dbo].[FileHistory] CHECK CONSTRAINT [Fk_FileHistory_FileStatus]
GO
ALTER TABLE [dbo].[FileImportant]  WITH CHECK ADD  CONSTRAINT [Fk_FileImportant_Employee] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employee] ([Id])
GO
ALTER TABLE [dbo].[FileImportant] CHECK CONSTRAINT [Fk_FileImportant_Employee]
GO
ALTER TABLE [dbo].[FileImportant]  WITH CHECK ADD  CONSTRAINT [Fk_FileImportant_FileInfo] FOREIGN KEY([FileId])
REFERENCES [dbo].[FileInfo] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[FileImportant] CHECK CONSTRAINT [Fk_FileImportant_FileInfo]
GO
ALTER TABLE [dbo].[FileInfo]  WITH CHECK ADD  CONSTRAINT [Fk_FileInfo_Directory] FOREIGN KEY([DirectoryId])
REFERENCES [dbo].[Directory] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[FileInfo] CHECK CONSTRAINT [Fk_FileInfo_Directory]
GO
ALTER TABLE [dbo].[FileInfo]  WITH CHECK ADD  CONSTRAINT [Fk_FileInfo_Employee] FOREIGN KEY([Owner])
REFERENCES [dbo].[Employee] ([Id])
GO
ALTER TABLE [dbo].[FileInfo] CHECK CONSTRAINT [Fk_FileInfo_Employee]
GO
ALTER TABLE [dbo].[FileShare]  WITH CHECK ADD  CONSTRAINT [Fk_FileShare_Employee] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employee] ([Id])
GO
ALTER TABLE [dbo].[FileShare] CHECK CONSTRAINT [Fk_FileShare_Employee]
GO
ALTER TABLE [dbo].[FileShare]  WITH CHECK ADD  CONSTRAINT [Fk_FileShare_FileInfo] FOREIGN KEY([FileId])
REFERENCES [dbo].[FileInfo] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[FileShare] CHECK CONSTRAINT [Fk_FileShare_FileInfo]
GO
ALTER TABLE [dbo].[FileShare]  WITH CHECK ADD  CONSTRAINT [Fk_FileShare_FilePermission] FOREIGN KEY([Permission])
REFERENCES [dbo].[FilePermission] ([Id])
GO
ALTER TABLE [dbo].[FileShare] CHECK CONSTRAINT [Fk_FileShare_FilePermission]
GO
ALTER TABLE [dbo].[SpecialDepartment]  WITH CHECK ADD  CONSTRAINT [Fk_SpecialDepartment_Department] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Department] ([Id])
GO
ALTER TABLE [dbo].[SpecialDepartment] CHECK CONSTRAINT [Fk_SpecialDepartment_Department]
GO
ALTER TABLE [dbo].[SpecialDepartment]  WITH CHECK ADD  CONSTRAINT [Fk_SpecialDepartment_Directory] FOREIGN KEY([DirectoryId])
REFERENCES [dbo].[Directory] ([Id])
GO
ALTER TABLE [dbo].[SpecialDepartment] CHECK CONSTRAINT [Fk_SpecialDepartment_Directory]
GO
ALTER TABLE [dbo].[Trash]  WITH CHECK ADD  CONSTRAINT [Fk_Trash_FileInfo] FOREIGN KEY([FileId])
REFERENCES [dbo].[FileInfo] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Trash] CHECK CONSTRAINT [Fk_Trash_FileInfo]
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetDirFromDeptId]    Script Date: 10/03/2022 08:47:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetDirFromDeptId] @DeptId INT
AS
BEGIN
	IF EXISTS (SELECT DepartmentId FROM SpecialDepartment WHERE DepartmentId = @DeptId)
	BEGIN
		;WITH Cte(Id, Name, ParentId, DepartmentId) AS
		(
			SELECT p.Id, p.Name, p.ParentId, p.DepartmentId
			FROM Directory p
			WHERE p.Id IN (SELECT DirectoryId
						   FROM SpecialDepartment
						   WHERE DepartmentId = @DeptId)
			UNION ALL
			SELECT c.Id, c.Name, c.ParentId, c.DepartmentId
			FROM Directory c JOIN Cte cte ON c.ParentId = cte.Id
		)
		SELECT Id, Name, ParentId, DepartmentId
		FROM Cte
	END
	ELSE
	BEGIN
		;WITH Cte(Id, Name, ParentId, DepartmentId) AS
		(
			SELECT p.Id, p.Name, p.ParentId, p.DepartmentId
			FROM Directory p
			WHERE p.DepartmentId = @DeptId
			UNION ALL
			SELECT c.Id, c.Name, c.ParentId, c.DepartmentId
			FROM Directory c JOIN Cte cte ON c.ParentId = cte.Id
		)
		SELECT Id, Name, ParentId, DepartmentId
		FROM Cte
	END
END

GO
/****** Object:  StoredProcedure [dbo].[Proc_GetDirFromFileId]    Script Date: 10/03/2022 08:47:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetDirFromFileId] @FileId uniqueidentifier
AS
BEGIN
	;WITH Cte AS
	(
		SELECT Id, Name, ParentId, DepartmentId
		FROM Directory 
		WHERE ParentId IS NULL
		UNION ALL
		SELECT t.Id, CAST(Cte.Name +'/'+ t.Name AS NVARCHAR(200)), t.ParentId, t.DepartmentId
		FROM Directory t
		INNER JOIN Cte ON t.ParentId = Cte.Id
	)
	SELECT c.*
	FROM Cte c JOIN FileInfo f ON c.Id = f.DirectoryId
	WHERE f.Id = @FileId
END

GO
/****** Object:  StoredProcedure [dbo].[Proc_GetDirFromId]    Script Date: 10/03/2022 08:47:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetDirFromId] @DirId INT
AS
BEGIN
	;WITH Cte AS
	(
		SELECT Id, Name, ParentId, DepartmentId
		FROM Directory 
		WHERE ParentId IS NULL
		UNION ALL
		SELECT t.Id, CAST(Cte.Name +'/'+ t.Name AS NVARCHAR(200)), t.ParentId, t.DepartmentId
		FROM Directory t
		INNER JOIN Cte ON t.ParentId = Cte.Id
	)
	SELECT c.*
	FROM Cte c
	WHERE c.Id = @DirId
END


GO
/****** Object:  StoredProcedure [dbo].[Proc_GetDirFromPath]    Script Date: 10/03/2022 08:47:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetDirFromPath] @Path NVARCHAR(200)
AS
BEGIN
	;WITH Cte AS
	(
		SELECT Id, Name, ParentId, DepartmentId
		FROM Directory 
		WHERE ParentId IS NULL
		UNION ALL
		SELECT t.Id, CAST(Cte.Name +'/'+ t.Name AS NVARCHAR(200)), t.ParentId, t.DepartmentId
		FROM Directory t
		INNER JOIN Cte ON t.ParentId = Cte.Id
	)
	SELECT c.*
	FROM Cte c
	WHERE c.Name = @Path
END


GO
USE [master]
GO
ALTER DATABASE [ECM] SET  READ_WRITE 
GO
