using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using pryAgrocomercioDAL;
using pryAgrocomercioBLL.EntityCollection.Interfaces;

namespace pryAgrocomercioBLL.EntityCollection
{
    public class clsUnidades : clsAbstractBase<Unidades>, IUnidades
    {
        public string UniDescripcion(int pnUniCod)
        {
            return this.Find(Uni => Uni.UniCod == pnUniCod).Select(Uni => Uni.UniDescripcion).First();
        }
    }
}
