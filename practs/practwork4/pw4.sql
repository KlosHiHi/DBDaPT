--Task1
EXEC sp_adduser 'login1', 'user1';
EXEC sp_adduser 'login2', 'user2';

CREATE USER [user3] FOR LOGIN [login3]
CREATE USER [user4] FOR LOGIN [login4]

EXEC sp_addlogin 'isppLogin312', 'Password!';
EXEC sp_addsrvrolemember 'securityadmin', 'isppLogin312';

--Task2
EXEC sp_addrolemember 'db_owner', 'user1';
EXEC sp_addrolemember 'db_datareader', 'user2';
EXEC sp_addrolemember 'db_datawriter', 'user2';

EXEC sp_droprolemember 'db_datawriter', 'user2';

--Task3
GRANT INSERT, DELETE ON dbo.Ticket TO user3;

GRANT SELECT, UPDATE ([Name], Email)
ON dbo.Visitor 
TO user4;

DENY SELECT ON dbo.Visitor TO user2;
DENY UPDATE ([Name])
ON dbo.Visitor 
TO user4;

--Task4
DECLARE @i INT = 1;
WHILE @i < 5
	BEGIN
	EXEC('CREATE USER [userReader' + @i +'] FOR LOGIN [reader' + @i + '] WITH DEFAULT_SCHEMA=[ispp3101] ');
	EXEC('EXEC sp_addrolemember ''db_datareader'', ''userReader' + @i + '''');
	SET @i += 1;
	END;

--Task5
