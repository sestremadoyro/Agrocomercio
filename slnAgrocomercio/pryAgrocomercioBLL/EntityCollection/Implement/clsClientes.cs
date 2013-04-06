using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Objects.SqlClient;
using pryAgrocomercioDAL;
using pryAgrocomercioBLL.EntityCollection.Interfaces;

namespace pryAgrocomercioBLL.EntityCollection
{
    public class clsClientes : clsAbstractBase<Clientes>, IClientes
    {
        DataRow drForm;

#region FUNCIONES DE MANTENIMIENTO
        public Boolean Insert(Object oForm)
        {
            try
            {
                drForm = FormToDataRow(oForm);

                Clientes Cliente = new Clientes();
                Cliente.CliCod = Convert.ToInt32(drForm["nCliCod"]);
                Cliente.CliTipoDoc = drForm["cCliTipoDoc"].ToString();
                Cliente.CliTipoPer = drForm["cCliTipoPer"].ToString();
                Cliente.CliNumDoc = drForm["cCliNumDoc"].ToString();
                Cliente.CliNombre = drForm["cCliNombre"].ToString().ToUpper();
                Cliente.CliDireccion = drForm["cCliDireccion"].ToString().ToUpper();
                Cliente.CliRepresen = drForm["cCliRepresen"].ToString().ToUpper();
                Cliente.CliTelefono = drForm["cCliTelefono"].ToString();
                Cliente.CliFecRegis = Convert.ToDateTime(drForm["dCliFecRegis"]);
                Cliente.CliCreAsig = 0;
                Cliente.CliEstado = true;
                 
                Add(Cliente);
                SaveChanges();

                Cliente = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }
#endregion

#region FUNCIONES DE CONSULTA
        public Clientes GetCliente(int pcCliCod)
        {
            return this.Find(Cli => Cli.CliCod == pcCliCod).First<Clientes>();
        }
        public int MaxCliCod()
        {
            if (this.GetAll().Count() > 0)
            {
                return this.GetAll().Max<Clientes>(Cli => Cli.CliCod);
            }
            return 0;
        }
        public DataTable BuscarClientes(string pcCliNombre)
        {
            pcCliNombre = '%' + pcCliNombre.Replace(' ', '%') + '%';

            return ToDataTable(this.Find(Cli => Cli.CliEstado && SqlFunctions.PatIndex(pcCliNombre, Cli.CliNombre) > 0)
                .Select(Cli => new
                {
                    Cli.CliCod,
                    Cli.CliNombre
                })
                .AsQueryable());
        }
#endregion
        

       
    }
}
