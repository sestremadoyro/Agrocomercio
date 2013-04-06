using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using pryAgrocomercioDAL;
using pryAgrocomercioBLL.EntityCollection.Interfaces;

namespace pryAgrocomercioBLL.EntityCollection
{
    public class clsTransportistas : clsAbstractBase<Transportistas>, ITransportistas
    {
        public Transportistas GetTransportista(int pnTraCod)
        {
            return this.Find(Tra => Tra.TraCod == pnTraCod).First<Transportistas>(); ;
        }
        public int MaxTraCod()
        {
            if (this.GetAll().Count() > 0)
            {
                return this.GetAll().Max<Transportistas>(Tra => Tra.TraCod);
            }
            return 0;
        }
        public DataTable GetList()
        {
            var result = this.GetAll().OrderBy(Trans => Trans.TraRazonSocial)
                .Select(Trans => new
                {
                    Trans.TraCod,
                    Trans.TraRazonSocial,
                    Trans.TraDireccion,
                    Trans.TraRuc,
                    Trans.TraTelefono,
                    Trans.TraFecRegis,
                    Trans.TraEstado

                });

            return ToDataTable<object>(result.AsQueryable());
        }
        public DataTable GetListActivos()
        {
            var result = this.GetAll().OrderBy(Trans => Trans.TraRazonSocial)
                .Where(Trans => Trans.TraEstado.Value == true)
                .Select(Trans => new
                {
                    Trans.TraCod,
                    Trans.TraRazonSocial,
                    Trans.TraDireccion,
                    Trans.TraRuc,
                    Trans.TraTelefono,
                    Trans.TraFecRegis,
                    Trans.TraEstado

                });

            return ToDataTable<object>(result.AsQueryable());
        }
        public int  DeleteTransportista(int traCod)
        {
            try
            {
                clsOperaciones lstOperaciones = new clsOperaciones();
                
                //valida transportista tiene operaciones
                IQueryable<Operaciones> listaOperacion ;
                listaOperacion = lstOperaciones.Find(Ope => Ope.TraCod == traCod);

                if (listaOperacion.Count() > 0)
                    return 0;
                else
                {
                    //borrado logico
                    var Transportista = this.Find(Trans => Trans.TraCod == traCod).FirstOrDefault();
                    Transportista.TraEstado = false;
                    this.Update(Transportista);
                    this.SaveChanges();
                    return 1;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
