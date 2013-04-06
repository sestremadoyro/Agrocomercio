using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using pryAgrocomercioDAL.MaestrosDataSetTableAdapters;
using pryAgrocomercioDAL;

namespace pryAgrocomercioBLL
{
    public  class cBase
    {
        #region propiedades comunes
        private string _DescripcionError;
        private int _NroError;

        public string DescripcionError
        {
            get { return _DescripcionError; }
            set { _DescripcionError = value; }
        }
        public int NroError
        {
            get { return _NroError; }
            set { _NroError = value; }
        }
        #endregion
        #region metodos comunes

        // extrae imagen de BD, devuelve un arreglo de bits
        private Byte[] ObtenerImagen(int FotoID, string NombreTabla, string Nomcampo)
        {
            Byte[] lbyImage1;

            ArticulosTableAdapter ArticulosAdapter = new ArticulosTableAdapter();
            SqlConnection conexion = new SqlConnection(ArticulosAdapter.Connection.ConnectionString);
            conexion.Open();

            String strSelect = "Select Imagen From " + NombreTabla + " Where " + Nomcampo + " = " + FotoID.ToString();
            SqlCommand SqlCom = new SqlCommand(strSelect, conexion);

            Object objeto = SqlCom.ExecuteScalar();
            if (objeto == DBNull.Value)
                lbyImage1 = null;

            else
                lbyImage1 = (Byte[])objeto;
            return lbyImage1;

        }
        //copia el arreglo de bits en un archivo fisico (imagen)
        private void EscribirEnArchivo(string strPath, ref  Byte[] Buffer)
        {
            // Crea un archivo 
            FileStream newFile = new FileStream(strPath, FileMode.Create);
            // escribe en el archivo 
            newFile.Write(Buffer, 0, Buffer.Length);
            // cierra archivo
            newFile.Close();
        }

        protected void CargarImagen(int codigo, string nomTabla, string nomCampo, string rutaPlato)
        {
            //ruta donde se almacena las imagenes de las propiedades
            string path = HttpContext.Current.Server.MapPath(rutaPlato);

            //extrae la imagen de la BD y la convierte en Arreglo de BYTES
            Byte[] byteImage;
            byteImage = ObtenerImagen(codigo, nomTabla, nomCampo);
            //Arreglo de BYTES esta vacio?
            if (!(byteImage == null))
            {
                //path += rutaPlato;
                EscribirEnArchivo(path, ref byteImage);         //copia imagen a carpeta
            }
        }
        #endregion
    }
}
