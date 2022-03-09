using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Models;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShoppingCart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingController : ControllerBase
    {
        string fileName = "./Data/Storage.json";
        string jsonString = System.IO.File.ReadAllText("./Data/Storage.json");

        // GET: api/<ShoppingController>
        [HttpGet]
        public Object getShoppinCarts()
        {
            List<Cart> shoppingCarts;
            try
            {
                shoppingCarts = JsonSerializer.Deserialize<List<Cart>>(jsonString)!;
            }
            catch (Exception e)
            {
                shoppingCarts = new List<Cart>();
            }
            
            return shoppingCarts;
        }

        // GET: api/<ShoppingController>/obtenertotalcompra/{id}
        [Route("ObtenerTotalCompra/{id}")]
        [HttpGet("{id}")]
        public Object ObtenerTotalCompra(int id)
        {
            List<Cart> shoppingCarts;
            try
            {
                shoppingCarts = JsonSerializer.Deserialize<List<Cart>>(jsonString)!;
            }
            catch (Exception e)
            {
                shoppingCarts = new List<Cart>();
            }
            var total = shoppingCarts[id-1].totalCompra;
            return total;
        }

        // GET api/<ShoppingController>/obtenercantidaddeitems/{id}
        [Route("ObtenerCantidadDeItems/{id}")]
        [HttpGet("{id}")]
        public Object ObtenerCantidadDeItems(int id)
        {
            List<Cart> shoppingCarts;
            try
            {
                shoppingCarts = JsonSerializer.Deserialize<List<Cart>>(jsonString)!;
            }
            catch (Exception e)
            {
                shoppingCarts = new List<Cart>();
            }
            var totalItem = shoppingCarts[id-1].listaItems.Sum(item => item.cantidad);
            return totalItem;
        }

        // PUT api/<ShoppingController>/agregaritem/{id}
        [Route("AgregarItem/{id}")]
        [HttpPut("{id}")]
        public Object AgregarItem(int id, [FromBody] Item item)
        {
            List<Cart> shoppingCarts;
            try
            {
                shoppingCarts = JsonSerializer.Deserialize<List<Cart>>(jsonString)!;
            }
            catch (Exception e)
            {
                shoppingCarts = new List<Cart>();
            }
            Cart shoppingCart;
            bool validIndex = shoppingCarts.Count >= id;

            if (validIndex)
            {
                shoppingCart = shoppingCarts[id-1];
            }
            else
            {
                shoppingCart = new Cart();
            }

            shoppingCart.listaItems.Add(item);
            shoppingCart.totalCompra += (item.precio * item.cantidad);

            if (validIndex)
            {
                shoppingCarts[id - 1] = shoppingCart;
            }
            else
            {
                shoppingCarts.Add(shoppingCart);
            }
            jsonString = JsonSerializer.Serialize<List<Cart>>(shoppingCarts);
            System.IO.File.WriteAllText(fileName, jsonString);

            return shoppingCart;
        }

        // PUT api/<ShoppingController>/comprar/{id}
        [Route("Comprar/{id}")]
        [HttpPut("{id}")]
        public Object Comprar( int id)
        {
            List<Cart> shoppingCarts;
            try
            {
                shoppingCarts = JsonSerializer.Deserialize<List<Cart>>(jsonString)!;
            }
            catch (Exception e)
            {
                return NotFound();
            }
            shoppingCarts[id-1].fechaCompra= DateTime.Now;

            jsonString = JsonSerializer.Serialize<List<Cart>>(shoppingCarts);
            System.IO.File.WriteAllText(fileName, jsonString);

            return shoppingCarts[id-1];
        }
    }
}
