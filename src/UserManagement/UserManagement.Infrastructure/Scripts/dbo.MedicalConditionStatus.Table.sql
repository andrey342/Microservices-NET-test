USE [AsistoTAD_UserManagementDB]
GO

INSERT INTO [dbo].[MedicalConditionStatus] (Id, Name) VALUES
(NEWID(), 'Activo'),
(NEWID(), 'Resuelto'),
(NEWID(), 'En Remisión'),
(NEWID(), 'Crónico'),
(NEWID(), 'En Tratamiento');
GO