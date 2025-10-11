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
    public partial class DetalleDeArticulos : System.Web.UI.Page
    {
        public Articulo articuloSeleccionado { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Validar parámetro 'id' en la URL
                string parametroId = Request.QueryString["id"];
                if (string.IsNullOrEmpty(parametroId) || !int.TryParse(parametroId, out int id))
                {
                    Response.Redirect("ListaDeArticulos.aspx");
                    return;
                }

                ArticuloNegocio articuloNegocio = new ArticuloNegocio();
                Articulo articuloSeleccionado = articuloNegocio.BuscarArticuloPorId(id);

                if (articuloSeleccionado == null)
                {
                    Response.Redirect("ListaDeArticulos.aspx");
                    return;
                }

                // Cargar los datos del artículo en los controles
                txtId.Text = articuloSeleccionado.IdArticulo.ToString();
                txtNombre.Text = articuloSeleccionado.NombreArticulo;
                txtCodigo.Text = articuloSeleccionado.CodigoArticulo;
                txtCategoria.Text = articuloSeleccionado.Categoria.DescripcionCategoria;
                txtMarca.Text = articuloSeleccionado.Marca.DescripcionMarca;
                txtPrecio.Text = articuloSeleccionado.PrecioArticulo.ToString("C");

                // Cargar imágenes en el carrusel
                repImagenes.DataSource = articuloSeleccionado.Imagenes;
                repImagenes.DataBind();
            }
        }
    }
}