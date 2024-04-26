using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaModelo
{
    public class MProducto
    {
		public int IdProducto { get; set; }
		public string nombreProducto { get; set; }
		public string Descripcion { get; set; }
		public decimal precioProducto { get; set; }
		public string Proveedor { get; set; }
		public int IdCategoria { get; set; }
		public int Stock { get; set; }

	}
}
