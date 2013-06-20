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
	where d.tdoCod = 0 and o.OpeEstado not in ('A','R')
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
		where o.OpeEstado not in ('A','R')
		group by d.ArtCod, a.ArtStock, a.ArtStockIni, a.ArtStockFac
		) as a
where a.ArtStock <> stock OR ArtStockFac <> stockVen

--REVISAR DETALLES ACTIVOS CUANDO RECIEN SE HAN REGISTRADO LA OPERACION Y NO SE PROCESO
select O.OpeTipo, D.*, L.LotCod,L.LotNro,L.LotStock, L.LotEstado 
from Operaciones o
inner join DetOperacion d on o.OpeCod = d.OpeCod  
inner join  LotesArt l on d.LotCod = l.LotCod 
where o.OpeEstado = 'R'


--IDENTIFICAR LOTES INACTIVOS POR ANULACION DE OPERACIONES
select o.OpeCod,o.OpeTipo,d.dtpCod,d.ArtCod, d.LotCod,d.dtpCantidad,
l.LotNro,l.LotStock, l.LotEstado  
from Operaciones o
inner join DetOperacion d on o.OpeCod = d.OpeCod  
inner join  LotesArt l on d.LotCod = l.LotCod 
where o.OpeEstado = 'A' AND OpeTipo = 'C'
order by LotCod





--ARTICULOS CON STOCK EN NEGATIVO
SELECT *  FROM Articulos WHERE ArtStock < 0



--COMPRAR NRO DE LOTES CON LOTE MAXIMO
SELECT * FROM (select a.ArtCod, a.ArtStock, SUM(LotStock) AS LotStock,
				COUNT(LotCod) AS NroLotes,
				max(LotNro) AS lotMax
				from Articulos a
				INNER join LotesArt l on a.ArtCod = L.ArtCod 
				WHERE L.LotEstado != 'I'
				GROUP BY a.ArtCod, a.ArtStock
				) AS A
WHERE NroLotes <> lotMax 

select o.OpeCod,o.OpeTipo,o.OpeEstado,d.dtpCod,d.ArtCod, d.LotCod,d.dtpCantidad,
l.LotNro,l.LotStock, l.LotEstado  
from Operaciones o
inner join DetOperacion d on o.OpeCod = d.OpeCod  
inner join  LotesArt l on d.LotCod = l.LotCod 
where  d.ArtCod = 58
order by LotCod

--revisar nullos en los lotes
select o.OpeCod,o.OpeEstado,o.OpeTipo,d.dtpCod,D.ArtCod, d.dtpCantidad,
L.*
from LotesArt L
INNER join DetOperacion d on d.LotCod = l.LotCod 
INNER join Operaciones o on o.OpeCod = d.OpeCod  
where l.ArtCod IS NULL
order by L.ArtCod, LotCod, dtpCod 

 

--CUADRE DE DETALLES DE COMPRAS Y VENTAS CON STOCK DE LOTES
SELECT * FROM (
		select L.ArtCod, l.LotNro, l.LotStock,  
		sum(case when o.OpeTipo = 'C' then dtpCantidad else -dtpCantidad end) as stock
		from LotesArt L
		INNER join DetOperacion d on d.LotCod = l.LotCod 
		INNER join Operaciones o on o.OpeCod = d.OpeCod  
		where o.OpeEstado not in ('A','R')
		GROUP BY L.ArtCod, l.LotNro, l.LotStock
	) AS A
WHERE LotStock <> stock
ORDER BY A.ArtCod, LotNro



--REVISAR ESTADOS DE LOS LOTES
select o.OpeCod,o.OpeTipo,o.OpeEstado,d.dtpCod,d.ArtCod, d.LotCod,d.dtpCantidad,
l.LotNro,l.LotStock, l.LotEstado  
from Operaciones o
inner join DetOperacion d on o.OpeCod = d.OpeCod  
inner join  LotesArt l on d.LotCod = l.LotCod 
where  LotEstado = 'A' AND LotStock = 0
order by LotCod


--LOTES HUERFANOS CON STOCK
SELECT * FROM LotesArt WHERE ArtCod = 664
SELECT * FROM DetOperacion WHERE ArtCod = 664	

SELECT DISTINCT L.* FROM LotesArt L  --1104
LEFT JOIN DetOperacion D ON L.LotCod = D.LotCod 
WHERE D.ArtCod IS NULL
AND LotStock >0



--CUADRE DE STOCK DE ARTICULOS CON STOCK DE LOTES
SELECT * FROM (select a.ArtCod, a.ArtStock, SUM(LotStock) AS LotStock,
				MIN(LotNro) AS lotMIN,
				max(LotNro) AS lotMax
				from Articulos a
				INNER join LotesArt l on a.ArtCod = L.ArtCod 
				WHERE L.LotEstado != 'I' 
				GROUP BY a.ArtCod, a.ArtStock
				) AS A
WHERE ArtStock <> LotStock 
 


select o.OpeCod,o.OpeTipo,o.OpeEstado,d.dtpCod,d.ArtCod, d.LotCod,d.dtpCantidad,
l.LotNro,l.LotStock, l.LotEstado  
from Operaciones o
inner join DetOperacion d on o.OpeCod = d.OpeCod  
inner join  LotesArt l on d.LotCod = l.LotCod 
where  d.ArtCod = 786
order by LotCod






select L.ArtCod, l.LotNro, l.LotStock, A.ArtStock,
		(case when o.OpeTipo = 'C' then dtpCantidad else -dtpCantidad end) as stock,
		A.ArtStockIni
		from LotesArt L
		INNER join DetOperacion d on d.LotCod = l.LotCod 
		INNER join Operaciones o on o.OpeCod = d.OpeCod  
		INNER join Articulos A on A.ArtCod = L.ArtCod 
		where o.OpeEstado not in ('A','R') AND L.ArtCod = 663
	

select o.OpeCod,o.OpeTipo,o.OpeEstado,d.dtpCod,d.ArtCod, d.LotCod,d.dtpCantidad
from Operaciones o
inner join DetOperacion d on o.OpeCod = d.OpeCod  
where  d.ArtCod = 56
order by LotCod








SELECT * FROM LotesArt WHERE ArtCod = 773
SELECT * FROM DetOperacion WHERE ArtCod = 30



select * from LotesArt where ArtCod = 250
select * from DetOperacion where ArtCod = 250
select * from DetOperacion where LotCod = 250











