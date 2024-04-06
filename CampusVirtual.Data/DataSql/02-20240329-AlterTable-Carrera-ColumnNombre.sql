
IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Carrera')
BEGIN

	IF EXISTS (SELECT * FROM sys.columns WHERE name = 'Nombre')
	BEGIN 

		ALTER TABLE Carrera ALTER COLUMN Nombre VARCHAR(100);

	END

END
