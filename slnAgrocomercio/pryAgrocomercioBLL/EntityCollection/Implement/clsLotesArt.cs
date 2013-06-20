using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using pryAgrocomercioDAL;
using pryAgrocomercioBLL.EntityCollection.Interfaces;
using System.Data;

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
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DisminuirLotStock(int nArtCod, decimal nCantidad)
        {
            try
            {
                var Lotes = this.Find(Lot => Lot.ArtCod == nArtCod && Lot.LotEstado == "A" && Lot.LotStock > 0).
                    OrderBy(Lot => Lot.LotNro);

                foreach (LotesArt Lot in Lotes)
                {
                    var oLote = GetLoteArt(nArtCod, Lot.LotCod);
                    oLote.LotFecModi = DateTime.Now;
                    if (nCantidad > oLote.LotStock)
                    {
                        nCantidad -= (decimal)oLote.LotStock;
                        oLote.LotStock = 0;
                        oLote.LotEstado = "I";
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
            catch(Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region FUNCIONES DE CONSULTA
        public LotesArt GetLoteArt(int ArtCod, string cTipReg = "FIRST")
        {
            try
            {
                List<LotesArt> result = this.Find(Lot => Lot.ArtCod == ArtCod && Lot.LotStock > 0 && Lot.LotEstado == "A")
                                   .OrderBy(Lot => Lot.LotNro).ToList();

                if (result.Count() > 0)
                {
                    if (cTipReg == "FIRST")
                        return result.First();
                    else
                        return result.OrderByDescending(Lot => Lot.LotNro).First();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        
           

            //if (cTipReg == "FIRST")
            //{
            //     result = this.Find(Lot => Lot.ArtCod == ArtCod && Lot.LotStock > 0 && Lot.LotEstado == true)
            //    .OrderBy(Lot => Lot.LotNro);
            //}
            //else
            //{
            //     result = this.Find(Lot => Lot.ArtCod == ArtCod && Lot.LotStock > 0 && Lot.LotEstado == true)
            //        .OrderByDescending(Lot => Lot.LotNro);
            //}
            //if (result.Count() > 0)
            //{    
            //    return result.First();                
            //}

            return null;
        }
        public LotesArt GetLoteArt(int ArtCod, int LotCod)
        {
            var result = this.Find(Lot => Lot.ArtCod == ArtCod && Lot.LotCod == LotCod);
            if (result.Count() > 0)
            {
                return result.First();
            }
            return null;
        }
        public decimal GetLoteArtStock(int ArtCod)
        {
            decimal nTotalStock = 0;
            var result = this.Find(Lot => Lot.LotEstado == "A" && Lot.ArtCod == ArtCod);

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
            var result = this.Find(Lot => Lot.ArtCod == ArtCod);

            if (result.Count() > 0)
            {
                return result.Max(Lot => Lot.LotNro);
            }
            return 0;
        }
        public int MaxLotesCod()
        {
            if (this.GetAll().Count() > 0)
            {
                return this.GetAll().Max<LotesArt>(Lot => Lot.LotCod);
            }
            return 0;
        }

        public Double GetCostoPromedio(int ArtCod, double nTasIGV, Double pnPrecio = 0.0)
        {
            Double nCostoProm = 0;
            Decimal nIGV = (Decimal)(nTasIGV + 1);
            List<Decimal> lstPre = null;

            var result = this.Find(Pre => Pre.ArtCod == ArtCod && Pre.LotPrecioVen > 0);

            if (result.Count() > 0)
            {
                //lstPre = result.Select(Pre => (Decimal)((((Pre.LprPrecio - (Pre.LprPrecio * Pre.LprDscto / 100)) * nIGV) +
                //        (Pre.LprPrecio / 100)) * (Pre.Articulos.Proveedores.PrvGanancia > 0 ? Pre.Articulos.Proveedores.PrvGanancia : 1))).ToList();

                lstPre = result.Select(Pre => (Decimal)Pre.LotPrecioVen).ToList();

                if (pnPrecio > 0)
                {
                    pnPrecio = (pnPrecio * (Double)nIGV) + pnPrecio / 100;
                    Double prGanancia = (Double)result.Select(Pre => Pre.Articulos.Proveedores.PrvGanancia).FirstOrDefault();
                    pnPrecio *= prGanancia > 0 ? prGanancia : 1;
                    lstPre.Add((Decimal)pnPrecio);
                }
            }
            if (lstPre != null)
                nCostoProm = Math.Round((Double)lstPre.Average(), 2);
            else
                nCostoProm = 0.0;
            return nCostoProm;
        }


        //public ListaPrecios GetArticuloPrecio(int ArtCod, decimal pLprPrecio)
        //{
        //    var result = this.Find(Pre => Pre.ArtCod == ArtCod && Pre.LprPrecio == pLprPrecio);
        //    return result.FirstOrDefault();
        //}
        //public ListaPrecios GetPrecio(int pLprCod)
        //{
        //    var result = this.Find(Pre => Pre.LprCod == pLprCod);

        //    return result.First<ListaPrecios>();
        //}

        public List<LotesArt> GetListaLotes(int pArtCod)
        {
            var lstPreciosTmp = Find(Pre => Pre.ArtCod == pArtCod && Pre.LotEstado == "A");
            return lstPreciosTmp.ToList();
        }


        #endregion



    }
}