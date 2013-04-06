using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using pryAgrocomercioDAL;
using pryAgrocomercioBLL.EntityCollection.Interfaces;

namespace pryAgrocomercioBLL.EntityCollection
{
    public class clsTipoPersonal:  clsAbstractBase<TipoPersonal>, ITipoPersonal 
    {
        public DataTable GetList()
        {
            var result = this.GetAll().OrderBy(tipo => tipo.tpeDescripcion)
                .Select(tipoPer => new
                {
                    tipoPer.tpeCod,
                    tipoPer.tpeDescripcion

                });

            return ToDataTable<object>(result.AsQueryable());
        }

        public TipoPersonal GetTipoPersonal(int tpeCod)
        {
            try
            {
                var Result = this.Find(tipoPer => tipoPer.tpeCod == tpeCod);
                if (Result.Count() == 0)
                    return null;
                else
                    return Result.First<TipoPersonal>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteTipoPersonal(int tpeCod)
        {
            try
            {
                var TipoPersonal = this.Find(tipoPer => tipoPer.tpeCod == tpeCod).FirstOrDefault();
                this.Delete(TipoPersonal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int MaxLotCod()
        {
            var result = this.GetAll();

            if (result.Count() > 0)
            {
                return result.Max<TipoPersonal>(tipoPer => tipoPer.tpeCod);
            }
            return 0;
        }
    }
}
