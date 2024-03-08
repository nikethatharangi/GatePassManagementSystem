using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GatePassManagementSystem.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GatePassManagementSystem.Pages.NonreturnableGP
{
    public class testModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        private readonly Common cm;
        public testModel(ApplicationDbContext db)
        {
            cm = new Common();
            _db = db;
        }

        public void OnGet()
        {

        }

        public IActionResult OnPost(List<NonReturnItemDsc> items)
        {
            try
            {
                foreach (var item in items)
                {
                    //item.NonReturnableGPId = "NP000031";
                    _db.NonReturnItemDsc.Add(item);
                }

                _db.SaveChanges();
                return new JsonResult("Data saved successfully");
            }
            catch (Exception ex)
            {
                cm.Logwrite($"Error in CreateNGPModel OnPost method : {ex.Message}\nInner Exception: {ex.InnerException?.Message}");
                return new JsonResult($"Error saving data: {ex.Message}");
            }
        }

    }
}
