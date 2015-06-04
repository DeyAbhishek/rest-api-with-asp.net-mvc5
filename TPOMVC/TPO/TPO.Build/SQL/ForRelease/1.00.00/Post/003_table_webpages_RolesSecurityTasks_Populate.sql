IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'webpages_RolesSecurityTasks')
AND EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'SecurityTask')
BEGIN

	DELETE FROM [dbo].[webpages_RolesSecurityTasks]

	DECLARE @LeadOperatorRoleId int = 2
	,		@ManagerRoleId int = 3
	,		@OperatorRoleId int = 4
	,		@TechnicianRoleId int = 5
	,		@SupervisorRoleId int = 6
	,		@SystemAdminRoleId int = 7 

	-- Technician
	INSERT INTO dbo.webpages_RolesSecurityTasks (RoleId, SecurityTaskId)
	SELECT @TechnicianRoleId, SecurityTaskId
	FROM dbo.SecurityTask
	WHERE SecurityTaskId In (1,4,6,8,10,12,14,16,18,20,22,24,26,28,29,31,33,35,37,40,41,43,45,47,
		49,51,53,55,57,60,61,63,65,68,69,71,73,75,77,79,82,85,87,89,91,93,95,97,99,101,103,105,107,
		109,111,113,115,117,119,121,123,125,127,129,131,133,135,137,139,141,143,145,147,149,151,153,
		155,157,159,161,163,165,167,169,171,173,175,177,179,181,183,185,187,189,191,193,195,197,199,
		201,203,205,207,209,211,213,215,217,219,221,223,225,227,229,231,233,235,237,239,241,243,245,
		247,249,251,253	)

	
	-- Manager
	INSERT INTO dbo.webpages_RolesSecurityTasks (RoleId, SecurityTaskId)
	SELECT @ManagerRoleId, SecurityTaskId
	FROM dbo.SecurityTask
	WHERE SecurityTaskId In (
		1,3,5,7,9,11,13,15,17,19,21,23,25,27,29,31,33,35,37,39,41,43,45,47,49,51,53,55,57,59,61,63,65,
		67,69,71,73,75,77,79,81,83,85,87,89,91,93,95,97,99,101,103,105,107,109,111,113,115,117,119,121,
		123,125,127,129,131,133,135,137,139,141,143,145,147,149,151,153,155,157,159,161,163,165,167,169,
		171,173,175,177,179,181,183,185,187,189,191,193,195,197,199,201,203,205,207,209,211,213,215,217,
		219,221,223,225,227,229,231,233,235,237,239,241,243,245,247,249,251,253	
		)

	--Operator
	INSERT INTO dbo.webpages_RolesSecurityTasks (RoleId, SecurityTaskId)
	SELECT @OperatorRoleId, SecurityTaskId
	FROM dbo.SecurityTask
	WHERE SecurityTaskId In (
		2,4,6,8,10,12,14,16,18,20,22,24,26,28,30,32,34,36,38,40,41,43,45,47,49,51,53,55,57,60,61,63,65,
		68,69,71,73,75,77,79,82,85,87,89,91,93,95,97,99,101,103,105,107,109,111,113,115,117,119,121,123,
		125,127,129,131,133,135,137,139,141,143,145,147,149,151,153,155,157,159,161,163,165,167,169,171,
		173,175,177,179,181,183,185,187,189,191,193,195,197,199,201,203,205,207,209,211,213,215,217,219,
		221,223,225,227,229,231,233,235,237,239,241,243,245,247,249,251,253
	)

	--Supervisor
	INSERT INTO dbo.webpages_RolesSecurityTasks (RoleId, SecurityTaskId)
	SELECT @SupervisorRoleId, SecurityTaskId
	FROM dbo.SecurityTask
	WHERE SecurityTaskId In (
		1,3,5,7,9,11,13,15,17,20,22,24,26,28,29,31,33,35,37,39,41,43,45,47,49,51,53,55,57,59,61,63,65,67,
		69,71,73,75,77,79,81,85,87,89,91,93,95,97,99,101,103,105,107,109,111,113,115,117,119,121,123,125,
		127,129,131,133,135,137,139,141,143,145,147,149,151,153,155,157,159,161,163,165,167,169,171,173,
		175,177,179,181,183,185,187,189,191,193,195,197,199,201,203,205,207,209,211,213,215,217,219,221,
		223,225,227,229,231,233,235,237,239,241,243,245,247,249,251,253
	)

	--Lead Operator
	INSERT INTO dbo.webpages_RolesSecurityTasks (RoleId, SecurityTaskId)
	SELECT @LeadOperatorRoleId, SecurityTaskId
	FROM dbo.SecurityTask
	WHERE SecurityTaskId In (
		2,4,6,8,10,12,14,16,18,20,22,24,26,28,30,32,34,36,38,40,41,43,45,47,49,51,53,55,57,60,61,63,65,
		68,69,71,73,75,77,79,82,85,87,89,91,93,95,97,99,101,103,105,107,109,111,113,115,117,119,121,123,
		125,127,129,131,133,135,137,139,141,143,145,147,149,151,153,155,157,159,161,163,165,167,169,171,
		173,175,177,179,181,183,185,187,189,191,193,195,197,199,201,203,205,207,209,211,213,215,217,219,
		221,223,225,227,229,231,233,235,237,239,241,243,245,247,249,251,253
	)

	-- System Admin
	INSERT INTO dbo.webpages_RolesSecurityTasks (RoleId, SecurityTaskId)
	SELECT @SystemAdminRoleId, SecurityTaskId
	FROM dbo.SecurityTask
	WHERE SecurityTaskId In (
		1,3,5,7,9,11,13,15,17,19,21,23,25,27,29,31,33,35,37,39,41,43,45,47,49,51,53,55,57,59,61,63,65,67,
		69,71,73,75,77,79,81,83,85,87,89,91,93,95,97,99,101,103,105,107,109,111,113,115,117,119,121,123,
		125,127,129,131,133,135,137,139,141,143,145,147,149,151,153,155,157,159,161,163,165,167,169,171,
		173,175,177,179,181,183,185,187,189,191,193,195,197,199,201,203,205,207,209,211,213,215,217,219,
		221,223,225,227,229,231,233,235,237,239,241,243,245,247,249,251,253,255,257,259,261,263,265,267,
		269,271,273,275,277	
	)

	-- LEGACY SUPPORT 
	-- TODO: Remove
	INSERT INTO [dbo].[webpages_RolesSecurityTasks] ([RoleId], [SecurityTaskId])
	SELECT DISTINCT [RoleId], [SecurityTaskId] 
	FROM (
		SELECT r.[RoleId], t.[SecurityTaskId], r.RoleName, t.Name [Task]
		FROM [dbo].[webpages_Roles] r
		, [dbo].[SecurityTask] t
		WHERE r.RoleName = 'Lead Operator'
		AND t.Name IN ('Log In', 'Lead Operator')
		UNION
		SELECT r.[RoleId], t.[SecurityTaskId], r.RoleName, t.Name [Task]
		FROM [dbo].[webpages_Roles] r
		, [dbo].[SecurityTask] t
		WHERE r.RoleName = 'System Administrator'
		AND t.Name IN ('Log In', 'Systems Administrator')
		UNION
		SELECT r.[RoleId], t.[SecurityTaskId], r.RoleName, t.Name [Task]
		FROM [dbo].[webpages_Roles] r
		, [dbo].[SecurityTask] t
		WHERE r.RoleName = 'QC Lab Technician'
		AND t.Name IN ('Log In', 'QC Lab Technician')
		UNION
		SELECT r.[RoleId], t.[SecurityTaskId], r.RoleName, t.Name [Task]
		FROM [dbo].[webpages_Roles] r
		, [dbo].[SecurityTask] t
		WHERE r.RoleName = 'QC Lab Technician'
		AND t.Name IN ('Log In', 'QC Lab Technician')
		UNION
		SELECT r.[RoleId], t.[SecurityTaskId], r.RoleName, t.Name [Task]
		FROM [dbo].[webpages_Roles] r
		, [dbo].[SecurityTask] t
		WHERE r.RoleName = 'Supervisor'
		AND t.Name IN ('Log In', 'Supervisor')
		UNION
		SELECT r.[RoleId], t.[SecurityTaskId], r.RoleName, t.Name [Task]
		FROM [dbo].[webpages_Roles] r
		, [dbo].[SecurityTask] t
		WHERE r.RoleName = 'Manager'
		AND t.Name IN ('Log In', 'Manager')
		UNION
		SELECT r.[RoleId], t.[SecurityTaskId], r.RoleName, t.Name [Task]
		FROM [dbo].[webpages_Roles] r
		, [dbo].[SecurityTask] t
		WHERE r.RoleName = 'Operator'
		AND t.Name IN ('Log In', 'Operator')
	) roles

END
GO