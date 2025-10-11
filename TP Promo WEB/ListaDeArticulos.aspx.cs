using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TP_Promo_WEB
{
    public partial class ListaDeArticulos : System.Web.UI.Page
    {
        public List<Articulo> ListaArticulos { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            ArticuloNegocio articuloNegocio = new ArticuloNegocio();

            try
            {
                ListaArticulos = articuloNegocio.Listar();
                if (!IsPostBack)
                {
                    repetidorDeArticulos.DataSource = ListaArticulos;
                    repetidorDeArticulos.DataBind();
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
            }
        }
        protected void btnContinuar_Click(object sender, EventArgs e)
        {
            Button boton = (Button)sender;
            int idArticulo = Convert.ToInt32(boton.CommandArgument);
            Cupon voucher = (Cupon)Session["voucher"];
            if (voucher != null)
            {
                voucher.idArticulo = idArticulo;

                // actualizo el objeto en seion
                Session["voucher"] = voucher;
            }
            Response.Redirect("Formulario.aspx");
        }

    }
}