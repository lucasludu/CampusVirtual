
IF NOT EXISTS (SELECT * FROM SYS.databases WHERE name = 'CampusVirtual')
BEGIN
	CREATE DATABASE CampusVirtual;
END

USE CampusVirtual;