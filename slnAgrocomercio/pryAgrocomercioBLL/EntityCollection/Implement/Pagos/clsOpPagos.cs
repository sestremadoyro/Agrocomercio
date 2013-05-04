using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
//using pryAgrocomercioDAL.Implement;
using pryAgrocomercioBLL.EntityCollection.Interfaces;
using pryAgrocomercioDAL;


namespace pryAgrocomercioBLL.EntityCollection
{
    //public class clsCabletra : clsLetra_cabDao
    public class clsCabletra : clsAbstractBase<cab_letra>, Iletra_cabecera
    {
        public cab_letra GetCabLetra(int OpeCod)
        {
            return this.Find(Ope => Ope.icodigo == OpeCod).First<cab_letra>();
        }
        public DataTable GetList(string _valor, String _estado, String _cliente)
        {

            if (_estado == "T")
                return ToDataTable(this.Find(Let => Let.valor == _valor && Let.PrvRazon.Contains(_cliente))
                    .OrderByDescending(Let => Let.FecEmesion)
                    .Select(Let => new
                    {
                        Let.valor,
                        Let.icodigo,
                        Let.numDoc,
                        Let.PrvRazon,
                        Let.FecEmesion,
                        Let.itotcuota,
                        Let.nmontocuota,
                        montoPag = Let.nmontocuota - Let.opetotPagpen,
                        Let.opetotPagpen,
                        Let.cmoneda,
                        Let.nintpag,
                        Let.estado,
                        Let.dfecultpago,
                        Let.fec_nxtpg,
                        Let.ctippago
                    })
                    .AsQueryable());
            else if (_estado == "CANC")
                return ToDataTable(this.Find(Let => Let.valor == _valor && Let.estado == _estado && Let.PrvRazon.Contains(_cliente))
                .OrderByDescending(Let => Let.FecEmesion)
                .Select(Let => new
                {
                    Let.valor,
                    Let.icodigo,
                    Let.numDoc,
                    Let.PrvRazon,
                    Let.FecEmesion,
                    Let.itotcuota,
                    Let.nmontocuota,
                    montoPag = Let.nmontocuota - Let.opetotPagpen,
                    Let.opetotPagpen,
                    Let.cmoneda,
                    Let.nintpag,
                    Let.estado,
                    Let.dfecultpago,
                    Let.fec_nxtpg,
                    Let.ctippago
                })
                .AsQueryable());
            else
                return ToDataTable(this.Find(Let => Let.valor == _valor && Let.estado != _estado && Let.PrvRazon.Contains(_cliente))
                .OrderByDescending(Let => Let.FecEmesion)
                .Select(Let => new
                {
                    Let.valor,
                    Let.icodigo,
                    Let.numDoc,
                    Let.PrvRazon,
                    Let.FecEmesion,
                    Let.itotcuota,
                    Let.nmontocuota,
                    montoPag = Let.nmontocuota - Let.opetotPagpen,
                    Let.opetotPagpen,
                    Let.cmoneda,
                    Let.nintpag,
                    Let.estado,
                    Let.dfecultpago,
                    Let.fec_nxtpg,
                    Let.ctippago
                })
                .AsQueryable());
        }
    }

    //public class clsdetletra : clsdet_LetraDao
    public class clsdetletra : clsAbstractBase<det_letra>, Idet_let
    {
        public long Maxdetletra_cod()
        {
            if (this.GetAll().Count() > 0)
            {
                return this.GetAll().Max<det_letra>(Let => Let.idetletra);
            }
            return 0;
        }
        public long Mindetletpend_cod(Int64 _icodletra)
        {
            if (this.GetAll().Count() > 0)
            {
                return this.Find(Let => Let.icodletra == _icodletra && Let.cestado == "1").Min<det_letra>(Let => Let.idetletra);
            }
            return 0;
        }

        public int fndet_letraInsertar(det_letra odet_letra)
        {
            this.Add(odet_letra);
            this.SaveChanges();
            return 0;
        }
        public det_letra GetDetLetra(int OpeCod)
        {
            return this.Find(Ope => Ope.idetletra == OpeCod).First<det_letra>();
        }
        public int fnDetLetraUpdate(det_letra odet_letra)
        {
            this.Update(odet_letra);
            this.SaveChanges();
            return 0;
        }
        public DataTable GetList(int _valor)
        {
            return ToDataTable(this.Find(Let => Let.icodletra == _valor)
                .OrderBy(Let => Let.dfecvenc)
                .Select(Let => new
                {
                    Let.icodletra,//00
                    Let.inumletra,//1
                    cod_unic = Let.ccodletra,//2
                    monto = Let.nmonto,//3
                    Let.cestado,//4
                    Let.ninteres,//5
                    txtFecVen = Let.dfecvenc,//6
                    Let.dfecpago,//7
                    Let.dfecmod,//8
                    Let.idetletra,//9
                    num_let = Let.cnumletra//10
                })
                .AsQueryable());
        }
    }

