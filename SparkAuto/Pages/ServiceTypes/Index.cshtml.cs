using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SparkAuto.Data;
using SparkAuto.Utility;

namespace SparkAuto.Pages.ServiceTypes
{
    [Authorize(Roles = SD.AdminEndUser)]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ApplicationDbContext db, ILogger<IndexModel> logger)
        {
            _db = db;
            _logger = logger;
        }

        [BindProperty]
        public IList<ServiceType> ServiceType { get; set; }

        public async Task<IActionResult> OnGet()
        {
            ServiceType = await _db.ServiceTypes.ToListAsync();
            return Page();
        }
    }
}
