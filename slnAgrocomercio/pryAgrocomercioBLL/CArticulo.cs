using System;
using System.Collections.Generic;
using System.Configuration ;
using System.IO;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient ;
using System.Web;
using System.Web.UI ;
using pryAgrocomercioDAL.MaestrosDataSetTableAdapters;
using pryAgrocomercioDAL;

namespace pryAgrocomercioBLL.Maestros
{
    public class CArticulo : cBase 
    {
        
 
#region propiedades_Capa_de_Datos
        private ArticulosTableAdapter  _ArticuloAdapter = null;
        public ArticulosTableAdapter ArticuloAdapter
        {
            get
            {
                if (_ArticuloAdapter == null)
                    _ArticuloAdapter = new ArticulosTableAdapter();
                return _ArticuloAdapter;
            }
        }
        private int _ArtCod;
        public int ArtCod
        {
            get
            {
                if (_ArtCod == null)
                    return 0;
                else
                    return _ArtCod;
            }
            set
            { _ArtCod = value; }
        }
        private string _ArtDescripcion;
        public string ArtDescripcion
        {
            get
            {
                if (_ArtDescripcion == null)
                    return "";
                else
                    return _ArtDescripcion;
            }
            set
            { _ArtDescripcion = value; }
        }

        private decimal  _ArtStock;
        public decimal  ArtStock
        {
            get
            {
                if (_ArtStock == null)
                    return 0;
                else
                    return _ArtStock;
            }
            set
            { _ArtStock = value; }
        }

        private decimal _ArtCostoProm;
        public decimal ArtCostoProm
        {
            get
            {
                if (_ArtCostoProm == null)
                    return 0;
                else
                    return _ArtCostoProm;
            }
            set
            { _ArtCostoProm = value; }
        }
        private decimal _ArtStockMax;
        public decimal ArtStockMax
        {
            get
            {
                if (_ArtStockMax == null)
                    return 0;
                else
                    return _ArtStockMax;
            }
            set
            { _ArtStockMax = value; }
        }
        private decimal _ArtStockMin;
        public decimal ArtStockMin
        {
            get
            {
                if (_ArtStockMin == null)
                    return 0;
                else
                    return _ArtStockMin;
            }
            set
            { _ArtStockMin = value; }
        }
        private decimal _ArtStockFac;
        public decimal ArtStockFac
        {
            get
            {
                if (_ArtStockFac == null)
                    return 0;
                else
                    return _ArtStockFac;
            }
            set
            { _ArtStockFac = value; }
        }
        
