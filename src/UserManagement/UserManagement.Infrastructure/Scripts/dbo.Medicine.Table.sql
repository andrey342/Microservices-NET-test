USE [AsistoTAD_UserManagementDB]
GO

INSERT INTO [dbo].[Medicine] ([Id], [Name]) VALUES
-- Cardiovasculares
(NEWID(), 'Enalapril'),
(NEWID(), 'Amlodipino'),
(NEWID(), 'Atenolol'),
(NEWID(), 'Simvastatina'),

-- Analgésicos
(NEWID(), 'Paracetamol'),
(NEWID(), 'Ibuprofeno'),
(NEWID(), 'Tramadol'),

-- Anticoagulantes
(NEWID(), 'Sintrom'),
(NEWID(), 'Warfarina'),

-- Antidiabéticos
(NEWID(), 'Metformina'),
(NEWID(), 'Glibenclamida'),

-- Psiquiátricos
(NEWID(), 'Alprazolam'),
(NEWID(), 'Sertralina'),
(NEWID(), 'Lorazepam'),

-- Digestivos
(NEWID(), 'Omeprazol'),
(NEWID(), 'Pantoprazol'),

-- Respiratorios
(NEWID(), 'Salbutamol'),
(NEWID(), 'Bromuro de Ipratropio'),

-- Suplementos
(NEWID(), 'Calcio + Vitamina D'),
(NEWID(), 'Vitamina B12');
GO