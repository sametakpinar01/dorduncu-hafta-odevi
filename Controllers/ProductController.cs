using Microsoft.AspNetCore.Mvc;
using dorduncu_hafta_odevi.Models;
using System.Collections.Generic;
using System.Linq;

namespace dorduncu_hafta_odevi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        // Ürünleri depolamak için private static liste
        private static List<Product> _products = new List<Product>
        {
            new Product { Id = 1, Name = "Laptop", Price = 1500.99m, Category = "Electronics" },
            new Product { Id = 2, Name = "Mouse", Price = 25.50m, Category = "Accessories" }
        };

        // GET: api/product (Tüm ürünleri listele)
        [HttpGet]
        public IActionResult GetProducts()
        {
            return Ok(_products); // Ürün listesini JSON formatında döndürmek için 200 OK döner
        }

        // GET: api/product/{id} (Belirli bir ürünü getir)
        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound(); // Ürün bulunamazsa 404 döner
            }
            return Ok(product); // Bulunan ürünü JSON formatında döner
        }

        // POST: api/product (Yeni ürün ekle)
        [HttpPost]
        public IActionResult CreateProduct([FromBody] Product product)
        {
            // Yeni bir ID oluştur (mevcut en büyük ID'ye 1 ekle)
            product.Id = _products.Any() ? _products.Max(p => p.Id) + 1 : 1;
            _products.Add(product); // Ürünü listeye ekle
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product); // 201 Oluşturuldu döner
        }

        // PUT: api/product/{id} (Ürünü güncelle)
        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] Product updatedProduct)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound(); // Ürün bulunamazsa 404 döner
            }
            product.Name = updatedProduct.Name;
            product.Price = updatedProduct.Price;
            product.Category = updatedProduct.Category;
            return NoContent(); // Başarılı güncelleme için 204 döner
        }

        // DELETE: api/product/{id} (Ürünü sil)
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound(); // Ürün bulunamazsa 404 döner
            }
            _products.Remove(product); // Ürünü listeden sil
            return NoContent(); // Başarılı silme için 204 döner
        }
    }
}