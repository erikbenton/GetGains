using GetGains.Core.Models.Exercises;
using GetGains.Core.Models.Instructions;
using GetGains.Core.Models.Workouts;
using Microsoft.EntityFrameworkCore;

namespace GetGains.Data.Services;

public class GainsDbContext : DbContext
{
    public GainsDbContext(DbContextOptions<GainsDbContext> options)
        : base(options)
    {

    }

    public DbSet<Workout> Workouts => Set<Workout>();

    public DbSet<Exercise> Exercises => Set<Exercise>();

    public DbSet<Instruction> Instructions => Set<Instruction>();
}
