CREATE TABLE [dbo].[Conge]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Date_debut] DATE NOT NULL, 
    [Date_fin] DATE NOT NULL, 
    [ConfirmeParSquad] BIT NOT NULL, 
    [REF_TypeCongeID] INT NULL, 
    [REF_StatutCongeID] INT NULL, 
    [UtilisateurID] INT NULL, 
    CONSTRAINT [FK_Conge_ToTypeConge] FOREIGN KEY ([REF_TypeCongeID]) REFERENCES [REF_TypeConge]([Id]), 
    CONSTRAINT [FK_Conge_ToREF_StatutConge] FOREIGN KEY ([REF_StatutCongeID]) REFERENCES [REF_StatutConge]([Id]), 
    CONSTRAINT [FK_Conge_ToUtilisateur] FOREIGN KEY ([UtilisateurID]) REFERENCES [Utilisateur]([Id]) 
)
