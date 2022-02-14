using Microsoft.EntityFrameworkCore;

namespace SqlChangesTest.Schema;

public class TheContext : DbContext
{
    #region Public Constructors

    public TheContext(DbContextOptions<TheContext> options) : base(options)
    {
    }

    #endregion Public Constructors

    #region Public Properties

    public DbSet<AnotherTable> AnotherTable => Set<AnotherTable>();
    public DbSet<MyTable> MyTables => Set<MyTable>();

    #endregion Public Properties

    #region Protected Methods

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MyTable>()
            .Property(b => b.Name)
            .IsRequired()
            .HasMaxLength(50);
    }

    #endregion Protected Methods
}