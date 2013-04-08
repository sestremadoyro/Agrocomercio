using System;
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
            return this.Find(Atr => Atr.AtrTipoCod == pcAtrTipoCod && Atr.AtrNivel == 1 && Atr.AtrEstado == true).ToList<Atributos>()  ;
        }

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public List<Atributos> ListDataAtributos(int pcAtrTipoCod)
        {
            return this.Find(Atr => Atr.AtrTipoCod == pcAtrTipoCod && Atr.AtrNivel == 1 && Atr.AtrEstado == true).ToList<Atributos>();
        }

        
    }
}
