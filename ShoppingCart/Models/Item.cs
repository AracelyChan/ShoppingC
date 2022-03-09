using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Models
{
    /// <summary>Class <c>Item</c> modelo del objeto
    /// </summary>
    public class Item
    {
        public String nombre { get; set; }
        public int cantidad { get; set; } = 0;
        public double precio { get; set; } = 0;

    }
}
