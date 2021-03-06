USE [tadmap]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 11/02/2008 16:20:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRoles]    Script Date: 11/02/2008 16:20:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRoles](
	[UserId] [uniqueidentifier] NOT NULL,
	[Role] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_UserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OpenIds]    Script Date: 11/02/2008 16:20:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[OpenIds](
	[OpenIdUrl] [varchar](255) NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_OpenIds] PRIMARY KEY CLUSTERED 
(
	[OpenIdUrl] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Images]    Script Date: 11/02/2008 16:20:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Images](
	[Id] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](256) NOT NULL,
	[Description] [nvarchar](1024) NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[Key] [nvarchar](512) NOT NULL,
	[ZoomLevels] [smallint] NULL,
	[TileSize] [smallint] NULL,
 CONSTRAINT [PK_Picture] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[GetUserImages]    Script Date: 11/02/2008 16:20:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GetImages
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[GetUserImages] 
	-- Add the parameters for the stored procedure here
	@OpenIdUrl varchar(256)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT
		[Id],
		[Title],
		[Description],
		[Key],
		[ZoomLevels],
		[TileSize]
	FROM
		Images INNER JOIN OpenIds
	ON
		Images.UserId = OpenIds.UserId
	WHERE
		OpenIds.OpenIdUrl = @OpenIdUrl
		

