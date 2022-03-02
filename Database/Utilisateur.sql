CREATE TABLE [dbo].[Utilisateur]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Nom] NVARCHAR(50) NULL, 
    [Prenom] NVARCHAR(50) NULL, 
    [Email] NVARCHAR(100) NULL, 
    [ImageUrl] NVARCHAR(MAX) NULL, 
    [Tel] NVARCHAR(MAX) NULL, 
    [NomUtilisateur] NVARCHAR(100) NULL, 
    [MotDePasse] NVARCHAR(100) NULL, 
    [REF_FonctionId] INT NULL, 
    [REF_RoleId] INT NULL, 
    CONSTRAINT [FK_Utilisateur_ToREF_Fonction] FOREIGN KEY ([REF_FonctionId]) REFERENCES [REF_Fonction]([Id]), 
    CONSTRAINT [FK_Utilisateur_ToREF_Role] FOREIGN KEY ([REF_RoleId]) REFERENCES [REF_Role]([Id]) 
)
