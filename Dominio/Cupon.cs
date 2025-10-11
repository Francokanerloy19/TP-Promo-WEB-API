using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Cupon
    {
        public string codigoVoucher { get; set; }
        public DateTime? fechaCanje { get; set; }
        public int? idClinte { get; set; }
        public int? idArticulo { get; set; }
    }
}
