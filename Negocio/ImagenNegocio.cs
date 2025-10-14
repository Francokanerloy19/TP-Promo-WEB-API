using Dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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


        public void agregarImagen(int idProducto, List<string> imagenes)
        {
            {
                AccesoDatos datos = new AccesoDatos();

                try
                {
                    // Iteramos la lista de imágenes
                    for (int i = 0; i < imagenes.Count; i++)
                    {
                        datos.limpiarParametros(); // Limpiamos parámetros del ciclo anterior
                        datos.setearConsulta("INSERT INTO IMAGENES (IdArticulo, ImagenUrl) VALUES (@idProducto, @url)");
                        datos.setearParametros("@idProducto", idProducto);
                        datos.setearParametros("@url", imagenes[i]);

                        datos.ejecutarAccion();
                    }


                }
                catch (Exception ex)
                {


                    throw ex;
                }
                finally
                {
                    datos.cerrarConexion();
                }
            }
        }

    }
}
