CREATE TABLE [dbo].[Traçabilite]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [DateCreation] DATE NOT NULL, 
    [DateMiseAJour] DATE NOT NULL, 
    [CreePar] NVARCHAR(50) NOT NULL, 
    [SupprimePar] NVARCHAR(50) NOT NULL, 
    [Descriptif] NVARCHAR(50) NOT NULL, 
    [REF_TypeTraçabiliteId] INT NOT NULL, 
    [REF_DecisionId] INT NOT NULL, 
    CONSTRAINT [FK_Traçabilite_ToREF_TypeTraçabilite] FOREIGN KEY ([REF_TypeTraçabiliteId]) REFERENCES [REF_TypeTraçabilite]([Id]), 
    CONSTRAINT [FK_Traçabilite_ToDecision] FOREIGN KEY ([REF_DecisionId]) REFERENCES [REF_Decision]([Id]) 
)
