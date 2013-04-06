using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using pryAgrocomercioBLL.EntityCollection;
using pryAgrocomercioDAL;

namespace AgrocomercioWEB
{
    public partial class wfrmInicio : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strUsuario = this.ExtraeQueryStringCadena("u");
            string strClave = this.ExtraeQueryStringCadena("c");

            clsUsuario oUsuario = new clsUsuario();
            Usuarios objUsuario = oUsuario.GetUsuario(strUsuario, strClave);
            if (objUsuario == null)
            {
                this.MessageBox("Usuario o Contraseña Incorrecta");
                Response.Redirect("~/index.html");
            }
            else
            {
                //guarda el objeto USUARIO en la varible de SESSION
                AgregarVariableSession("oUsuario", objUsuario);
            }

               
        }
    }
}