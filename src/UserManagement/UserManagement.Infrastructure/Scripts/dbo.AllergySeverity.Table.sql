USE [AsistoTAD_UserManagementDB]
GO

INSERT INTO [dbo].[AllergySeverity] ([Id], [Name], [Order]) VALUES
(NEWID(), 'Leve', 1),
(NEWID(), 'Moderada', 2),
(NEWID(), 'Grave', 3);
GO