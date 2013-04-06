using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using pryAgrocomercioDAL;

namespace pryAgrocomercioBLL.EntityCollection.Interfaces
{
    public interface INotas : IAbstractBase<Notas>
    {
    }
    public interface INota_lista : IAbstractBase<nota_list>
    {
    }
    public interface Iviewrel_not_fact : IAbstractBase<rel_not_fact>
    {
    }
    public interface Irel_notfactura : IAbstractBase<rel_notfactura>
    {
    }


    public interface IRepNotGen : IAbstractBase<RepNotGen>
    {
    }
    public interface Iclsviewrel_not_fac : IAbstractBase<rel_not_fact>
    {
    }

    //public interface Iletra_listaDao : IAbstractBase<list_letra>
    //{
    //}
    //public interface Ifac_pnd_letDao : IAbstractBase<fac_pnd_let>
    //{
    //}
}
