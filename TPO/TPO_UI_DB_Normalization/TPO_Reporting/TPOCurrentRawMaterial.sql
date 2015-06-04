CREATE TABLE [dbo].[TPOCurrentRawMaterial]
(
	[ID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [PlantID] INT NOT NULL, 
	[LineID] NVARCHAR(10) NOT NULL,
    [RawMaterialReceivedID] INT NULL, 
    [DateEntered] DATETIME NOT NULL, 
    [EnteredBy] NVARCHAR(100) NOT NULL, 
    [LastModified] DATETIME NOT NULL, 
    [ModifiedBy] NVARCHAR(100) NOT NULL, 
    CONSTRAINT [FK_TPOCurrentRawMaterial_Plant] FOREIGN KEY ([PlantID]) REFERENCES [Plant]([ID]),
	CONSTRAINT [FK_TPOCurrentRawMaterial_RawMaterialReceived] FOREIGN KEY ([RawMaterialReceivedID]) REFERENCES [RawMaterialReceived]([ID])
)

--Suggested future index of Plant and Line

