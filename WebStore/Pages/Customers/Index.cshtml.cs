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
    public class IndexModel : PageModel
    {
        private readonly WebStore.Data.StoreContext _context;

        public IndexModel(WebStore.Data.StoreContext context)
        {
            _context = context;
        }

        public IList<Customer> Customer { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Customers != null)
            {
                //Customer = await _context.Customers.ToListAsync();
                Customer = await _context.Customers
                            .Include(c => c.Orders)
                            .AsNoTracking()
                            .ToListAsync();
            }
        }
    }
}
