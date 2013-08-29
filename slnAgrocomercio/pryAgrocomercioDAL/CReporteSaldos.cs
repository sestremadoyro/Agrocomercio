using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace pryAgrocomercioDAL
{
   public  class CReporteSaldos
    {
        public DataTable fnListaSaldos(int prvCodigo)
        {
            string cadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings["AgrocomercioConnectionString"].ConnectionString.ToString();

            SqlConnection con = new SqlConnection(cadenaConexion);
            con.Open();
            string cadenaSelect = " SELECT        Proveedores.PrvCod, Proveedores.PrvRazon, Articulos.ArtCod, Articulos.ArtDescripcion, Articulos.ArtStock AS StockFisico, Articulos.ArtCostoProm, ";
                    cadenaSelect += " Articulos.ArtStock * Articulos.ArtCostoProm AS StockValorado";
                    cadenaSelect += " FROM            Articulos INNER JOIN";
                    cadenaSelect += " Proveedores ON Articulos.PrvCod = Proveedores.PrvCod";
                    cadenaSelect += " WHERE        Proveedores.PrvCod = " + prvCodigo.ToString();
                    cadenaSelect += " AND        Articulos.ArtEstado = 1 ";
                    cadenaSelect += " ORDER BY Articulos.ArtDescripcion";

            SqlDataAdapter adapter = new SqlDataAdapter(cadenaSelect, con);
            DataSet dsSaldos = new DataSet();

            adapter.Fill(dsSaldos, "Saldos");

            con.Close();

            return dsSaldos.Tables["Saldos"];
        }
        public DataTable fnListaSaldosRestringida(int prvCodigo)
        {
            string cadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings["AgrocomercioConnectionString"].ConnectionString.ToString();

            SqlConnection con = new SqlConnection(cadenaConexion);
            con.Open();
            string cadenaSelect = " SELECT        Proveedores.PrvCod, Proveedores.PrvRazon, Articulos.ArtCod, Articulos.ArtDescripcion, Articulos.ArtStock AS StockFisico, Articulos.ArtFecVen ";
            cadenaSelect += " FROM            Articulos INNER JOIN";
            cadenaSelect += " Proveedores ON Articulos.PrvCod = Proveedores.PrvCod";
            cadenaSelect += " WHERE        Proveedores.PrvCod = " + prvCodigo.ToString();
            cadenaSelect += " AND        Articulos.ArtEstado = 1 ";
            cadenaSelect += " ORDER BY Articulos.ArtDescripcion";

            SqlDataAdapter adapter = new SqlDataAdapter(cadenaSelect, con);
            DataSet dsSaldos = new DataSet();

            adapter.Fill(dsSaldos, "Saldos");

            con.Close();

            return dsSaldos.Tables["Saldos"];
        }

    }
}
