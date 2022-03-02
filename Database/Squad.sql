CREATE TABLE [dbo].[Squad]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UtilisateurId] INT NOT NULL, 
    CONSTRAINT [FK_Squad_ToUtilisateur] FOREIGN KEY ([UtilisateurId]) REFERENCES [Utilisateur]([Id])
)
