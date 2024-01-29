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
    public class CreateWorkerModel : PageModel
    {

        private readonly INotyfService _notify;
        private readonly Common cm;
        private readonly ApplicationDbContext _db;
        public List<Department> deplist { get; set; }
     
        [BindProperty]
        public Model.Workers Workers { get; set; }
        public Department Department { get; set; }
        public IEnumerable<Model.Department> DeptName { get; set; }
        public CreateWorkerModel(ApplicationDbContext db, INotyfService notyf)
        {
            _db = db;
            cm = new Common();
            _notify = notyf;
        }

        public void OnGet()
        {
            deplist = GetDropdownDataDept();
        }

        public List<Department> GetDropdownDataDept()
        {
            try
            {
                var dropdownData = _db.Department.ToList();
                dropdownData.Insert(0, new Department { Id = 0, DeptName = "Select" });
                return dropdownData;
            }
            catch (Exception ex)
            {
                cm.Logwrite("Error in CreateUserModel GetDropdownDataDept method :" + ex.Message);
                return new List<Department>();
            }
        }

        public async Task<IActionResult> OnPost()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (Workers.Name == null)
                    {
                        _notify.Error("Please Enter Full Name", 5);
                        deplist = GetDropdownDataDept();
                        return RedirectToPage("CreateWorker");
                    }
                    
                    else if (Workers.DeptId == 0)
                    {
                        _notify.Error("Please Select Department", 5);
                        deplist = GetDropdownDataDept();
                        return RedirectToPage("CreateWorker");
                    }
                    
                    else
                    {
                        await _db.Workers.AddAsync(Workers);
                        await _db.SaveChangesAsync();
                        _notify.Success("User Created Successfully", 3);
                        return RedirectToPage("CreateWorker");

                    }
                }

            }
            catch (Exception ex)
            {
                cm.Logwrite($"Error in CreateUserModel OnPost method: {ex.Message}\nInner Exception: {ex.InnerException?.Message}");
            }
            return RedirectToPage("CreateWorker");
        }
    }
}
