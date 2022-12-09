using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebStore.Data;
using WebStore.Models;

namespace WebStore.Pages.Products
{
    public class DetailsModel : PageModel
    {
        private readonly WebStore.Data.StoreContext _context;

        public DetailsModel(WebStore.Data.StoreContext context)
        {
            _context = context;
        }

      public Product Product { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            //var product = await _context.Products.FirstOrDefaultAsync(m => m.Id == id);
            //var product = await _context.Products.FindAsync(id);
            var product = await _context.Products
                            .FromSqlInterpolated($"select * from products where id = {id}")
                            .FirstOrDefaultAsync();

            if (product == null)
            {
                return NotFound();
            }
            else 
            {
                Product = product;
            }
            return Page();
        }
    }
}
