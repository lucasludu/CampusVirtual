IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Materia')
BEGIN
	DROP TABLE Materia;
END
GO
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Materia')
BEGIN
	CREATE TABLE Materia (
		Id INT IDENTITY(1,1),
		Nombre VARCHAR(50),
		Anio INT,
		Cuatrimestre INT,
		HorasSemanales INT,
		IdCarrera INT NOT NULL
	);
	ALTER TABLE Materia ADD CONSTRAINT pk_Materia PRIMARY KEY (Id);
	ALTER TABLE Materia ADD CONSTRAINT fk_Materia_Carrera FOREIGN KEY (IdCarrera) REFERENCES Carrera (Id);
END