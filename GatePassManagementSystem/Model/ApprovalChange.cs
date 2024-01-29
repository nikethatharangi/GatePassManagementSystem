using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GatePassManagementSystem.Model
{
    public class ApprovalChange
    {
        [Key]
        public int ChId { get; set; }
        public string FullName { get; set; }
        public int deptId { get; set; }

       
    }
}
