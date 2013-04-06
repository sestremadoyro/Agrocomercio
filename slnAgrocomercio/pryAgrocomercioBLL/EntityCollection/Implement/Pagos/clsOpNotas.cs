using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
//using pryAgrocomercioDAL.Implement;
using pryAgrocomercioBLL.EntityCollection.Interfaces;
using pryAgrocomercioDAL;


namespace pryAgrocomercioBLL.EntityCollection
{
    //public class clsOpeNotas : clsNotasDao
    public class clsOpeNotas : clsAbstractBase<Notas>, INotas
    {
        public Notas GetNota(int OpeCod)
        {
            return this.Find(Ope => Ope.inota == OpeCod).First<Notas>();
        }
        public int fnNotaInsert(Notas oNota)
        {
            this.Add(oNota);
            this.SaveChanges();
            return 0;
        }
        public int fnNotaUpdate(Notas oNota)
        {
            this.Update(oNota);
            this.SaveChanges();
            return 0;
        }
        public DataTable GetListNotas(int _Prov, String _Valor, String _moneda)
        {
            return ToDataTable(this.Find(Not => Not.iprvcod == _Prov
                && Not.cestadoNota != "C" && Not.ctipo == _Valor && Not.OpeMoneda==_moneda)
            .OrderByDescending(Not => Not.dfecreg)
            .Select(Not => new
            {
                ccodnota = Not.ccodnota,
                dfecreg = Not.dfecreg,
                iprvcod = Not.iprvcod,
                nmontoNota = Not.nmontoNota,
                cestadoNota = Not.cestadoNota,
                nmntutilizado = Not.nmontoNota - Not.nmntutilizado,
                Not.inota,
                Not.cobservaciones,
                Not.ctipNota

            })
            .AsQueryable());
        }

        public long MaxOpeCod()
        {
            if (this.GetAll().Count() > 0)
            {
                return this.GetAll().Max<Notas>(Not => Not.inota);
            }
            return 0;
        }

    }
    //public class clsrel_notfactura : clsrel_notfacturaDao
    public class clsrel_notfactura : clsAbstractBase<rel_notfactura>, Irel_notfactura
    {
        public rel_notfactura GetRelNota(int irelnotfac)
        {
            return this.Find(Ope => Ope.irelnotfac == irelnotfac).First<rel_notfactura>();
        }
        public int fnNotaInsert(rel_notfactura oNota)
        {
            this.Add(oNota);
            this.SaveChanges();
            return 0;
        }
        public int fnNotaUpdate(rel_notfactura oNota)
        {
            this.Update(oNota);
            this.SaveChanges();
            return 0;
        }
        //public DataTable GetListrelacion(int _Prov, String _Valor)
        //{
        //    return ToDataTable(this.Find(Not => Not.iprvcod == _Prov
        //        && Not.cestadoNota != "C" && Not.ctipo == _Valor)
        //    .OrderByDescending(Not => Not.dfecreg)
        //    .Select(Not => new
        //    {
        //        ccodnota = Not.ccodnota,
        //        dfecreg = Not.dfecreg,
        //        iprvcod = Not.iprvcod,
        //        nmontoNota = Not.nmontoNota,
        //        cestadoNota = Not.cestadoNota,
        //        nmntutilizado = Not.nmontoNota - Not.nmntutilizado,
        //        Not.inota,
        //        Not.cobservaciones
        //    })
        //    .AsQueryable());
        //}

