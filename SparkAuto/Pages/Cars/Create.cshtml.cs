using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SparkAuto.Data;
using SparkAuto.Model;
using SparkAuto.Utility;

namespace SparkAuto.Pages.Cars
{
    [Authorize(Roles = SD.AdminEndUser)]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Car Car { get; set; }

        public IActionResult OnGet(string userId = null)
        {
            Car = new Car();
            //ViewData["UserId"] = new SelectList(_db.ApplicationUsers, "Id", "Id");
            if (userId == null)
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                userId = claim.Value;
            }

            Car.UserId = userId;

            return Page();
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _db.Cars.Add(Car);
            await _db.SaveChangesAsync();

            return RedirectToPage("./Index", new { userId = Car.UserId });
        }
    }
}