    //public class clsfactPag : clsfac_letDao
    public class clsfac_x_letra : clsAbstractBase<fac_x_letra>, Ifac_x_letra
    {
        public DataTable GetList(int tipOpe)
        {

            return ToDataTable(this.Find(Let => '1' == '1' && Let.icodletra == tipOpe)
            .OrderByDescending(Let => Let.PrvRazon)
            .Select(Let => new
            {
                Let.icodletra,
                Let.valor,
                Let.dfecemision,
                Let.dfecultpag,
                Let.dopCod,
                Let.OpeCod,
                Let.numfac,
                Let.PrvRazon,
                Let.opetotpagar,
                Let.OpeTotal,
                Let.OpeTotPagPen,
                Let.PrvCod,
                Let.moneda,
                Let.opeestado,
                Let.estado

            })
            .AsQueryable());

        }
        public DataTable GetList_factura(String _filtro, String _tipo, int _codProveedor, String _moneda,
             DateTime _dfregdesde, DateTime _dfreghasta, DateTime _dpagdesde, DateTime _dpaghasta, DateTime _dvendesde, DateTime _dvenhasta, String _estado, int _tracod)
        {
            return ToDataTable(this.Find(Let => Let.valor == _tipo
                && (_filtro.Substring(0, 1) == "1" || (_filtro.Substring(0, 1) == "2" && Let.PrvCod == _codProveedor))
                && (_filtro.Substring(1, 1) == "1" || (_filtro.Substring(1, 1) == "2" && Let.moneda == _moneda))
                && (_filtro.Substring(2, 1) == "1" || (_filtro.Substring(2, 1) == "2" && Let.dfecemision >= _dfregdesde))
                && (_filtro.Substring(3, 1) == "1" || (_filtro.Substring(3, 1) == "2" && Let.dfecemision <= _dfreghasta))
                && (_filtro.Substring(4, 1) == "1" || (_filtro.Substring(4, 1) == "2" && Let.dfecultpag >= _dpagdesde))
                && (_filtro.Substring(5, 1) == "1" || (_filtro.Substring(5, 1) == "2" && Let.dfecultpag <= _dpaghasta))
                && (_filtro.Substring(6, 1) == "1" || (_filtro.Substring(6, 1) == "2" && Let.dfecnxtvct >= _dvendesde))
                && (_filtro.Substring(7, 1) == "1" || (_filtro.Substring(7, 1) == "2" && Let.dfecnxtvct <= _dvenhasta))
                && (_filtro.Substring(8, 1) == "1" || (_filtro.Substring(8, 1) == "2" && Let.opeestado == _estado))
                && (_filtro.Substring(9, 1) == "1" || (_filtro.Substring(9, 1) == "2" && Let.percod == _tracod)))
            .OrderByDescending(Let => new { Let.PrvRazon, Let.dfecemision, Let.numfac})
            .Select(Let => new
            {
                Let.icodletra,
                Let.valor,
                Let.dfecemision,
                Let.dfecultpag,
                Let.dfecnxtvct,
                Let.dopCod,
                Let.OpeCod,
                Let.numfac,
                Let.PrvRazon,
                Let.opetotpagar,
                Let.OpeTotal,
                Let.OpeTotPagPen,
                Let.PrvCod,
                Let.moneda,
                Let.opeestado,
                Let.estado,
                Let.percod,
                Let.tra,
                Let.idet_letra,
                Let.nsalvenc
            })
            .AsQueryable());
        }
    }

