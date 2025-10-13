using Dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace api_producto.Models
{
    public class ArticuloDto
    {

        public string CodigoArticulo { get; set; }
        
        public string NombreArticulo { get; set; }
        
        public string DescripcionArticulo { get; set; }
       
        public decimal PrecioArticulo { get; set; }

        public int IdMarca { get; set; }
        public int IdCategoria { get; set; }

        public int IdImagen { get; set; }
    }
}