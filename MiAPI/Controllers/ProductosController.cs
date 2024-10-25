using MiAPI.Model;
using Microsoft.AspNetCore.Mvc;

namespace MiAPI.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class ProductosController : Controller
    {
        private static List<Producto> listaproductos = new List<Producto> {
            new Producto { Id= 1,Nombre ="Laptop", Precio=1500},
            new Producto { Id=2, Nombre="Mouse", Precio=25}
            };

        [HttpGet]
        public ActionResult<IEnumerable<Producto>> GetProductos() { return Ok(listaproductos); }

        public IActionResult Index()
        {
            return View();
        }

        // POST: api/productos
        [HttpPost]
        public ActionResult<Producto> CreateProducto([FromBody] Producto newProducto)
        {
            if (newProducto == null || string.IsNullOrEmpty(newProducto.Nombre) || newProducto.Precio <= 0)
            {
                return BadRequest("Datos inválidos");
            }

            // Generar un nuevo Id basado en el último producto existente
            var nuevoId = listaproductos.Max(p => p.Id) + 1;
            newProducto.Id = nuevoId;
            listaproductos.Add(newProducto);

            return CreatedAtAction(nameof(GetProductos), new { id = newProducto.Id }, newProducto);
        }

        // PUT: api/productos/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateProducto(int id, [FromBody] Producto updatedProducto)
        {
            var producto = listaproductos.FirstOrDefault(p => p.Id == id);
            if (producto == null)
            {
                return NotFound();
            }

            if (updatedProducto == null || string.IsNullOrEmpty(updatedProducto.Nombre) || updatedProducto.Precio <= 0)
            {
                return BadRequest("Datos inválidos");
            }

            // Actualizar el producto existente
            producto.Nombre = updatedProducto.Nombre;
            producto.Precio = updatedProducto.Precio;

            return NoContent();
        }

        // DELETE: api/productos/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteProducto(int id)
        {
            var producto = listaproductos.FirstOrDefault(p => p.Id == id);
            if (producto == null)
            {
                return NotFound();
            }

            listaproductos.Remove(producto);
            return NoContent();
        }
    }
}

