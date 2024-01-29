using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using GatePassManagementSystem.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GatePassManagementSystem.Pages
{
    public class LoginModel : PageModel
    {
        private readonly INotyfService _notify;
        private readonly Common cm;
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public Model.User Users { get; set; }
        public Department Department { get; set; }
        public IEnumerable<Model.Department> DeptName { get; set; }
        public LoginModel(ApplicationDbContext db, INotyfService notyf)
        {
            _db = db;
            _notify = notyf;
            cm = new Common();
        }

        
        public void OnGet()
        {
            
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                
                var user = await _db.User
                        .Where(u => u.UserName == Users.UserName)
                        .FirstOrDefaultAsync();
                

                if (user != null && user.Password == Users.Password)
                {

                    HttpContext.Session.SetString("Username", user.UserName);

                    HttpContext.Session.SetString("UserId", user.Id.ToString());
                    HttpContext.Session.SetString("FullName", user.FullName);
                    HttpContext.Session.SetString("EPFNo", user.EPFNumber);
                    HttpContext.Session.SetString("Roleid", user.RoleId.ToString());
                    HttpContext.Session.SetString("DepartId", user.DepartId.ToString());
                    HttpContext.Session.SetString("Email", user.Email);

                    var dept = _db.Department.Where(u => u.Id == user.DepartId).Select(u => u.DeptName).FirstOrDefault();
                    HttpContext.Session.SetString("DepartName", dept.ToString());

                    return RedirectToPage("Index");
                }
                else if(user == null)
                {
                    TempData["lblerror"] = "Username is wrong!!!";
                }
                else if((user != null && user.Password != Users.Password))
                {
                    TempData["lblerror"] = "Password is wrong!!!";
                }
            }
            catch (Exception ex)
            {
                cm.Logwrite("Error in LoginModel OnPostAsync method :" + ex.Message);
            }

            return Page();
        }
    }
}

