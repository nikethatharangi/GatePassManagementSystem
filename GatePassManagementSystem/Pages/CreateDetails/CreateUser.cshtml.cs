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
    public class CreateUserModel : PageModel
    {
        private readonly INotyfService _notify;
        private readonly Common cm;
        private readonly ApplicationDbContext _db;
        public List<Department> deplist { get; set; }
        public List<UserRole> rolelist { get; set; }
        [BindProperty]
        public Model.User User { get; set; }
        public Department Department { get; set; }
        public IEnumerable<Model.Department> DeptName { get; set; }
        public CreateUserModel(ApplicationDbContext db, INotyfService notyf)
        {
            _db = db;
            cm = new Common();
            _notify = notyf;
        }

        public void OnGet()
        {
            deplist = GetDropdownDataDept();
            rolelist = GetDropdownDataRole();
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

        public List<UserRole> GetDropdownDataRole()
        {
            try
            {
                var dropdownDatar = _db.UserRole.ToList();
                dropdownDatar.Insert(0, new UserRole { Id = 0, RoleName = "Select" });
                return dropdownDatar;
            }
            catch (Exception ex)
            {
                cm.Logwrite("Error in CreateUserModel GetDropdownDataRole method :" + ex.Message);
                return new List<UserRole>();
            }
        }

        public async Task<IActionResult> OnPost()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if(User.FullName == null)
                    {
                        _notify.Error("Please Enter Full Name", 5);
                        deplist = GetDropdownDataDept();
                        rolelist = GetDropdownDataRole();
                        return RedirectToPage("CreateUser");
                    }
                    else if(User.UserName == null)
                    {
                        _notify.Error("Please Enter Username", 5);
                        deplist = GetDropdownDataDept();
                        rolelist = GetDropdownDataRole();
                        return RedirectToPage("CreateUser");
                    }
                    else if(User.Password == null)
                    {
                        _notify.Error("Please Enter Password", 5);
                        deplist = GetDropdownDataDept();
                        rolelist = GetDropdownDataRole();
                        return RedirectToPage("CreateUser");
                    }
                    else if (User.DepartId == 0 )
                    {
                        _notify.Error("Please Select Department", 5);
                        deplist = GetDropdownDataDept();
                        rolelist = GetDropdownDataRole();
                        return RedirectToPage("CreateUser");
                    }
                    else if (User.RoleId == 0)
                    {
                        _notify.Error("Please Select Role", 5);
                        deplist = GetDropdownDataDept();
                        rolelist = GetDropdownDataRole();
                        return RedirectToPage("CreateUser");
                    }
                    else
                    {
                        await _db.User.AddAsync(User);
                        await _db.SaveChangesAsync();
                        _notify.Success("User Created Successfully", 3);
                        return RedirectToPage("CreateUser");

                    }
                }
                
            }
            catch (Exception ex)
            {
                cm.Logwrite($"Error in CreateUserModel OnPost method: {ex.Message}\nInner Exception: {ex.InnerException?.Message}");
            }
            return RedirectToPage("CreateUser");
        }
    }
}
