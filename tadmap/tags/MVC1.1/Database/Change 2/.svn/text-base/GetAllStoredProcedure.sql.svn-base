USE [tadmap]
GO
/****** Object:  StoredProcedure [dbo].[GetUserImages]    Script Date: 11/25/2008 22:17:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GetImages
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[GetAllImages] 
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
		Images		

END
