using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using api_producto.Models;
using Dominio;
using Negocio;

namespace api_producto.Controllers
{
    public class ProductoController : ApiController
    {
        // GET: api/Producto
        //Listar Producto
        public IEnumerable<Articulo> Get()
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            return negocio.Listar();
        }

        // GET: api/Producto/
        //Buscar producto
        public Articulo Get(int id)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            return negocio.BuscarArticuloPorId(id);
        }


        // POST: api/Producto
        //Alta Producto
        public void Post([FromBody] ArticuloDto articulo)
        {
            ImagenNegocio imagenNegocio = new ImagenNegocio();
            ArticuloNegocio articuloNegocio = new ArticuloNegocio();

            Articulo nuevo = new Articulo
            {
                CodigoArticulo = articulo.CodigoArticulo,
                NombreArticulo = articulo.NombreArticulo,
                DescripcionArticulo = articulo.DescripcionArticulo,
                PrecioArticulo = articulo.PrecioArticulo,
                Marca = new Marca { IdMarca = articulo.IdMarca },
                Categoria = new Categoria { IdCategoria = articulo.IdCategoria }
            };

            // ejemplo para validar el funcionamiento
            // Agregar imágenes a un Producto.
            int idArticulo = 3; // solo para prueba

           
            // Creamos una lista vacía de ejempolo
            List<string> urls = new List<string>();

            
            foreach (var imagen in articulo.Imagenes)
            {
                urls.Add(imagen.ImagenUrl);
            }

            // Llamamos al método
            
            // Recuperamos el Id del articulo agregado
            imagenNegocio.agregarImagen(idArticulo, urls);
        }

        // PUT: api/Articulo/id
        public void Put(int id, [FromBody] ArticuloDto articulo)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            Articulo nuevo = new Articulo();
            ImagenNegocio imgnegocio = new ImagenNegocio();

            nuevo.CodigoArticulo = articulo.CodigoArticulo;
            nuevo.NombreArticulo = articulo.NombreArticulo;
            nuevo.DescripcionArticulo = articulo.DescripcionArticulo;
            nuevo.PrecioArticulo = articulo.PrecioArticulo;
            nuevo.Marca = new Marca { IdMarca = articulo.IdMarca };
            nuevo.Categoria = new Categoria { IdCategoria = articulo.IdCategoria };
            nuevo.Imagenes = articulo.Imagenes;
            nuevo.IdArticulo = id;

            negocio.modificar(nuevo);


        }

        // DELETE: api/Producto/5
        public void Delete(int id)
        {
            try
            {
                ArticuloNegocio articuloNegocio = new ArticuloNegocio();
                articuloNegocio.eliminar(id);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
