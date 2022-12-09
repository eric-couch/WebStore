using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebStore.Data;
using WebStore.Models;

namespace WebStore.Pages.Customers
{
    public class DetailsModel : PageModel
    {
        private readonly WebStore.Data.StoreContext _context;

        public DetailsModel(WebStore.Data.StoreContext context)
        {
            _context = context;
        }

      public Customer Customer { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Customers == null)
            {
                return NotFound();
            }

            //var customer = await _context.Customers.FirstOrDefaultAsync(m => m.Id == id);
            Customer = await _context.Customers
                .Include(c => c.Orders)
                .ThenInclude(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .AsSplitQuery()
                .FirstOrDefaultAsync(c => c.Id == id);

            var FirstAndLastName = (from c in _context.Customers
                                    where c.Id == id
                                    select new { c.FirstName, c.LastName }).FirstOrDefault();
            

            if (Customer == null)
            {
                return NotFound();
            }
            //else
            //{
            //    Customer = customer;
            //}
            return Page();
        }
    }
}
