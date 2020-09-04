using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoerksAPI.Models.Contexts
{
    public class WorkoutContext : DbContext
    {
        public WorkoutContext(DbContextOptions<WorkoutContext> options)
             : base(options)
        {
        }

        public DbSet<Workout> Workouts { get; set; }
    }
}
