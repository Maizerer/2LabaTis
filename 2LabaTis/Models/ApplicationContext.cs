using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace _2LabaTis.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Car> Cars { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) :
        base(options)
        {
            Database.EnsureCreated();
        }
    }

}
