using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GatePassManagementSystem.Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        [ForeignKey("Department")]
        public int DepartId { get; set; }
        public Department Department { get; set; }

        public string Designation { get; set; }
        public string EPFNumber { get; set; }
        public string RFIDNumber { get; set; }
        public string NICNumber { get; set; }

        [ForeignKey("UserRole")]
        public int RoleId { get; set; }
        public UserRole UserRole { get; set; }

        public string FullNameAndEPFNo => $"{FullName} - {EPFNumber}";
        public ICollection<PersonalGP> PersonalGP { get; set; }
    }
}
