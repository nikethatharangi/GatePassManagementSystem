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
        public int Id { get; set; }

        [ForeignKey("ReturnableGP")]
        public string ReturnableGPId { get; set; }
        public ReturnableGP ReturnableGP { get; set; }

        public string Description { get; set; }
        public int Quantity { get; set; }
    }
}
