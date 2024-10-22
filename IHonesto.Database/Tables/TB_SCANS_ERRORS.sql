﻿CREATE TABLE [dbo].[TB_SCANS_ERRORS]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
	[ProductId] INT NULL, 
	[PlataformId] INT NULL,
	[StrategyId] INT NULL,
    [Message] VARCHAR(MAX) NOT NULL,
    [Metadados] VARCHAR(MAX) NULL,
    [CreatedAt] DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT [FK_TB_SCANS_ERRORS_PRODUCTS] FOREIGN KEY ([ProductId]) REFERENCES [TB_PRODUCTS]([Id]),
    CONSTRAINT [FK_TB_SCANS_ERRORS_PLATAFORMS] FOREIGN KEY ([PlataformId]) REFERENCES [TB_PLATAFORMS]([Id]),
    CONSTRAINT [FK_TB_SCANS_ERRORS_STRATEGIES] FOREIGN KEY ([StrategyId]) REFERENCES [TB_STRATEGIES]([Id])
)
