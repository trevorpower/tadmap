USE [tadmap]
GO
/****** Object:  StoredProcedure [dbo].[GetUserImages]    Script Date: 11/09/2008 15:02:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GetImages
-- Create date: 
-- Description:	
-- =============================================
ALTER PROCEDURE [dbo].[GetUserImages] 
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
		[TileSize],
		[DateAdded],
		[ImageSet]
	FROM
		Images INNER JOIN OpenIds
	ON
		Images.UserId = OpenIds.UserId
	WHERE
		OpenIds.OpenIdUrl = @OpenIdUrl
		

END
