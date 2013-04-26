using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using pryAgrocomercioDAL;
using pryAgrocomercioBLL.EntityCollection.Interfaces;

namespace pryAgrocomercioBLL.EntityCollection
{
    public class clsDocumenOperacion : clsAbstractBase<DocumenOperacion>, IDocumenOperacion
    {
        public clsDocumenOperacion()
            : base()
        {

        }

        public clsDocumenOperacion(AgrocomercioEntities _AgroEntity)
            : base(_AgroEntity)
        {

        }


        DataRow drForm;

#region FUNCIONES DE MANTENIMIENTO

        public Boolean Guardar(Object oForm, ref long pnDopCod)
        {
            drForm = FormToDataRow(oForm);

            DocumenOperacion DocumenOpe = new DocumenOperacion();

            long nOpeCod = Convert.ToInt64(drForm["nOpeCod"]);
            int NroImpre = 0;
            String cProceso = drForm["cProceso"].ToString();
            try
            {
                //DATOS DEL DOCUMENTO DE LA OPERACION
                if (cProceso == "NEW" || cProceso == "NEWDOC")
                {
                    DocumenOpe = new DocumenOperacion();
                    pnDopCod = MaxDopCod() + 1;
                    NroImpre = 0;
                }
                else if (cProceso == "EDIT")
                {
                    pnDopCod = long.Parse(drForm["nDopCod"].ToString());
                    DocumenOpe = GetDocumenOperacion(pnDopCod);
                    NroImpre = (int)DocumenOpe.dopNroImpre;
                }

                //DATOS DE NUEVO DOCUMENTO DE OPERACION
                DocumenOpe.dopCod = pnDopCod;
                DocumenOpe.dopNroSerie = drForm["cdopNroSerie"].ToString();
                DocumenOpe.dopNumero = drForm["cdopNumero"].ToString();
                DocumenOpe.OpeCod = nOpeCod;
                DocumenOpe.tdoCod = Convert.ToInt32(drForm["ntdoCod"]);
                DocumenOpe.dopMoneda = drForm["cOpeMoneda"].ToString();
                DocumenOpe.dopNroImpre = NroImpre;
                DocumenOpe.dopFecEmision = Convert.ToDateTime(drForm["dOpeFecEmision"]);
                DocumenOpe.dopEstado = "A";
                DocumenOpe.dopPunPartida = drForm["cdopPunPartida"].ToString();
                if (DocumenOpe.tdoCod == 2)
                    DocumenOpe.dopFecTraslado = Convert.ToDateTime(drForm["ddopFecTraslado"]);

                if (cProceso == "NEW" || cProceso == "NEWDOC")
                    Add(DocumenOpe);
                else if (cProceso == "EDIT")
                    Update(DocumenOpe);
                
                SaveChanges();               

            }
            catch (Exception ex)
            {
                throw ex;
            }
            DocumenOpe = null;
            return true;
        }
        public Boolean ActualizarImpresion(int pnDopCod)
        {
            DocumenOperacion DocumenOpe = new DocumenOperacion();
            try
            {
                DocumenOpe = GetDocumenOperacion((int)pnDopCod);

                //DATOS DE NUEVO DOCUMENTO DE OPERACION
                DocumenOpe.dopNroImpre += 1;
                DocumenOpe.dopFecUltImpre = DateTime.Now;
                Update(DocumenOpe);
                SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            DocumenOpe = null;
            return true;
        }

#endregion

#region FUNCIONES DE CONSULTA
        public DocumenOperacion GetDocumenOperacion(long dopCod)
        {
            return this.Find(Doc => Doc.dopCod == dopCod).First<DocumenOperacion>();
        }
        public DocumenOperacion GetDocumenOperacion(int _OpeCod, int _tdoCod)
        {
            return this.Find(Doc => Doc.OpeCod == _OpeCod && Doc.tdoCod == _tdoCod).FirstOrDefault<DocumenOperacion>();
        }
        public long MaxDopCod()
        {
            var result = this.GetAll();
            if (result.Count() > 0)
            {
                return result.Max<DocumenOperacion>(Doc => Doc.dopCod);
            }
            return 0;
        }
        public string MaxDopNroSerie(int _tdoCod)
        {
            string cNroSerie = "001";
            //var cNroSerie = this.Find(Doc => Doc.tdoCod == _tdoCod).Max<DocumenOperacion>(Doc => decimal.Parse(Doc.dopNroSerie));
            var lstLista = this.Find(Doc => Doc.tdoCod == _tdoCod);
            if (lstLista.Count() > 0)
            {
                cNroSerie = lstLista.Max<DocumenOperacion>(Doc => decimal.Parse(Doc.dopNroSerie)).ToString().PadLeft(3, '0');
            }

            return cNroSerie;
        }

        public object GetListDocumenOperacion(int _OpeCod)
        {
            //AgrocomercioEntities AgroEnt = new AgrocomercioEntities();
            clsAtributos lstAtributos = new clsAtributos();

            List<DocumenOperacion> lstDocOper = this.Find(Doc => Doc.OpeCod == _OpeCod && Doc.dopEstado == "A").ToList<DocumenOperacion>();
            var Oper = lstDocOper.First().Operaciones.OpeTipo;
            List<Atributos> lstAtrib = lstAtributos.Find(Atr => Atr.AtrTipoCod == (Oper == "V" ? 3 : 5)).ToList<Atributos>();

            var DocDescri = from Doc in lstDocOper
                            join Atr in lstAtrib on Doc.tdoCod.ToString() equals Atr.AtrCodigo.ToString().Trim()
                            orderby Doc.dopCod descending
                            select new
                            {
                                dopCod = Doc.dopCod,
                                AtrDescripcion = Atr.AtrDescripcion
                            };

            return DocDescri.ToList();

        }

        public int fnDocOpeUpdate(DocumenOperacion obj)
        {
            this.Update(obj);
            this.SaveChanges();
            return 0;
        }
        public DocumenOperacion GetDocumenOperaciona(Int64 dopCod)
        {
            return this.Find(Doc => Doc.dopCod == dopCod).First<DocumenOperacion>();
        }
        public DocumenOperacion GetListOperaciones(Int64 _Letcodigo)
        {
            DataTable tabla;
            tabla = ToDataTable(this.Find(Let => Let.dopCod == _Letcodigo)
             .OrderByDescending(Let => Let.dopCod)
             .Select(Let => new
             {
                 Let.dopCod,
                 Let.dopEstado,
                 Let.dopFecCancela,
                 Let.dopFecEmision,
                 Let.dopFecUltImpre,
                 Let.dopMoneda,
                 Let.dopNroImpre,
                 Let.icodletra,
                 Let.OpeCod,
                 Let.tdoCod,
                 Let.dopNroSerie,
                 Let.dopNumero
             })
             .AsQueryable());
            DocumenOperacion obj = new DocumenOperacion();

            foreach (DataRow row in tabla.Rows)
            {
                //utilizar el objeto row
                obj.dopCod = Convert.ToInt64(row[0]);
                obj.dopEstado = Convert.ToString(row[1]);
                if (row[2].ToString().Length > 0)
                    obj.dopFecCancela = Convert.ToDateTime(row[2]);
                if (row[3].ToString().Length > 0)
                    obj.dopFecEmision = Convert.ToDateTime(row[3]);
                if (row[4].ToString().Length > 0)
                    obj.dopFecUltImpre = Convert.ToDateTime(row[4]);
                if (row[5].ToString().Length > 0)
                    obj.dopMoneda = Convert.ToString(row[5]);
                if (row[6].ToString().Length > 0)
                    obj.dopNroImpre = Convert.ToInt32(row[6]);
                if (row[7].ToString().Length > 0)
                    obj.icodletra = Convert.ToInt32(row[7]);
                if (row[8].ToString().Length > 0)
                    obj.OpeCod = Convert.ToInt64(row[8]);
                if (row[9].ToString().Length > 0)
                    obj.tdoCod = Convert.ToInt32(row[9]);
                if (row[10].ToString().Length > 0)
                    obj.dopNroSerie = Convert.ToString(row[10]);
                if (row[11].ToString().Length > 0)
                    obj.dopNumero = Convert.ToString(row[11]);
            }
            return obj;
        }
#endregion

        
    }
}
