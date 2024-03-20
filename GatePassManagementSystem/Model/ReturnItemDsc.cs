using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GatePassManagementSystem.Model
{
    public class ReturnItemDsc
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReturnItemDscId { get; set; }
        public string ReturnableGPId { get; set; }
        public string ReGPId { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        
        public DateTime OutTime { get; set; }
        public DateTime InTime { get; set; }
        public DateTime SinIntime { get; set; }
        public DateTime SinOuttime { get; set; }
    }
}
