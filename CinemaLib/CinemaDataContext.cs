using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaLib
{
    class CinemaDataContext : DbContext
    {
        public CinemaDataContext() 
            : base("CinemaDB") 
        { }

        public DbSet<Chairs> Chairs { get; set; }
        public DbSet<Films> Films { get; set; }
        public DbSet<Sessions> Sessions { get; set; }
        public DbSet<Tickets> Tickets { get; set; }
    }
}
