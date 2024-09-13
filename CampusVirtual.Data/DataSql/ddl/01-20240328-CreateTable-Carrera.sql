
IF EXISTS (SELECT * FROM sys.tables	WHERE name = 'Carrera')
BEGIN
	DROP TABLE Carrera;
END
GO 
IF NOT EXISTS (SELECT * FROM sys.tables	WHERE name = 'Carrera')
BEGIN
	CREATE TABLE Carrera (
		Id INT IDENTITY(1,1),
		Nombre VARCHAR(50),
		Descripcion TEXT,
		CantAnios INT
	);
	ALTER TABLE Carrera ADD CONSTRAINT pk_Carrera PRIMARY KEY (Id)
END
GO