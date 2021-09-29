using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AIPProducts.Models;
using Microsoft.AspNetCore.Authorization;
using AIPProducts.Authentication;
using Microsoft.AspNetCore.JsonPatch;

namespace AIPProducts.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/Products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly APIBDContext _context;
        public ProductsController(APIBDContext context)
        {
            _context = context;
        }
        //api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }
        //api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }
        //api/Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.ProductsId) return BadRequest();
            _context.Entry(product).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id)) return NotFound();
                else throw;
            }
            return NoContent();
        }
        //api/Products/5
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchProduct(int id, JsonPatchDocument Product)
        {
           
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            { 
                Product.ApplyTo(product);
                await _context.SaveChangesAsync();
            }
            return Ok();

        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetProduct",  new { id = product.ProductsId }, product);
        }
            
        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductsId == id);
        }
    }
}
