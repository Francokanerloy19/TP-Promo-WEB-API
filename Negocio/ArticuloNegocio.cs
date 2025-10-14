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

                datos.setearParametros("@img1", art.Imagenes[0].ImagenUrl);
                datos.setearParametros("@img2", art.Imagenes[1].ImagenUrl);
                datos.setearParametros("@img3", art.Imagenes[2].ImagenUrl);

                datos.setearConsulta("UPDATE IMAGENES SET ImagenUrl=@img1 WHERE Id=( SELECT MIN(Id) FROM IMAGENES WHERE IdArticulo =@id)");
                datos.ejecutarAccion();
                datos.cerrarConexion();
                datos.setearConsulta("UPDATE IMAGENES SET ImagenUrl=@img2 WHERE Id=( SELECT MIN(Id)+1 FROM IMAGENES WHERE IdArticulo =@id)");
                datos.ejecutarAccion();
                datos.cerrarConexion();
                datos.setearConsulta("UPDATE IMAGENES SET ImagenUrl=@img3 WHERE Id=( SELECT MIN(Id)+2 FROM IMAGENES WHERE IdArticulo =@id)");
                datos.ejecutarAccion();
                datos.cerrarConexion();

                datos.setearConsulta("UPDATE ARTICULOS SET IdMarca = @Mrca WHERE Id = @id");
                datos.ejecutarAccion();
                datos.cerrarConexion();
                datos.setearConsulta("UPDATE ARTICULOS SET IdCategoria = @Ctgria WHERE Id = @id");
                datos.ejecutarAccion();
                datos.cerrarConexion();
                datos.setearConsulta("UPDATE ARTICULOS SET Codigo=@cod, Nombre=@nom, Descripcion=@desc, Precio=@Prec WHERE Id=@id");
                datos.ejecutarAccion();
                datos.cerrarConexion();



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
