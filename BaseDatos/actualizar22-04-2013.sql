USE [Agrocomercio]
GO

/****** Object:  View [dbo].[fac_x_letra]    Script Date: 04/23/2013 00:01:26 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--select * from Operaciones
--/*
ALTER VIEW [dbo].[fac_x_letra] (
icodletra, valor, dfecemision, dfecultpag, dfecnxtvct, dopCod, OpeCod, numfac, 
PrvRazon, opetotpagar, OpeTotal, OpeTotPagPen, PrvCod, moneda, opeestado, estado, 
percod, tra, idet_letra, nsalvenc


)
AS --*/

select icodletra, valor, dfecemision, dfecultpag, dfecnxtvct, dopCod, OpeCod, numfac, PrvRazon, opetotpagar, OpeTotal, OpeTotPagPen, PrvCod, moneda, opeestado, estado, percod, tra, idet_letra,
Convert(numeric(17,2), coalesce ((select nmonto - (select coalesce (sum(nmonto),0) from Pag_letras where idetletra= idet_letra)from det_letra where idetletra= idet_letra ),0)* nporDesc/100 ) as nsalvenc from (
select icodletra, opetipo as valor,
cast (dopfecemision as date) as dfecemision, 
cast ((select max(dfecpago) from det_letra where icodletra=do.icodletra)as date) as dfecultpag, 
cast ((select min(dfecvenc) from det_letra where icodletra=do.icodletra and cestado=1)as date) as dfecnxtvct,
dopCod, op.OpeCod , dopNroSerie +'-'+ dopNumero as numfac,
case when OpeTipo ='C' then 
(select PrvRazon from Proveedores where PrvCod =  OP.PrvCod)
else 
(select CliNombre  from Clientes  where CliCod  =  OP.CliCod ) end as PrvRazon, 
opetotpagar , OpeTotal,OpeTotPagPen
, case when OpeTipo ='C' then 
OP.PrvCod else OP.CliCod  end as PrvCod,dopMoneda as moneda,
opeestado, case when opeestado ='P' then 'Pendiente' else 'Cancelado' end as estado
,percod, (select perNombres+' '+perApellidoPat from Personal PER where per.perCod= OP.PerCod) as tra
,(select MIN(idetletra) from det_letra where icodletra=do.icodletra and cestado =1) as idet_letra
,OP.nporDesc
from DocumenOperacion as DO
inner join Operaciones as OP on do.OpeCod= OP.OpeCod

where ((tdoCod in (2, 4) and OpeTipo='C') or (tdoCod in (3, 5) and OpeTipo='V'))  and icodletra is not null and OpeTipPago ='CR'

) as tabla


GO