    public class clsvwtodo_movimiento : clsAbstractBase<vwtodo_movimiento>, Ivwtodo_movimiento
    {
        public DataTable GetList(int tipOpe)
        {

            return ToDataTable(this.Find(Let => '1' == '1' && Let.icodletra == tipOpe)
            .OrderByDescending(Let => Let.PrvRazon)
            .Select(Let => new
            {
                Let.tipPag,
                Let.icodletra,
                Let.valor,
                Let.dfecemision,
                Let.dfecultpag,
                Let.dfecnxtvct,
                Let.dopCod,
                Let.OpeCod,
                Let.numfac,
                Let.PrvRazon,
                Let.opetotpagar,
                Let.OpeTotal,
                Let.OpeTotPagPen,
                Let.PrvCod,
                Let.moneda,
                Let.opeestado,
                Let.estado,
                Let.percod,
                Let.tra,
                Let.idet_letra,
                Let.nsalvenc,
                Let.tdocod,
                Let.tipdoc

            })
            .AsQueryable());

        }
        public DataTable GetList_factura(
            String _filtro, String _tipo, int _codProveedor, 
            String _moneda, DateTime _dfregdesde, DateTime _dfreghasta, 
            DateTime _dpagdesde, DateTime _dpaghasta, DateTime _dvendesde, 
            DateTime _dvenhasta, String _estado, int _tracod, 
            String _tip_pago, int _tip_doc)
        {
            return ToDataTable(this.Find(Let => Let.valor == _tipo
                && (_filtro.Substring(0, 1) == "1" || (_filtro.Substring(0, 1) == "2" && Let.PrvCod == _codProveedor))
                && (_filtro.Substring(1, 1) == "1" || (_filtro.Substring(1, 1) == "2" && Let.moneda == _moneda))
                && (_filtro.Substring(2, 1) == "1" || (_filtro.Substring(2, 1) == "2" && Let.dfecemision >= _dfregdesde))
                && (_filtro.Substring(3, 1) == "1" || (_filtro.Substring(3, 1) == "2" && Let.dfecemision <= _dfreghasta))
                && (_filtro.Substring(4, 1) == "1" || (_filtro.Substring(4, 1) == "2" && Let.dfecultpag >= _dpagdesde))
                && (_filtro.Substring(5, 1) == "1" || (_filtro.Substring(5, 1) == "2" && Let.dfecultpag <= _dpaghasta))
                && (_filtro.Substring(6, 1) == "1" || (_filtro.Substring(6, 1) == "2" && Let.dfecnxtvct >= _dvendesde))
                && (_filtro.Substring(7, 1) == "1" || (_filtro.Substring(7, 1) == "2" && Let.dfecnxtvct <= _dvenhasta))
                && (_filtro.Substring(8, 1) == "1" || (_filtro.Substring(8, 1) == "2" && Let.opeestado == _estado))
                && (_filtro.Substring(9, 1) == "1" || (_filtro.Substring(9, 1) == "2" && Let.percod == _tracod))
                && (_filtro.Substring(10, 1) == "1" || (_filtro.Substring(10, 1) == "2" && Let.tipPag == _tip_pago))
                && (_filtro.Substring(11, 1) == "1" || (_filtro.Substring(11, 1) == "2" && Let.tdocod == _tip_doc))                
                )
            .OrderByDescending(Let => new { Let.PrvRazon, Let.dfecemision, Let.numfac })
            .Select(Let => new
            {
                Let.tipPag,
                Let.icodletra,
                Let.valor,
                Let.dfecemision,
                Let.dfecultpag,
                Let.dfecnxtvct,
                Let.dopCod,
                Let.OpeCod,
                Let.numfac,
                Let.PrvRazon,
                Let.opetotpagar,
                Let.OpeTotal,
                Let.OpeTotPagPen,
                Let.PrvCod,
                Let.moneda,
                Let.opeestado,
                Let.estado,
                Let.percod,
                Let.tra,
                Let.idet_letra,
                Let.nsalvenc,
                Let.tdocod,
                Let.tipdoc
            })
            .AsQueryable());
        }
    }
    //public class clsfactpendPag : clsfac_pnd_letDao
    public class clsFac_pnd_let : clsAbstractBase<fac_pnd_let>, Ifac_pnd_let
    {
        public DataTable GetList(int _LetEst, string tipOpe, string _mon)
        {
            if (_LetEst == 0)
            {
                if (_mon == "TDD")
                    return ToDataTable(this.Find(Let => '1' == '1' && Let.valor == tipOpe)
                    .OrderByDescending(Let => Let.PrvRazon)
                    .Select(Let => new
                    {
                        Let.valor,
                        Let.dopCod,
                        Let.numfac,
                        Let.PrvRazon,
                        Let.opetotpagar,
                        Let.dfecemision,
                        Let.PrvCod,
                        Let.monedas,
                        Let.OpeTotal
                    })
                    .AsQueryable());
                else
                    return ToDataTable(this.Find(Let => '1' == '1' && Let.valor == tipOpe && Let.monedas == _mon)
                    .OrderByDescending(Let => Let.PrvRazon)
                    .Select(Let => new
                    {
                        Let.valor,
                        Let.dopCod,
                        Let.numfac,
                        Let.PrvRazon,
                        Let.opetotpagar,
                        Let.dfecemision,
                        Let.PrvCod,
                        Let.monedas,
                        Let.OpeTotal
                    })
                    .AsQueryable());
            }
            else
            {
                if (_mon == "TDD")
                    return ToDataTable(this.Find(Let => Let.PrvCod == _LetEst && Let.valor == tipOpe)
                    .OrderByDescending(Let => Let.PrvRazon)
                    .Select(Let => new
                    {
                        Let.valor,
                        Let.dopCod,
                        Let.numfac,
                        Let.PrvRazon,
                        Let.opetotpagar,
                        Let.dfecemision,
                        Let.PrvCod,
                        Let.monedas,
                        Let.OpeTotal
                    })
                    .AsQueryable());
                else
                    return ToDataTable(this.Find(Let => Let.PrvCod == _LetEst && Let.valor == tipOpe && Let.monedas == _mon)
                    .OrderByDescending(Let => Let.PrvRazon)
                    .Select(Let => new
                    {
                        Let.valor,
                        Let.dopCod,
                        Let.numfac,
                        Let.PrvRazon,
                        Let.opetotpagar,
                        Let.dfecemision,
                        Let.PrvCod,
                        Let.monedas,
                        Let.OpeTotal
                    })
                    .AsQueryable());

            }
        }
        //public long MaxOpeCod()
        //{
        //    if (this.GetAll().Count() > 0 ){
        //        return this.GetAll().Max<Operaciones>(Ope => Ope.OpeCod);                
        //    }
        //    return 0;            
        //}
        public DataTable GetLista(String filtro, int cod_prov, String _factura, DateTime _fecha_factura, String _Moneda, int _codtra, String _valor)
        {

            return ToDataTable(this.Find(Let => Let.valor == _valor
                && (filtro.Substring(0, 1) == "1" || (filtro.Substring(0, 1) == "2" && Let.PrvCod == cod_prov))
                && (filtro.Substring(1, 1) == "1" || (filtro.Substring(1, 1) == "2" && Let.numfac.Contains(_factura)))
                && (filtro.Substring(2, 1) == "1" || (filtro.Substring(2, 1) == "2" && Let.dfecemision == _fecha_factura))
                && (filtro.Substring(3, 1) == "1" || (filtro.Substring(3, 1) == "2" && Let.monedas == _Moneda))
                && (filtro.Substring(4, 1) == "1" || (filtro.Substring(4, 1) == "2" && Let.percod == _codtra)))
                   .OrderByDescending(Let => new { Let.PrvRazon, Let.dfecemision })
                   .Select(Let => new
                    {
                        Let.valor,
                        Let.dopCod,
                        Let.numfac,
                        Let.PrvRazon,//1
                        Let.opetotpagar,
                        Let.dfecemision,
                        Let.PrvCod,
                        Let.monedas,
                        Let.OpeTotal,
                        Let.percod,
                        Let.tranom
                    })
                    .AsQueryable());
            }
    }

