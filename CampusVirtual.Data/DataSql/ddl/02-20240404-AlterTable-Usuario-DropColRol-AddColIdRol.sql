
IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Usuario')
BEGIN
	IF EXISTS (SELECT * FROM sys.columns WHERE name = 'Rol')
	BEGIN
		
		IF EXISTS (SELECT * FROM sys.check_constraints WHERE name = 'CK__Usuario__Rol__37703C52')
		BEGIN
			ALTER TABLE Usuario DROP CONSTRAINT CK__Usuario__Rol__37703C52;
		END

		ALTER TABLE Usuario DROP COLUMN Rol;
		
		IF NOT EXISTS (SELECT * FROM sys.columns WHERE name = 'IdRol')
		BEGIN
			ALTER TABLE Usuario ADD IdRol INT NOT NULL;
		END

	END
END