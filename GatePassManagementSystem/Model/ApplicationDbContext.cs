using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GatePassManagementSystem.Model
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        
        public DbSet<PersonalGP> PersonalGP { get; set; }
        public DbSet<WorkerGP> WorkerGP { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<Workers> Workers { get; set; }
        public DbSet<ApprovalChange> ApprovalChange { get; set; }

        public DbSet<NonReturnableGP> NonReturnableGP { get; set; }
        public DbSet<NonReturnItemDsc> NonReturnItemDsc { get; set; }
        //public DbSet<ReturnableGP> ReturnableGP { get; set; }
        //public DbSet<ReturnItemDsc> ReturnItemDsc { get; set; }
    }
}
