CREATE TABLE [dbo].[MAP_Traçabilite_Projet]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [TraçabiliteId] INT NOT NULL, 
    [ProjetId] INT NOT NULL, 
    CONSTRAINT [FK_MAP_Traçabilite_Projet_ToTraçabilite] FOREIGN KEY ([TraçabiliteId]) REFERENCES [Traçabilite]([Id]), 
    CONSTRAINT [FK_MAP_Traçabilite_Projet_ToConge] FOREIGN KEY ([ProjetId]) REFERENCES [Projet]([Id])
)
