using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using pryAgrocomercioBLL.EntityCollection;
using pryAgrocomercioDAL;
using System.Xml;
using System.Collections;
using OboutInc.EasyMenu_Pro;

namespace AgrocomercioWEB
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected bool CargaNombreUsuario()
        {
            BasePage oPagina = new BasePage();

            Usuarios objUsuario = (Usuarios)oPagina.LeerVariableSesion("oUsuario");

            if (objUsuario != null)
                lblUsuario.Text = "Bienvenido " + objUsuario.usrLogin.ToString();
            else
            {
                return false;
                //oPagina.MessageBox("El Usuario tiene que Iniciar una Sesion");
                //String scriptMsj = "";
                //scriptMsj = "window.location = '~/index.html'";
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "MENSAJE", scriptMsj, true);
                //Server.Transfer("~/index.html");
            }
            return true;

        }

        //protected void CargaMenuUsuario()
        //{
        //    BasePage oPagina = new BasePage();

        //    Usuarios objUsuario = (Usuarios)oPagina.LeerVariableSesion("oUsuario");

        //    if (objUsuario != null)
        //        if (objUsuario.RolCod == 1)  //administrador
        //        {
        //            pnlAdministrador.Visible = true;
        //            pnlVendedor.Visible = false;
        //            pnlAlmacen.Visible = false;
        //        }
        //        else
        //            if (objUsuario.RolCod == 2)//vendedor
        //            {
        //                pnlAdministrador.Visible = false;
        //                pnlVendedor.Visible = true;
        //                pnlAlmacen.Visible = false;
        //            }
        //            else
        //                if (objUsuario.RolCod == 3)//almacen
        //                {
        //                    pnlAdministrador.Visible = false;
        //                    pnlVendedor.Visible = false;
        //                    pnlAlmacen.Visible = true;
        //                }



        //}
        protected void Page_Load(object sender, EventArgs e)
        {
            Boolean bInicio = true;
            try
            {
                if (CargaNombreUsuario())
                    CargaMenuUsuario();
                else {
                    bInicio = false;
                }

            }
            catch
            {
                bInicio = false;                
            }
            if (!bInicio)
            {
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "MENSAJE", "window.location = 'index.html'", true);
                //BasePage oPagina = new BasePage();
                Response.Redirect("/index.html");
                //Server.Transfer("/index.html");
            }
            
            
        }

        protected void butCierre_Click(object sender, EventArgs e)
        {
            //elimina objeto sesión que contiene objeto USUARIO

            BasePage oPaginaBase = new BasePage();
            oPaginaBase.EliminarVariableSesion("oUsuario");

            Response.Redirect("~/index.html");

        }

        protected void LeerSubmenu(XmlNode menu, string IDParent, Boolean isTopMenu = false)
        {
            string cUrl = "";
            string menuTitle = "";
            string cDescription = "";
            string menuItemID = "";
            int nNumCor = 1;

            EasyMenu menuEM = new EasyMenu();
            menuEM.ID = IDParent + "_Items";
            menuEM.StyleFolder = "App_Themes/TemaAgrocomercio/EasyMenu/Horizontal1";
            menuEM.Width = "140";
            menuEM.ShowEvent = MenuShowEvent.MouseOver;
            menuEM.Align = isTopMenu ? MenuAlign.Under : MenuAlign.Advanced;
            menuEM.AttachTo = IDParent;

            foreach (XmlNode Item in menu)
            {
                cUrl = Item.Attributes["url"].Value;
                menuTitle = Item.Attributes["title"].Value;
                cDescription = Item.Attributes["description"].Value;
                menuItemID = menuEM.ID + "_" + nNumCor.ToString();

                if (menuTitle.Length * 10 > int.Parse(menuEM.Width.Replace("px","")))
                    menuEM.Width = (menuTitle.Length * 10).ToString();
                menuEM.AddItem(new OboutInc.EasyMenu_Pro.MenuItem(menuItemID, menuTitle, "", cUrl, "", ""));

                if (cUrl == "#")
                    LeerSubmenu(Item, menuItemID);
                nNumCor++;
            }
            Menu_placeHolder.Controls.Add(menuEM);

        }

        protected void CargaMenuUsuario()
        {
            BasePage oPagina = new BasePage();
            Usuarios objUsuario = (Usuarios)oPagina.LeerVariableSesion("oUsuario");
            string cUrl = "";
            string menuTitle = "";
            //string cDescription = "";
            int nNumCor = 1;
            string menuItemID = "";
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(Server.MapPath("~/" + objUsuario.Roles.rolMenu.ToString()));

            XmlNode SiteMap = xmlDoc.LastChild;

            ///MENU PRINCIAL
            foreach (XmlNode menuNode in SiteMap)
            {
                cUrl = menuNode.Attributes["url"].Value;
                menuTitle = menuNode.Attributes["title"].Value;
                //cDescription = menuNode.Attributes["description"].Value;
                menuItemID = menuTitle + "_" + nNumCor.ToString();

                EasymenuMain.AddItem(new OboutInc.EasyMenu_Pro.MenuItem(menuItemID, menuTitle, "", "", "", ""));
                EasymenuMain.AddSeparator("MenuSeparator" + nNumCor.ToString(), "|");

                if (cUrl == "#")
                    LeerSubmenu(menuNode, menuItemID, true);

                nNumCor++;
            }
        }


    }
}
