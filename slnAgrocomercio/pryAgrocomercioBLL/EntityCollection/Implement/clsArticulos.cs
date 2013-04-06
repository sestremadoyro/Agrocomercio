using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using pryAgrocomercioDAL;
using pryAgrocomercioBLL.EntityCollection.Interfaces;
using System.Text.RegularExpressions;
using System.Data.Objects.SqlClient;

namespace pryAgrocomercioBLL.EntityCollection
{
    public class clsArticulos : clsAbstractBase<Articulos>, IArticulos
    {
        public clsArticulos()
            : base()
        {

        }

        public clsArticulos(AgrocomercioEntities _AgroEntity)
            : base(_AgroEntity)
        {

        }

        public Articulos GetArticulo(int ArtCod)
        {
            try
            {
                var Result = this.Find(Art => Art.ArtCod == ArtCod);
                if (Result.Count() == 0)
                    return null;
                else
                    return Result.First<Articulos>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Articulos> GetArticulosStock()
        {
            try
            {
                var Result = this.Find(Art => Art.ArtStock > 0 && Art.Artestado).ToList();
                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Articulos> GetArticulosStockByPrvCod(int PrvCod)
        {
            try
            {
                var Result = this.Find(Art => Art.ArtStock > 0 && Art.Artestado && Art.PrvCod == PrvCod).ToList();
                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetUnidadCod(int ArtCod)
        {
            return (int)this.Find(Art => Art.ArtCod == ArtCod).First<Articulos>().UniCod;
        }

        public List<Articulos> GetArticulosByPrvCod(int PrvCod)
        {
            try
            {
                var Result = this.Find(Art => Art.Artestado && Art.PrvCod == PrvCod).ToList();
                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable BuscarArticulos(string pcArtDescri, Boolean bConStock = false)
        {
            pcArtDescri = '%' + pcArtDescri.Replace(' ', '%') + '%';

            IQueryable<Object> Result;
            if (bConStock)
            {
                Result = this.Find(Art => Art.ArtStock > 0 && Art.Artestado && SqlFunctions.PatIndex(pcArtDescri, Art.ArtDescripcion) > 0)
                        .Select(Art => new
                        {
                            Art.ArtCod,
                            Art.ArtDescripcion
                        });
            }
            else
            {
                Result = this.Find(Art => Art.Artestado && SqlFunctions.PatIndex(pcArtDescri, Art.ArtDescripcion) > 0)
                         .Select(Art => new
                         {
                             Art.ArtCod,
                             Art.ArtDescripcion
                         });
            }


            return ToDataTable(Result.AsQueryable());
        }
        
    }
}
