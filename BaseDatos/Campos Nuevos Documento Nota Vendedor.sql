Use Agrocomercio 

ALTER TABLE [dbo].[DocumenOperacion]
ADD [dopDocComple] [varchar](50) NULL
GO


update DocumenOperacion set dopDocComple = ''


INSERT INTO [dbo].[Atributos]
           ([AtrTipoCod],[AtrNivel],[AtrCodigo],[AtrDescripcion],[AtrEstado])
     VALUES (5,1,4,'Nota de Descargo','True'),(5,1,5,'Nota de Descargo','True'),(3,1,7,'Nota de Pedido Vendedor','True')
GO
