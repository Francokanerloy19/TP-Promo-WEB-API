using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Negocio
{
    public class ArticuloNegocio
    {
        public List<Articulo> Listar()
        {
            List<Articulo> Lista = new List<Articulo>();
            AccesoDatos accesoDatos = new AccesoDatos();
            try
            {
                accesoDatos.setearConsulta("SELECT a.Id AS IdArticulo, a.Codigo AS CodigoArticulo, a.Nombre AS NombreArticulo, a.Descripcion AS DescripcionArticulo, a.IdMarca, m.Descripcion AS DescripcionMarca, a.IdCategoria, c.Descripcion AS DescripcionCategoria, a.Precio AS PrecioArticulo FROM ARTICULOS a INNER JOIN MARCAS m ON a.IdMarca = m.Id INNER JOIN CATEGORIAS c ON a.IdCategoria = c.Id;");
                accesoDatos.ejecutarConsulta();

                while (accesoDatos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.IdArticulo = (int)accesoDatos.Lector["IdArticulo"];
                    aux.CodigoArticulo = (string)accesoDatos.Lector["CodigoArticulo"];
                    aux.NombreArticulo = (string)accesoDatos.Lector["NombreArticulo"];
                    aux.DescripcionArticulo = (string)accesoDatos.Lector["DescripcionArticulo"];
                    aux.PrecioArticulo = (decimal)accesoDatos.Lector["PrecioArticulo"];

                    aux.Marca = new Marca();
                    aux.Marca.IdMarca = (int)accesoDatos.Lector["IdMarca"];
                    aux.Marca.DescripcionMarca = (string)accesoDatos.Lector["DescripcionMarca"];

                    aux.Categoria = new Categoria();
                    aux.Categoria.IdCategoria = (int)accesoDatos.Lector["IdCategoria"];
                    aux.Categoria.DescripcionCategoria = (string)accesoDatos.Lector["DescripcionCategoria"];

                    ImagenNegocio imagen = new ImagenNegocio();
                    aux.Imagenes = imagen.ObtenerImagenesPorArticulo(aux.IdArticulo);

                    Lista.Add(aux);
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
            return Lista;
        }

        public Articulo BuscarArticuloPorId(int id)
        {
            Articulo articulo = null;
            AccesoDatos accesoDatos = new AccesoDatos();

            try
            {
                accesoDatos.setearConsulta("SELECT a.Id AS IdArticulo, a.Codigo AS CodigoArticulo, a.Nombre AS NombreArticulo, a.Descripcion AS DescripcionArticulo, a.IdMarca, m.Descripcion AS DescripcionMarca, a.IdCategoria, c.Descripcion AS DescripcionCategoria, a.Precio AS PrecioArticulo FROM ARTICULOS a INNER JOIN MARCAS m ON a.IdMarca = m.Id INNER JOIN CATEGORIAS c ON a.IdCategoria = c.Id WHERE a.Id = @id");

                accesoDatos.setearParametros("@Id", id);
                accesoDatos.ejecutarConsulta();

                if (accesoDatos.Lector.Read())
                {
                    articulo = new Articulo
                    {
                        IdArticulo = (int)accesoDatos.Lector["IdArticulo"],
                        CodigoArticulo = (string)accesoDatos.Lector["CodigoArticulo"],
                        NombreArticulo = (string)accesoDatos.Lector["NombreArticulo"],
                        DescripcionArticulo = (string)accesoDatos.Lector["DescripcionArticulo"],
                        PrecioArticulo = (decimal)accesoDatos.Lector["PrecioArticulo"]
                    };

                    articulo.Marca = new Marca
                    {
                        IdMarca = (int)accesoDatos.Lector["IdMarca"],
                        DescripcionMarca = (string)accesoDatos.Lector["DescripcionMarca"]
                    };

                    articulo.Categoria = new Categoria
                    {
                        IdCategoria = (int)accesoDatos.Lector["IdCategoria"],
                        DescripcionCategoria = (string)accesoDatos.Lector["DescripcionCategoria"]
                    };

                    ImagenNegocio imagen = new ImagenNegocio();
                    articulo.Imagenes = imagen.ObtenerImagenesPorArticulo(articulo.IdArticulo);
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

            return articulo;
        }

      
        public int Alta(Articulo art)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(@"INSERT INTO ARTICULOS (Codigo, Nombre, Descripcion, IdMarca, IdCategoria, Precio) OUTPUT INSERTED.Id VALUES (@Codigo, @Nombre, @Descripcion, @IdMarca, @IdCategoria, @Precio)");
       
                datos.setearParametros("@Codigo", art.CodigoArticulo);
                datos.setearParametros("@Nombre", art.NombreArticulo);
                datos.setearParametros("@Descripcion", art.DescripcionArticulo);
                datos.setearParametros("@Marca", art.Marca.IdMarca);
                datos.setearParametros("@Categoria", art.Categoria.IdCategoria);
                datos.setearParametros("@Precio", art.PrecioArticulo);

                //Llama al metodo ejecutarEscalar que devuelve un solo dato obteniendo el id generado
                int idGenerado = (int)datos.ejecutarEscalar();

                return idGenerado;
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
        
        public void modificar(Articulo art)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {


                // Actualizo los datos del artículo
                datos.setearConsulta("UPDATE ARTICULOS SET IdMarca=@Mrca, IdCategoria=@Ctgria, Codigo=@cod, Nombre=@nom, Descripcion=@desc, Precio=@Prec WHERE Id=@id");
                datos.setearParametros("@id", art.IdArticulo);
                datos.setearParametros("@cod", art.CodigoArticulo);
                datos.setearParametros("@nom", art.NombreArticulo);
                datos.setearParametros("@desc", art.DescripcionArticulo);
                datos.setearParametros("@Mrca", art.Marca.IdMarca);
                datos.setearParametros("@Ctgria", art.Categoria.IdCategoria);
                datos.setearParametros("@Prec", art.PrecioArticulo);
                datos.ejecutarAccion();

            
                for (int i = 0; i < art.Imagenes.Count; i++)
                {
                    // Limpio parámetros anteriores
                    datos.limpiarParametros();

                    // Nueva consulta con desplazamiento dinámico
                    datos.setearConsulta(@"UPDATE IMAGENES SET ImagenUrl = @img WHERE Id = (SELECT MIN(Id) + @posicion FROM IMAGENES WHERE IdArticulo = @idArticulo)");

                    datos.setearParametros("@img", art.Imagenes[i].ImagenUrl);
                    datos.setearParametros("@posicion", i);
                    datos.setearParametros("@idArticulo", art.IdArticulo);
                    datos.ejecutarAccion();
                    datos.cerrarConexion();
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

        public void eliminar(int idArticulo)
        {
            AccesoDatos accesoDatos = new AccesoDatos();
            try
            {
                accesoDatos.limpiarParametros();
                accesoDatos.setearConsulta("DELETE FROM VOUCHERS WHERE IdArticulo = @id");
                accesoDatos.setearParametros("@id", idArticulo);
                accesoDatos.ejecutarAccion();

                accesoDatos.limpiarParametros();
                accesoDatos.setearConsulta("DELETE FROM IMAGENES WHERE IdArticulo = @id");
                accesoDatos.setearParametros("@id", idArticulo);
                accesoDatos.ejecutarAccion();

                accesoDatos.limpiarParametros();
                accesoDatos.setearConsulta("DELETE FROM ARTICULOS WHERE Id = @id");
                accesoDatos.setearParametros("@id", idArticulo);
                accesoDatos.ejecutarAccion();

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                accesoDatos.cerrarConexion();
            }
        }

    }
}
