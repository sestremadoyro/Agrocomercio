ALTER TABLE [dbo].[DocumenOperacion]
ADD [dopFecTraslado] [smalldatetime] NULL,
ADD [dopPunPartida] [varchar](100) NULL
GO


update DocumenOperacion set dopPunPartida = ''

	


ALTER TABLE [dbo].[DocumenOperacion]
ADD DEFAULT GETDATE() FOR [dopFecTraslado]
GO

ALTER TABLE [dbo].[DocumenOperacion]
ADD DEFAULT '' FOR [dopPunPartida]
GO