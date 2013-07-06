using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Collections.Generic;
using pryAgrocomercioDAL;
using pryAgrocomercioBLL.EntityCollection.Interfaces;
using System.Web;

namespace pryAgrocomercioBLL.EntityCollection
{
    public abstract class clsAbstractBase<T> : IAbstractBase<T> where T : class 
    {
        protected AgrocomercioEntities AgroEntidades;
        public int gcUsrCod;

        public clsAbstractBase(){
            AgroEntidades = new AgrocomercioEntities();

            HttpContext contexto = HttpContext.Current;
            Usuarios objUsuario;

            if (contexto.Session["oUsuario"] == null)
                gcUsrCod = 999;
            else
            {
                objUsuario = (Usuarios)(contexto.Session["oUsuario"]);
                if (objUsuario == null)
                    gcUsrCod = 999;
                else
                    gcUsrCod = (int)objUsuario.perCod;
            }                
        }
        public clsAbstractBase(AgrocomercioEntities _AgroEntidades)
        {
            AgroEntidades = _AgroEntidades;

            HttpContext contexto = HttpContext.Current;
            Usuarios objUsuario;

            if (contexto.Session["oUsuario"] != null)
                gcUsrCod = 999;
            else
            {
                objUsuario = (Usuarios)(contexto.Session["oUsuario"]);
                if (objUsuario == null)
                    gcUsrCod = 999;
                else
                    gcUsrCod = (int)objUsuario.perCod;
            }
        }       


        public void Add(T pEntity)
        {
            AgroEntidades.AddObject(pEntity.GetType().Name, pEntity);
        }
        public void Delete(T pEntity)
        {
            AgroEntidades.DeleteObject(pEntity);
        }
        public void Attach(T pEntity)
        {
            AgroEntidades.AttachTo(pEntity.GetType().Name, pEntity);
        }
        public void Detach(T pEntity)
        {
            AgroEntidades.Detach(pEntity);
        }
        public void Update(T pEntity)
        {
            AgroEntidades.ApplyCurrentValues<T>(pEntity.GetType().Name, pEntity);
        }
        public T FindBy(Expression<Func<T, bool>> expression)
        {
            return Find(expression).SingleOrDefault();
        }
        public IQueryable<T> Find(Expression<Func<T, bool>> where)
        {
            return AgroEntidades.CreateObjectSet<T>().Where(where);            
        }
        public IQueryable<T> GetAll()
        {
            return AgroEntidades.CreateObjectSet<T>();
        }
        public void SaveChanges()
        {
            AgroEntidades.SaveChanges();
        }
        public DataTable ToDataTable<T>(IQueryable<T> varlist)
        {
            DataTable dtReturn = new DataTable();

            // column names
            PropertyInfo[] oProps = null;
            
            if (varlist == null) return dtReturn;
            
            foreach (T rec in varlist)
            {
                // Use reflection to get property names, to create table, Only first time, others will follow
                if (oProps == null)
                {
                    oProps = ((Type)rec.GetType()).GetProperties();
                    foreach (PropertyInfo pi in oProps)
                    {
                        Type colType = pi.PropertyType;
                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                            colType = colType.GetGenericArguments()[0];
                        
                        dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                    }
                }

                DataRow dr = dtReturn.NewRow();
                
                foreach (PropertyInfo pi in oProps)
                {
                    dr[pi.Name] = pi.GetValue(rec, null) == null ? DBNull.Value : pi.GetValue
                    (rec, null);
                }
                
                dtReturn.Rows.Add(dr);
            }
            return dtReturn;
        }

        public DataRow FormToDataRow(Object oForm)
        {
            DataTable dtReturn = new DataTable();

            PropertyInfo[] oProps = null;

            oProps = ((Type)oForm.GetType()).GetProperties();
            foreach (PropertyInfo pi in oForm.GetType().GetProperties())
            {
                Type colType = pi.PropertyType;
                if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                    colType = colType.GetGenericArguments()[0];

                if (pi.Name.StartsWith("c") || pi.Name.StartsWith("d") || pi.Name.StartsWith("n") || pi.Name.StartsWith("b"))
                    dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
            }
                
            DataRow dr = dtReturn.NewRow();                
            foreach (PropertyInfo pi in oProps)
            {
                if (pi.Name.StartsWith("c") || pi.Name.StartsWith("d") || pi.Name.StartsWith("n") || pi.Name.StartsWith("b"))
                    dr[pi.Name] = pi.GetValue(oForm, null) == null ? DBNull.Value : pi.GetValue(oForm, null);
            }
            dtReturn.Rows.Add(dr);
            return dtReturn.Rows[0];
        }
       
    }
}
