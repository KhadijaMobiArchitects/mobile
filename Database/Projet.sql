CREATE TABLE [dbo].[Projet]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Nom ] NVARCHAR(100) NOT NULL, 
    [Pourcentage] REAL NOT NULL DEFAULT 0, 
    [DateCreation] DATE NOT NULL, 
    [DateFin] DATE NULL, 
    [SquadID] INT NULL, 
    [REF_SituationProjetID] INT NULL, 
    CONSTRAINT [FK_Projet_ToSquad] FOREIGN KEY ([SquadID]) REFERENCES [Squad]([Id]), 
    CONSTRAINT [FK_Projet_ToREF_SituationProjet] FOREIGN KEY ([REF_SituationProjetID]) REFERENCES [REF_SituationProjet]([Id]) 
)
