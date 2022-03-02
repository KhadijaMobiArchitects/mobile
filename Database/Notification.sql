CREATE TABLE [dbo].[Notification]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Titre] NVARCHAR(50) NOT NULL, 
    [Message] NVARCHAR(MAX) NOT NULL, 
    [Date] DATETIME NOT NULL, 
    [EstLis] BIT NOT NULL, 
    [TraçabiliteId] INT NOT NULL, 
    CONSTRAINT [FK_Notification_ToTraçabilite] FOREIGN KEY ([TraçabiliteId]) REFERENCES [Traçabilite]([Id])
)
