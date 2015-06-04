CREATE TABLE [dbo].[QCPrintSpecification]
(
	[PlantID] INT NOT NULL, 
    [TypeID] INT NOT NULL, 
    [Min] FLOAT NOT NULL, 
    [Max] FLOAT NOT NULL, 
    [DateEntered] DATETIME NOT NULL, 
    [EnteredBy] NVARCHAR(100) NOT NULL, 
    [LastModified] DATETIME NOT NULL, 
    [ModifiedBy] NVARCHAR(100) NOT NULL, 
    CONSTRAINT [FK_QCPrintSpecification_Plant] FOREIGN KEY ([PlantID]) REFERENCES [Plant]([ID]),
	CONSTRAINT [FK_QCPrintSpecification_QCPrintSpecificationType] FOREIGN KEY ([TypeID]) REFERENCES [QCPrintSpecificationType]([ID]), 
    PRIMARY KEY ([PlantID], [TypeID])
)
