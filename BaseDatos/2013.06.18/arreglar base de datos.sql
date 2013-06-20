/****** Script para el comando SelectTopNRows de SSMS  ******/
--SELECT * FROM Articulos

--OPERACIONES DUPLICADAS
SELECT * FROM Operaciones 
WHERE OpeSubTotal IN (SELECT OpeSubTotal
	FROM Operaciones WHERE OpeEstado != 'A' GROUP BY OpeSubTotal HAVING COUNT(OpeCod) >1)
AND OpeEstado != 'A'
ORDER BY OpeSubTotal, OpeCod


--DOCUMENTOS DUPLICADAS
select o.OpeCod, d.tdoCod, count(dopCod), MAX(dopCod) from Operaciones o
inner join DocumenOperacion d on o.OpeCod = d.OpeCod  
where o.OpeEstado != 'A'
group by o.OpeCod, d.tdoCod 
having count(dopCod) > 1


--INCONSISTENCIA DE ESTADOS DE DETALLES Y OPERACIONES
select O.OpeTipo, D.*
from Operaciones o
inner join DetOperacion d on o.OpeCod = d.OpeCod  
where o.OpeEstado != 'A'
and d.dtpEstado = 0 or d.dtpEstado is null 



--COMPARAR STOCK INICAL DEL ARTICULO CON DOCUMENTO DE INICIO
select OpeTipo, OpeCod, dopCod, dopNroSerie,
a.ArtCod, LotCod, isnull(dtpCantidad,0) as dtpCantidad, ArtStockIni, ArtStock 
from Articulos a
left join (
	select o.OpeTipo, o.OpeCod, d.dopCod, dopNroSerie,
	de.ArtCod, de.LotCod, dtpCantidad
	from Operaciones o
	inner join DocumenOperacion d on o.OpeCod = d.OpeCod  
	inner join DetOperacion de on o.OpeCod = de.OpeCod  
	where d.tdoCod = 0 and o.OpeEstado != 'A'
	) as dd on a.ArtCod = dd.ArtCod 
where isnull(dtpCantidad,0) <> ArtStockIni



--CUADRE DE DETALLES DE COMPRAS Y VENTAS CON STOCK DE ARTICULOS y STOCK FACTURADO
select ArtCod, ArtStockIni, ArtStock, stock, ArtStockFac,  stockVen, stockCom
from (select d.ArtCod, a.ArtStock, a.ArtStockIni, a.ArtStockFac, 
		sum(case when o.OpeTipo = 'C' then dtpCantidad else -dtpCantidad end) as stock,
		sum(case when o.OpeTipo = 'V' then dtpCantidad else 0 end) as stockVen,
		sum(case when o.OpeTipo = 'C' then dtpCantidad else 0 end) as stockCom
		from Operaciones o
		inner join DetOperacion d on o.OpeCod = d.OpeCod  
		inner join Articulos a on a.ArtCod = d.ArtCod 
		where o.OpeEstado != 'A'
		group by d.ArtCod, a.ArtStock, a.ArtStockIni, a.ArtStockFac
		) as a
where a.ArtStock <> stock





select * from DetOperacion where ArtCod = 785

select O.OpeTipo, D.*
from Operaciones o
inner join DetOperacion d on o.OpeCod = d.OpeCod  
where o.OpeEstado != 'A'
AND D.ArtCod = 53
ORDER BY OpeCod 



--select OpeSubTotal, count(OpeCod) from Operaciones where OpeEstado != 'A'
--group by OpeSubTotal having count(OpeCod) > 1


--select * from Operaciones where OpeEstado != 'A'
--and OpeSubTotal in (select OpeSubTotal from Operaciones where OpeEstado != 'A'
--group by OpeSubTotal having count(OpeCod) > 1)


--select * from DocumenOperacion where OpeCod in (247)
--select * from Operaciones where OpeCod in (247)
--select o.OpeTipo, d.*
--from Operaciones o
--inner join DetOperacion d on o.OpeCod = d.OpeCod  
--where o.OpeEstado != 'A' and d.ArtCod  = 817

--select * from Articulos where ArtCod = 4


--select * from LotesArt where ArtCod = 53
--select * from DetOperacion where ArtCod  = 53
--select * from Operaciones where OpeCod = 163

