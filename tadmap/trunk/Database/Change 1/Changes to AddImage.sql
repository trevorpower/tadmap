USE [tadmap]
GO
/****** Object:  StoredProcedure [dbo].[AddImage]    Script Date: 11/09/2008 14:02:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Trevor Power
-- Create date: 
-- Description:	
-- =============================================
ALTER PROCEDURE [dbo].[AddImage] 
	-- Add the parameters for the stored procedure here
	@OpenIdUrl varchar(256),
	@Id UNIQUEIDENTIFIER,
	@Title nvarchar(256),
	@Description nvarchar(1024),
	@StorageKey nvarchar(512),
	@ZoomLevels INT,
	@TileSize INT,
	@ImageSet TINYINT
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

	INSERT INTO [Images](Id, Title, [Description], UserId, [Key], ZoomLevels, TileSize, DateAdded, ImageSet)
	VALUES (@Id, @Title, @Description, @UserId, @StorageKey, @ZoomLevels, @TileSize, GETDATE(), @ImageSet)

END
