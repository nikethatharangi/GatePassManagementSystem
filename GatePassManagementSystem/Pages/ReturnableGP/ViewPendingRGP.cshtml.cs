using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GatePassManagementSystem.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GatePassManagementSystem.Pages.ReturnableGP
{
    public class ViewPendingRGPModel : PageModel
    {
        public readonly ApplicationDbContext _db;
        public readonly Common cm;

        public ViewPendingRGPModel(ApplicationDbContext db)
        {
            _db = db;
            cm = new Common();
        }

        public IEnumerable<Model.PersonalGP> PersonalGPs;
        public ApplicationDbContext Db => _db;

        public int deptId { get; set; }
        public string Fullname { get; set; }
        public string DeptHead { get; set; }
        public string deptName { get; set; }
        public string DeptGm { get; set; }

        public void OnGet()
        {
        }
    }
}