        private int _UniCod;
        public int UniCod
        {
            get
            {
                if (_UniCod == null)
                    return 0;
                else
                    return _UniCod;
            }
            set
            { _UniCod = value; }
        }
        private bool  _ArtEstado;
        public bool  ArtEstado
        {
            get
            {
                if (_ArtEstado == null)
                    return false;
                else
                    return _ArtEstado;
            }
            set
            { _ArtEstado = value; }
        }
        private DateTime   _ArtFecRegis;
        public DateTime  ArtFecRegis
        {
            get { return _ArtFecRegis; }
            set
            { _ArtFecRegis = value; }
        }
        private DateTime _ArtFecModi;
        public DateTime ArtFecModi
        {
            get
            {
                return _ArtFecModi;
            }
            set
            { _ArtFecModi = value; }
        }
        private DateTime _ArtFecVen;
        public DateTime ArtFecVen
        {
            get
            {
                return _ArtFecVen;
            }
            set
            { _ArtFecVen = value; }
        }
        private int _PrvCod;
        public int PrvCod
        {
            get
            {
                if (_PrvCod == null)
                    return 0;
                else
                    return _PrvCod;
            }
            set
            { _PrvCod = value; }
        }
        private decimal _ArtPeso;
        public decimal ArtPeso
        {
            get
            {
                if (_ArtPeso == null)
                    return 0;
                else
                    return _ArtPeso;
            }
            set
            { _ArtPeso = value; }
        }

#endregion
#region metodos_estaticos

#endregion
#region metodos_publicos_ABM
        public CArticulo():base()
        {
        }
        public CArticulo(int pArtCod)
        {
            DataRow drArticulo = this.fnDatosArticulo(pArtCod);
            this.ArtDescripcion = drArticulo["ArtDescripcion"].ToString();
            this.ArtStock = Convert.ToDecimal(drArticulo["ArtStock"]);
            this.ArtCostoProm = Convert.ToDecimal(drArticulo["ArtCostoProm"]);
            this.ArtStockMax = Convert.ToDecimal(drArticulo["ArtStockMax"]);
            this.ArtStockMin = Convert.ToDecimal(drArticulo["ArtStockMin"]);
            this.UniCod = Convert.ToInt32(drArticulo["UniCod"]);
            this.ArtFecRegis = Convert.ToDateTime(drArticulo["ArtFecRegis"]);
            this.ArtFecModi = Convert.ToDateTime(drArticulo["ArtFecModi"]);
            this.ArtEstado = Convert.ToBoolean(drArticulo["ArtEstado"]);
            this.ArtFecVen  = Convert.ToDateTime(drArticulo["ArtFecVen"]);
            this.PrvCod = Convert.ToInt32(drArticulo["PrvCod"]);
            this.ArtPeso = Convert.ToDecimal(drArticulo["ArtPeso"]);

        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public MaestrosDataSet.ArticulosDataTable fnListaArticulos(bool opcionTodos)
        {
            MaestrosDataSet.ArticulosDataTable ArticulosDataTable = new MaestrosDataSet.ArticulosDataTable();
            if(opcionTodos)
                ArticulosDataTable = ArticuloAdapter.GetData();
            else
                ArticulosDataTable = ArticuloAdapter.GetDataActivos();

            return ArticulosDataTable;
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false )]
        public DataRow fnDatosArticulo(int pArtCod)
        {
            MaestrosDataSet.ArticulosDataTable oArticulo = new MaestrosDataSet.ArticulosDataTable();
            oArticulo = ArticuloAdapter.GetDataByArtCod(pArtCod);
            return oArticulo.Rows[0];
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert , true)]
        public int fnArticuloInsert()
        {
            int lnResp = 0;
            try
            {
                int NuevoArtCod = Convert.ToInt32( ArticuloAdapter.MAXARTCOD()) + 1;
                lnResp = ArticuloAdapter.Insert(NuevoArtCod, this.PrvCod, this.ArtCostoProm, this.ArtDescripcion, this.ArtFecModi,this.ArtFecRegis, this.ArtFecVen, this.ArtStock, this.ArtStockFac, this.ArtStockMax, this.ArtStockMin,  this.ArtEstado, "",this.UniCod,this.ArtPeso );
                if (lnResp <= 0)
                {
                    NroError = 996;
                    DescripcionError = "No se pudo INSERTAR el registro </br>";
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
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update , true)]
        public int fnArticuloUpdate()
        {
            MaestrosDataSet.ArticulosRow oArticuloRow = (MaestrosDataSet.ArticulosRow)this.fnDatosArticulo(this.ArtCod );
            int regAfectados = 0;

            //valida si el codigo existe
            if (oArticuloRow == null)
            {
                NroError = 999;
                DescripcionError += "El código no existe";
                return 0;

            }

            oArticuloRow.ArtDescripcion  = this.ArtDescripcion ;
            oArticuloRow.ArtStock = this.ArtStock ;
            oArticuloRow.ArtCostoProm = this.ArtCostoProm ;
            oArticuloRow.ArtStockMax = this.ArtStockMax ;
            oArticuloRow.ArtStockMin = this.ArtStockMin ;
            //oArticuloRow.ArtFecRegis = this.ArtFecRegis ;
            oArticuloRow.ArtFecModi = this.ArtFecModi;
            oArticuloRow.UniCod= this.UniCod;
            oArticuloRow.ArtFecVen = this.ArtFecVen;
            oArticuloRow.PrvCod = this.PrvCod;
            oArticuloRow.ArtPeso = this.ArtPeso;
            oArticuloRow.Artestado = this.ArtEstado;

            try
            {
                regAfectados = ArticuloAdapter.Update(oArticuloRow);
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
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete , true)]
        public int fnArticuloDelete(int pArtCod)
        {
            int regAfectados = 0;
            try
            {
                regAfectados = ArticuloAdapter.DeleteLogical(pArtCod,false );
                if (regAfectados <= 0)
                {
                    NroError = 998;
                    DescripcionError = "No se pudo ELIMINAR el registro </br>";
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

        //[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        //public int fnStockArticuloUpdate(int pArtCod, decimal pArtStock)
        //{
        //    MaestrosDataSet.ArticulosRow oArticuloRow = (MaestrosDataSet.ArticulosRow)this.fnDatosArticulo(pArtCod);
        //    int regAfectados = 0;

        //    //valida si el codigo existe
        //    if (oArticuloRow == null)
        //    {
        //        NroError = 999;
        //        DescripcionError += "El código no existe";
        //        return 0;

        //    }

        //    oArticuloRow.proComTotal += proStock;
        //    oArticuloRow.proStock = oProductosRow.proComTotal - oProductosRow.proVenTotal;

        //    try
        //    {
        //        regAfectados = ProductoAdapter.Update(oProductosRow);
        //        if (regAfectados <= 0)
        //        {
        //            NroError = 997;
        //            DescripcionError = "No se pudo ACTUALIZAR el registro </br>";
        //        }
        //        return regAfectados;
        //    }
        //    catch (SqlException ex)
        //    {
        //        NroError = ex.Number;
        //        DescripcionError = ex.Message;
        //        return 0;
        //    }
        //}

#endregion

    }

}