    //public class clsLetra : clsLetraDao
    public class clsLetra : clsAbstractBase<letra>, Iletra
    {
        public letra GetLetra(int OpeCod)
        {
            return this.Find(Ope => Ope.icodigo == OpeCod).First<letra>();
        }
        public long MaxOpeCod()
        {
            if (this.GetAll().Count() > 0)
            {
                return this.GetAll().Max<letra>(Let => Let.icodigo);
            }
            return 0;
        }
        public int fnletraInsertar(letra oLEtra)
        {
            this.Add(oLEtra);
            this.SaveChanges();
            return 0;
        }
        public int fnletraupdate(letra oLEtra)
        {
            this.Update(oLEtra);
            this.SaveChanges();
            return 0;            
        }
    }
    public class clstbliqCobranza : clsAbstractBase<tbliqCobranza>, ItbliqCobranza
    {
        public tbliqCobranza GetPago(int OpeCod)
        {
            return this.Find(Ope => Ope.idliqcob == OpeCod).First<tbliqCobranza>();
        }
        public int MaxOpeCod()
        {
            if (this.GetAll().Count() > 0)
            {
                return this.GetAll().Max<tbliqCobranza>(Ope => Ope.idliqcob);
            }
            return 0;
        }
        public int fnletraInsertar(tbliqCobranza oliquid)
        {
            this.Add(oliquid);
            this.SaveChanges();
            return 0;
        }
        public int fnletraupdate(tbliqCobranza oLiquid)
        {
            this.Update(oLiquid);
            this.SaveChanges();
            return 0;
        }
        
    }

