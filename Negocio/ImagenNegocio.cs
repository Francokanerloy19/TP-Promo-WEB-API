using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ImagenNegocio
    {
        
        public List<Imagen> ObtenerImagenesPorArticulo(int idArticulo)
        {
            List<Imagen> imagenes = new List<Imagen>();
            AccesoDatos accesoDatos = new AccesoDatos();

            try
            {
                accesoDatos.setearConsulta("SELECT Id, IdArticulo, ImagenUrl FROM IMAGENES WHERE IdArticulo= " + idArticulo + "");
                accesoDatos.ejecutarConsulta();

                while (accesoDatos.Lector.Read())
                {
                    Imagen aux = new Imagen
                    {
                      IdImagen = (int)accesoDatos.Lector["Id"],
                      IdArticulo = (int)accesoDatos.Lector["IdArticulo"],
                      ImagenUrl = (string)accesoDatos.Lector["ImagenUrl"]
                    };
                    imagenes.Add(aux);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                accesoDatos.cerrarConexion();
            }
            return imagenes;
        }


        public void ModificarImagenes(List<Imagen> imagenes)
        {
            foreach (var img in imagenes)
            {
                if (img == null || string.IsNullOrEmpty(img.ImagenUrl))
                    continue;

                AccesoDatos datos = new AccesoDatos();
                try
                {
                    datos.setearConsulta("UPDATE IMAGENES SET ImagenUrl=@url WHERE Id=@id");
                    datos.setearParametros("@url", img.ImagenUrl);
                    datos.setearParametros("@id", img.IdImagen);
                    datos.ejecutarAccion();
                }
                finally
                {
                    datos.cerrarConexion();
                }
            }
        }

    }
}
