using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Net.Mime.MediaTypeNames;

namespace TP_Promo_WEB
{
    public partial class Formulario : System.Web.UI.Page
    {
        public Cupon voucher { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {


        }

        protected void btnParticipar_Click(object sender, EventArgs e)
        {
            Cliente cliente = new Cliente();

            ClienteNegocio clienteNegocio = new ClienteNegocio();
            cliente.DNI = txtDNI.Text;
            cliente.ciudad = txtCiudad.Text;
            cliente.codPostal = int.Parse(txtCP.Text);
            cliente.direccion = txtDireccion.Text;
            cliente.email = validationtxtEmail.Text;
            cliente.apellido = TxtApellido.Text;
            cliente.nombre = TextNombre.Text;

            Cliente clienteExistente = clienteNegocio.buscarDNI(cliente.DNI);
            if (clienteExistente == null)
            {
                // Agrego el nuevo cliente
                clienteNegocio.agregar(cliente);
                // Lo busco para recuperar el ID generado
                cliente = clienteNegocio.buscarDNI(cliente.DNI);
            }
            else
            {
                cliente = clienteExistente;
            }



            // Recupero el objeto Cupon de sesión
            Cupon voucher = (Cupon)Session["voucher"];

            if (voucher != null)
            {
                // Completo los datos que necesites
                voucher.fechaCanje = DateTime.Now;
                cliente = clienteNegocio.buscarDNI(cliente.DNI);
                voucher.idClinte = cliente.id;
                

                CuponNegocio negocio = new CuponNegocio();
                negocio.ModificarCupon(voucher);

                // limpio el objeto guardado en sesión 
                Session.Remove("voucher");

                Response.Redirect("Final.aspx", false);
            }

        }

        protected void txtDNI_TextChanged(object sender, EventArgs e)
        {
            ClienteNegocio clienteNegocio = new ClienteNegocio();

            Cliente cliente = new Cliente();
            cliente = clienteNegocio.buscarDNI(txtDNI.Text);

            if (cliente != null)
            {
                txtCiudad.Text = cliente.ciudad.ToString();
                txtCP.Text = cliente.codPostal.ToString();
                txtDireccion.Text = cliente.direccion.ToString();
                validationtxtEmail.Text = cliente.email.ToString();
                TxtApellido.Text = cliente.apellido.ToString();
                TextNombre.Text = cliente.nombre.ToString();


            }
            
        }
    }
}