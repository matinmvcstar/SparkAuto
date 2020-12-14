using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SparkAuto.Data;
using SparkAuto.Model;
using SparkAuto.Model.ViewModel;
using SparkAuto.Utility;

namespace SparkAuto.Users
{
    [Authorize(Roles = SD.AdminEndUser)]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public UserListViewModel UserListVM { get; set; }
        //بجای کد زیر بعد از اضافه کردن به userlistviewmodel از userlistviewmodel استفاده می کنیم
        //public List<ApplicationUser> ApplicationUserList { get; set; }

        public async Task<IActionResult> OnGet(int productPage = 1,
            string seachEmail = null,
            string searchName = null,
            string searchBirth = null)
        {
            UserListVM = new UserListViewModel()
            {
                ApplicationUserList = await _db.ApplicationUsers.ToListAsync()
            };
            StringBuilder param = new StringBuilder();
            param.Append("/Users?productPage=:");

            ///جستجو جدول
            param.Append("&searchEmail=");
            if(seachEmail != null)
            {
                param.Append(seachEmail);
            }
            param.Append("&searchName=");
            if (searchName != null)
            {
                param.Append(searchName);
            }
            param.Append("&searchBirth=");
            if (searchBirth != null)
            {
                param.Append(searchBirth);
            }

            if(seachEmail != null)
            {
                UserListVM.ApplicationUserList = await _db.ApplicationUsers.Where(p => p.Email.ToLower().Contains(seachEmail.ToLower())).ToListAsync();
            }
            else
            {
                if(searchName != null)
                {
                    UserListVM.ApplicationUserList = await _db.ApplicationUsers.Where(p => p.FirstName.ToLower().Contains(searchName.ToLower())).ToListAsync();
                }
                else
                {
                    if (searchName != null)
                    {
                        UserListVM.ApplicationUserList = await _db.ApplicationUsers.Where(p => p.LastName.ToLower().Contains(searchName.ToLower())).ToListAsync();
                    }
                    else
                    {
                        if (searchBirth != null)
                        {
                            UserListVM.ApplicationUserList = await _db.ApplicationUsers.Where(p => p.BirthDate.ToLower().Contains(searchBirth.ToLower())).ToListAsync();
                        }
                    }
                }
            }

            ////جستجو جدول
            
            var count = (double)UserListVM.ApplicationUserList.Count / SD.PageinationUsersPageSize;
            var countup = Math.Ceiling(count);

            UserListVM.PagingInfo = new PagingInfo
            {
                CurrentPage = productPage,
                ItemsPerPage = SD.PageinationUsersPageSize,
                TotalItems = (int)countup,
                UrlParam = param.ToString()
            };

            UserListVM.ApplicationUserList = UserListVM.ApplicationUserList.OrderBy(p => p.Email)
                .Skip((productPage - 1) * SD.PageinationUsersPageSize)
                .Take(SD.PageinationUsersPageSize).ToList();

            //ApplicationUserList = await _db.ApplicationUsers.ToListAsync();
            return Page();
        }
    }
}
