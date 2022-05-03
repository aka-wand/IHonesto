﻿CREATE TABLE [dbo].[TB_SCANS]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
	[ProductId] INT NOT NULL, 
	[PlataformId] INT NOT NULL,
	[StrategyId] INT NOT NULL,
	[Status] INT NOT NULL,
    [Price] VARCHAR(50) NULL,
    [Metadados] VARCHAR(MAX) NULL,
    [CreatedAt] DATETIME NOT NULL DEFAULT GETDATE(), 
    CONSTRAINT [FK_TB_SCANS_PRODUCTS] FOREIGN KEY ([ProductId]) REFERENCES [TB_PRODUCTS]([Id]),
    CONSTRAINT [FK_TB_SCANS_PLATAFORMS] FOREIGN KEY ([PlataformId]) REFERENCES [TB_PLATAFORMS]([Id]),
    CONSTRAINT [FK_TB_SCANS_STRATEGIES] FOREIGN KEY ([StrategyId]) REFERENCES [TB_STRATEGIES]([Id])
)