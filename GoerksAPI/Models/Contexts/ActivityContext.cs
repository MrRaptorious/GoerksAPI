using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoerksAPI.Models.Contexts
{
    public class ActivityContext : DbContext
    {
        public ActivityContext(DbContextOptions<ActivityContext> options)
            : base(options)
        {
        }
        public DbSet<CardioActivity> CardioActivities { get; set; }
        public DbSet<StrenghtActivitySet> StrenghtActivitySets { get; set; }
    }
}
