using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CSTest._21_Database._02_EntityFramework._01_DatabaseFirst.Models;

public partial class ShopDbContext : DbContext
{
    public ShopDbContext()
    {
    }

    public ShopDbContext(DbContextOptions<ShopDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Office> Offices { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Salesrep> Salesreps { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=localhost,1436;Initial Catalog=ShopDB;User ID=sa;Password=Password1;Encrypt=False")
        .LogTo(Console.WriteLine,
                    new[] { DbLoggerCategory.Database.Command.Name },
                    LogLevel.Information);

    public IQueryable<Customer> GetCustomerInfoById(int id)
                        => FromExpression(() => GetCustomerInfoById(id));

    public string? GetCustomerCompanyById(int id)
        => throw new NotSupportedException();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustNum).HasName("PK__CUSTOMER__6A01161A87C6B609");

            entity.ToTable("CUSTOMERS");

            entity.Property(e => e.CustNum)
                .ValueGeneratedNever()
                .HasColumnName("CUST_NUM");
            entity.Property(e => e.Company)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("COMPANY");
            entity.Property(e => e.CreditLimit)
                .HasColumnType("money")
                .HasColumnName("CREDIT_LIMIT");
            entity.Property(e => e.CustRep).HasColumnName("CUST_REP");

            entity.HasOne(d => d.CustRepNavigation).WithMany(p => p.Customers)
                .HasForeignKey(d => d.CustRep)
                .HasPrincipalKey(d => d.EmplNum)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("HASREP");
        });

        modelBuilder.Entity<Office>(entity =>
        {
            entity.HasKey(e => e.Office1).HasName("PK__OFFICES__6A36566EE2672D25");

            entity.ToTable("OFFICES");

            entity.Property(e => e.Office1)
                .ValueGeneratedNever()
                .HasColumnName("OFFICE");
            entity.Property(e => e.City)
                .IsRequired()
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("CITY");
            entity.Property(e => e.Mgr).HasColumnName("MGR");
            entity.Property(e => e.Region)
                .IsRequired()
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("REGION");
            entity.Property(e => e.Sales)
                .HasColumnType("money")
                .HasColumnName("SALES");
            entity.Property(e => e.Target)
                .HasColumnType("money")
                .HasColumnName("TARGET");

            entity.HasOne(d => d.MgrNavigation).WithMany(p => p.Offices)
                .HasForeignKey(d => d.Mgr)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("HASMGR");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderNum).HasName("PK__ORDERS__6C473C95BCF31E84");

            entity.ToTable("ORDERS");

            entity.Property(e => e.OrderNum)
                .ValueGeneratedNever()
                .HasColumnName("ORDER_NUM");
            entity.Property(e => e.Amount)
                .HasColumnType("money")
                .HasColumnName("AMOUNT");
            entity.Property(e => e.Cust).HasColumnName("CUST");
            entity.Property(e => e.Mfr)
                .IsRequired()
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MFR");
            entity.Property(e => e.OrderDate)
                .HasColumnType("datetime")
                .HasColumnName("ORDER_DATE");
            entity.Property(e => e.Product)
                .IsRequired()
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("PRODUCT");
            entity.Property(e => e.Qty).HasColumnName("QTY");
            entity.Property(e => e.Rep).HasColumnName("REP");

            entity.HasOne(d => d.CustNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.Cust)
                .HasConstraintName("PLACEDBY");

            entity.HasOne(d => d.RepNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.Rep)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("TAKENBY");

            entity.HasOne(d => d.ProductNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => new { d.Mfr, d.Product })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ISFOR");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => new { e.MfrId, e.ProductId }).HasName("PK__PRODUCTS__59EE64633ABAAE52");

            entity.ToTable("PRODUCTS");

            entity.Property(e => e.MfrId)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MFR_ID");
            entity.Property(e => e.ProductId)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("PRODUCT_ID");
            entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("DESCRIPTION");
            entity.Property(e => e.Price)
                .HasColumnType("money")
                .HasColumnName("PRICE");
            entity.Property(e => e.QtyOnHand).HasColumnName("QTY_ON_HAND");
        });

        modelBuilder.Entity<Salesrep>(entity =>
        {
            entity.HasKey(e => e.EmplNum).HasName("PK__SALESREP__1D72C15016575292");

            entity.ToTable("SALESREPS");

            entity.Property(e => e.EmplNum)
                .ValueGeneratedNever()
                .HasColumnName("EMPL_NUM");
            entity.Property(e => e.Age).HasColumnName("AGE");
            entity.Property(e => e.HireDate)
                .HasColumnType("datetime")
                .HasColumnName("HIRE_DATE");
            entity.Property(e => e.Manager).HasColumnName("MANAGER");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("NAME");
            entity.Property(e => e.Quota)
                .HasColumnType("money")
                .HasColumnName("QUOTA");
            entity.Property(e => e.RepOffice).HasColumnName("REP_OFFICE");
            entity.Property(e => e.Sales)
                .HasColumnType("money")
                .HasColumnName("SALES");
            entity.Property(e => e.Title)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("TITLE");

            entity.HasOne(d => d.ManagerNavigation).WithMany(p => p.InverseManagerNavigation)
                .HasForeignKey(d => d.Manager)
                .HasConstraintName("FK__SALESREPS__MANAG__534D60F1");

            entity.HasOne(d => d.RepOfficeNavigation).WithMany(p => p.Salesreps)
                .HasForeignKey(d => d.RepOffice)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("WORKSIN");
        });

         modelBuilder
            .HasDbFunction(typeof(ShopDbContext)
            .GetMethod(nameof(GetCustomerInfoById), new[] { typeof(int) }))
            .HasName("sfCustomerInfoById")
            .HasSchema("dbo");

        modelBuilder
          .HasDbFunction(typeof(ShopDbContext)
          .GetMethod(nameof(GetCustomerCompanyById), new[] { typeof(int) })!)
          .HasName("sfCustomerCompanyById")
          .HasSchema("dbo");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
