using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoerksAPI.Models.Contexts
{
    public class ActivityCatalogContext : DbContext
    {
        public ActivityCatalogContext(DbContextOptions<ActivityCatalogContext> options)
     : base(options)
        {
        }

        public DbSet<ActivityCatalog> ActivityTypes { get; set; }
    }
}
