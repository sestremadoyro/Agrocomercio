using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using pryAgrocomercioDAL;
using pryAgrocomercioBLL.EntityCollection.Interfaces;
using System.Web;

namespace pryAgrocomercioBLL.EntityCollection
{
    public class clsDetOperacion : clsAbstractBase<DetOperacion>, IDetOperacion
    {
        public clsDetOperacion()
            : base()
        {

        }

        public clsDetOperacion(AgrocomercioEntities _AgroEntity)
            : base(_AgroEntity)
        {

        }


#region FUNCIONES DE MANTENIMIENTO
        public Boolean Guardar(ref Operaciones Operacion)
        {
            clsArticulos lstArticulos = new clsArticulos(AgroEntidades);
            clsListaPrecios lstListaPre = new clsListaPrecios(AgroEntidades);
            clsLotesArt lstLotesArt = new clsLotesArt(AgroEntidades);
            ListaPrecios oPrecio = new ListaPrecios();
            
            long nDtpCod = 0;
            int nLotCod = 0;
            int lnArtCod = 0;

            HttpContext contexto = HttpContext.Current;
            
            try
            {
                if (contexto.Session["dtDetOperacion"] != null)
                {
                    DataTable dtDetOperacion = (DataTable)(contexto.Session["dtDetOperacion"]);
                    nDtpCod = MaxDtpCod();
                    nLotCod = lstLotesArt.MaxLotCod();
                    int nCntPrecioNuevo = 0;
                    
                    //DATOS DEL DETALLE DE LA OPERACION
                    foreach (DataRow row in dtDetOperacion.Rows)
                    {
                        lnArtCod = int.Parse(row["ArtCod"].ToString());
                        nDtpCod++;
                        nLotCod++;

                        DetOperacion DetOper = new DetOperacion();
                        DetOper.dtpCod = nDtpCod;
                        DetOper.OpeCod = Operacion.OpeCod;
                        DetOper.ArtCod = lnArtCod;
                        DetOper.dtpCantidad = decimal.Parse(row["dtpCantidad"].ToString());
                        DetOper.dtpPrecioVen = decimal.Parse(row["dtpPrecioVen"].ToString());
                        DetOper.dtpDscto = decimal.Parse(row["dtpDscto"].ToString());
                        DetOper.dtpSubTotal = decimal.Parse(row["dtpSubTotal"].ToString());
                        DetOper.UniCod = lstArticulos.GetUnidadCod((int)DetOper.ArtCod);
                        DetOper.dtpEstado = true;

                        if (Operacion.OpeTipo == "C")
                        {
                            DetOper.LotCod = nLotCod;

                            //SI EL ARTICULO TIENE UN PRECIO NUEVO SE AGREGA
                            int nLprCod = lstListaPre.Guardar(lnArtCod, (decimal)DetOper.dtpPrecioVen, (decimal)DetOper.dtpDscto, nCntPrecioNuevo);
                            
                            //VERIFICA SI ARTICULO NUEVO NO TIENE PRECIOS EN LA TABLA
                            oPrecio = lstListaPre.GetArticuloPrecio(lnArtCod, (decimal)DetOper.dtpPrecioVen);

                            //SI NO TIENE PRECIOS, INCREMENTA EL CODIGO EN 1
                            if (oPrecio == null)
                                nCntPrecioNuevo++;

                            DetOper.LotesArt = new LotesArt();
                            DetOper.LotesArt.LotCod = nLotCod;
                            DetOper.LotesArt.LotNro = int.Parse(row["LotNro"].ToString());
                            DetOper.LotesArt.LotStock = DetOper.dtpCantidad;
                            DetOper.LotesArt.LprCod = nLprCod;
                            DetOper.LotesArt.LotFecRegis = DateTime.Today;
                            DetOper.LotesArt.LotFecVenci = DateTime.Today.AddDays(30);
                            DetOper.LotesArt.LotFecModi = DateTime.Now;
                            DetOper.LotesArt.LotEstado = false;
                        }
                        else
                            DetOper.LotCod = lstLotesArt.GetLotCod(lnArtCod);

                        Operacion.DetOperacion.Add(DetOper);

                    }
                    lstListaPre.SaveChanges();
                }          

            }
            catch (Exception ex)
            {
                throw ex;
            }
            lstArticulos = null;
            lstListaPre = null;
            lstLotesArt = null;
            oPrecio = null;
            return true;
        }
        public Boolean Procesar(ref Operaciones Operacion)
        {
            clsLotesArt colLotesArt = new clsLotesArt(AgroEntidades);
            DetOperacion oDetOperacion;
            try
            {
                //DATOS DEL DETALLE DE LA OPERACION
                foreach (DetOperacion Det in Operacion.DetOperacion )
                {
                    oDetOperacion = GetDetOperacion((int)Det.dtpCod);
                    oDetOperacion.Articulos.ArtFecModi = DateTime.Now;
                    oDetOperacion.LotesArt.LotFecModi = DateTime.Now;
                    if (Operacion.OpeTipo == "C") // PARA COMPRAS
                    {
                        oDetOperacion.Articulos.ArtStock += (decimal)Det.dtpCantidad;
                        oDetOperacion.LotesArt.LotEstado = true;                        
                    }                        
                    else // PARA VENTAS
                    {
                        oDetOperacion.Articulos.ArtStock -= (decimal)Det.dtpCantidad;
                        oDetOperacion.Articulos.ArtStockFac += (decimal)Det.dtpCantidad;
                        if (Det.dtpCantidad > oDetOperacion.LotesArt.LotStock)
                        {
                            //oDetOperacion.LotesArt.LotStock = 0;
                            //oDetOperacion.LotesArt.LotEstado = false;
                            colLotesArt.DisminuirLotStock((int)oDetOperacion.ArtCod, (decimal)Det.dtpCantidad);
                        }
                        else
                        {
                            oDetOperacion.LotesArt.LotStock -= (decimal)Det.dtpCantidad;
                            oDetOperacion.LotesArt.LotEstado = (oDetOperacion.LotesArt.LotStock > 0);
                        }
                        
                    }
                    Update(oDetOperacion);
                }
                SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }
        public Boolean DeleteDetOperacion(int OpeCod, string cTipo = "DET")
        {
            clsLotesArt lstLoteArt = new clsLotesArt();
            try
            {
                var Detalles = this.Find(Det => Det.OpeCod == OpeCod);

                foreach (DetOperacion Detalle in Detalles)
                {
                    if (cTipo == "ALL")
                        lstLoteArt.DeleteLoteArticulo((int)Detalle.LotCod);
                    this.Delete(Detalle);
                }
                this.SaveChanges();
                lstLoteArt.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public Boolean UpdatePrecio(long nOpeCod, int nArtCod, decimal nPrecio)
        {
            DetOperacion DetOper = GetDetOperacion((int)nOpeCod, nArtCod);
            DetOper.LotesArt.ListaPrecios.LprPrecio = nPrecio;
            Update(DetOper);
            SaveChanges();

            return true;
        }
#endregion

#region FUNCIONES DE CONSULTA
        public DetOperacion GetDetOperacion(int _dtpCod)
        {
            return this.Find(Det => Det.dtpCod == _dtpCod).First<DetOperacion>();
        }
        public DetOperacion GetDetOperacion(int _OpeCod, int _ArtCod)
        {
            return this.Find(Det => Det.OpeCod == _OpeCod && Det.ArtCod == _ArtCod).First<DetOperacion>();
        }
        public long MaxDtpCod()
        {
            if (this.GetAll().Count() > 0)
            {
                return this.GetAll().Max<DetOperacion>(Det => Det.dtpCod);
            }
            return 0;
        }
        public DataTable GetListDetOperacion(int OpeCod)
        {
            return ToDataTable(this.Find(Det => Det.OpeCod == OpeCod)
                .Select(Det => new
                {
                    Det.ArtCod,
                    Det.Articulos.ArtPeso,
                    Det.LotesArt.LotNro,
                    Det.Articulos.ArtDescripcion,
                    Det.Articulos.Unidades.UniAbrev,
                    Det.dtpCantidad,
                    Det.dtpPrecioVen,
                    Det.dtpDscto,
                    Det.dtpSubTotal
                })
                .AsQueryable());

        }

#endregion       

    }
}
