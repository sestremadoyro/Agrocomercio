using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using pryAgrocomercioDAL;
using pryAgrocomercioBLL.EntityCollection.Interfaces;

namespace pryAgrocomercioBLL.EntityCollection
{
    public class clsRoles :  clsAbstractBase<Roles>, IRoles
    {
          public DataTable GetList()
        {
            var result = this.GetAll().OrderBy(rol => rol.rolDescripcion)
                .Select(rol => new
                {
                    rol.rolCod ,
                    rol.rolDescripcion

                });

            return ToDataTable<object>(result.AsQueryable());
        }

        public Roles  GetRoles(int rolCod)
        {
            try
            {
                var Result = this.Find(rol => rol.rolCod == rolCod);
                if (Result.Count() == 0)
                    return null;
                else
                    return Result.First<Roles>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteRoles(int rolCod)
        {
            try
            {
                var Rol = this.Find(rol => rol.rolCod == rolCod).FirstOrDefault();
                this.Delete(Rol);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int MaxLotCod()
        {
            var result = this.GetAll();

            if (result.Count() > 0)
            {
                return result.Max<Roles>(rol => rol.rolCod);
            }
            return 0;
        }
            }

    
}
