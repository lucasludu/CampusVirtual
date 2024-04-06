IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Rol')
BEGIN
	IF NOT EXISTS (SELECT * FROM Rol WHERE Nombre IN ('Admin', 'Student'))
	BEGIN
		INSERT INTO Rol (Nombre) VALUES ('Admin'), ('Student');
	END
END
