IF EXISTS (SELECT * from INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'TPOReworkRoll' AND COLUMN_NAME = 'ProductID')
BEGIN
	EXEC sys.sp_rename @objname = N'dbo.TPOReworkRoll.ProductID', @newname = 'TPOProductID', @objtype = 'COLUMN'
END
GO

IF EXISTS (select * from INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'TPOReworkRoll')
	AND NOT EXISTS (
    SELECT TOP 1 1
	FROM sys.objects
	WHERE type_desc LIKE '%CONSTRAINT'
	AND OBJECT_NAME(OBJECT_ID)='FK_TPOReworkRoll_ProdLines'
	AND OBJECT_NAME(parent_object_id) ='TPOReworkRoll'
)
BEGIN
	ALTER TABLE [TPOReworkRoll] 
	ADD CONSTRAINT FK_TPOReworkRoll_ProdLines FOREIGN KEY 
	(LineID) REFERENCES ProdLines (ID)
END
GO

IF EXISTS (select * from INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'TPOReworkRoll')
	AND NOT EXISTS (
    SELECT TOP 1 1
	FROM sys.objects
	WHERE type_desc LIKE '%CONSTRAINT'
	AND OBJECT_NAME(OBJECT_ID)='FK_TPOReworkRoll_TPOProducts'
	AND OBJECT_NAME(parent_object_id) ='TPOReworkRoll'
)
BEGIN
	-- Remove [TPOLineScrap] records that will not meet the new Foreign Key constraint.
	DELETE FROM dbo.TPOLineScrap 
	WHERE ReworkRollId IN (
		SELECT [Id]
		FROM [dbo].[TpoReworkRoll]
		WHERE [TPOProductId] NOT IN (SELECT [Id] FROM TPOProducts)
	)
	-- Remove [TpoReworkRoll] records that will not meet the new Foreign Key constraint.
	DELETE FROM [dbo].[TpoReworkRoll]
	WHERE [TPOProductId] NOT IN (SELECT [Id] FROM TPOProducts)

	ALTER TABLE [TPOReworkRoll] 
	ADD CONSTRAINT FK_TPOReworkRoll_TPOProducts FOREIGN KEY 
	(TPOProductID) REFERENCES TPOProducts (ID)
END
GO

IF EXISTS (select * from INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'TPOReworkRoll')
AND NOT EXISTS (SELECT * from INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'TPOReworkRoll' AND COLUMN_NAME = 'Width')
BEGIN
	ALTER TABLE [TPOReworkRoll]
	ADD [Width] FLOAT NOT NULL
END
GO

IF EXISTS (select * from INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'TPOReworkRoll')
AND NOT EXISTS (SELECT * from INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'TPOReworkRoll' AND COLUMN_NAME = 'WidthUoMID')
BEGIN
	ALTER TABLE [TPOReworkRoll]
	ADD [WidthUoMID] INT NOT NULL
END
GO

IF EXISTS (select * from INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'TPOReworkRoll')
	AND NOT EXISTS (
    SELECT TOP 1 1
	FROM sys.objects
	WHERE type_desc LIKE '%CONSTRAINT'
	AND OBJECT_NAME(OBJECT_ID)='FK_TPOReworkRoll_WidthUnitOfMeasure'
	AND OBJECT_NAME(parent_object_id) ='TPOReworkRoll'
)
BEGIN
	ALTER TABLE [TPOReworkRoll] 
	ADD CONSTRAINT FK_TPOReworkRoll_WidthUnitOfMeasure FOREIGN KEY 
	(WidthUoMID) REFERENCES UnitOfMeasure (ID)
END
GO