END
GO
/****** Object:  StoredProcedure [dbo].[AddImage]    Script Date: 11/02/2008 16:20:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Trevor Power
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[AddImage] 
	-- Add the parameters for the stored procedure here
	@OpenIdUrl varchar(256),
	@Id UNIQUEIDENTIFIER,
	@Title nvarchar(256),
	@Description nvarchar(1024),
	@StorageKey nvarchar(512),
	@ZoomLevels INT,
	@TileSize INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @UserId UNIQUEIDENTIFIER
	SET @UserId = (
		SELECT UserId FROM OpenIds
		WHERE OpenIdUrl = @OpenIdUrl
	)

	INSERT INTO [Images](Id, Title, [Description], UserId, [Key], ZoomLevels, TileSize)
	VALUES (@Id, @Title, @Description, @UserId, @StorageKey, @ZoomLevels, @TileSize)

END
GO
/****** Object:  StoredProcedure [dbo].[GetUserRoles]    Script Date: 11/02/2008 16:20:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Trevor Power
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[GetUserRoles] 
	-- Add the parameters for the stored procedure here
	@OpenIdUrl varchar(255)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT [Role] as RoleName
	FROM OpenIds INNER JOIN UserRoles   
	ON OpenIds.UserId = UserRoles.UserId
	WHERE OpenIds.OpenIdUrl = @OpenIdUrl

END
GO
/****** Object:  StoredProcedure [dbo].[UpdateImageTitle]    Script Date: 11/02/2008 16:20:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Trevor Power
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[UpdateImageTitle] 
	-- Add the parameters for the stored procedure here
	@id UNIQUEIDENTIFIER, 
	@title NVARCHAR(255),
	@openIdUrl NVARCHAR(1024)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	UPDATE Images
	SET Title = @title
	WHERE Id = @id
	AND UserId = (
		SELECT UserId FROM OpenIds
		WHERE OpenIdUrl = @OpenIdUrl
	)	

END
GO
/****** Object:  StoredProcedure [dbo].[GetUserId]    Script Date: 11/02/2008 16:20:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Name
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[GetUserId] 
	-- Add the parameters for the stored procedure here
	@OpenIdUrl varchar(255) 
AS
BEGIN
	
	SELECT UserId FROM OpenIds
	WHERE OpenIdUrl = @OpenIdUrl

END
GO
/****** Object:  StoredProcedure [dbo].[GetOpenIdsByUser]    Script Date: 11/02/2008 16:20:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Name
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[GetOpenIdsByUser] 
	-- Add the parameters for the stored procedure here
	@UserId uniqueidentifier
AS
BEGIN
	
	SELECT OpenIdUrl FROM OpenIds
	WHERE UserId = @UserId

END
GO
/****** Object:  StoredProcedure [dbo].[AttachOpenId]    Script Date: 11/02/2008 16:20:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Trevor Power
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[AttachOpenId] 
	-- Add the parameters for the stored procedure here
	@OpenIdUrl varchar(255), 
	@UserId uniqueidentifier
AS
BEGIN

	INSERT INTO OpenIds VALUES (@OpenIdUrl, @UserId)


END
GO
/****** Object:  StoredProcedure [dbo].[DetachOpenId]    Script Date: 11/02/2008 16:20:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Name
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[DetachOpenId] 
	-- Add the parameters for the stored procedure here
	@OpenIdUrl varchar(255), 
	@UserId uniqueidentifier
AS
BEGIN

	DELETE FROM OpenIds
	WHERE OpenIdUrl = @OpenIdUrl
	AND UserId = @UserId		   

END
GO
/****** Object:  StoredProcedure [dbo].[DetachOpenIdByUser]    Script Date: 11/02/2008 16:20:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Trevor Power
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[DetachOpenIdByUser] 
	@UserId UNIQUEIDENTIFIER
AS
BEGIN

	DELETE FROM OpenIds
	WHERE UserId = @UserId

END
GO
/****** Object:  StoredProcedure [dbo].[GetUser]    Script Date: 11/02/2008 16:20:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Trevor Power
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[GetUser] 
	-- Add the parameters for the stored procedure here
	@OpenIdUrl varchar(255)
AS
BEGIN

	DECLARE @UserId UNIQUEIDENTIFIER

	SELECT @UserId = UserId FROM OpenIds
	WHERE OpenIdUrl = @OpenIdUrl

	SELECT @OpenIdUrl AS OpenId, [Name], [Id] FROM Users
	WHERE [Id] = @UserId

	SELECT Role
	FROM UserRoles INNER JOIN Users
	ON Users.Id = UserRoles.UserId
	WHERE [Id] = @UserId

END
GO
/****** Object:  StoredProcedure [dbo].[UpdateImageDescription]    Script Date: 11/02/2008 16:20:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Trevor Power
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[UpdateImageDescription] 
	-- Add the parameters for the stored procedure here
	@id UNIQUEIDENTIFIER, 
	@description NVARCHAR(1024),
	@openIdUrl NVARCHAR(1024)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	UPDATE Images
	SET Description = @description
	WHERE Id = @id
	AND UserId = (
		SELECT UserId FROM OpenIds
		WHERE OpenIdUrl = @OpenIdUrl
	)	

END
GO
/****** Object:  StoredProcedure [dbo].[GetImage]    Script Date: 11/02/2008 16:20:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Name
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[GetImage] 
	-- Add the parameters for the stored procedure here
	@ImageId UNIQUEIDENTIFIER
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT
		[Id],
		[Title],
		[Description],
		[Key],
		[ZoomLevels],
		[TileSize]
	FROM
		Images
	WHERE
		Id = @ImageId
END
GO
/****** Object:  StoredProcedure [dbo].[AddUser]    Script Date: 11/02/2008 16:20:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Trevor Power
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[AddUser] 
	@id UNIQUEIDENTIFIER
AS
BEGIN
	
	INSERT INTO Users
	(Id, [Name]) 
	VALUES 
	(@id, '')

	INSERT INTO UserRoles
	(UserId, [Role])
	VALUES
	(@id, 'Collector')
END
GO
/****** Object:  ForeignKey [FK_Images_Users]    Script Date: 11/02/2008 16:20:12 ******/
ALTER TABLE [dbo].[Images]  WITH CHECK ADD  CONSTRAINT [FK_Images_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Images] CHECK CONSTRAINT [FK_Images_Users]
GO
/****** Object:  ForeignKey [FK_OpenIds_Users]    Script Date: 11/02/2008 16:20:12 ******/
ALTER TABLE [dbo].[OpenIds]  WITH CHECK ADD  CONSTRAINT [FK_OpenIds_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[OpenIds] CHECK CONSTRAINT [FK_OpenIds_Users]
GO
/****** Object:  ForeignKey [FK_UserRoles_Users]    Script Date: 11/02/2008 16:20:12 ******/
ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_Users]
GO
