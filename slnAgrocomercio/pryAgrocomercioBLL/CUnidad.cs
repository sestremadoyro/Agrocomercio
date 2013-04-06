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
   public  class CUnidad : cBase 
    {
        #region propiedades_Capa_de_Datos
        private UNIDADESTableAdapter _UnidadesAdapter = null;
        private int _uniCod;
        private string _uniDescripcion;
        private decimal  _uniFactor;
        private int  _uniSuperior;
        private DateTime _uniFecRegis;
        private bool _uniEstado;

        public UNIDADESTableAdapter  UnidadesAdapter
        {
            get
            {
                if (_UnidadesAdapter == null)
                    _UnidadesAdapter = new UNIDADESTableAdapter();
                return _UnidadesAdapter;
            }
        }
        public int UniCod
        {
            get
            {
                if (_uniCod == 0)
                    return 0;
                else
                    return _uniCod;
            }
            set { _uniCod = value; }
        }
        public string UniDescripcion
        {
            get
            {
                if (_uniDescripcion == null)
                    return "";
                else
                    return _uniDescripcion;
            }
            set { _uniDescripcion = value; }
        }
        public decimal  UniFactor
        {
            get
            {
                if (_uniFactor == null)
                    return 0;
                else
                    return _uniFactor;
            }
            set { _uniFactor = value; }
        }
        public int  UniSuperior
        {
            get
            {
                if (_uniSuperior == null)
                    return 0;
                else
                    return _uniSuperior;
            }
            set { _uniSuperior = value; }
        }

        public DateTime UniFecRegis
        {
            get
            {
                return _uniFecRegis;
            }
            set { _uniFecRegis = value; }
        }
        public bool UniEstado
        {
            get { return _uniEstado; }
            set { _uniEstado = value; }
        }
        #endregion

        #region metodos_publicos_ABM
        public CUnidad(): base()
        {        }
        public CUnidad(int pUniCod)
        {
            DataRow drUnidad = this.fnDatosUnidad (pUniCod);
            this.UniCod  = Convert.ToInt32(drUnidad["UniCod"]);
            this.UniDescripcion  = drUnidad["UniDescripcion"].ToString();
            this.UniFactor  = Convert.ToDecimal( drUnidad["UniFactor"]);
            this.UniSuperior  =  Convert.ToInt32( drUnidad["UniSuperior"]);
            this.UniFecRegis  = Convert.ToDateTime( drUnidad["UniFecRegis"].ToString());
            this.UniEstado = Convert.ToBoolean( drUnidad["UniEstado"]);
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false )]
        public DataRow fnDatosUnidad(int pUniCod)
        {
            MaestrosDataSet.UNIDADESDataTable  dtUnidad = new MaestrosDataSet.UNIDADESDataTable();
            dtUnidad = UnidadesAdapter.GetDataByUniCod(pUniCod);
            return dtUnidad.Rows[0];
        }
        public MaestrosDataSet.UNIDADESDataTable fnListaUnidades()
        {
            MaestrosDataSet.UNIDADESDataTable UnidadesDataTable = new MaestrosDataSet.UNIDADESDataTable();
            UnidadesDataTable = UnidadesAdapter.GetData();
            return UnidadesDataTable;
        }
        public MaestrosDataSet.UNIDADESDataTable fnListaDescripcionUnidades()
        {
            MaestrosDataSet.UNIDADESDataTable UnidadesDataTable = new MaestrosDataSet.UNIDADESDataTable();
            UnidadesDataTable = UnidadesAdapter.GetData();
            return UnidadesDataTable;
        }

        #endregion
    }
}
