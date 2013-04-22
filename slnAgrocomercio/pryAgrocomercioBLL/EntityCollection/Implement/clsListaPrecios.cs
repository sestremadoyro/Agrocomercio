using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using pryAgrocomercioDAL;
using pryAgrocomercioBLL.EntityCollection.Interfaces;

namespace pryAgrocomercioBLL.EntityCollection
{
    public class clsListaPrecios : clsAbstractBase<ListaPrecios>, IListaPrecios
    {
        public clsListaPrecios()
            : base()
        {

        }

        public clsListaPrecios(AgrocomercioEntities _AgroEntity)
            : base(_AgroEntity)
        {

        }

        #region FUNCIONES DE MANTENIMIENTO

        public int Guardar(int pnArtCod, decimal pnPrecioVen, decimal pnDscto, int cntNuevo)
        {
            ListaPrecios oPrecio = new ListaPrecios();
            int nLprPrecio = 0;
            try
            {
                //SI EL ARTICULO TIENE UN PRECIO NUEVO SE AGREGA
                oPrecio = GetArticuloPrecio(pnArtCod, pnPrecioVen);

                if (oPrecio == null)
                {
                    nLprPrecio = MaxLprCod() + 1 + cntNuevo;
                    oPrecio = new ListaPrecios();
                    oPrecio.LprCod = nLprPrecio;
                    oPrecio.ArtCod = pnArtCod;
                    oPrecio.LprPrecio = pnPrecioVen;
                    oPrecio.LprDscto = pnDscto;
                    oPrecio.LprFecRegis = DateTime.Now;
                    oPrecio.LprEstado = true;
                    Add(oPrecio);
                }
                else
                {
                    nLprPrecio = oPrecio.LprCod;
                }
            }
            catch (Exception ex)
            {
                oPrecio = null;
                throw ex;
            }
            oPrecio = null;
            return nLprPrecio;
        }

        #endregion

        #region FUNCIONES DE CONSULTA
        public ListaPrecios GetArticuloPrecio(int ArtCod)
        {
            var result = this.Find(Pre => Pre.ArtCod == ArtCod);

            if (result.Count() > 0)
            {
                return result.OrderByDescending(Pre => Pre.LprFecRegis).First<ListaPrecios>();
            }
            return null;
        }
        public ListaPrecios GetArticuloPrecio(int ArtCod, decimal pLprPrecio)
        {
            var result = this.Find(Pre => Pre.ArtCod == ArtCod && Pre.LprPrecio == pLprPrecio);
            return result.FirstOrDefault();
        }
        public ListaPrecios GetPrecio(int pLprCod)
        {
            var result = this.Find(Pre => Pre.LprCod == pLprCod);

            return result.First<ListaPrecios>();
        }

        public int MaxLprCod()
        {
            var result = this.GetAll();
            if (result.Count() > 0)
                return result.Max<ListaPrecios>(Pre => Pre.LprCod);
            return 0;
        }
        public int GetLprCod(int ArtCod, decimal LprPrecio)
        {
            var result = this.Find(Pre => Pre.ArtCod == ArtCod && Pre.LprPrecio == LprPrecio).OrderByDescending(Pre => Pre.LprFecRegis);

            if (result.Count() > 0)
            {
                return result.First<ListaPrecios>().LprCod;
            }
            return 0;
        }
        public DataTable GetListaPreciosLotes(int pArtCod)
        {
            clsLotesArt lstLotesArt = new clsLotesArt();

            List<LotesArt> lstLotesTmp = lstLotesArt.GetAll().ToList();

            var lstPreciosTmp = this.GetAll().Where(Pre => Pre.ArtCod == pArtCod && Pre.LprEstado == true)
                    .Select(Pre => new
                    {
                        Pre.LprCod,
                        Pre.ArtCod,
                        Pre.LprFecRegis,
                        Pre.LprPrecio,
                        Pre.LprDscto,
                        Pre.LprEstado
                    });

            var result = from lstPrecios in lstPreciosTmp.AsEnumerable()
                         join lstLotes in lstLotesTmp on lstPrecios.LprCod.ToString() equals lstLotes.LprCod.ToString().Trim()
                         orderby lstPrecios.LprCod
                         select new
                         {
                             lstPrecios.LprCod,
                             lstPrecios.ArtCod,
                             lstPrecios.LprFecRegis,
                             lstPrecios.LprPrecio,
                             lstPrecios.LprDscto,
                             lstPrecios.LprEstado,
                             lstLotes.LotCod,
                             lstLotes.LotNro,
                             lstLotes.LotStock,
                             lstLotes.LotFecRegis,
                             lstLotes.LotFecVenci,
                             lstLotes.LotFecModi,
                             lstLotes.LotEstado
                         };
            return ToDataTable<object>(result.AsQueryable());
        }

        public int MaxListaPrecioCod()
        {
            if (this.GetAll().Count() > 0)
            {
                return this.GetAll().Max<ListaPrecios>(Pre => Pre.LprCod);
            }
            return 0;
        }

        public Double GetCostoPromedio(int ArtCod, double nTasIGV)
        {
            Double nCostoProm = 0;
            var result = this.Find(Pre => Pre.ArtCod == ArtCod && Pre.LprPrecio > 0);

            if (result.Count() > 0)
                nCostoProm += (double)result.Select(Pre => (Pre.LprPrecio - (Pre.LprPrecio * Pre.LprDscto / 100) +
                        (Pre.LprPrecio / 100)) * (Pre.Articulos.Proveedores.PrvGanancia > 0 ? Pre.Articulos.Proveedores.PrvGanancia : 1)).Average();

            return Math.Round(nCostoProm, 2);
        }

        #endregion


    }
}