USE [AsistoTAD_UserManagementDB]
GO

INSERT [dbo].[KeyStatus] ([Id], [Name], [Default]) VALUES (NEWID(), N'Activo', 1)
INSERT [dbo].[KeyStatus] ([Id], [Name], [Default]) VALUES (NEWID(), N'Inactivo', 0)
GO