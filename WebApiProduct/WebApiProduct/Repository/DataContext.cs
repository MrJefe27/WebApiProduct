using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApiProduct.Models;

namespace WebApiProduct.Repository
{
    public class DataContext : DbContext
    {       
        public DataContext (DbContextOptions<DataContext> options) : base(options)
        {            
        } 

        public DbSet<Products> Products {get; set;}
    }
}