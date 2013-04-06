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
    public class CProveedor : cBase 
    {

        #region propiedades_Capa_de_Datos
        private ProveedoresTableAdapter _ProveedorAdapter = null;
        private int _prvCod;
        private string _prvRazon;
        private string _PrvNumDoc;
        private string _prvDireccion;
        private string _PrvContacto;
        private string _prvTelefono;
        private DateTime   _prvFecRegis;
        private bool  _prvEstado;
        private decimal  _PrvGanancia;
        private decimal _PrvDscto;
        
        public ProveedoresTableAdapter ProveedorAdapter {
            get {
                if (_ProveedorAdapter == null)
                    _ProveedorAdapter = new ProveedoresTableAdapter();
                return _ProveedorAdapter;
            }
        }
        public int prvCodigo{
            get{
                if (_prvCod == 0)
                    return 0;
                else
                    return _prvCod ;
            }
            set{ _prvCod  = value; }
        }
       public string PrvNumDoc
        {
            get{
                if (_PrvNumDoc == null)
                    return "";
                else
                    return _PrvNumDoc;
            }
            set { _PrvNumDoc = value; }
        }
        public string prvRazon
        {
            get{
                if (_prvRazon == null)
                    return "";
                else
                    return _prvRazon;
            }
            set { _prvRazon = value; }
        }
        public string prvDireccion
        {
            get
            {
                if (_prvDireccion == null)
                    return "";
                else
                    return _prvDireccion;
            }
            set { _prvDireccion = value; }
        }
        public string PrvContacto
        {
            get
            {
                if (_PrvContacto == null)
                    return "";
                else
                    return _PrvContacto;
            }
            set { _PrvContacto = value; }
        }
        public string prvTelefono
        {
            get
            {
                if (_prvTelefono == null)
                    return "";
                else
                    return _prvTelefono;
            }
            set { _prvTelefono = value; }
        }
        public DateTime  prvFecRegis
        {
            get
            {
                     return _prvFecRegis;
            }
            set { _prvFecRegis = value; }
        }
        public bool  prvEstado
        {
            get {    return _prvEstado;   }
            set { _prvEstado = value; }
        }
        public decimal  PrvGanancia
        {
            get
            {
                if (_PrvGanancia == null)
                    return 0;
                else
                    return _PrvGanancia;
            }
            set { _PrvGanancia = value; }
        }
        public decimal PrvDscto
        {
            get
            {
                if (_PrvDscto == null)
                    return 0;
                else
                    return _PrvDscto;
            }
            set { _PrvDscto = value; }
        }
#endregion
#region metodos_estaticos

#endregion
#region metodos_publicos_ABM
        public CProveedor(): base()
        {        }
        public CProveedor(int pPrvCodigo)
        {
            DataRow drProveedor = this.fnDatosProveedor(pPrvCodigo);
            this.prvCodigo = Convert.ToInt32(drProveedor["prvCodigo"]);
            this.prvRazon = drProveedor["prvRazon"].ToString();
            this.PrvNumDoc = drProveedor["PrvNumDoc"].ToString();
            this.prvDireccion = drProveedor["prvDireccion"].ToString();
            this.PrvContacto = drProveedor["PrvContacto"].ToString();
            this.prvFecRegis = Convert.ToDateTime( drProveedor["PrvFecRegis"].ToString());
            this.prvTelefono = drProveedor["prvTelefono"].ToString();
            this.PrvGanancia = Convert.ToDecimal( drProveedor["PrvGanancia"]);
            this.PrvDscto = Convert.ToDecimal(drProveedor["PrvDscto"]);
            this.prvEstado = Convert.ToBoolean( drProveedor["prvEstado"]);
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false )]
        public DataRow fnDatosProveedor(int pPrvCodigo)
        {
            MaestrosDataSet.ProveedoresDataTable dtProveedor = new MaestrosDataSet.ProveedoresDataTable();
            dtProveedor = ProveedorAdapter.GetDataByPrvCodigo(pPrvCodigo);
            return dtProveedor.Rows[0];
        }
        public MaestrosDataSet.ProveedoresDataTable fnListaProveedores(bool OpcionTodos)
        {
            MaestrosDataSet.ProveedoresDataTable proveedoresDataTable = new MaestrosDataSet.ProveedoresDataTable();
            if (OpcionTodos)
                proveedoresDataTable = ProveedorAdapter.GetData();
            else
                proveedoresDataTable = ProveedorAdapter.GetDataActivos();
            return proveedoresDataTable;
        }
        
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert , true)]
        public int fnProveedorInsert()
        {
            int lnResp = 0;
            this.prvCodigo = Convert.ToInt32(ProveedorAdapter.MaxPrvCodigo()) + 1;
            try
            {
                lnResp = ProveedorAdapter.Insert(this.prvCodigo , this.PrvNumDoc, this.prvRazon, this.prvDireccion , this.prvTelefono,this.PrvContacto,this.prvFecRegis , this.prvEstado,"" ,this.PrvGanancia,this.PrvDscto  );
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
        public int fnProveedorUpdate()
        {
            MaestrosDataSet.ProveedoresRow oProveedorRow = (MaestrosDataSet.ProveedoresRow)this.fnDatosProveedor(prvCodigo);
            int regAfectados = 0;

            //valida si el codigo existe
            if (oProveedorRow == null)
            {
                NroError = 999;
                DescripcionError += "El código no existe";
                return 0;

            }

            oProveedorRow.PrvRazon = this.prvRazon;
            oProveedorRow.PrvDireccion = this.prvDireccion;
            oProveedorRow.PrvNumDoc = this.PrvNumDoc;
            oProveedorRow.PrvTelefono = this.prvTelefono ;
            oProveedorRow.PrvFecRegis = this.prvFecRegis ;
            oProveedorRow.PrvContacto  = this.PrvContacto ;
            oProveedorRow.PrvEstado = this.prvEstado;
            oProveedorRow.PrvGanancia  = this.PrvGanancia;
            oProveedorRow.PrvDscto = this.PrvDscto;

            try
            {
                regAfectados = ProveedorAdapter.Update(oProveedorRow);
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
        public int fnProvedorDelete(int prvCodigo)
        {
            int regAfectados = 0;
            int totArtProveedor = 0;
            try
            {
                //Nro de articulos vinculados al Proveedor
                totArtProveedor = ProveedorAdapter.TotArtProveedor(prvCodigo).Value ;
                
                if (totArtProveedor > 0)
                {
                    NroError = 998;
                    DescripcionError = "No se pudo ELIMINAR el registro, tiene Articulos Relacionados";
                }
                else
                    regAfectados = ProveedorAdapter.DeleteLogical(prvCodigo, false);

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
