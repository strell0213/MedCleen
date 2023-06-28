using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace MaksDiplom
{
    internal class AppContext : DbContext
    {
        public DbSet<Pacient> Pacients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<RegistryPerson> RegistryPersons { get; set; }
        public DbSet<VisitList> VisitLists { get; set; }

        public AppContext() : base("DefaultConnection") { }
    }
}
