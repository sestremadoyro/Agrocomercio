using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using pryAgrocomercioDAL;
using pryAgrocomercioBLL.EntityCollection.Interfaces;

namespace pryAgrocomercioBLL.EntityCollection
{
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

        public object ListAtributos(int pcAtrTipoCod)
        {
            return this.Find(Atr => Atr.AtrTipoCod == pcAtrTipoCod && Atr.AtrNivel == 1 && Atr.AtrEstado == true).ToList();
        }

        
    }
}
