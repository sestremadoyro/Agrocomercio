using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using pryAgrocomercioDAL;

namespace pryAgrocomercioBLL.EntityCollection.Interfaces
{
    public interface Iletra : IAbstractBase<letra>
    {
    }
    public interface Iletra_cabecera : IAbstractBase<cab_letra>
    {
    }
    public interface Iletra_lista : IAbstractBase<list_letra>
    {
    }
    public interface IvwList_let_detalle : IAbstractBase<vwList_let_detalle>
    {
    }
    public interface Ifac_pnd_let : IAbstractBase<fac_pnd_let>
    {
    }

    public interface Ifac_let : IAbstractBase<fac_x_letra>
    {
    }
    public interface Idet_let : IAbstractBase<det_letra>
    {
    }
    public interface Ilist_det_let : IAbstractBase<list_detLetra>
    {
    }
    public interface Ipag_pend : IAbstractBase<Pag_pendientes>
    {
    }
    public interface Ipag_pendNow : IAbstractBase<pag_pendnow>
    {
    }
    public interface Ipag_let : IAbstractBase<Pag_letras>
    {
    }
    public interface IProvletra : IAbstractBase<proveedor_letra>
    {
    }

    public interface IRepNotDet : IAbstractBase<RepNotDet>
    {
    }
    public interface ItbliqCobranza : IAbstractBase<tbliqCobranza>
    {
    }
    public interface Idetalle_pagos : IAbstractBase<detalle_pagos> { 
    }
    public interface Idetalle_pagos_now : IAbstractBase<detalle_pagos_now>
    {
    }
}
