using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using FormularioDinamico.Domain;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace FormularioDinamico.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<SubCategoria> SubCategorias { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Categoria>().Property(e => e.Slug).HasMaxLength(60).IsRequired();
            modelBuilder.Entity<Categoria>().Property(e => e.Descricao).HasMaxLength(160).IsRequired();
            modelBuilder.Entity<SubCategoria>().Property(e => e.Slug).HasMaxLength(60).IsRequired();
            modelBuilder.Entity<SubCategoria>().Property(e => e.Descricao).HasMaxLength(160).IsRequired();
            modelBuilder.Entity<Campo>().Property(e => e.Descricao).HasMaxLength(60).IsRequired();
            modelBuilder.Entity<Item>().Property(e => e.Texto).HasMaxLength(60).IsRequired();

            base.OnModelCreating(modelBuilder);
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}