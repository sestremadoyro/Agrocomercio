ALTER TABLE [dbo].[DocumenOperacion]
ADD [dopFecTraslado] [smalldatetime] NULL,
ADD [dopPunPartida] [varchar](100) NULL
GO


update DocumenOperacion set dopPunPartida = ''

	


