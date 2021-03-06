using System;
using System.Data.Entity;
using System.Linq;

namespace Mountains_Code_First
{
    public class MountainsContext : DbContext
    {
        
        public MountainsContext()
            : base("name=MountainsContext")
        {

        }

        public virtual DbSet<Country> Countries { get; set; }

        public virtual DbSet<Mountain> Mountains { get; set; }
        public virtual DbSet<Peak> Peaks { get; set; }

       
    }

    
}