using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using pryAgrocomercioDAL;
using pryAgrocomercioBLL.EntityCollection.Interfaces;
using System.Web;
using System.Text.RegularExpressions;

namespace pryAgrocomercioBLL.EntityCollection
{
    public class clsOperaciones : clsAbstractBase<Operaciones>, IOperaciones
    {
        public clsOperaciones()
            : base()
        {

        }

        public clsOperaciones(AgrocomercioEntities _AgroEntity)
            : base(_AgroEntity)
        {

        }

        DataRow drForm;

#region FUNCIONES DE MANTENIMIENTO
        public Boolean Guardar(Object oForm, string _OpeTipo, ref long pnDopCod, ref int pntcmCod)
        {
            drForm = FormToDataRow(oForm);

            clsDocumenOperacion lstDocumenOpe = new clsDocumenOperacion(AgroEntidades);
            clsDetOperacion lstDetOperacion = new clsDetOperacion(AgroEntidades);
            clsTipoCambios colTipoCambio = new clsTipoCambios(AgroEntidades);
            Operaciones Operacion = new Operaciones();

            long nOpeCod = Convert.ToInt64(drForm["nOpeCod"]);
            long _nDopCod = pnDopCod;
            String cProceso = drForm["cProceso"].ToString();
            int nDetallesChanged = int.Parse(drForm["bDetallesChanged"].ToString());
            Boolean bDetallesChanged = nDetallesChanged==1?true:false;
            int ntcmCod = 0;
            try
            {
                Operacion = GetOperacion(nOpeCod);

                if (Operacion != null)
                    cProceso = "EDIT";
                
                if (cProceso == "NEW")
                {
                    Operacion = new Operaciones();
                }
                else if (cProceso == "EDIT")
                {
                    if (bDetallesChanged)
                    {
                        if (_OpeTipo == "C") //PARA COMPRAS
                            lstDetOperacion.DeleteDetOperacion((int)nOpeCod, "ALL");
                        else //PARA VENTAS
                            lstDetOperacion.DeleteDetOperacion((int)nOpeCod);                     
                    }
                    
                    Operacion = GetOperacion(nOpeCod);
                }

                if ((drForm["cOpeMoneda"].ToString() == "PEN") ||
                    (drForm["cOpeMoneda"].ToString() == "USD" && Convert.ToDouble(drForm["nTipoCambioOri"]) == Convert.ToDouble(drForm["nTipoCambio"])))
                    ntcmCod = Convert.ToInt32(drForm["ntcmCod"]);
                else
                {
                    ntcmCod = colTipoCambio.MaxTcmCod() + 1;
                    Operacion.TipoCambios = new TipoCambios();
                    Operacion.TipoCambios.tcmCod = ntcmCod;
                    Operacion.TipoCambios.tcmMoneda = drForm["cOpeMoneda"].ToString();
                    Operacion.TipoCambios.tcmCambio = Convert.ToDecimal(drForm["nTipoCambio"]);
                    Operacion.TipoCambios.tcmFecha = DateTime.Today;
                    Operacion.TipoCambios.tcmfecmod = DateTime.Today;
                }

                //DATOS DE LA OPERACION               
                Operacion.OpeCod = nOpeCod;
                Operacion.OpeTipo = _OpeTipo;
                Operacion.OpeFecEmision = Convert.ToDateTime(drForm["dOpeFecEmision"]);
                Operacion.ZonCod = Convert.ToInt32(drForm["cZonCod"]);
                if (_OpeTipo == "C")
                {
                    if ((Int32)drForm["ntdoCod"] > 3)
                        Operacion.PerCod = (Int32)drForm["nOpePrvCod"];
                    else
                        Operacion.PrvCod = Convert.ToInt32(drForm["nOpePrvCod"]);
                    Operacion.OpeOrdCompra = "";
                }
                else
                {
                    if (drForm["ntdoCod"].ToString() == "7")
                        Operacion.PerCod = (Int32)drForm["nOpeCliCod"];
                    else 
                    {
                        Operacion.CliCod = Convert.ToInt32(drForm["nOpeCliCod"]);
                        Operacion.PerCod = (Int32)drForm["nOpeVendedor"];
                    }
                    Operacion.OpeOrdCompra = drForm["COpeOrdCompra"].ToString();
                }
                Operacion.OpeMoneda = drForm["cOpeMoneda"].ToString();
                Operacion.OpeTipPago = drForm["cOpeTipPago"].ToString();
                Operacion.tcmCod = ntcmCod;
                Operacion.OpeSubTotal = (Decimal)drForm["nOpeSubTotal"];
                Operacion.OpeDscto = (Decimal)drForm["nOpeDscto"];
                Operacion.OpeImpuesto = (Decimal)drForm["nOpeImpuesto"];
                Operacion.OpeFlete = (Decimal)drForm["nOpeFlete"];
                Operacion.OpeTotal = (Decimal)drForm["nOpeTotal"];
                if (Operacion.OpeEstado != "P")
                    Operacion.OpeEstado = "R";
                Operacion.OpeModifica = DateTime.Now;
                Operacion.UsrCod = gcUsrCod;
                                
                if (Operacion.OpeTipPago == "CR")
                {
                    Operacion.OpeTipCiclo = drForm["cOpeTipCiclo"].ToString();
                    Operacion.OpeCiclo = (Int32)drForm["nOpeCiclo"];
                }
                else {
                    Operacion.OpeTipCiclo = "D";
                    Operacion.OpeCiclo = 0;
                }
                
                if (bDetallesChanged)
                    lstDetOperacion.Guardar(ref Operacion);

                if (cProceso == "NEW")
                    Add(Operacion);
                else if (cProceso == "EDIT")
                    Update(Operacion);
                SaveChanges();
                
                lstDocumenOpe.Guardar(oForm, ref pnDopCod);
                pntcmCod = ntcmCod;
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            lstDocumenOpe = null;
            Operacion = null;
            return true;
        }
        public Boolean Anular(int pnOpeCod)
        {
            clsDocumenOperacion lstDocOperacion = new clsDocumenOperacion();
            clsDetOperacion lstDetOperacion = new clsDetOperacion();

            Operaciones Operacion;
            DocumenOperacion DocumenOpe = new DocumenOperacion();
            DetOperacion DetalleOpe = new DetOperacion();
            
            try
            {
                
                Operacion = GetOperacion(pnOpeCod);

                foreach (DocumenOperacion Doc in Operacion.DocumenOperacion.ToList())
                {
                    DocumenOpe = lstDocOperacion.GetDocumenOperacion((int)Doc.dopCod);
                    DocumenOpe.dopEstado = "I";
                    lstDocOperacion.Update(DocumenOpe);
                }

                foreach (DetOperacion Det in Operacion.DetOperacion.ToList())
                {
                    DetalleOpe = lstDetOperacion.GetDetOperacion((int)Det.dtpCod);
                    if (Operacion.OpeEstado == "P" || Operacion.OpeEstado == "C") // SOLO ENTRA SI EL ESTADO ES PROCESADO O CERRADO
                    {
                        if (Operacion.OpeTipo == "C") // PARA COMPRAS
                            DetalleOpe.Articulos.ArtStock -= (decimal)Det.dtpCantidad;
                        else // PARA VENTAS
                        {
                            DetalleOpe.Articulos.ArtStock += (decimal)Det.dtpCantidad;
                            DetalleOpe.Articulos.ArtStockFac -= (decimal)Det.dtpCantidad;
                        }
                    }

                    DetalleOpe.dtpEstado = false;
                    if (Operacion.OpeTipo == "C") // PARA COMPRAS
                    {
                        DetalleOpe.LotesArt.LotEstado = "I";
                        DetalleOpe.LotesArt.LotPrecioCom = 0;
                        DetalleOpe.LotesArt.LotPrecioVen = 0;
                        DetalleOpe.LotesArt.LotStock = 0;
                    }                    
                    else // PARA VENTAS
                    {
                        DetalleOpe.LotesArt.LotEstado = "A";
                        DetalleOpe.LotesArt.LotStock += (decimal)Det.dtpCantidad;
                    }
                    
                    lstDetOperacion.Update(DetalleOpe);
                }
                                
                Operacion.OpeEstado = "A";
                 Operacion.OpeModifica = DateTime.Now;
                Update(Operacion);

                SaveChanges();
                lstDocOperacion.SaveChanges();
                lstDetOperacion.SaveChanges();                
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return true;
        }
        public Boolean Procesar(Object oForm, string _OpeTipo, ref long pnDopCod, ref int pntcmCod)
        {
            drForm = FormToDataRow(oForm);

            clsDetOperacion lstDetOperaciones = new clsDetOperacion();

            Operaciones Operacion = new Operaciones();
            String cProceso = drForm["cProceso"].ToString();
            int nOpeCod = Convert.ToInt32(drForm["nOpeCod"]);
            try
            {
                Guardar(oForm, _OpeTipo, ref pnDopCod, ref pntcmCod);

                Operacion = GetOperacion(nOpeCod);
                Operacion.OpeEstado = "P";                
                Operacion.OpeModifica = DateTime.Now;
                Update(Operacion);
                lstDetOperaciones.Procesar(ref Operacion);
                
                SaveChanges();                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }
        public Boolean Cerrar(Object oForm)
        {
            drForm = FormToDataRow(oForm);

            Operaciones Operacion = new Operaciones();
            String cProceso = drForm["cProceso"].ToString();
            int nOpeCod = Convert.ToInt32(drForm["nOpeCod"]);
            try
            {
                Operacion = GetOperacion(nOpeCod);
                Operacion.OpeEstado = "C";
                Operacion.OpeModifica = DateTime.Now;
                Operacion.OpeFecCancela = DateTime.Now;
                Update(Operacion);
                SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }
#endregion

#region FUNCIONES DE CONSULTA
        public Operaciones GetOperacion(long OpeCod)
        {
            return this.Find(Ope => Ope.OpeCod == OpeCod).FirstOrDefault();
        }
        public DataTable GetListOperaciones(string _OpeTipo, Boolean bPorFechas, DateTime _dFecIni, DateTime _dFecFin, string _OpeEstado = "", int _nPrvCod = 0, int _nCliCod = 0, string _cNroSerie = "", string _cNroDoc = "")
        {
            clsAtributos lstAtributos = new clsAtributos(AgroEntidades);

            try
            {
                var lstAtrib = AgroEntidades.Atributos.Where(Atr => Atr.AtrTipoCod == 6);

                var lstOpe = (this.Find(Ope => Ope.OpeTipo == _OpeTipo &&
                    ((bPorFechas && (DateTime)Ope.OpeFecEmision >= _dFecIni && (DateTime)Ope.OpeFecEmision <= _dFecFin) || (!bPorFechas)) &&
                    ((_OpeEstado == "" && Ope.OpeEstado != "C" && Ope.OpeEstado != "A") || (_OpeEstado != "" && Ope.OpeEstado == _OpeEstado)) &&
                    ((_nPrvCod != 0 && Ope.PrvCod == _nPrvCod) || (_nPrvCod == 0)) &&
                    ((_nCliCod != 0 && Ope.CliCod == _nCliCod) || (_nCliCod == 0)) &&
                    ((_cNroSerie != "" && Ope.DocumenOperacion.Any(Doc => Doc.dopNroSerie == _cNroSerie)) || (_cNroSerie == "")) &&
                    ((_cNroDoc != "" && Ope.DocumenOperacion.Any(Doc => Doc.dopNumero == _cNroDoc)) || (_cNroDoc == ""))
                    ));

                var result = from Ope in lstOpe
                             join Atr in lstAtrib on Ope.OpeEstado equals Atr.AtrCodigo.Trim()
                             orderby Ope.OpeEstado descending, Ope.OpeCod descending
                             select new
                             {
                                 Ope.OpeCod,
                                 PrvRazon = Ope.DocumenOperacion.All(Do => Do.tdoCod < 4 ) ? Ope.Proveedores.PrvRazon :
                                                Ope.Personal.perNombres + " " + Ope.Personal.perApellidoPat + " " + Ope.Personal.perApellidoMat,
                                 CliNombre = Ope.DocumenOperacion.All(Do => Do.tdoCod != 7) ? Ope.Clientes.CliNombre :
                                                Ope.Personal.perNombres + " " + Ope.Personal.perApellidoPat + " " + Ope.Personal.perApellidoMat,
                                 Ope.OpeMoneda,
                                 OpeTotal = Math.Round((decimal)(Ope.OpeTotal/ Ope.TipoCambios.tcmCambio),2),
                                 Ope.OpeFecEmision,
                                 Ope.OpeEstado,
                                 Atr.AtrDescripcion
                             };

                return ToDataTable<object>(result.AsQueryable());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public long MaxOpeCod()
        {
            if (this.GetAll().Count() > 0)
            {
                return this.GetAll().Max<Operaciones>(Ope => Ope.OpeCod);
            }
            return 0;
        }
        public Boolean ValidarAnulacion(int _OpeCod, ref string pcResult)
        {
            Boolean bResult = true;
            pcResult = "";
            //clsLotesArt lstLotes = new clsLotesArt();
            clsDetOperacion lstDetOpe = new clsDetOperacion();

            var ResultA = lstDetOpe.Find(Det => Det.OpeCod == _OpeCod
                && Det.Operaciones.OpeTipo == "C"
                && Det.Operaciones.OpeEstado == "P"
                && Det.LotesArt.LotStock != Det.dtpCantidad);

            if (ResultA.Count() != 0)
            {
                pcResult = "Esta compra no se puede ANULAR, El lote Comprado fue Afectado por una Venta. Debe Anular Primero la Venta.";
                bResult = false;
            }
            else{
                var ResultB = Find(Ope => Ope.OpeCod == _OpeCod
                    && Ope.DocumenOperacion.Any(Doc => Doc.icodletra != null));
                
                if (ResultB.Count() != 0)
                {
                    pcResult = "Esta Operacion no se Puede Anular por que ya se le Genero Letras";
                    bResult = false;
                }
            }

            return bResult;
        }

#endregion

#region FUNCIONES DE REPORTES
        public DataTable RepComprasProveedorXMes(int _PrvCod, Boolean _bPorMes, DateTime _dFecPro)
        {
            clsDocumenOperacion colDocumentos = new clsDocumenOperacion();

            try
            {
                var lstOperaciones = Find(Ope => Ope.OpeTipo == "C" && (Ope.OpeEstado == "P" || Ope.OpeEstado == "C") &&
                    ((_PrvCod != 9999 && ((int)Ope.PrvCod) == _PrvCod) || (_PrvCod == 9999)) &&
                    ((_bPorMes && ((DateTime)Ope.OpeFecEmision).Year == _dFecPro.Year && ((DateTime)Ope.OpeFecEmision).Month == _dFecPro.Month) || (!_bPorMes))).ToList();
                //((_bPorFecha && (DateTime)Ope.OpeFecEmision >= _dFecIni && (DateTime)Ope.OpeFecEmision <= _dFecFin) || (!_bPorFecha))).ToList();   
                var ListaOpeCod = lstOperaciones.Select(O => O.OpeCod).ToList<long>();
                var documentos = colDocumentos.Find(D => D.tdoCod == 3 && ListaOpeCod.Contains(D.OpeCod)).ToList();

                var lstComprasPrv = from Ope in lstOperaciones
                                    join doc in documentos on Ope.OpeCod equals doc.OpeCod into dGru
                                    from dGru2 in dGru.DefaultIfEmpty()
                                    orderby Ope.Proveedores.PrvRazon
                                    select new
                                    {
                                        Ope.OpeCod,
                                        Ope.PrvCod,
                                        Ope.Proveedores.PrvRazon,
                                        Ope.OpeFecEmision,
                                        NroFactura = dGru2 == null ? "-" : (dGru2.dopNroSerie + "-" + dGru2.dopNumero),
                                        nTotalUSD = Ope.OpeMoneda == "USD" ? (double)Ope.OpeTotal : 0.0,
                                        nTotalPEN = Ope.OpeMoneda == "PEN" ? (double)Ope.OpeTotal : 0.0
                                    };


                return ToDataTable<object>(lstComprasPrv.AsQueryable());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable RepComprasProveedorGeneral(int _PrvCod, Boolean _bPorFecha, DateTime _dFecIni, DateTime _dFecFin)
        {
            clsDocumenOperacion colDocumentos = new clsDocumenOperacion();
            clsdetletra colDetLetras = new clsdetletra();

            try
            {
                var lstOperaciones = Find(Ope => Ope.OpeTipo == "C" && (Ope.OpeEstado == "P" || Ope.OpeEstado == "C") &&
                    ((_PrvCod != 9999 && ((int)Ope.PrvCod) == _PrvCod) || (_PrvCod == 9999)) &&
                    ((_bPorFecha && (DateTime)Ope.OpeFecEmision >= _dFecIni && (DateTime)Ope.OpeFecEmision <= _dFecFin) || (!_bPorFecha))).ToList();

                var ListaOpeCod = lstOperaciones.Select(O => O.OpeCod).ToList<long>();
                var documentos = colDocumentos.Find(D => D.tdoCod == 3 && ListaOpeCod.Contains(D.OpeCod)).ToList();

                var ListaCodLetra = documentos.Where(O => O.icodletra != null).Select(O => (int)O.icodletra).ToList<int>();
                var detLetras = colDetLetras.Find(Del => ListaCodLetra.Contains(Del.icodletra)).ToList();

                var lstComprasPrv = from Ope in lstOperaciones
                                    join doc in documentos on Ope.OpeCod equals doc.OpeCod into dGru
                                    from dGru2 in dGru.DefaultIfEmpty()
                                    join le in detLetras on (dGru2 == null ? 0 : dGru2.icodletra) equals le.icodletra into dGruLe
                                    from dGruLe2 in dGruLe.DefaultIfEmpty()
                                    orderby Ope.Proveedores.PrvRazon
                                    select new
                                    {
                                        Ope.OpeCod,
                                        Ope.PrvCod,
                                        Ope.Proveedores.PrvRazon,
                                        Ope.OpeFecEmision,
                                        NroFactura = dGru2 == null ? "-" : (dGru2.dopNroSerie + "-" + dGru2.dopNumero),
                                        letraTip = "",
                                        LetraCant = dGruLe2 == null ? "-" : dGruLe2.inumletra.ToString().PadLeft(2, '0') + '/' + dGruLe2.letra.itotcuota.ToString().PadLeft(2, '0'),
                                        dFecVenc = dGruLe2 == null ? Ope.OpeFecCancela : dGruLe2.dfecvenc,
                                        cEstado = dGruLe2 == null ? "PEND" : (dGruLe2.cestado == "2" ? "CANCELADO" : "PEND"),
                                        nCompraUSD = Ope.OpeMoneda == "USD" ? (double)Ope.OpeTotal : 0.0,
                                        nCompraPEN = Ope.OpeMoneda == "PEN" ? (double)Ope.OpeTotal : 0.0,
                                        nDsctoUSD = Ope.OpeMoneda == "USD" ? (double)Ope.OpeDscto : 0.0,
                                        nDsctoPEN = Ope.OpeMoneda == "PEN" ? (double)Ope.OpeDscto : 0.0,
                                        nPagoUSD = dGruLe2 == null ? 0.0 : Ope.OpeMoneda == "USD" && dGruLe2.cestado == "2" ? (double)dGruLe2.ninteres : 0.0,
                                        nPagoPEN = dGruLe2 == null ? 0.0 : Ope.OpeMoneda == "PEN" && dGruLe2.cestado == "2" ? (double)dGruLe2.ninteres : 0.0,
                                        nSaldoUSD = dGruLe2 == null ? 0.0 : Ope.OpeMoneda == "USD" ? (double)dGruLe2.nmonto - (double)dGruLe2.ninteres : 0.0,
                                        nSaldoPEN = dGruLe2 == null ? 0.0 : Ope.OpeMoneda == "PEN" ? (double)dGruLe2.nmonto - (double)dGruLe2.ninteres : 0.0
                                    };


                return ToDataTable<object>(lstComprasPrv.AsQueryable());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataTable RepVentasXComprar(int pnCliCod = 0)
        {
            clsDocumenOperacion colDocumentos = new clsDocumenOperacion();
            clsdetletra colDetLetras = new clsdetletra();

            try
            {
                var lstOperaciones = Find(Ope => Ope.OpeTipo == "V" && (Ope.OpeEstado == "P" || Ope.OpeEstado == "C") &&
                                            ((pnCliCod != 0 && (int)Ope.CliCod == pnCliCod) || (pnCliCod == 0))
                                            ).ToList();

                var ListaOpeCod = lstOperaciones.Select(O => O.OpeCod).ToList<long>();
                var facturas = colDocumentos.Find(D => D.tdoCod == 3 && ListaOpeCod.Contains(D.OpeCod)).ToList();
                var notas = colDocumentos.Find(D => D.tdoCod == 4 && ListaOpeCod.Contains(D.OpeCod)).ToList();
                var Zonas = AgroEntidades.Atributos.Where(D => D.AtrTipoCod == 4).ToList();

                var ListaCodLetra = facturas.Where(O => O.icodletra != null).Select(O => (int)O.icodletra).ToList<int>();
                var detLetras = (from det in colDetLetras.Find(Del => ListaCodLetra.Contains(Del.icodletra)).ToList()
                                group det by det.icodletra into Gru
                                select new { 
                                    icodletra = Gru.Key,
                                    dFecPago = Gru.Select(P => P.dfecpago).Max(),
                                    nMonPago = Gru.Select(P => P.nmonto).Sum()
                                }).ToList();

                var lstComprasPrv = from Ope in lstOperaciones
                                    join Cli in AgroEntidades.Clientes.ToList() on Ope.CliCod equals Cli.CliCod
                                    join Zo in Zonas on Ope.ZonCod.ToString() equals Zo.AtrCodigo
                                    join Per in AgroEntidades.Personal.ToList() on Ope.PerCod equals Per.perCod 
                                    join fac in facturas on Ope.OpeCod equals fac.OpeCod into dGru
                                        from fac2 in dGru.DefaultIfEmpty()
                                    join not in facturas on Ope.OpeCod equals not.OpeCod into dNot
                                        from not2 in dNot.DefaultIfEmpty()
                                    join le in detLetras on (fac2 == null ? 0 : fac2.icodletra) equals le.icodletra into dGruLe
                                        from dGruLe2 in dGruLe.DefaultIfEmpty()
                                    orderby Cli.CliNombre
                                    select new
                                    {
                                        Ope.OpeCod,
                                        Ope.OpeFecEmision,
                                        NroNota = not2 == null ? "-" : (not2.dopNroSerie + "-" + not2.dopNumero),
                                        NroFactura = fac2 == null ? "-" : (fac2.dopNroSerie + "-" + fac2.dopNumero),                                        
                                        Cli.CliNombre,
                                        Zona = Zo.AtrDescripcion,
                                        Vendedor = Per.perNombres,
                                        dFecPago = (dGruLe2 == null ? null : dGruLe2.dFecPago),
                                        Monto_USD = Ope.OpeMoneda == "USD" ? (double)Ope.OpeTotal : 0.0,
                                        ACta_USD = Ope.OpeMoneda == "USD" ? (dGruLe2 == null ? 0.0 : (double)dGruLe2.nMonPago) : 0.0,
                                        Saldo_USD = (Ope.OpeMoneda == "USD" ? (double)Ope.OpeTotal : 0.0) - (Ope.OpeMoneda == "USD" ? (dGruLe2 == null ? 0.0 : (double)dGruLe2.nMonPago) : 0.0),
                                        Monto_PEN = Ope.OpeMoneda == "PEN" ? (double)Ope.OpeTotal : 0.0,
                                        ACta_PEN = Ope.OpeMoneda == "PEN" ? (dGruLe2 == null ? 0.0 : (double)dGruLe2.nMonPago) : 0.0,                                        
                                        Saldo_PEN = (Ope.OpeMoneda == "PEN" ? (double)Ope.OpeTotal : 0.0) - (Ope.OpeMoneda == "PEN" ? (dGruLe2 == null ? 0.0 : (double)dGruLe2.nMonPago) : 0.0),
                                        nSaldoTotal = (Ope.OpeMoneda == "USD" ? (double)Ope.OpeTotal : 0.0) - (Ope.OpeMoneda == "USD" ? (dGruLe2 == null ? 0.0 : (double)dGruLe2.nMonPago) : 0.0) +
                                                 (Ope.OpeMoneda == "PEN" ? (double)Ope.OpeTotal : 0.0) - (Ope.OpeMoneda == "PEN" ? (dGruLe2 == null ? 0.0 : (double)dGruLe2.nMonPago) : 0.0),
                                        Cli.CliCreAsig,
                                        Cli.CliDireccion
                                    };

                                                       

                return ToDataTable<object>(lstComprasPrv.AsQueryable());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable RepResumenCliente()
        {
            clsDocumenOperacion colDocumentos = new clsDocumenOperacion();
            clsdetletra colDetLetras = new clsdetletra();

            try
            {
                var lstOperaciones = Find(Ope => Ope.OpeTipo == "V" && (Ope.OpeEstado == "P" || Ope.OpeEstado == "C") ).ToList();

                var ListaOpeCod = lstOperaciones.Select(O => O.OpeCod).ToList<long>();
                var facturas = colDocumentos.Find(D => D.tdoCod == 3 && ListaOpeCod.Contains(D.OpeCod)).ToList();
                var notas = colDocumentos.Find(D => D.tdoCod == 4 && ListaOpeCod.Contains(D.OpeCod)).ToList();
                var Zonas = AgroEntidades.Atributos.Where(D => D.AtrTipoCod == 4).ToList();

                var ListaCodLetra = facturas.Where(O => O.icodletra != null).Select(O => (int)O.icodletra).ToList<int>();
                var detLetras = (from det in colDetLetras.Find(Del => ListaCodLetra.Contains(Del.icodletra)).ToList()
                                 group det by det.icodletra into Gru
                                 select new
                                 {
                                     icodletra = Gru.Key,
                                     dFecPago = Gru.Select(P => P.dfecpago).Max(),
                                     nMonPago = Gru.Select(P => P.nmonto).Sum()
                                 }).ToList();

                var lstComprasPrv = from Ope in lstOperaciones
                                    join Cli in AgroEntidades.Clientes.ToList() on Ope.CliCod equals Cli.CliCod
                                    join Zo in Zonas on Ope.ZonCod.ToString() equals Zo.AtrCodigo
                                    join Per in AgroEntidades.Personal.ToList() on Ope.PerCod equals Per.perCod
                                    join fac in facturas on Ope.OpeCod equals fac.OpeCod into dGru
                                    from fac2 in dGru.DefaultIfEmpty()
                                    join not in facturas on Ope.OpeCod equals not.OpeCod into dNot
                                    from not2 in dNot.DefaultIfEmpty()
                                    join le in detLetras on (fac2 == null ? 0 : fac2.icodletra) equals le.icodletra into dGruLe
                                    from dGruLe2 in dGruLe.DefaultIfEmpty()
                                    orderby Cli.CliNombre
                                    select new
                                    {
                                        Ope.OpeCod,
                                        Cli.CliNombre,
                                        Anio = Convert.ToDateTime(Ope.OpeFecEmision).Year.ToString(),
                                        Mes = Convert.ToDateTime(Ope.OpeFecEmision).ToString("m"),
                                        Dia_mes = Convert.ToDateTime(Ope.OpeFecEmision).ToString("dd-m"),
                                        NroFactura = fac2 == null ? "-" : (fac2.dopNroSerie + "-" + fac2.dopNumero),
                                        NroNota = not2 == null ? "-" : (not2.dopNroSerie + "-" + not2.dopNumero),
                                        Zona = Zo.AtrDescripcion,
                                        dFecPago = (dGruLe2 == null ? null : dGruLe2.dFecPago),
                                        nVentaUSD = Ope.OpeMoneda == "USD" ? (double)Ope.OpeTotal : 0.0,
                                        nPagoUSD = Ope.OpeMoneda == "USD" ? (dGruLe2 == null ? 0.0 : (double)dGruLe2.nMonPago) : 0.0,
                                        nVentaPEN = Ope.OpeMoneda == "PEN" ? (double)Ope.OpeTotal : 0.0,
                                        nPagoPEN = Ope.OpeMoneda == "PEN" ? (dGruLe2 == null ? 0.0 : (double)dGruLe2.nMonPago) : 0.0,
                                        nSaldoUSD = (Ope.OpeMoneda == "USD" ? (double)Ope.OpeTotal : 0.0) - (Ope.OpeMoneda == "USD" ? (dGruLe2 == null ? 0.0 : (double)dGruLe2.nMonPago) : 0.0),
                                        nSaldoPEN = (Ope.OpeMoneda == "PEN" ? (double)Ope.OpeTotal : 0.0) - (Ope.OpeMoneda == "PEN" ? (dGruLe2 == null ? 0.0 : (double)dGruLe2.nMonPago) : 0.0),
                                        nSaldo = (Ope.OpeMoneda == "USD" ? (double)Ope.OpeTotal : 0.0) - (Ope.OpeMoneda == "USD" ? (dGruLe2 == null ? 0.0 : (double)dGruLe2.nMonPago) : 0.0) +
                                                 (Ope.OpeMoneda == "PEN" ? (double)Ope.OpeTotal : 0.0) - (Ope.OpeMoneda == "PEN" ? (dGruLe2 == null ? 0.0 : (double)dGruLe2.nMonPago) : 0.0)                                        
                                    };



                return ToDataTable<object>(lstComprasPrv.AsQueryable());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
        public DataTable ReporteKardex(int pPrvCod = 0, int pArtCod = 0)
        {
            clsDocumenOperacion colDocumentos = new clsDocumenOperacion();
            clsdetletra colDetLetras = new clsdetletra();
            clsDetOperacion colDetOpera = new clsDetOperacion();
            int nNumCor = 0;
            try
            {
                var lstDetOperaciones = colDetOpera.Find(Det => Det.Operaciones.DocumenOperacion.All(Doc => Doc.tdoCod != 0) && (Det.Operaciones.OpeEstado == "P" || Det.Operaciones.OpeEstado == "C") && (bool)Det.dtpEstado).ToList();
                var lstDocumentos = from doc in colDocumentos.Find(Det => (Det.Operaciones.OpeEstado == "P" || Det.Operaciones.OpeEstado == "C")).ToList()
                                    group doc by doc.OpeCod into GroupDet
                                    select new { OpeCod = GroupDet.Key, GroupDet };

                //Det.Operaciones.DocumenOperacion.OrderByDescending(Doc => Doc.dopCod).Select(Doc => new { Document = Doc.dopNroSerie.ToString() + "-"+ Doc.dopNumero.ToString() } ).First().Document,

                var lstTemporal = (from Det in lstDetOperaciones
                                   join Doc in lstDocumentos on Det.OpeCod equals Doc.OpeCod into dGru
                                   from Doc2 in dGru.DefaultIfEmpty()
                                   where (pPrvCod == 0 || (pPrvCod != 0 && pPrvCod == Det.Articulos.PrvCod)) &&
                                   (pArtCod == 0 || (pArtCod != 0 && pArtCod == Det.ArtCod))
                                   orderby Det.Articulos.PrvCod, Det.Articulos.ArtDescripcion, Det.Operaciones.OpeFecEmision, Det.dtpCod
                                select new
                                {
                                    nCorrela = ++nNumCor,
                                    Det.Articulos.PrvCod,
                                    Det.Articulos.Proveedores.PrvRazon,
                                    Det.ArtCod,
                                    Det.Articulos.ArtDescripcion,
                                    Det.Articulos.ArtStockIni,
                                    Unidad = Det.Articulos.Unidades.UniDescripcion,
                                    Det.Articulos.ArtCostoProm,
                                    CostoInicial = Det.Articulos.ArtStockIni * Det.Articulos.ArtCostoProm,
                                    Det.Operaciones.OpeFecEmision,
                                    Documento = Doc2 == null ? "-" : (Doc2.GroupDet.OrderByDescending(p => p.dopFecEmision).FirstOrDefault().dopNroSerie.ToString() + "-" + Doc2.GroupDet.OrderByDescending(p => p.dopFecEmision).FirstOrDefault().dopNumero.ToString()),
                                    Decripcion = (Det.Operaciones.OpeTipo == "C" ? "Compra " : "Venta ") + Det.Articulos.ArtDescripcion,
                                    nCom_Cantidad = (Det.Operaciones.OpeTipo == "V" ? null : Det.dtpCantidad),
                                    nCom_PreUnitario = (Det.Operaciones.OpeTipo == "V" ? null : Det.dtpPrecioVen),
                                    nCom_Costo = (Det.Operaciones.OpeTipo == "V" ? null : Det.dtpSubTotal),
                                    nVen_Cantidad = (Det.Operaciones.OpeTipo == "C" ? null : Det.dtpCantidad),
                                    nVen_PreUnitario = (Det.Operaciones.OpeTipo == "C" ? null : Det.dtpPrecioVen),
                                    nVen_Costo = (Det.Operaciones.OpeTipo == "C" ? null : Det.dtpSubTotal)
                                }).ToList();


                var lstKardex = from Kar in lstTemporal
                                select new
                                {
                                    Kar.PrvCod, Kar.PrvRazon,
                                    Kar.ArtCod, Kar.ArtDescripcion, Kar.ArtStockIni, Kar.Unidad, Kar.OpeFecEmision, 
                                    Kar.Documento, Kar.Decripcion, Kar.ArtCostoProm,
                                    Kar.nCom_Cantidad, Kar.nCom_PreUnitario, Kar.nCom_Costo, Kar.nVen_Cantidad,
                                    Kar.nVen_PreUnitario, Kar.nVen_Costo,
                                    nSal_Cantidad = (decimal)Kar.ArtStockIni +
                                                    (lstTemporal.Where(De => Kar.ArtCod == De.ArtCod && De.nCorrela <= Kar.nCorrela)
                                                                     .Select(De => (De.nCom_Cantidad == null ? 0 : De.nCom_Cantidad) - (De.nVen_Cantidad == null ? 0 : De.nVen_Cantidad)).Sum()),
                                    nSal_CostoTotal = ((decimal)Kar.ArtStockIni +
                                                    (lstTemporal.Where(De => Kar.ArtCod == De.ArtCod && De.nCorrela <= Kar.nCorrela)
                                                                     .Select(De => (De.nCom_Cantidad == null ? 0 : De.nCom_Cantidad) - (De.nVen_Cantidad == null ? 0 : De.nVen_Cantidad)).Sum())) * Kar.ArtCostoProm
                                };


                //nSal_CostoTotal = (decimal)Kar.CostoInicial +
                //                                    (lstTemporal.Where(De => Kar.ArtCod == De.ArtCod && De.nCorrela <= Kar.nCorrela)
                //                                                     .Select(De => (De.nCom_Costo == null ? 0 : De.nCom_Costo) - (De.nVen_Costo == null ? 0 : De.nVen_Costo)).Sum())



                return ToDataTable<object>(lstKardex.AsQueryable());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<object> ReporteTotalProveedor(int pPrvCod = 0, int pArtCod = 0)
        {
            clsdetletra colDetLetras = new clsdetletra();
            clsDetOperacion colDetOpera = new clsDetOperacion();
            clsArticulos colArticulos = new clsArticulos();
            try
            {
                var lstDetOperaciones = colDetOpera.Find(Det => Det.Operaciones.DocumenOperacion.All(Doc => Doc.tdoCod != 0) && (Det.Operaciones.OpeEstado == "P" || Det.Operaciones.OpeEstado == "C") && (bool)Det.dtpEstado).ToList();

                var lstOperaciones = (from Det in lstDetOperaciones
                                      group Det by Det.ArtCod into GroupDet
                                      select new
                                      {
                                          ArtCod = GroupDet.Key,
                                          nCom_Cantidad = GroupDet.Sum(Det => (Det.Operaciones.OpeTipo == "V" ? 0 : Det.dtpCantidad)),
                                          nCom_Total = GroupDet.Sum(Det => (Det.Operaciones.OpeTipo == "V" ? 0 : Det.dtpSubTotal)),
                                          nVen_Cantidad = GroupDet.Sum(Det => (Det.Operaciones.OpeTipo == "C" ? 0 : Det.dtpCantidad)),
                                          nVen_Total = GroupDet.Sum(Det => (Det.Operaciones.OpeTipo == "C" ? 0 : Det.dtpSubTotal))
                                       }).ToList();


                var lstResult = from Art in colArticulos.GetAll().ToList()
                                where (pPrvCod == 0 || (pPrvCod != 0 && pPrvCod == Art.PrvCod)) &&
                                   (pArtCod == 0 || (pArtCod != 0 && pArtCod == Art.ArtCod))
                                orderby Art.Proveedores.PrvRazon, Art.ArtDescripcion
                                join Ope in lstOperaciones on Art.ArtCod equals Ope.ArtCod into dGru
                                    from Ope2 in dGru.DefaultIfEmpty()
                                select new
                                {
                                    Art.PrvCod,
                                    Art.Proveedores.PrvRazon,
                                    Art.ArtCod,
                                    Art.ArtDescripcion,
                                    Art.ArtStockIni,
                                    nTotalIni = Math.Round((decimal)(Art.ArtStockIni * Art.ArtCostoProm),2),
                                    nCom_Cantidad = Ope2 == null ? 0 : (Ope2.nCom_Cantidad),
                                    nCom_Total = Ope2 == null ? 0 : (Ope2.nCom_Total),
                                    nVen_Cantidad = Ope2 == null ? 0 : (Ope2.nVen_Cantidad),
                                    nVen_Total = Ope2 == null ? 0 : (Ope2.nVen_Total),
                                    Art.ArtStock,
                                    nTotalFin = Math.Round((decimal)((Art.ArtStock * Art.ArtCostoProm)), 2),
                                };
                //nTotalFin = Math.Round((decimal)((Art.ArtStockIni * Art.ArtCostoProm) + (Ope2 == null ? 0 : (Ope2.nCom_Total - Ope2.nVen_Total ))), 2),

                return lstResult.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

#endregion

          
                
    }
}
