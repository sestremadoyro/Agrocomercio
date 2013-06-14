/****** Script para el comando SelectTopNRows de SSMS  ******/
--SELECT pre.ArtCod, lo.LotCod, lo.LotNro, lo.LotStock, lo.LotEstado,
--lo.LprCod, pre.LprPrecio, pre.LprDscto, pre.LprEstado
--FROM LotesArt lo
--inner join ListaPrecios pre on lo.LprCod = pre.LprCod 
--order by pre.ArtCod,  lo.LotCod


ALTER TABLE [dbo].[LotesArt]
ADD [ArtCod] int NULL,
ADD [LotPrecioCom] numeric(18, 2) NULL,
ADD [LotPrecionVen] numeric(18, 2) NULL
GO

update LotesArt set ArtCod = l.ArtCod,
LotPrecioCom = l.LprPrecio - (l.LprPrecio*l.LprDscto/100),
LotPrecioVen = l.LprPrecio - (l.LprPrecio*l.LprDscto/100)  
from ListaPrecios l where l.LprCod = LotesArt.LprCod 




ALTER TABLE [dbo].[ListaPrecios] DROP CONSTRAINT [FK_LISTAPRECIOS_ARTICULOS]
GO


ALTER TABLE [dbo].[LotesArt] DROP CONSTRAINT [FK_LotesArt_ListaPrecios]
GO


DROP TABLE [dbo].[ListaPrecios]
GO

ALTER TABLE [dbo].[LotesArt] DROP COLUMN [LprCod]
GO

ALTER TABLE [dbo].[LotesArt]  WITH CHECK ADD  CONSTRAINT [FK_LotesArt_Articulos] FOREIGN KEY([ArtCod])
REFERENCES [dbo].[Articulos] ([ArtCod])
GO

ALTER TABLE [dbo].[LotesArt] CHECK CONSTRAINT [FK_LotesArt_Articulos]
GO
