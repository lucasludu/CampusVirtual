
IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Usuario')
BEGIN
	IF EXISTS (SELECT * FROM sys.columns WHERE name = 'IdCarrera')
	BEGIN
		ALTER TABLE Usuario ALTER COLUMN IdCarrera INT NULL;
	END
END