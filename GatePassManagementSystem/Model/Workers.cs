using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GatePassManagementSystem.Model
{
    public class Workers
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("Department")]
        public int DeptId { get; set; }
        public Department Department { get; set; }
        public string Name { get; set; }
        public string NIC { get; set; }
        public string EPFNo { get; set; }
        public string Designation { get; set; }
        public string RFIDNumber { get; set; }
        public string FullNameAndEPFNo => $"{Name} - {EPFNo}";
        public ICollection<WorkerGP> WorkerGP { get; set; }
    }
}
