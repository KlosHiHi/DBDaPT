--Table1
USE [ispp3101]
GO

/****** Object:  Table [dbo].[CinemaUser]    Script Date: 19.11.2025 21:39:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CinemaUser](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Login] [nchar](50) NOT NULL,
	[PasswordHash] [nchar](200) NOT NULL,
	[FailTryAuthQuantity] [int] NOT NULL,
	[UnlockDate] [datetime2](7) NULL,
	[RoleId] [int] NOT NULL,
 CONSTRAINT [PK_CinemaUser] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[CinemaUser] ADD  CONSTRAINT [DF_CinemaUser_FailTryAuthCount]  DEFAULT ((0)) FOR [FailTryAuthQuantity]
GO

ALTER TABLE [dbo].[CinemaUser]  WITH CHECK ADD  CONSTRAINT [FK_CinemaUser_CinemaUserRole] FOREIGN KEY([RoleId])
REFERENCES [dbo].[CinemaUserRole] ([RoleId])
GO

ALTER TABLE [dbo].[CinemaUser] CHECK CONSTRAINT [FK_CinemaUser_CinemaUserRole]
GO

--Table2
USE [ispp3101]
GO

/****** Object:  Table [dbo].[CinemaPrivilege]    Script Date: 19.11.2025 21:39:30 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CinemaPrivilege](
	[PrivilegeId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_CinemaPrivilege] PRIMARY KEY CLUSTERED 
(
	[PrivilegeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

--Table3
USE [ispp3101]
GO

/****** Object:  Table [dbo].[CinemaRolePrivilege]    Script Date: 19.11.2025 21:39:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CinemaRolePrivilege](
	[RoleId] [int] NOT NULL,
	[PrivilegeId] [int] NOT NULL,
 CONSTRAINT [PK_CinemaRolePrivilege] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC,
	[PrivilegeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[CinemaRolePrivilege]  WITH CHECK ADD  CONSTRAINT [FK_CinemaRolePrivilege_CinemaPrivilege] FOREIGN KEY([PrivilegeId])
REFERENCES [dbo].[CinemaPrivilege] ([PrivilegeId])
GO

ALTER TABLE [dbo].[CinemaRolePrivilege] CHECK CONSTRAINT [FK_CinemaRolePrivilege_CinemaPrivilege]
GO

ALTER TABLE [dbo].[CinemaRolePrivilege]  WITH CHECK ADD  CONSTRAINT [FK_CinemaRolePrivilege_CinemaUserRole] FOREIGN KEY([RoleId])
REFERENCES [dbo].[CinemaUserRole] ([RoleId])
GO

ALTER TABLE [dbo].[CinemaRolePrivilege] CHECK CONSTRAINT [FK_CinemaRolePrivilege_CinemaUserRole]
GO


--Table 4
USE [ispp3101]
GO

/****** Object:  Table [dbo].[CinemaUserRole]    Script Date: 19.11.2025 21:40:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CinemaUserRole](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](20) NULL,
 CONSTRAINT [PK_CinemaUserRole] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