    //public class clslistdetletra : clslistdet_LetraDao
    public class clslistdetletra : clsAbstractBase<list_detLetra>, Ilist_det_let
    {
        public DataTable GetList(String _LetEst, DateTime _fec_desde, DateTime _fec_hasta, int _ccodprd, string _Valor)
        {
            string cEsta = "";
            switch (_LetEst)
            {
                case ("N"):
                    cEsta = "1";
                    break;
                case ("P"):
                    cEsta = "2";
                    break;
                default:
                    cEsta = "";
                    break;
            }

            return ToDataTable(this.Find(DetLet =>
                ((DetLet.cestado == cEsta && cEsta != "") || (cEsta == ""))
                && ((DetLet.PrvCod == _ccodprd && _ccodprd != 0) || (_ccodprd == 0))
                && (DateTime)(DetLet.dfecven) >= _fec_desde && (DateTime)(DetLet.dfecven) <= _fec_hasta
                && DetLet.valor == _Valor)
                .OrderBy(DetLet => new { DetLet.ccodletra, DetLet.dfecven })
                
                .Select(DetLet => new
                {
                    DetLet.dfecven,
                    DetLet.cuota,
                    DetLet.ccodletra,
                    DetLet.cnumletra,
                    DetLet.PrvRazon,
                    DetLet.estado,
                    DetLet.dfecpag,
                    DetLet.cmoneda,
                    DetLet.mnt_cuota,
                    DetLet.mnt_letra,
                    DetLet.Saldo_total,
                    DetLet.ninteres,
                    DetLet.PrvCod,
                    DetLet.cestado,
                    DetLet.icodletra,
                    DetLet.idetletra
                })
                 .AsQueryable());
        }
    }

    //public class clsOpePagos : clsLetra_listDao
    public class clsList_letra : clsAbstractBase<list_letra>, Iletra_lista
    {
        public DataTable GetListOperaciones(String _LetEst)
        {
            return ToDataTable(this.Find(Let => Let.estado != _LetEst)
            .OrderByDescending(Let => Let.FecEmesion)
            .Select(Let => new{
                Let.valor,
                Let.numDoc,
                Let.fac_acum,
                Let.codPer,
                Let.PrvRazon,
                Let.FecEmesion,
                Let.itotcuota,
                Let.nmontocuota,
                Let.pagpendiente,
                Let.cmoneda,
                Let.nintpag,
                Let.estado,
                Let.dfecultpago,
                Let.fec_nxtpg,
                Let.icodletra
            })
            .AsQueryable());
        }
        public DataTable GetList_letra(String _filtro, String _letra, DateTime _fecvenc, 
            String _LetEst,String _moneda, int _CodTra, int id_persona, String _valor)
        {
            return ToDataTable(this.Find(Let => Let.valor == _valor
                && (_filtro.Substring(0, 1) == "1" || (_filtro.Substring(0, 1) == "2" && Let.numDoc.Contains(_letra)))
                && (_filtro.Substring(1, 1) == "1" || (_filtro.Substring(1, 1) == "2" && Let.fec_nxtpg <= _fecvenc))
                && (_filtro.Substring(2, 1) == "1" || (_filtro.Substring(2, 1) == "2" && Let.estado == _LetEst))
                && (_filtro.Substring(3, 1) == "1" || (_filtro.Substring(3, 1) == "2" && Let.cmoneda == _moneda))
                && (_filtro.Substring(4, 1) == "1" || (_filtro.Substring(4, 1) == "2" && Let.PerCod == _CodTra))
                && (_filtro.Substring(5, 1) == "1" || (_filtro.Substring(5, 1) == "2" && Let.codPer == id_persona))
                )
            .OrderByDescending(Let => new { Let.TraNombre, Let.PrvRazon, Let.FecEmesion })
            .Select(Let => new
            {
                Let.valor,
                Let.numDoc,
                Let.fac_acum,
                Let.codPer,
                Let.PrvRazon,
                Let.FecEmesion,
                Let.itotcuota,
                Let.nmontocuota,
                Let.pagpendiente,
                Let.cmoneda,
                Let.nintpag,
                Let.estado,
                Let.dfecultpago,
                Let.fec_nxtpg,
                Let.icodletra,
                Let.ctippago,
                Let.PerCod,
                Let.TraNombre
            })
            .AsQueryable());
        }
    }

