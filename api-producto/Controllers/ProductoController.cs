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
        public IEnumerable<Articulo> Get()
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            return negocio.Listar();
        }

        // GET: api/Producto/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Producto
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Articulo/id
        public void Put(int id, [FromBody] ArticuloDto articulo)
        {
            if (articulo == null)
                throw new Exception("El objeto artículo es nulo.");

            if (articulo.IdMarca == 0 || articulo.IdCategoria == 0)
                throw new Exception("El artículo debe tener una marca y una categoría.");

            if (articulo.Imagenes == null)
                articulo.Imagenes = new List<Imagen>();

            ArticuloNegocio negocio = new ArticuloNegocio();
            Articulo nuevo = new Articulo
            {
                IdArticulo = id,
                CodigoArticulo = articulo.CodigoArticulo,
                NombreArticulo = articulo.NombreArticulo,
                DescripcionArticulo = articulo.DescripcionArticulo,
                PrecioArticulo = articulo.PrecioArticulo,
                Marca = new Marca { IdMarca = articulo.IdMarca },
                Categoria = new Categoria { IdCategoria = articulo.IdCategoria },
                Imagenes = articulo.Imagenes
            };

            negocio.modificar(nuevo);
        }

        // DELETE: api/Producto/5
        public void Delete(int id)
        {
        }
    }
}
