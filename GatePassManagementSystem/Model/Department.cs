using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GatePassManagementSystem.Model
{
    public class Department
    {
        [Key]
        public int Id { get; set; }
        public string DeptName { get; set; }
        public string Hod { get; set; }
        public string Dgm { get; set; }
        public string Gm { get; set; }
        public string Md { get; set; }
        public string TempAprvl { get; set; }

        public ICollection<PersonalGP> PersonalGP { get; set; }
        public ICollection<WorkerGP> WorkerGP { get; set; }
        public ICollection<User> User { get; set; }
        public ICollection<NonReturnableGP> NonReturnableGP { get; set; }
        public ICollection<ReturnableGP> ReturnableGP { get; set; }
    }
}
