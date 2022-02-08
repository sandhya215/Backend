using Microsoft.EntityFrameworkCore;
using KanbanBoard.Models;

namespace KanbanBoard.Contexts
{
    public class ApplicationDbContext : DbContext
    {


        public ApplicationDbContext()
        {

        }
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        {
           optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=KanbanBoardDB;Username=postgres;Password=Nalgonda@123");
         }
        public virtual DbSet<Login> Login { get; set; }

        public virtual DbSet<Project> Project { get; set; }

        public virtual DbSet<Signup> Signup { get; set; }

        public virtual DbSet<Tasks> Tasks { get; set; }

        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<TeamMembers> TeamMembers { get; set; }

    }
}