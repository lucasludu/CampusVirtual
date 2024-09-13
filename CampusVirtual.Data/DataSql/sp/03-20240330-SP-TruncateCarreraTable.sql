IF OBJECT_ID('TruncateCarreraTable', 'P') IS NOT NULL
BEGIN
    DROP PROCEDURE TruncateCarreraTable;
END
GO
CREATE PROCEDURE TruncateCarreraTable
AS
BEGIN
    SET NOCOUNT ON;
	IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Usuario')
	BEGIN
		-- Elimino la restricción de clave externa en la tabla Usuario
		ALTER TABLE [dbo].[Usuario] DROP CONSTRAINT fk_Usuario_Carrera;
		
		IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Materia')
		BEGIN
			-- Elimino la restricción de clave externa en la tabla Materia
			ALTER TABLE [dbo].[Materia] DROP CONSTRAINT fk_Materia_Carrera;
			-- Truncar la tabla Carrera
			TRUNCATE TABLE [dbo].[Carrera];
		END
		-- Volver agregar la restricción de clave externa en la tabla Usuario
		ALTER TABLE [dbo].[Usuario] ADD CONSTRAINT fk_Usuario_Carrera FOREIGN KEY (IdCarrera) REFERENCES [dbo].[Carrera] (Id);
		-- Volver agregar la restricción de clave externa en la tabla Materia
		ALTER TABLE [dbo].[Materia] ADD CONSTRAINT fk_Materia_Carrera FOREIGN KEY (IdCarrera) REFERENCES [dbo].[Carrera] (Id);
	END
END
GO
