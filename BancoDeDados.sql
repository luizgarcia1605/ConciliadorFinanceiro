USE master
GO

IF (SELECT COUNT(1) FROM sys.databases WHERE name = 'ConciliadorFinanceiro') > 0
BEGIN
	USE master
	DROP DATABASE [ConciliadorFinanceiro]
END

CREATE DATABASE ConciliadorFinanceiro
GO

USE ConciliadorFinanceiro
GO

CREATE TABLE [LancamentoFinanceiro]
(
	[Id]					INT IDENTITY(1, 1) PRIMARY KEY
,	[DataHoraLancamento]	DATETIME DEFAULT GETDATE() NOT NULL
,	[Valor]					FLOAT NOT NULL
,	[Tipo]					INT NOT NULL
,	[Status]				INT NOT NULL
)