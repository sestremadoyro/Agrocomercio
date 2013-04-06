using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using pryAgrocomercioDAL;
using pryAgrocomercioBLL.EntityCollection.Interfaces;

namespace pryAgrocomercioBLL.EntityCollection
{
    public class clsUsuario : clsAbstractBase<Usuarios>, IUsuario
    {
        public Usuarios GetUsuario(string user, string clave)
        {
            var result = this.Find(usuarios => usuarios.usrLogin == user && usuarios.usrClave == clave);
            if (result.Count() == 0)
                return null;
            else
                return result.First<Usuarios>();

        }

        public DataTable GetList()
        {
            var result = this.GetAll().OrderBy(Usr => Usr.perCod)
                .Select(Usr => new
                {
                    Usr.usrCod,
                    Usr.usrLogin,
                    Usr.usrClave,
                    Usr.RolCod,
                    Usr.perCod
                });

            return ToDataTable<object>(result.AsQueryable());
        }
        public DataTable GetListUsuarios()
        {
            var result = this.GetAll().OrderBy(Usr => Usr.perCod)
                .Select(Usr => new
                {
                    Usr.usrCod,
                    Usr.usrLogin,
                    Usr.usrClave,
                    Usr.RolCod,
                    Usr.perCod,
                    Usr.Personal.perNombres,
                    Usr.Personal.perApellidoPat,
                    Usr.Personal.perApellidoMat
                });

            return ToDataTable<object>(result.AsQueryable());
        }

        public object GetUsuarioPorRol(int rolCod)
        {
            return this.Find(Usr => Usr.RolCod == rolCod);
        }
        public Usuarios GetUsuario(int usrCod)
        {
            return this.Find(Usr => Usr.usrCod == usrCod).First<Usuarios>();
        }
        public int MaxpnUserCod()
        {
            if (this.GetAll().Count() > 0)
            {
                return this.GetAll().Max<Usuarios>(Usr => Usr.usrCod);
            }
            return 0;
        }

        public void DeleteUsuario(int usrCod)
        {
            try
            {
                var Usuario = this.Find(Usr => Usr.usrCod == usrCod).FirstOrDefault();
                this.Delete(Usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
