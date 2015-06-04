CREATE TABLE [dbo].[User]
(
	[ID] INT NOT NULL PRIMARY KEY, 
    [PlantID] INT NOT NULL, 
    [Username] NVARCHAR(50) NULL, 
    [FullName] NVARCHAR(100) NOT NULL, 
    [DateEntered] DATETIME NOT NULL, 
    [EnteredBy] NVARCHAR(100) NOT NULL, 
    [LastModified] DATETIME NOT NULL, 
    [ModifiedBy] NVARCHAR(100) NOT NULL, 
    CONSTRAINT [FK_User_Plant] FOREIGN KEY ([PlantID]) REFERENCES [Plant]([ID])
)
