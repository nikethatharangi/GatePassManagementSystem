using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GatePassManagementSystem.Model
{
    public class ReturnableGP
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Key]
        [Required]
        public string ReturnableGPId { get; set; }
        public string Description { get; set; }
        public string Reason { get; set; }

        [ForeignKey("Department")]
        public int DepId { get; set; }
        public Department Department { get; set; }

        public string AShod { get; set; }
        public string ASdgm { get; set; }
        public string ASgm { get; set; }
        public string ASmd { get; set; }
        public string ASacc { get; set; }
        public string ASguard { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        public bool ChkifDeptHeadUn { get; set; }
        public string Barcode { get; set; }
        public bool ChkSinthawatta { get; set; }
        public bool ChkMadu { get; set; }

        public DateTime OutTime { get; set; }
        public DateTime InTime { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime AShodtime { get; set; }
        public DateTime ASdgmtime { get; set; }
        public DateTime ASgmtime { get; set; }
        public DateTime ASmdtime { get; set; }
        public DateTime ASacctime { get; set; }
        public DateTime SinIntime { get; set; }
        public DateTime SinOuttime { get; set; }
        public string RejctReason { get; set; }
        public string CreateUser { get; set; }

        public int ChApprvlId { get; set; }
    }
}
