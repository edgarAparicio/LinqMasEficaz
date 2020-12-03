using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqEntityFramework
{
    public class DbContextLinqEntityFramework : DbContext
    {
        public DbSet<Carro> Carros { get; set; }
    }
}
