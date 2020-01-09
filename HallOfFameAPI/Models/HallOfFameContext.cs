using Microsoft.EntityFrameworkCore;

namespace HallOfFameAPI.Models
{
    public class HallOfFameContext: DbContext
    {
        public HallOfFameContext(DbContextOptions<HallOfFameContext> options)
            : base(options)
        {
        }

        public DbSet<Person> Person { get; set; }
        public DbSet<Skill> Skill { get; set; }
    }
}
