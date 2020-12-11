using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SparkAuto.Data;

namespace SparkAuto.Pages.ServiceTypes
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<CreateModel> _logger;

        public CreateModel(ApplicationDbContext db, ILogger<CreateModel> logger)
        {
            _db = db;
            _logger = logger;
        }

        [BindProperty]
        public ServiceType ServiceType { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(ServiceType serviceType)
        {
            if(ModelState.IsValid)
            {
                _db.ServiceTypes.Add(serviceType);
                await _db.SaveChangesAsync();

                return RedirectToPage("Index");
            }

            return Page();
        }
    }
}