        public long MaxOpeCod()
        {
            if (this.GetAll().Count() > 0)
            {
                return this.GetAll().Max<rel_notfactura>(Not => Not.irelnotfac);
            }
            return 0;
        }

    }
    //public class clsListNotas : clsNota_listaDao
    public class clsListNotas : clsAbstractBase<nota_list>, INota_lista
    {
        //public Operaciones GetOperacion(int OpeCod)
        //    //{
        //    //    return this.Find(Ope => Ope.OpeCod == OpeCod).First<Operaciones>();
        //    //}
        public DataTable GetList(String _LetEst, int _cod_prd, DateTime _fecdesde, DateTime _fechasta, String _valor)
        {
            DataTable datos;
            if (_cod_prd == 0 && _LetEst == "T")
                datos = ToDataTable(this.Find(Not => "1" == "1" && Not.valor == _valor)
                .OrderByDescending(Not => Not.FecEmesion)
                .Select(Not => new
                {
                    Not.inota,
                    Not.ccodnota,
                    Not.FecEmesion,
                    Not.iprvcod,
                    Not.PrvRazon,
                    Not.nmontoNota,
                    Not.cestadoNota,
                    Not.nmntutilizado,
                    Not.estado,
                    Not.moneda,
                    Not.ctipNota,
                    Not.tiponota 

                })
                .AsQueryable());
            else if (_cod_prd == 0 && _LetEst != "T")
                datos = ToDataTable(this.Find(Not => Not.cestadoNota == _LetEst && Not.valor == _valor)
                .OrderByDescending(Not => Not.FecEmesion)
                .Select(Not => new
                {
                    Not.inota,
                    Not.ccodnota,
                    Not.FecEmesion,
                    Not.iprvcod,
                    Not.PrvRazon,
                    Not.nmontoNota,
                    Not.cestadoNota,
                    Not.nmntutilizado,
                    Not.estado,
                    Not.moneda,
                    Not.ctipNota,
                    Not.tiponota 

                })
                .AsQueryable());
            else if (_cod_prd != 0 && _LetEst != "T")
                datos = ToDataTable(this.Find(Not => Not.cestadoNota == _LetEst && Not.iprvcod == _cod_prd && Not.valor == _valor)
                .OrderByDescending(Not => Not.FecEmesion)
                .Select(Not => new
                {
                    Not.inota,
                    Not.ccodnota,
                    Not.FecEmesion,
                    Not.iprvcod,
                    Not.PrvRazon,
                    Not.nmontoNota,
                    Not.cestadoNota,
                    Not.nmntutilizado,
                    Not.estado,
                    Not.moneda,
                    Not.ctipNota,
                    Not.tiponota 

                })
                .AsQueryable());
            else
                datos = ToDataTable(this.Find(Not => Convert.ToDateTime(Not.FecEmesion) >= _fecdesde
                     && Convert.ToDateTime(Not.FecEmesion) <= _fechasta && Not.iprvcod == _cod_prd && Not.valor == _valor)
                 .OrderByDescending(Not => Not.FecEmesion)
                 .Select(Not => new
                 {
                     Not.inota,
                     Not.ccodnota,
                     Not.FecEmesion,
                     Not.iprvcod,
                     Not.PrvRazon,
                     Not.nmontoNota,
                     Not.cestadoNota,
                     Not.nmntutilizado,
                     Not.estado,
                     Not.moneda,
                     Not.ctipNota,
                     Not.tiponota 

                 })
                 .AsQueryable());
            return datos;
        }

        public DataTable GetLista(  String _bus, String _Tipo, String _estado, 
                                    String _moneda, int _codProv, DateTime _fecdesde, 
                                    DateTime _fechasta, String _valor, int _letra)
        {
            String tTipo= _bus .Substring (0,1);

            DataTable datos;
            datos = ToDataTable(this.Find(Not=> Not.valor == _valor
                && (_bus.Substring(0, 1) == "1" || (_bus.Substring(0, 1) == "2" && Not.ctipNota == _Tipo))
                && (_bus.Substring(1, 1) == "1" || (_bus.Substring(1, 1) == "2"&& Not.cestadoNota ==_estado))
                && (_bus.Substring(2, 1) == "1" || (_bus.Substring(2, 1) == "2" && Not.moneda == _moneda))
                && (_bus.Substring(3, 1) == "1" || (_bus.Substring(3, 1) == "2" && Not.iprvcod == _codProv))
                && (_bus.Substring(4, 1) == "1" || (_bus.Substring(4, 1) == "2" && Not.FecEmesion >=_fecdesde ))
                && (_bus.Substring(5, 1) == "1" || (_bus.Substring(5, 1) == "2" && Not.FecEmesion >= _fechasta))
                && (_bus.Substring(6, 1) == "1" || (_bus.Substring(6, 1) == "2" && Not.letra == _letra)))
            .OrderByDescending(Not => Not.FecEmesion)
            .Select(Not => new
            {
                Not.valor,
                Not.inota,
                Not.ccodnota,
                Not.FecEmesion,
                Not.iprvcod,
                Not.PrvRazon,
                Not.nmontoNota,
                Not.cestadoNota,
                Not.nmntutilizado,
                Not.estado,
                Not.moneda,
                Not.ctipNota,
                Not.tiponota,
                Not.cobservaciones,
                Not.letra
            })
            .AsQueryable());
            return datos;
        }
    }
    //public class clsreptNotasGen : clsNotasGeneralDao 
    public class clsreptNotasGen : clsAbstractBase<RepNotGen>, IRepNotGen
    {
       
