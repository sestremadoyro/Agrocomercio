using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using pryAgrocomercioDAL;
using pryAgrocomercioBLL.EntityCollection.Interfaces;

namespace pryAgrocomercioBLL.EntityCollection
{
    public class clsLotesArt : clsAbstractBase<LotesArt>, ILotesArt
    {
        public clsLotesArt()
            : base()
        {

        }

        public clsLotesArt(AgrocomercioEntities _AgroEntity)
            : base(_AgroEntity)
        {

        }


        #region FUNCIONES DE MANTENIMIENTO
        public void DeleteLoteArticulo(int LotCod)
        {
            try
            {
                var Lote = this.Find(Lot => Lot.LotCod == LotCod).FirstOrDefault();
                this.Delete(Lote);
            }
            catch
            {

            }
        }

        public void DisminuirLotStock(int nArtCod, decimal nCantidad)
        {
            try
            {
                var Lotes = this.Find(Lot => Lot.ListaPrecios.ArtCod == nArtCod && Lot.LotEstado && Lot.LotStock > 0).
                    OrderBy(Lot => Lot.LotNro);

                foreach (LotesArt Lot in Lotes)
                {
                    var oLote = GetLoteArt(nArtCod, Lot.LotCod);
                    oLote.LotFecModi = DateTime.Now;
                    if (nCantidad > oLote.LotStock)
                    {
                        nCantidad -= (decimal)oLote.LotStock;
                        oLote.LotStock = 0;
                        oLote.LotEstado = false;
                    }
                    else
                    {
                        oLote.LotStock -= nCantidad;
                        break;
                    }
                    Update(oLote);
                }
                SaveChanges();
            }
            catch
            {

            }
        }
        #endregion

        #region FUNCIONES DE CONSULTA
        public LotesArt GetLoteArt(int ArtCod)
        {
            var result = this.Find(Lot => Lot.ListaPrecios.ArtCod == ArtCod && Lot.LotStock > 0 && Lot.LotEstado == true)
                .OrderBy(Lot => Lot.LotNro);

            if (result.Count() > 0)
            {
                return result.First();
            }
            return null;
        }
        public LotesArt GetLoteArt(int ArtCod, int LotCod)
        {
            var result = this.Find(Lot => Lot.ListaPrecios.ArtCod == ArtCod && Lot.LotCod == LotCod);
            if (result.Count() > 0)
            {
                return result.First();
            }
            return null;
        }
        public decimal GetLoteArtStock(int ArtCod)
        {
            decimal nTotalStock = 0;
            var result = this.Find(Lot => Lot.LotEstado == true && Lot.ListaPrecios.ArtCod == ArtCod);

            if (result.Count() > 0)
                nTotalStock += (decimal)result.Select(Lot => Lot.LotStock).Sum();
            return nTotalStock;
        }
        public int GetLotCod(int ArtCod)
        {
            var result = GetLoteArt(ArtCod);

            if (result != null)
            {
                return result.LotCod;
            }
            return 0;
        }

        public int MaxLotCod()
        {
            var result = this.GetAll();

            if (result.Count() > 0)
            {
                return result.Max<LotesArt>(Lot => Lot.LotCod);
            }
            return 0;
        }
        public int MaxLotNro(int ArtCod)
        {
            var result = this.Find(Lot => Lot.ListaPrecios.ArtCod == ArtCod);

            if (result.Count() > 0)
            {
                return result.Max(Lot => Lot.LotNro);
            }
            return 0;
        }
        public LotesArt GetLotesPrecio(int pLprCod)
        {
            var result = this.Find(Lot => Lot.LprCod == pLprCod);

            return result.First<LotesArt>();
        }
        public int MaxLotesCod()
        {
            if (this.GetAll().Count() > 0)
            {
                return this.GetAll().Max<LotesArt>(Lot => Lot.LotCod);
            }
            return 0;
        }
        #endregion


    }
}