using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GstartedMvc.Models;

namespace GstartedMvc.Data
{
    public class GstartedMvcContext : DbContext
    {
        public GstartedMvcContext (DbContextOptions<GstartedMvcContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movie { get; set; } = default!;
    }
}
