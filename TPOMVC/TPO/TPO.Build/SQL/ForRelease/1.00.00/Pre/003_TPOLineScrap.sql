IF EXISTS (SELECT * from INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'TPOLineScrap' AND COLUMN_NAME = 'LenghtUoMID')
BEGIN
	EXEC sys.sp_rename @objname = N'dbo.TPOLineScrap.LenghtUoMID', @newname = 'LengthUoMID', @objtype = 'COLUMN'
END
GO

