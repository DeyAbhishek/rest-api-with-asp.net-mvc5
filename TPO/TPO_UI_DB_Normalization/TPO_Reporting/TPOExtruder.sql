CREATE TABLE [dbo].[TPOExtruder]
(
	[ID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Code] NVARCHAR(5) NOT NULL, 
    [Description] NVARCHAR(50) NULL, 
    [DateEntered] DATETIME NOT NULL, 
    [EnteredBy] NVARCHAR(100) NOT NULL, 
    [LastModified] DATETIME NOT NULL, 
    [ModifiedBy] NVARCHAR(100) NOT NULL
)
