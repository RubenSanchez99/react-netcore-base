using Microsoft.EntityFrameworkCore;

namespace ReactNetCoreBase.Auth
{
    public class ExtraAuthorizeDbContext : DbContext
    {
        public DbSet<RoleToPermissions> RolesToPermissions { get; set; }

        public ExtraAuthorizeDbContext(DbContextOptions<ExtraAuthorizeDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RoleToPermissions>().Property<string>("_permissionsInRole")
                .IsUnicode(false).HasColumnName("PermissionsInRole").IsRequired();
        }
    }
}