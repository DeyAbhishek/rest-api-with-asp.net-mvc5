/*
	EXAMPLE "PRE" script.

	The PRE scripts are to be used when a change cannot be supported by making changes to the database project.  This should not happen often.

	This script file serves as an example of formatting. Note that you MUST ALWAYS check for existence of tables/columns/constraints before making DDL changes. There is a second template for stored procedures.

	A good test is, could this script be re-run over and over without causing an exception? if running the script twice will cause an error, correct it before adding to the pre/post folder.

	NAMING:
		###_[ObjectType]_[ObjectName]_[Action].sql
		Scripts will be run in numeric order.
		ex: 001_tbl_WorkOrderShiftData_Alter.sql - the first script to be executed , alters the schema of the WorkOrderShiftData table.
		ex: 002_sproc_MySproc_Create.sql - the second script to be executed , creates the stored procedure "MySproc".

*/
-- DROP COLUMNS - Verify that they already exist.
IF EXISTS (SELECT * from INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'WorkOrderShiftData' AND COLUMN_NAME = 'ProductionShift')
BEGIN
	ALTER TABLE [WorkOrderShiftData]
	DROP COLUMN [ProductionShift]
END
GO

IF EXISTS (SELECT * from INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'WorkOrderShiftData' AND COLUMN_NAME = 'EnteredDate')
BEGIN
	ALTER TABLE [WorkOrderShiftData]
	DROP COLUMN [EnteredDate]
END
GO

IF EXISTS (SELECT * from INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'WorkOrderShiftData' AND COLUMN_NAME = 'ModifiedDate')
BEGIN
	ALTER TABLE [WorkOrderShiftData]
	DROP COLUMN [ModifiedDate]
END
GO
	
-- ADD columns - test that they DO NOT already exist, AND that the table DOES
IF EXISTS (select * from INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'WorkOrderShiftData')
AND NOT EXISTS (SELECT * from INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'WorkOrderShiftData' AND COLUMN_NAME = 'ShiftID')
BEGIN
	ALTER TABLE [WorkOrderShiftData]
	ADD [ShiftID] INT NOT NULL
END
GO

IF EXISTS (select * from INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'WorkOrderShiftData')
AND NOT EXISTS (SELECT * from INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'WorkOrderShiftData' AND COLUMN_NAME = 'ShiftID')
BEGIN
	ALTER TABLE [WorkOrderShiftData]
	ADD [DateEntered] DATETIME NOT NULL
END
GO

IF EXISTS (select * from INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'WorkOrderShiftData')
AND NOT EXISTS (SELECT * from INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'WorkOrderShiftData' AND COLUMN_NAME = 'ShiftID')
BEGIN
	ALTER TABLE [WorkOrderShiftData]
	ADD [LastModified] DATETIME NOT NULL
END
GO

-- ADD constraint - verify it does not already exist, AND that the table DOES
IF EXISTS (select * from INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'WorkOrderShiftData')
	AND NOT EXISTS (
    SELECT TOP 1 1
	FROM sys.objects
	WHERE type_desc LIKE '%CONSTRAINT'
	AND OBJECT_NAME(OBJECT_ID)='FK_WorkOrderShiftData_ProductionShift'
	AND OBJECT_NAME(parent_object_id) ='WorkOrderShiftData'
)
BEGIN
	ALTER TABLE [WorkOrderShiftData]
	ADD CONSTRAINT [FK_WorkOrderShiftData_ProductionShift] FOREIGN KEY (ShiftID) REFERENCES [ProductionShift] (ID)
END
GO

-- DROP constraint - verify it exists first
IF EXISTS (
    SELECT TOP 1 1
	FROM sys.objects
	WHERE type_desc LIKE '%CONSTRAINT'
	AND OBJECT_NAME(OBJECT_ID)='FK_WorkOrderShiftData_MyFakeConstraintName'
	AND OBJECT_NAME(parent_object_id) ='WorkOrderShiftData'
)
BEGIN
	ALTER TABLE dbo.[WorkOrderShiftData]
    DROP CONSTRAINT [FK_WorkOrderShiftData_MyFakeConstraintName]
END
GO