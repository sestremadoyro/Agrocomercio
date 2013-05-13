using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using pryAgrocomercioDAL.Reportes_DataSetTableAdapters;
using pryAgrocomercioDAL;

namespace pryAgrocomercioBLL
{
    public class CReportes : cBase 
    {

        #region propiedades_Capa_de_Datos
        private SaldosTableAdapter  _SaldosAdapter = null;
        public SaldosTableAdapter SaldosAdapter
        {
            get
            {
                if (_SaldosAdapter == null)
                    _SaldosAdapter = new SaldosTableAdapter();
                return _SaldosAdapter;
            }
        }


        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public Reportes_DataSet.SaldosDataTable fnListaSaldos(int  prvCodigo)
        {
            Reportes_DataSet.SaldosDataTable SaldosDataTable = new Reportes_DataSet.SaldosDataTable();
            SaldosDataTable = SaldosAdapter.SaldosGetData(prvCodigo);

            return SaldosDataTable;
        }
        #endregion
    }
}
