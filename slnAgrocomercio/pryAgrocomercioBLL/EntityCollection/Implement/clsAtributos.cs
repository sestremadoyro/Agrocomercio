using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using pryAgrocomercioDAL;
using pryAgrocomercioBLL.EntityCollection.Interfaces;
using System.ComponentModel;

namespace pryAgrocomercioBLL.EntityCollection
{
    [DataObject]
    public class clsAtributos : clsAbstractBase<Atributos>, IAtributos
    {
        public clsAtributos()
            : base()
        {

        }

        public clsAtributos(AgrocomercioEntities _AgroEntity)
            : base(_AgroEntity)
        {

        }

        public List<Atributos> ListAtributos(int pcAtrTipoCod)
        {
            try
            {
                return this.Find(Atr => Atr.AtrTipoCod == pcAtrTipoCod && Atr.AtrNivel == 1 && Atr.AtrEstado == true).ToList<Atributos>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public List<Atributos> ListDataAtributos(int pcAtrTipoCod)
        {
            return this.Find(Atr => Atr.AtrTipoCod == pcAtrTipoCod && Atr.AtrNivel == 1 && Atr.AtrEstado == true).ToList<Atributos>();
        }

        public List<Atributos> ListAtributosPadre()
        {
            return this.Find(Atr => Atr.AtrNivel == 0 && Atr.AtrEstado == true).ToList<Atributos>();
        }
        public DataTable ListDataAtributosToTable(int pcAtrTipoCod)
        {
            IQueryable<Object> Result;
            Result =  this.Find(Atr => Atr.AtrTipoCod == pcAtrTipoCod && Atr.AtrNivel == 1 && Atr.AtrEstado == true).OrderBy(Atr=>Atr.AtrCodigo );

            return ToDataTable(Result.AsQueryable());
        }
        public Atributos  GetAtributo(int pnAtrTipoCod, string pnAtrCodigo)
        {
            return this.Find(Atr => Atr.AtrTipoCod == pnAtrTipoCod && Atr.AtrCodigo == pnAtrCodigo).First<Atributos>();
        }
        public void DeleteAtributo(int pArtTipoCod, string pArtCod)
        {
            try
            {
                var Atributo = this.Find(Atr => Atr.AtrTipoCod == pArtTipoCod && Atr.AtrCodigo == pArtCod).FirstOrDefault();
                this.Delete(Atributo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
    }
}
