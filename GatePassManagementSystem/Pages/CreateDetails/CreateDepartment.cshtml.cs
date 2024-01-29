using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using GatePassManagementSystem.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GatePassManagementSystem.Pages.CreateDetails
{
    public class CreateDepartmentModel : PageModel
    {
        private readonly INotyfService _notify;
        private readonly Common cm;
        private readonly ApplicationDbContext _db;
    
        [BindProperty]
        public Department Department { get; set; }
        public CreateDepartmentModel(ApplicationDbContext db, INotyfService notyf)
        {
            _db = db;
            cm = new Common();
            _notify = notyf;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (Department.DeptName == null)
                    {
                        _notify.Error("Please Insert Department Name", 5);
                    }
                    else
                    {
                        await _db.Department.AddAsync(Department);
                        await _db.SaveChangesAsync();
                        _notify.Success("Department Created Successfully", 3);
                        return RedirectToPage("CreateDepartment");
                    }
                }
                
            }
            catch (Exception ex)
            {
                cm.Logwrite($"Error in CreateDepartmentModel OnPost method: {ex.Message}\nInner Exception: {ex.InnerException?.Message}");
            }
            return Page();
        }
    }
}
