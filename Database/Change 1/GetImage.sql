USE [tadmap]
GO
/****** Object:  StoredProcedure [dbo].[GetImage]    Script Date: 11/09/2008 14:10:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Name
-- Create date: 
-- Description:	
-- =============================================
ALTER PROCEDURE [dbo].[GetImage] 
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
		[TileSize],
		[DateAdded],
		[ImageSet]
	FROM
		Images
	WHERE
		Id = @ImageId
END
