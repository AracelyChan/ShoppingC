using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Models
{
    /// <summary>Class <c>ShoppingCart</c> modelo del objeto
    /// </summary>
    public class Cart
    {
        public DateTime fechaCompra { get; set; } = new DateTime();
        public List<Item> listaItems { get; set; } = new List<Item>();
        public double totalCompra { get; set; }
    }
}
