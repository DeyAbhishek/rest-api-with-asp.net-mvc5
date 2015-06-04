CREATE TABLE [dbo].[TPOCurrentScrim]
(
	[ID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [PlantID] INT NOT NULL, 
    [Scrim1RollID] INT NULL, 
    [Scrim1TypeID] INT NULL, 
	[Scrim2RollID] INT NULL,
	[Scrim2TypeID] INT NULL, 
	[FleeceRollID] INT NULL,
	[FleeceTypeID] INT NULL, 
	[LineID] NVARCHAR(10) NOT NULL,
    [ScrimPos] NVARCHAR(2) NOT NULL, 
    [DateEntered] DATETIME NOT NULL, 
    [EnteredBy] NVARCHAR(100) NOT NULL, 
    [LastModified] DATETIME NOT NULL, 
    [ModifiedBy] NVARCHAR(100) NOT NULL, 
    CONSTRAINT [FK_TPOCurrentScrim_Plant] FOREIGN KEY ([PlantID]) REFERENCES [Plant]([ID]),
	CONSTRAINT [FK_TPOCurrentScrim_Scrim1Roll] FOREIGN KEY ([Scrim1RollID]) REFERENCES [ScrimRoll]([ID]),
	CONSTRAINT [FK_TPOCurrentScrim_Scrim1Type] FOREIGN KEY ([Scrim1TypeID]) REFERENCES [ScrimType]([ID]),
	CONSTRAINT [FK_TPOCurrentScrim_Scrim2Roll] FOREIGN KEY ([Scrim2RollID]) REFERENCES [ScrimRoll]([ID]),
	CONSTRAINT [FK_TPOCurrentScrim_Scrim2Type] FOREIGN KEY ([Scrim2TypeID]) REFERENCES [ScrimType]([ID]),
	CONSTRAINT [FK_TPOCurrentScrim_FleeceRoll] FOREIGN KEY ([FleeceRollID]) REFERENCES [ScrimRoll]([ID]),
	CONSTRAINT [FK_TPOCurrentScrim_FleeceType] FOREIGN KEY ([FleeceTypeID]) REFERENCES [ScrimType]([ID])
)

GO

CREATE UNIQUE INDEX [IX_TPOCurrentScrim_LineID] ON [dbo].[TPOCurrentScrim] ([LineID])