    public class clsvwList_let_detalle : clsAbstractBase<vwList_let_detalle>, IvwList_let_detalle{
        public DataTable GetList_letra(String _filtro, String _letra, DateTime _fecvenc,
            String _LetEst, String _moneda, int _CodTra, int id_persona, String _valor)
        {
            return ToDataTable(this.Find(Let => Let.valor == _valor
                && (_filtro.Substring(0, 1) == "1" || (_filtro.Substring(0, 1) == "2" && Let.numDoc.Contains(_letra)))
                && (_filtro.Substring(1, 1) == "1" || (_filtro.Substring(1, 1) == "2" && Let.fec_nxtpg <= _fecvenc))
                && (_filtro.Substring(2, 1) == "1" || (_filtro.Substring(2, 1) == "2" && Let.estado == _LetEst))
                && (_filtro.Substring(3, 1) == "1" || (_filtro.Substring(3, 1) == "2" && Let.cmoneda == _moneda))
                && (_filtro.Substring(4, 1) == "1" || (_filtro.Substring(4, 1) == "2" && Let.PerCod == _CodTra))
                && (_filtro.Substring(5, 1) == "1" || (_filtro.Substring(5, 1) == "2" && Let.codPer == id_persona))
                )
            .OrderByDescending(Let => new { Let.TraNombre, Let.PrvRazon, Let.FecEmesion })
            .Select(Let => new
            {
                Let.numfac,
                Let.dopFecEmision,
                Let.OpeTotPagar,
                Let.saldo,
                Let.valor,
                Let.numDoc,
                Let.fac_acum,
                Let.codPer,
                Let.PrvRazon,
                Let.FecEmesion,
                Let.itotcuota,
                Let.nmontocuota,
                Let.pagpendiente,
                Let.cmoneda,
                Let.nintpag,
                Let.estado,
                Let.dfecultpago,
                Let.fec_nxtpg,
                Let.icodletra,
                Let.ctippago,
                Let.PerCod,
                Let.TraNombre
            })
            .AsQueryable());
        }
    }
    

    //public class clspag_Letra : clspag_LetDao
    public class clspag_Letra : clsAbstractBase<Pag_letras>, Ipag_let
    {
        public long Maxcod_pago()
        {
            if (this.GetAll().Count() > 0)
            {
                return this.GetAll().Max<Pag_letras>(Let => Convert.ToInt32(Let.icodpago));
            }
            return 0;
        }
        public long Maxidpaglet()
        {
            if (this.GetAll().Count() > 0)
            {
                return this.GetAll().Max<Pag_letras>(Let => Let.idpagletra);
            }
            return 0;
        }
        public int fnpag_letraInsertar(Pag_letras odet_letra)
        {
            this.Add(odet_letra);
            this.SaveChanges();
            return 0;
        }
    }

    //public class clspag_pend : clspag_pendDao
    public class clspag_pend : clsAbstractBase<Pag_pendientes>, Ipag_pend
    {
        public DataTable GetList(int _Vendedor, String _cliente, string _tipo)
        {
            if (_Vendedor == 0)
            {
                return ToDataTable(this.Find(DetPag => DetPag.OpeTipo == _tipo && DetPag.cliente.Contains(_cliente))
                        .OrderBy(DetPag => DetPag.vendedor)
                        .Select(DetPag => new
                        {
                            DetPag.icodletra,
                            DetPag.ult,
                            DetPag.inumletra,
                            DetPag.dfecvenc,
                            DetPag.idetletra,
                            DetPag.cod_vendedor,
                            DetPag.vendedor,
                            DetPag.DeudTot,
                            DetPag.pagTotAcum,
                            DetPag.mntCuot,
                            DetPag.pagoCuot,
                            DetPag.cod_cliente,
                            DetPag.cliente,
                            DetPag.ctippago,
                            DetPag.OpeTipo,
                            Sal_tot = DetPag.DeudTot - DetPag.pagTotAcum,
                            sal_cuot = DetPag.mntCuot - DetPag.pagoCuot
                        })
                        .AsQueryable());
            }
            else
            {
                return ToDataTable(this.Find(DetPag => DetPag.OpeTipo == _tipo
                    && DetPag.cliente.Contains(_cliente)
                    && DetPag.cod_vendedor == _Vendedor)
                        .OrderBy(DetPag => DetPag.vendedor)
                        .Select(DetPag => new
                        {
                            DetPag.icodletra,
                            DetPag.ult,
                            DetPag.inumletra,
                            DetPag.dfecvenc,
                            DetPag.idetletra,
                            DetPag.cod_vendedor,
                            DetPag.vendedor,
                            DetPag.DeudTot,
                            DetPag.pagTotAcum,
                            DetPag.mntCuot,
                            DetPag.pagoCuot,
                            DetPag.cod_cliente,
                            DetPag.cliente,
                            DetPag.ctippago,
                            DetPag.OpeTipo,
                            Sal_tot = DetPag.DeudTot - DetPag.pagTotAcum,
                            sal_cuot = DetPag.mntCuot - DetPag.pagoCuot
                        })
                        .AsQueryable());
            }
        }
    }

