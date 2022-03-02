CREATE TABLE [dbo].[MAP_Traçabilite_Conge]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [TraçabiliteId] INT NOT NULL, 
    [CongeId] INT NOT NULL, 
    CONSTRAINT [FK_MAP_Traçabilite_Conge_ToTraçabilite] FOREIGN KEY ([TraçabiliteId]) REFERENCES [Traçabilite]([Id]), 
    CONSTRAINT [FK_MAP_Traçabilite_Conge_ToConge] FOREIGN KEY ([CongeId]) REFERENCES [Conge]([Id])
)
