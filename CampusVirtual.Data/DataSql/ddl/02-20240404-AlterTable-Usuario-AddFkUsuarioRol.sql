IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Usuario')
BEGIN
	IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'fk_Usuario_Rol')
	BEGIN
		ALTER TABLE Usuario ADD CONSTRAINT fk_Usuario_Rol FOREIGN KEY (IdRol) REFERENCES Rol (Id);
	END
END