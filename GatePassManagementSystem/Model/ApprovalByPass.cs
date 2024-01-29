using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GatePassManagementSystem.Model
{
    public class ApprovalByPass
    {
        [Key]
        public int Id { get; set; }
        
        public DateTime FromDate { get; set; }
        public DateTime FromTime { get; set; }
        public DateTime ToDate { get; set; }
        public DateTime ToTime { get; set; }
        public bool ChkSuga { get; set; }
        public bool ChkDhar { get; set; }
        public bool ChkThus { get; set; }
        public bool ChkRuwa { get; set; }
        public bool ChkRoha { get; set; }
        public bool ChkDami { get; set; }
    }
}
