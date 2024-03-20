using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GatePassManagementSystem.Model
{
    public class NonReturnItemDsc
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("NonReturnItemDscId")]
        public int NonReturnItemDscId { get; set; }

        public string Description { get; set; }
        public int Quantity { get; set; }
        public string NonGPId { get; set; }

        public List<NonReturnItemDsc> NonReturnItemDscsl { get; set; }

    }
}
