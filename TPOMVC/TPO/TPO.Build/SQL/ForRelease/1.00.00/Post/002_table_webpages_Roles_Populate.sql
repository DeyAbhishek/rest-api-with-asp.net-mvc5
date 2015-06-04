IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'webpages_Roles')
BEGIN
	IF NOT EXISTS (SELECT TOP 1 1 FROM [dbo].[webpages_Roles] WHERE [RoleName] = 'System Administrator')
	BEGIN
		INSERT INTO [dbo].[webpages_Roles] ([RoleName])
		VALUES ('System Administrator')	
	END
	
	IF NOT EXISTS (SELECT TOP 1 1 FROM [dbo].[webpages_Roles] WHERE [RoleName] = 'Supervisor')
	BEGIN
		INSERT INTO [dbo].[webpages_Roles] ([RoleName])
		VALUES ('Supervisor')	
	END
	
	IF NOT EXISTS (SELECT TOP 1 1 FROM [dbo].[webpages_Roles] WHERE [RoleName] = 'Manager')
	BEGIN
		INSERT INTO [dbo].[webpages_Roles] ([RoleName])
		VALUES ('Manager')	
	END
	
	IF NOT EXISTS (SELECT TOP 1 1 FROM [dbo].[webpages_Roles] WHERE [RoleName] = 'Lead Operator')
	BEGIN
		INSERT INTO [dbo].[webpages_Roles] ([RoleName])
		VALUES ('Lead Operator')	
	END

	IF NOT EXISTS (SELECT TOP 1 1 FROM [dbo].[webpages_Roles] WHERE [RoleName] = 'Operator')
	BEGIN
		INSERT INTO [dbo].[webpages_Roles] ([RoleName])
		VALUES ('Operator')	
	END
	
	IF NOT EXISTS (SELECT TOP 1 1 FROM [dbo].[webpages_Roles] WHERE [RoleName] = 'QC Lab Technician')
	BEGIN
		INSERT INTO [dbo].[webpages_Roles] ([RoleName])
		VALUES ('QC Lab Technician')	
	END
END
