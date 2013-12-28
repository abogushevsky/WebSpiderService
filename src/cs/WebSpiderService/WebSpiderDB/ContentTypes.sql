CREATE TABLE [dbo].[ContentTypes]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	ContentType nvarchar (256) NOT NULL,
	FileExtension nvarchar (16) NULL
)