    //public class clspag_pendnow : clspag_pennowDao
    public class clspag_pendnow : clsAbstractBase<pag_pendnow>, Ipag_pendNow
    {
        public DataTable GetList(int _Vendedor, String _cliente, string _tipo)
        {
            if (_Vendedor == 0)
            {
                return ToDataTable(this.Find(DetPag => DetPag.OpeTipo == _tipo && DetPag.cliente.Contains(_cliente))
                        .OrderBy(DetPag => DetPag.vendedor)
                        .Select(DetPag => new
                        {
                            DetPag.icodletra,
                            DetPag.ult,
                            DetPag.inumletra,
                            DetPag.dfecvenc,
                            DetPag.idetletra,
                            DetPag.cod_vendedor,
                            DetPag.vendedor,
                            DetPag.DeudTot,
                            DetPag.pagTotAcum,
                            DetPag.mntCuot,
                            DetPag.pagoCuot,
                            DetPag.cod_cliente,
                            DetPag.cliente,
                            DetPag.ctippago,
                            DetPag.OpeTipo,
                            Sal_tot = DetPag.DeudTot - DetPag.pagTotAcum,
                            sal_cuot = DetPag.mntCuot - DetPag.pagoCuot
                        })
                        .AsQueryable());
            }
            else
            {
                return ToDataTable(this.Find(DetPag => DetPag.OpeTipo == _tipo
                    && DetPag.cliente.Contains(_cliente)
                    && DetPag.cod_vendedor == _Vendedor)
                        .OrderBy(DetPag => DetPag.vendedor)
                        .Select(DetPag => new
                        {
                            DetPag.icodletra,
                            DetPag.ult,
                            DetPag.inumletra,
                            DetPag.dfecvenc,
                            DetPag.idetletra,
                            DetPag.cod_vendedor,
                            DetPag.vendedor,
                            DetPag.DeudTot,
                            DetPag.pagTotAcum,
                            DetPag.mntCuot,
                            DetPag.pagoCuot,
                            DetPag.cod_cliente,
                            DetPag.cliente,
                            DetPag.ctippago,
                            DetPag.OpeTipo,
                            Sal_tot = DetPag.DeudTot - DetPag.pagTotAcum,
                            sal_cuot = DetPag.mntCuot - DetPag.pagoCuot
                        })
                        .AsQueryable());
            }
        }
    }

    //public class clsprovLetra : clsProvLetraDao
    public class clsprovLetra : clsAbstractBase<proveedor_letra>, IProvletra
    {
        public DataTable GetList(string _Moneda, string _cliente, string _tipo)
        {
            return ToDataTable(this.Find(ProvLet => 1 == 1)
                        .OrderBy(ProvLet => ProvLet.PrvRazon)
                        .Select(ProvLet => new
                        {
                            ProvLet.tipo,
                            ProvLet.icodletra,
                            ProvLet.valor,
                            ProvLet.dfecemision,
                            ProvLet.dopCod,
                            ProvLet.numfac,
                            ProvLet.PrvRazon,
                            ProvLet.opetotpagar,
                            ProvLet.PrvCod,
                            ProvLet.CMONEDA,
                            ProvLet.ITOTCUOTA,
                            ProvLet.IESTADO,
                            ProvLet.mntletDol,
                            ProvLet.SalDol,
                            ProvLet.MNTLTSOL,
                            ProvLet.salsol

                        })
                        .AsQueryable());
        }
    }

