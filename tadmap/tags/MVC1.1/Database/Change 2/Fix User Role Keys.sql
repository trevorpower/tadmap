/*
   25 November 200821:38:27
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
ALTER TABLE dbo.UserRoles
	DROP CONSTRAINT PK_UserRoles_1
GO
ALTER TABLE dbo.UserRoles ADD CONSTRAINT
	PK_UserRoles_1 PRIMARY KEY CLUSTERED 
	(
	UserId,
	Role
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
COMMIT
