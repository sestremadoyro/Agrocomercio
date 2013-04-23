using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Objects.SqlClient;
using pryAgrocomercioDAL;
using pryAgrocomercioBLL.EntityCollection.Interfaces;
using System.Reflection;

namespace pryAgrocomercioBLL.EntityCollection
{
    public class clsProveedores : clsAbstractBase<Proveedores>, IProveedores
    {
        DataRow drForm;

#region FUNCIONES DE MANTENIMIENTO
        public Boolean Insert(Object oForm) 
        {
            try
            {
                drForm = FormToDataRow(oForm);

                Proveedores Proveedor = new Proveedores();
                Proveedor.PrvCod = Convert.ToInt32(drForm["nPrvCod"]);
                Proveedor.PrvNumDoc = drForm["cPrvNumDoc"].ToString();
                Proveedor.PrvRazon = drForm["cPrvRazon"].ToString().ToUpper();
                Proveedor.PrvDireccion = drForm["cPrvDireccion"].ToString().ToUpper();
                Proveedor.PrvContacto = drForm["cPrvContacto"].ToString().ToUpper();
                Proveedor.PrvTelefono = drForm["cPrvTelefono"].ToString();
                Proveedor.PrvFecRegis = Convert.ToDateTime(drForm["dPrvFecRegis"]);
                Proveedor.PrvGanancia = 1;
                Proveedor.PrvDscto = 0;
                Proveedor.PrvEstado = true;

                Add(Proveedor);
                SaveChanges();

                Proveedor = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }            
            return true;
        }

#endregion

#region FUNCIONES DE CONSULTA
        public Proveedores GetProveedor(int pnPrvCod)
        {
            return this.Find(Prv => Prv.PrvCod == pnPrvCod).First<Proveedores>();
        }

        public new IQueryable<Proveedores> GetAll()
        {
            return base.GetAll().OrderBy(Prv => Prv.PrvRazon);
        }
        public List<Proveedores> GetProveedoresConArticulos()
        {
            return Find(Prv => Prv.Articulos.Count > 0).OrderBy(Prv => Prv.PrvRazon).ToList();
        }
        public int MaxPrvCod()
        {
            if (base.GetAll().Count() > 0)
            {
                return base.GetAll().Max<Proveedores>(Prv => Prv.PrvCod);
            }
            return 0;
        }
        public DataTable BuscarProveedores(string pcPrvDescri)
        {
            pcPrvDescri = '%' + pcPrvDescri.Replace(' ', '%') + '%';

            return ToDataTable<object>(this.Find(Prv => Prv.PrvEstado && SqlFunctions.PatIndex(pcPrvDescri, Prv.PrvRazon) > 0)
                .Select(Prv => new
                {
                    Prv.PrvCod,
                    Prv.PrvRazon
                })
                .AsQueryable());
        }
     
#endregion

       
    }
}
