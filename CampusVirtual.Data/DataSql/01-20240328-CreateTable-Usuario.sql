
IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Usuario')
BEGIN
	DROP TABLE Usuario;
END
GO
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Usuario')
BEGIN
	CREATE TABLE Usuario (
		Id INT IDENTITY(1,1),
		Nombre VARCHAR(50) NOT NULL,
		Apellido VARCHAR(50) NOT NULL,
		Dni VARCHAR(20) NOT NULL,
		Correo VARCHAR(50) NOT NULL,
		PasswordHash VARBINARY(MAX) NOT NULL,
		PasswordSalt VARBINARY(MAX) NOT NULL,
		IdCarrera INT NOT NULL,
		Rol NVARCHAR(20) CHECK (ROL IN ('Alumno', 'Admin'))
	);
	ALTER TABLE Usuario ADD CONSTRAINT pk_Usuario PRIMARY KEY (Id);
	ALTER TABLE Usuario ADD CONSTRAINT uk_Usuario_dni UNIQUE (Dni);
	ALTER TABLE Usuario ADD CONSTRAINT uk_Usuario_correo UNIQUE (Correo);
	ALTER TABLE Usuario ADD CONSTRAINT fk_Usuario_Carrera FOREIGN KEY (IdCarrera) REFERENCES Carrera (Id);
END
GO