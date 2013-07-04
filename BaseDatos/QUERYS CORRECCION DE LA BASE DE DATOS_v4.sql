--delete from DetOperacion where OpeCod in (511,528,533)
--delete from LotesArt where LotCod in (1204,1207,1217)
--delete from DocumenOperacion where OpeCod in (511,528,533)
--delete from Operaciones where OpeCod in (511,528,533)


--DELETE FROM DocumenOperacion where dopCod in (select MAX(dopCod) from Operaciones o
--												inner join DocumenOperacion d on o.OpeCod = d.OpeCod  
--												where o.OpeEstado != 'A'
--												group by o.OpeCod, d.tdoCod 
--												having count(dopCod) > 1)

--insert into DetOperacion
--SELECT DISTINCT (SELECT max(dtpCod)+1 FROM DetOperacion) as dtpCod, 20 as OpeCod,
--L.ArtCod, l.LotCod, l.LotStock, L.LotPrecioCom, 0 as dtpDscto, 
--l.LotStock * L.LotPrecioCom as dtpSubTotal, a.UniCod, 
--case when l.LotStock > 0 then 1 else 0 end as dtpEstado
--FROM LotesArt L  
--inner join Articulos a on a.ArtCod = l.ArtCod 
--LEFT JOIN DetOperacion D ON L.LotCod = D.LotCod 
--WHERE D.ArtCod IS NULL
--AND LotStock >0

--update DetOperacion set dtpCantidad = 125 where dtpCod = 444
--update DetOperacion set dtpEstado = 1 where dtpEstado is null

--update Articulos set ArtStockIni = a.dtpCantidad from (select OpeTipo, OpeCod, dopCod, dopNroSerie,
--											a.ArtCod, LotCod, isnull(dtpCantidad,0) as dtpCantidad, ArtStockIni, ArtStock 
--											from Articulos a
--											left join (
--												select o.OpeTipo, o.OpeCod, d.dopCod, dopNroSerie,
--												de.ArtCod, de.LotCod, dtpCantidad
--												from Operaciones o
--												inner join DocumenOperacion d on o.OpeCod = d.OpeCod  
--												inner join DetOperacion de on o.OpeCod = de.OpeCod  
--												where d.tdoCod = 0 and o.OpeEstado != 'A'
--												) as dd on a.ArtCod = dd.ArtCod 
--											where isnull(dtpCantidad,0) <> ArtStockIni) as a
--										where a.ArtCod = Articulos.ArtCod 


--update Articulos set ArtStock = A.stock, ArtStockFac = A.stockVen FROM (select ArtCod, ArtStockIni, ArtStock, stock, ArtStockFac,  stockVen, stockCom
--										from (select d.ArtCod, a.ArtStock, a.ArtStockIni, a.ArtStockFac, 
--												sum(case when o.OpeTipo = 'C' then dtpCantidad else -dtpCantidad end) as stock,
--												sum(case when o.OpeTipo = 'V' then dtpCantidad else 0 end) as stockVen,
--												sum(case when o.OpeTipo = 'C' then dtpCantidad else 0 end) as stockCom
--												from Operaciones o
--												inner join DetOperacion d on o.OpeCod = d.OpeCod  
--												inner join Articulos a on a.ArtCod = d.ArtCod 
--												where o.OpeEstado not in ('A','R')
--												group by d.ArtCod, a.ArtStock, a.ArtStockIni, a.ArtStockFac
--												) as a
--										where a.ArtStock <> stock OR ArtStockFac <> stockVen) AS A 
--										where a.ArtCod = Articulos.ArtCod 


--UPDATE LotesArt SET LotPrecioCom = 0, LotPrecioVen = 0 WHERE LotEstado = 'I'

----aca queda


--update LotesArt set LotNro = A.NroLotes  FROM (SELECT * FROM (select a.ArtCod, a.ArtStock, SUM(LotStock) AS LotStock,
--									COUNT(LotCod) AS NroLotes,
--									max(LotNro) AS lotMax
--									from Articulos a
--									INNER join LotesArt l on a.ArtCod = L.ArtCod 
--									WHERE L.LotEstado != 'I'
--									GROUP BY a.ArtCod, a.ArtStock
--									) AS A
--									WHERE NroLotes < lotMax ) AS A
--								WHERE A.ArtCod = LotesArt.ArtCod AND LotesArt.LotNro = A.lotMax  


--UPDATE LotesArt SET LotStock = A.stock  FROM (SELECT * FROM (
--											select L.ArtCod, l.LotNro, l.LotStock,  
--											sum(case when o.OpeTipo = 'C' then dtpCantidad else -dtpCantidad end) as stock
--											from LotesArt L
--											INNER join DetOperacion d on d.LotCod = l.LotCod 
--											INNER join Operaciones o on o.OpeCod = d.OpeCod  
--											where o.OpeEstado not in ('A','R')
--											GROUP BY L.ArtCod, l.LotNro, l.LotStock
--										) AS A
--									WHERE LotStock <> stock ) AS A
--								WHERE A.ArtCod = LotesArt.ArtCod AND A.LotNro = LotesArt.LotNro 

--UPDATE LotesArt SET LotEstado = 'A' WHERE LotEstado = '0' AND LotStock > 0
--UPDATE LotesArt SET LotEstado = '0' WHERE LotEstado = 'A' AND LotStock <= 0
--UPDATE LotesArt SET LotStock = 0, LotPrecioCom = 0, LotPrecioVen =0 WHERE LotEstado = 'I'


--UPDATE LotesArt SET LotEstado = 'I' WHERE LotCod IN (select L.LotCod
--													from Operaciones o
--													inner join DetOperacion d on o.OpeCod = d.OpeCod  
--													inner join  LotesArt l on d.LotCod = l.LotCod 
--													where o.OpeEstado = 'R' AND O.OpeTipo = 'C')


--DELETE FROM LotesArt WHERE LotCod IN (SELECT DISTINCT L.LotCod FROM LotesArt L  --1104
--										LEFT JOIN DetOperacion D ON L.LotCod = D.LotCod 
--										WHERE D.ArtCod IS NULL
--										AND LotStock >0) AND LotCod NOT IN (618,786,780)

--update DetOperacion set dtpSubTotal = dtpPrecioVen *dtpCantidad 
--where dtpCod in (select dtpCod from DetOperacion where dtpEstado = 1 and dtpSubTotal = 0 and dtpPrecioVen > 0 and dtpDscto <100)

--UPDATE Articulos SET ArtStock = 32 WHERE ArtCod = 663
--UPDATE Articulos SET ArtStock = 7 WHERE ArtCod = 786