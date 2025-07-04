USE [AsistoTAD_UserManagementDB]
GO

INSERT [dbo].[DependencyDegree] ([Id], [Name], [Description]) VALUES (NEWID(), N'III. Gran Dependencia', N'Necesita ayuda para realizar actividades básicas de la vida diaria varias veces al día. Pérdida total de autonomía física, mental, intelectual o sensorial. Precisa el apoyo indispensable y continuo de otra persona.')
INSERT [dbo].[DependencyDegree] ([Id], [Name], [Description]) VALUES (NEWID(), N'II. Dependencia Severa', N'Necesita ayuda para realizar actividades básicas de la vida diaria dos o tres veces al día, pero no requiere el apoyo permanente de un cuidador.')
INSERT [dbo].[DependencyDegree] ([Id], [Name], [Description]) VALUES (NEWID(), N'I. Dependencia Moderada', N'Necesita ayuda para realizar actividades básicas de la vida diaria, al menos una vez al día o tiene necesidades de apoyo intermitente o limitado para su autonomía personal.')
INSERT [dbo].[DependencyDegree] ([Id], [Name], [Description]) VALUES (NEWID(), N'Sin Dependencia Reconocida', N'No tiene valoracion de dependencia')
INSERT [dbo].[DependencyDegree] ([Id], [Name], [Description]) VALUES (NEWID(), N'Dependencia Desconodida', N'Se desconoce sí tiene dependencia reconocida o no')
GO