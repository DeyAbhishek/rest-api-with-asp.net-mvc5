﻿CREATE TABLE [dbo].[RawMaterialQC]
(
	[ID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [PlantID] INT NOT NULL, 
    [QCTechUserID] INT NULL, 
    [RawMaterialID] INT NOT NULL, 
    [RawMaterialLotID] NCHAR(10) NULL, 
    [VisualInspection] BIT NULL, 
    [SpecGrav] FLOAT NULL, 
    [ColorCoA] FLOAT NULL, 
    [ColorFS] FLOAT NULL, 
    [MFCoA] FLOAT NULL, 
    [MFFS] FLOAT NULL, 
    [ACCoA] FLOAT NULL, 
    [ACFS] FLOAT NULL, 
    [MoistCoA] FLOAT NULL, 
    [MoistFS] FLOAT NULL, 
    [CBCoA] FLOAT NULL, 
    [CBFS] FLOAT NULL, 
    [BoxCarTested] NVARCHAR(50) NULL, 
    [Comments] NVARCHAR(MAX) NULL, 
    [DateEntered] DATETIME NOT NULL, 
    [EnteredBy] NVARCHAR(100) NOT NULL, 
    [LastModified] DATETIME NOT NULL, 
    [ModifiedBy] NVARCHAR(100) NOT NULL, 
    CONSTRAINT [FK_RawMaterialQC_Plant] FOREIGN KEY ([PlantID]) REFERENCES [Plant]([ID]),
	CONSTRAINT [FK_RawMaterialQC_QCTechUser] FOREIGN KEY ([QCTechUserID]) REFERENCES [User]([ID])
)