        public DataTable GetList(String _LetEst, int _cod_prd, DateTime _fecdesde, DateTime _fechasta, String _valor, int tipo)
        {
            //tipo 2 : sin proveedor con estado
            //tipo 1: con proveeodr con estado
            //tipo 3: sin proveeodr sin estado
            //tipo 4: con proveeodr sin estado

            DataTable datos;            
                datos = ToDataTable(this.Find(Not =>
                        (Not.valor == _valor && Not.cestadoNota == _LetEst && Not.FecEmesion <= _fechasta && Not.FecEmesion >= _fecdesde && tipo == 2) ||
                        (Not.valor == _valor && Not.cestadoNota == _LetEst && Not.FecEmesion <= _fechasta && Not.FecEmesion >= _fecdesde && Not.iprvcod == _cod_prd && tipo == 1)||
                         (Not.valor == _valor && Not.FecEmesion <= _fechasta && Not.FecEmesion >= _fecdesde && tipo == 3)||
                         (Not.valor == _valor && Not.FecEmesion <= _fechasta && Not.FecEmesion >= _fecdesde && Not.iprvcod == _cod_prd && tipo == 4))
                .OrderByDescending(Not => Not.PrvRazon)
                .Select(Not => new
                {
                    Not.valor,
                    Not.TIPO,
                    Not.inota,
                    Not.ccodnota,
                    Not.FecEmesion,
                    Not.iprvcod,
                    Not.PrvRazon,
                    Not.cestadoNota,
                    Not.estado,
                    Not.COD_LETRA,
                    Not.TOTFACT,
                    Not.nmntdolares,
                    Not.nmntsoles,
                    Not.moneda


                })
                .AsQueryable());
            
            return datos;
        }

    }
    //public class clsreptNotasDet : clsNotasDetalleDao
    public class clsreptNotasDet : clsAbstractBase<RepNotDet>, IRepNotDet
    {

        public DataTable GetList(String _LetEst, int _cod_prd, DateTime _fecdesde, DateTime _fechasta, String _valor, int tipo)
        {
            //tipo 2 : sin proveedor con estado
            //tipo 1: con proveeodr con estado
            //tipo 3: sin proveeodr sin estado
            //tipo 4: con proveeodr sin estado

            DataTable datos;
            datos = ToDataTable(this.Find(Not =>
                    (Not.valor == _valor && Not.cestadoNota == _LetEst && Not.FecEmesion <= _fechasta && Not.FecEmesion >= _fecdesde && tipo == 2) ||
                    (Not.valor == _valor && Not.cestadoNota == _LetEst && Not.FecEmesion <= _fechasta && Not.FecEmesion >= _fecdesde && Not.iprvcod == _cod_prd && tipo == 1) ||
                     (Not.valor == _valor && Not.FecEmesion <= _fechasta && Not.FecEmesion >= _fecdesde && tipo == 3) ||
                     (Not.valor == _valor && Not.FecEmesion <= _fechasta && Not.FecEmesion >= _fecdesde && Not.iprvcod == _cod_prd && tipo == 4))
            .OrderByDescending(Not => Not.PrvRazon)
            .Select(Not => new
            {
                Not.valor,
                Not.TIPO,
                Not.inota,
                Not.ccodnota,
                Not.FecEmesion,
                Not.iprvcod,
                Not.PrvRazon,
                Not.cestadoNota,
                Not.estado,
                Not.COD_LETRA,
                Not.factura,
                Not.nmntdolares,
                Not.nmntsoles,
                Not.nmntdolutl,
                Not.nmntsolutl,
                Not.moneda


            })
            .AsQueryable());

            return datos;
        }

    }

    //public class clsviewrel_not_fac : clsviewrel_not_facDao
    public class clsviewrel_not_fac : clsAbstractBase<rel_not_fact>, Iclsviewrel_not_fac
    {
        public DataTable GetListNotas(int _cod_not)
        {
            return ToDataTable(this.Find(Not => Not.inota == _cod_not)
            .OrderByDescending(Not => Not.dfecreg)
            .Select(Not => new
            {
                inota = Not.inota,
                ccodnota = Not.ccodnota,
                dfecreg = Not.dfecreg,
                nmontonota = Not.nmonto,
                cestadoNota = "P",
                nmntutilizado = Not.nmonto,
                Not.dfecmod,
                Not.iusrcrc,
                Not.irelnotfac
            })
            .AsQueryable());
        }
    }

}
