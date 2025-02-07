using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GStarted.Modles;

namespace GStarted.Data
{
    public class GStartedContext : DbContext
    {
        public GStartedContext (DbContextOptions<GStartedContext> options)
            : base(options)
        {
        }

        public DbSet<GStarted.Modles.Movie> Movie { get; set; } = default!;
    }
}
