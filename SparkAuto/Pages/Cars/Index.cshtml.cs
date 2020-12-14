using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SparkAuto.Data;
using SparkAuto.Model;
using SparkAuto.Model.ViewModel;

namespace SparkAuto.Pages.Cars
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public CarAndCustomerVM CarAndCustVM { get;set; }

        [TempData]
        public string statusMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(string userId = null)
        {
            if (userId == null)
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                userId = claim.Value;
            }

            CarAndCustVM = new CarAndCustomerVM()
            {
                Cars = await _db.Cars.Where(c => c.UserId == userId).ToListAsync(),
                UserObj = await _db.ApplicationUsers.FirstOrDefaultAsync(u => u.Id == userId)
            };

            return Page();
        }
    }
}