    public class clsdetalle_pagos : clsAbstractBase<detalle_pagos>, Idetalle_pagos {
        public DataTable GetList_letra(String _filtro, String _tipo, int _codVend, int _codcli, String _estado,
            DateTime _dfvendesde,DateTime _dfvenhasta, DateTime _dpagdesde, DateTime _dpaghasta, 
            String _moneda,int _icod_let, int _idetLet  )
        {
            return ToDataTable(this.Find(Let => Let.OpeTipo == _tipo
                && (_filtro.Substring(0, 1) == "1" || (_filtro.Substring(0, 1) == "2" && Let.traCod== _codVend))
                && (_filtro.Substring(1, 1) == "1" || (_filtro.Substring(1, 1) == "2" && Let.cli_cod <= _codcli))
                && (_filtro.Substring(2, 1) == "1" || (_filtro.Substring(2, 1) == "2" && Let.cestado == _estado))
                && (_filtro.Substring(3, 1) == "1" || (_filtro.Substring(3, 1) == "2" && Let.dfecvenc >= _dfvendesde))
                && (_filtro.Substring(4, 1) == "1" || (_filtro.Substring(4, 1) == "2" && Let.dfecvenc <= _dfvenhasta))
                && (_filtro.Substring(5, 1) == "1" || (_filtro.Substring(5, 1) == "2" && Let.dfecpago >= _dpagdesde))
                && (_filtro.Substring(6, 1) == "1" || (_filtro.Substring(6, 1) == "2" && Let.dfecpago <= _dpaghasta))
                && (_filtro.Substring(7, 1) == "1" || (_filtro.Substring(7, 1) == "2" && Let.diaatr >=0))
                && (_filtro.Substring(8, 1) == "1" || (_filtro.Substring(8, 1) == "2" && Let.cmoneda == _moneda))
                && (_filtro.Substring(9, 1) == "1" || (_filtro.Substring(9, 1) == "2" && Let.icodletra == _icod_let))
                && (_filtro.Substring(10, 1) == "1" || (_filtro.Substring(10, 1) == "2" && Let.idetletra == _idetLet))
                )
            .OrderByDescending(Let => new { Let.vendedor, Let.CliNombre, Let.icodletra, Let.idetletra })
            .Select(Let => new
            {

                Let.vendedor,
                Let.ccodletra,
                Let.cnumletra,
                Let.cnumcuota,
                Let.CliNombre,
                Let.OpeFecEmision,
                Let.dfecvenc,
                Let.dfecpago,
                Let.diaatr,
                Let.cmoneda,
                Let.total,
                Let.saldo,
                Let.PAG_TOTAL,
                Let.mnt_cuota,
                Let.saldo_cuota,
                Let.pag_cuota,
                Let.ninteres,
                Let.traCod,
                Let.OpeTipo,
                Let.cestado,
                Let.idetletra,
                Let.icodletra,
                Let.cli_cod,
                Let.inumletra

            })
            .AsQueryable());
        }
    }
    //_now
    public class clsdetalle_pagos_now : clsAbstractBase<detalle_pagos_now>, Idetalle_pagos_now
    {
        public DataTable GetList_letra(String _filtro, String _tipo, int _codVend, int _codcli, String _estado,
            DateTime _dfvendesde, DateTime _dfvenhasta, DateTime _dpagdesde, DateTime _dpaghasta,
            String _moneda, int _icod_let, int _idetLet)
        {
            return ToDataTable(this.Find(Let => Let.OpeTipo == _tipo
                && (_filtro.Substring(0, 1) == "1" || (_filtro.Substring(0, 1) == "2" && Let.traCod == _codVend))
                && (_filtro.Substring(1, 1) == "1" || (_filtro.Substring(1, 1) == "2" && Let.cli_cod <= _codcli))
                && (_filtro.Substring(2, 1) == "1" || (_filtro.Substring(2, 1) == "2" && Let.cestado == _estado))
                && (_filtro.Substring(3, 1) == "1" || (_filtro.Substring(3, 1) == "2" && Let.dfecvenc >= _dfvendesde))
                && (_filtro.Substring(4, 1) == "1" || (_filtro.Substring(4, 1) == "2" && Let.dfecvenc <= _dfvenhasta))
                && (_filtro.Substring(5, 1) == "1" || (_filtro.Substring(5, 1) == "2" && Let.dfecpago >= _dpagdesde))
                && (_filtro.Substring(6, 1) == "1" || (_filtro.Substring(6, 1) == "2" && Let.dfecpago <= _dpaghasta))
                && (_filtro.Substring(7, 1) == "1" || (_filtro.Substring(7, 1) == "2" && Let.diaatr >= 0))
                && (_filtro.Substring(8, 1) == "1" || (_filtro.Substring(8, 1) == "2" && Let.cmoneda == _moneda))
                && (_filtro.Substring(9, 1) == "1" || (_filtro.Substring(9, 1) == "2" && Let.icodletra == _icod_let))
                && (_filtro.Substring(10, 1) == "1" || (_filtro.Substring(10, 1) == "2" && Let.idetletra == _idetLet))
                )
            .OrderByDescending(Let => new { Let.vendedor, Let.CliNombre, Let.icodletra, Let.idetletra })
            .Select(Let => new
            {

                Let.vendedor,
                Let.ccodletra,
                Let.cnumletra,
                Let.cnumcuota,
                Let.CliNombre,
                Let.OpeFecEmision,
                Let.dfecvenc,
                Let.dfecpago,
                Let.diaatr,
                Let.cmoneda,
                Let.total,
                Let.saldo,
                Let.PAG_TOTAL,
                Let.mnt_cuota,
                Let.saldo_cuota,
                Let.pag_cuota,
                Let.ninteres,
                Let.traCod,
                Let.OpeTipo,
                Let.cestado,
                Let.idetletra,
                Let.icodletra,
                Let.inumletra,
                Let.cli_cod,
                Let.ult

            })
            .AsQueryable());
        }
    }
}

