USE [AsistoTAD_UserManagementDB]
GO

INSERT [dbo].[ServiceType] ([Id], [Name]) VALUES (NEWID(), N'Atención Domiciliaria')
INSERT [dbo].[ServiceType] ([Id], [Name]) VALUES (NEWID(), N'Cuidados Médicos Especializados')
INSERT [dbo].[ServiceType] ([Id], [Name]) VALUES (NEWID(), N'Teleasistencia')
GO