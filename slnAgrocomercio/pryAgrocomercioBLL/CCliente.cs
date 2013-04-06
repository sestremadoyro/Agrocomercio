using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using pryAgrocomercioDAL.MaestrosDataSetTableAdapters;
using pryAgrocomercioDAL;

namespace pryAgrocomercioBLL.Maestros
{
    public class CCliente : cBase 
    {

#region propiedades_Capa_de_Datos
        private ClientesTableAdapter _ClienteAdapter = null;

        private int _cliCod;
        private string _CliTipoPer;
        private string _CliTipoDoc;
        private string _CliNumDoc;
        private string _CliNombres;
        private string _CliDireccion;
        private string _CliTelefono;
        private string _CliRepresen;
        private DateTime _CliFecRegis;
        private bool _CliEstado;

        public ClientesTableAdapter ClienteTableAdapter
        {
            get
            {
                if (_ClienteAdapter == null)
                    _ClienteAdapter = new ClientesTableAdapter();
                return _ClienteAdapter;
            }
        }
        public int CliCod
        {
            get
            {
                if (_cliCod == 0)
                    return 0;
                else
                    return _cliCod;
            }
            set { _cliCod = value; }
        }
        public string CliNombres
        {
            get
            {
                if (_CliNombres == null)
                    return "";
                else
                    return _CliNombres;
            }
            set { _CliNombres = value; }
        }
        public string CliTipoPer
        {
            get
            {
                if (_CliTipoPer == null)
                    return "";
                else
                    return _CliTipoPer;
            }
            set { _CliTipoPer = value; }
        }
        public string CliTipoDoc
        {
            get
            {
                if (_CliTipoDoc == null)
                    return "";
                else
                    return _CliTipoDoc;
            }
            set { _CliTipoDoc = value; }
        }
        public string CliDireccion
        {
            get
            {
                if (_CliDireccion == null)
                    return "";
                else
                    return _CliDireccion;
            }
            set { _CliDireccion = value; }
        }
        public string CliTelefono
        {
            get
            {
                if (_CliTelefono == null)
                    return "";
                else
                    return _CliTelefono;
            }
            set { _CliTelefono = value; }
        }
        public string CliNumDoc
        {
            get { return _CliNumDoc; }
            set { _CliNumDoc = value; }
        }
        public string CliRepresen
        {
            get { return _CliRepresen; }
            set { _CliRepresen = value; }
        }
        public DateTime CliFecRegis
        {
            get { return _CliFecRegis; }
            set { _CliFecRegis = value; }
        }
        public bool CliEstado
        {
            get { return _CliEstado; }
            set { _CliEstado = value; }
        }
        #endregion
        #region metodos_estaticos

        #endregion
        #region metodos_publicos_ABM
        public CCliente()
            : base()
        {
        }
        public CCliente(int pCliCodigo)
        {
            DataRow drCliente = this.fnDatosCliente(pCliCodigo);
            this.CliCod = Convert.ToInt32(drCliente["CliCod"]);
            this.CliNombres = drCliente["CliNombres"].ToString();
            this.CliTipoPer = drCliente["CliTipoPer"].ToString();
            this.CliTipoDoc = drCliente["CliTipoDoc"].ToString();
            this.CliDireccion = drCliente["cliDireccion"].ToString();
            this.CliTelefono = drCliente["cliTelefono"].ToString();
            this.CliNumDoc = drCliente["CliNumDoc"].ToString();
            this.CliRepresen = drCliente["CliRepresen"].ToString();
            this.CliFecRegis = Convert.ToDateTime(drCliente["CliFecRegis"].ToString());
            this.CliEstado = Convert.ToBoolean( drCliente["CliEstado"]);
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public DataRow fnDatosCliente(int pCliCodigo)
        {
            MaestrosDataSet.ClientesDataTable dtCliente = new MaestrosDataSet.ClientesDataTable();
            dtCliente = ClienteTableAdapter.GetDataByCliCod(pCliCodigo);
            return dtCliente.Rows[0];
        }
        public MaestrosDataSet.ClientesDataTable fnListaClientes(bool OpcionTodos)
        {
            MaestrosDataSet.ClientesDataTable clientesDataTable = new MaestrosDataSet.ClientesDataTable();
            if(OpcionTodos)
                clientesDataTable = ClienteTableAdapter.GetData();
            else
                clientesDataTable = ClienteTableAdapter.GetDataActivos();
            return clientesDataTable;
        }
        

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public int fnClienteInsert()
        {
            int lnResp = 0;
            this.CliCod = Convert.ToInt32(ClienteTableAdapter.MaxCliCod()) + 1;
            try
            {
                lnResp = ClienteTableAdapter.Insert(this.CliCod,this.CliTipoPer ,this.CliTipoDoc , this.CliNumDoc , this.CliNombres, this.CliDireccion , this.CliTelefono , this.CliRepresen , this.CliFecRegis ,this.CliEstado );
                if (lnResp <= 0)
                {
                    NroError = 996;
                    DescripcionError = "Error al INSERTAR el Registro </br>";
                }
                return lnResp;
            }
            catch (SqlException ex)
            {
                NroError = ex.Number;
                DescripcionError = ex.Message;
                return 0;
            }

        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        public int fnClienteUpdate()
        {
            MaestrosDataSet.ClientesRow oClienteRow = (MaestrosDataSet.ClientesRow )this.fnDatosCliente(this.CliCod);
            int regAfectados = 0;

            //valida si el codigo existe
            if (oClienteRow == null)
            {
                NroError = 999;
                DescripcionError += "El código no existe";
                return 0;

            }

            oClienteRow.CliNombre = this.CliNombres;
            oClienteRow.CliNumDoc = this.CliNumDoc;
            oClienteRow.CliTipoDoc = this.CliTipoDoc;
            oClienteRow.CliDireccion = this.CliDireccion;
            oClienteRow.CliTelefono = this.CliTelefono;
            oClienteRow.CliTipoPer = this.CliTipoPer;
            oClienteRow.CliRepresen = this.CliRepresen;
            oClienteRow.CliFecRegis = this.CliFecRegis;
            oClienteRow.CliEstado = this.CliEstado;

            try
            {
                regAfectados = ClienteTableAdapter.Update(oClienteRow);
                if (regAfectados <= 0)
                {
                    NroError = 997;
                    DescripcionError = "No se pudo ACTUALIZAR el registro </br>";
                }
                return regAfectados;
            }
            catch (SqlException ex)
            {
                NroError = ex.Number;
                DescripcionError = ex.Message;
                return 0;
            }


        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, true)]
        public int fnClienteDelete(int pCliCodigo)
        {
            int regAfectados = 0;
            int totOperaCliente = 0;
            try
            {
                //Nro de articulos vinculados al Proveedor
                totOperaCliente = ClienteTableAdapter.TotOperaCliente(pCliCodigo).Value;
                regAfectados = ClienteTableAdapter.DeleteLogical(pCliCodigo,false );
                if (totOperaCliente > 0)
                {
                    NroError = 998;
                    DescripcionError = "No se pudo ELIMINAR el registro, tiene Operaciones Relacionados";
                }
                else
                    regAfectados = ClienteTableAdapter.DeleteLogical(pCliCodigo, false);

                return regAfectados;
            }
            catch (SqlException ex)
            {
                NroError = ex.Number;
                DescripcionError = ex.Message;
                return 0;
            }
        }

        #endregion


    }
}