--select o.OpeCod,o.OpeTipo,d.dtpCod,d.ArtCod, d.LotCod,d.dtpCantidad,l.LotNro,l.LotStock, l.LotEstado  
--from Operaciones o
--inner join DetOperacion d on o.OpeCod = d.OpeCod  
--inner join  LotesArt l on d.LotCod = l.LotCod 
--where o.OpeEstado != 'A' --and d.ArtCod =  56
--order by ArtCod, LotCod, dtpCod 




--select o.OpeCod,o.OpeTipo,d.dtpCod,d.ArtCod, d.LotCod,d.dtpCantidad,l.LotNro,l.LotStock, l.LotEstado  
--from Operaciones o
--inner join DetOperacion d on o.OpeCod = d.OpeCod  
--inner join  LotesArt l on d.LotCod = l.LotCod 
--where o.OpeEstado != 'A' and d.dtpEstado = 1 and LotStock is null and LotEstado = 1
--order by ArtCod, LotCod, dtpCod 



--select o.OpeCod,o.OpeTipo,d.dtpCod,d.ArtCod, d.LotCod,d.dtpCantidad,l.LotNro,l.LotStock, l.LotEstado  
--from Operaciones o
--inner join DetOperacion d on o.OpeCod = d.OpeCod  
--inner join  LotesArt l on d.LotCod = l.LotCod 
--where o.OpeEstado != 'A' and LotStock > 0 and LotEstado = 0
--order by ArtCod, LotCod, dtpCod 


--select * from (
--select d.ArtCod, d.LotCod,l.LotNro,l.LotStock,l.LotEstado,
--sum(case when o.OpeTipo = 'C' then dtpCantidad else -dtpCantidad end) as stock
--from Operaciones o
--inner join DetOperacion d on o.OpeCod = d.OpeCod  
--inner join  LotesArt l on d.LotCod = l.LotCod 
--where o.OpeEstado != 'A' 
--group by  d.ArtCod, d.LotCod,l.LotNro,l.LotStock,l.LotEstado
--) as a
--where LotStock <> stock and stock >= 0
--and stock >= LotStock
--order by ArtCod, LotCod



--select a.ArtCod, a.ArtStock, LotStock
--from Articulos a
--inner join (
--		select ArtCod, sum(LotStock) as LotStock from (
--			select distinct d.ArtCod, l.LotStock
--			from LotesArt l 
--			inner join DetOperacion d on d.LotCod = l.LotCod 
--			where d.dtpEstado = 1 
--		) as aa
--		group by ArtCod
--		) as b on a.ArtCod = b.ArtCod  
--where LotStock <> ArtStock
--order by ArtCod




--update LotesArt set LotStock = 12  where LotCod = 996
--
--update DetOperacion set dtpEstado = 1 where dtpEstado is null


--update Articulos set ArtStockFac = (select stockven
--									from (
--									select d.ArtCod, a.ArtStock, a.ArtStockIni, a.ArtStockFac, 
--									sum(case when o.OpeTipo = 'C' then dtpCantidad else -dtpCantidad end) as stock,
--									sum(case when o.OpeTipo = 'V' then dtpCantidad else 0 end) as stockven
--									from Operaciones o
--									inner join DetOperacion d on o.OpeCod = d.OpeCod  
--									inner join Articulos a on a.ArtCod = d.ArtCod 
--									where o.OpeEstado != 'A'
--									group by d.ArtCod, a.ArtStock, a.ArtStockIni, a.ArtStockFac
--									) as a
--								where a.ArtCod = Articulos.ArtCod)


--update LotesArt set LotStock = (select stock from  (select * from (
--									select d.ArtCod, d.LotCod,l.LotNro,l.LotStock,l.LotEstado,
--									sum(case when o.OpeTipo = 'C' then dtpCantidad else -dtpCantidad end) as stock
--									from Operaciones o
--									inner join DetOperacion d on o.OpeCod = d.OpeCod  
--									inner join  LotesArt l on d.LotCod = l.LotCod 
--									where o.OpeEstado != 'A' 
--									group by  d.ArtCod, d.LotCod,l.LotNro,l.LotStock,l.LotEstado
--									) as a
--									where LotStock <> stock and stock >= 0
--									and stock >= LotStock) as a
--									where a.ArtCod = LotesArt.ArtCod and a.LotCod = LotesArt.LotCod 
--								) 

--update LotesArt set LotEstado = 1 where LotCod in (
--select d.LotCod  
--from Operaciones o
--inner join DetOperacion d on o.OpeCod = d.OpeCod  
--inner join  LotesArt l on d.LotCod = l.LotCod 
--where o.OpeEstado != 'A' and LotStock > 0 and LotEstado = 0 )

