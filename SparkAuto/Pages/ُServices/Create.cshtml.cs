using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SparkAuto.Data;
using SparkAuto.Model.ViewModel;
using SparkAuto.Utility;

namespace SparkAuto.Pages._ُServices
{
    [Authorize(Roles = SD.AdminEndUser)]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public CarServiceVM CarServiceVM { get; set; }

        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> OnGet(int carId)
        {
            CarServiceVM = new CarServiceVM
            {
                Car = await _db.Cars.Include(c => c.ApplicationUser).FirstOrDefaultAsync(c => c.Id == carId),
                ServiceHeader = new Model.ServiceHeader()
            };

            List<String> stServiceTypeInShopingCart = _db.serviceShoppingCarts
                .Include(c => c.ServiceType)
                .Where(c => c.CardId == carId)
                .Select(c => c.ServiceType.Name)
                .ToList();

            IQueryable<ServiceType> stService = from s in _db.ServiceTypes
                                                where !(stServiceTypeInShopingCart.Contains(s.Name))
                                                select s;

            CarServiceVM.ServiceShoppingCart = _db.serviceShoppingCarts.Include(c => c.ServiceType).Where(c => c.CardId == carId).ToList();
            CarServiceVM.ServiceHeader.TotalPrice = 0;

            foreach(var item in CarServiceVM.ServiceShoppingCart)
            {
                CarServiceVM.ServiceHeader.TotalPrice += item.ServiceType.Price;
            }

            return Page();
        }

        [BindProperty]
        public ServiceType ServiceType { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _db.ServiceTypes.Add(ServiceType);
            await _db.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
