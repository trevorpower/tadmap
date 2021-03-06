/*
   09 November 200813:59:43
   User: 
   Server: TREVOR-PC\SQLEXPRESS
   Database: tadmap
   Application: 
*/

/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Images ADD
	DateAdded datetime NOT NULL CONSTRAINT DF_Images_DateAdded DEFAULT getdate(),
	ImageSet tinyint NOT NULL CONSTRAINT DF_Images_ImageSet DEFAULT 0
GO
COMMIT
