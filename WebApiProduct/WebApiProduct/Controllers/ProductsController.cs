using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using WebApiProduct.Models;
using WebApiProduct.Repository;

namespace WebApiProduct.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly DataContext _context;
        
        public ProductsController(ILogger<ProductsController> logger, DataContext context){
            _logger = logger;
            _context = context;
        }

        [HttpGet (Name = "GetProducts")]
        public async Task<ActionResult<IEnumerable<Products>>> GetProducts(){
            var products = await _context.Products.ToListAsync();

            if(products.Count < 0)
                return NotFound();

            return products;
        }

        [HttpGet ("{id}",Name = "GetProductById")]
        public async Task<ActionResult<Products>> GetProductById(int id){         
            var result = await _context.Products.FindAsync(id);

            if(result == null)
                return NotFound();  

            return result;
        }

        [HttpPost]
        public async Task<ActionResult<Products>> Create(Products products){
            _context.Products.Add(products);
            await _context.SaveChangesAsync();

            return new CreatedAtRouteResult("GetProducts", new {id = products.Id}, products);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Products>> Update(int id, Products products){
            if (id != products.Id)
                return BadRequest();

            _context.Entry(products).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult<Products>> Delete(int id){
            var product = await _context.Products.FindAsync(id);

            if (product == null)
                return NotFound();

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return product;
        }
    }
}