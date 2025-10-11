using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Dominio
{
    public class Articulo
    {

        public int IdArticulo { get; set; }
        [DisplayName("Código")]
        public string CodigoArticulo { get; set; }
        [DisplayName("Nombre")]
        public string NombreArticulo { get; set; }
        [DisplayName("Descripción")]
        public string DescripcionArticulo { get; set; }
        [DisplayName("Precio")]
        public decimal PrecioArticulo { get; set; }

        public Marca Marca { get; set; }
        public Categoria Categoria { get; set; }
        public List<Imagen> Imagenes { get; set; }
    }
}
