CREATE TABLE [dbo].[Links]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[ParentId] INT NULL,
	[Url] nvarchar (MAX) NOT NULL,
	[ContentTypeId] INT NOT NULL, 
    CONSTRAINT [FK_Links_ContentTypes] FOREIGN KEY ([ContentTypeId]) REFERENCES [ContentTypes]([Id]) 
)
