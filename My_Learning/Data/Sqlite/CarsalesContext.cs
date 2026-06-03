/// <summary>
/// This class and associated entities were generated using the Entity Framework Core scaffolding tool, based on an existing SQLite database.
/// 
/// The command to run the tool was:
/// dotnet ef dbcontext scaffold "Data Source=<fullpath>\Car_Database.db" Microsoft.EntityFrameworkCore.Sqlite --no-pluralize -c CarsalesContext --namespace MyLearning.Data.Sqlite.Carsales --context-dir "$(pwd)\Sqlite"  --output-dir "$(pwd)\Sqlite\Carsales"
/// 
/// The context class' namespace is then changed to MyLearning.Data.Sqlite.
/// </summary>
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;   
using MyLearning.Data.Sqlite.Carsales;
using static MyLearning.Data.Sqlite.SqliteContextHelper;

namespace MyLearning.Data.Sqlite;

public partial class CarsalesContext : DbContext
{
    private readonly string connectionString;

    public CarsalesContext(IConfiguration configuration)
    {
        connectionString = configuration.GetSqliteConnection("CarsalesConnection") ?? throw new InvalidOperationException("Connection string not found.");
    }

    public CarsalesContext(DbContextOptions<CarsalesContext> options, IConfiguration configuration)
        : base(options)
    {
        connectionString = configuration.GetSqliteConnection("CarsalesConnection") ?? throw new InvalidOperationException("Connection string not found.");
    }

    public virtual DbSet<Brands> Brands { get; set; }

    public virtual DbSet<CarOptions> CarOptions { get; set; }

    public virtual DbSet<CarParts> CarParts { get; set; }

    public virtual DbSet<CarVins> CarVins { get; set; }

    public virtual DbSet<CustomerOwnership> CustomerOwnership { get; set; }

    public virtual DbSet<Customers> Customers { get; set; }

    public virtual DbSet<Dealers> Dealers { get; set; }

    public virtual DbSet<ManufacturePlant> ManufacturePlant { get; set; }

    public virtual DbSet<Models> Models { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlite(connectionString);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Brands>(entity =>
        {
            entity.HasKey(e => e.BrandId);

            entity.Property(e => e.BrandId).HasColumnName("brand_id");
            entity.Property(e => e.BrandName)
                .HasColumnType(GetTextDbType(50))
                .HasColumnName("brand_name");
        });

        modelBuilder.Entity<CarOptions>(entity =>
        {
            entity.HasKey(e => e.OptionSetId);

            entity.ToTable("Car_Options");

            entity.Property(e => e.OptionSetId).HasColumnName("option_set_id");
            entity.Property(e => e.ChassisId).HasColumnName("chassis_id");
            entity.Property(e => e.Color)
                .HasColumnType(GetTextDbType(30))
                .HasColumnName("color");
            entity.Property(e => e.EngineId).HasColumnName("engine_id");
            entity.Property(e => e.ModelId).HasColumnName("model_id");
            entity.Property(e => e.OptionSetPrice).HasColumnName("option_set_price");
            entity.Property(e => e.PremiumSoundId).HasColumnName("premium_sound_id");
            entity.Property(e => e.TransmissionId).HasColumnName("transmission_id");

            entity.HasOne(d => d.Chassis).WithMany(p => p.CarOptionsChassis)
                .HasForeignKey(d => d.ChassisId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Engine).WithMany(p => p.CarOptionsEngine)
                .HasForeignKey(d => d.EngineId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Model).WithMany(p => p.CarOptions).HasForeignKey(d => d.ModelId);

            entity.HasOne(d => d.PremiumSound).WithMany(p => p.CarOptionsPremiumSound).HasForeignKey(d => d.PremiumSoundId);

            entity.HasOne(d => d.Transmission).WithMany(p => p.CarOptionsTransmission)
                .HasForeignKey(d => d.TransmissionId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<CarParts>(entity =>
        {
            entity.HasKey(e => e.PartId);

            entity.ToTable("Car_Parts");

            entity.Property(e => e.PartId).HasColumnName("part_id");
            entity.Property(e => e.ManufactureEndDate)
                .HasColumnType("DATE")
                .HasColumnName("manufacture_end_date");
            entity.Property(e => e.ManufacturePlantId).HasColumnName("manufacture_plant_id");
            entity.Property(e => e.ManufactureStartDate)
                .HasColumnType("DATE")
                .HasColumnName("manufacture_start_date");
            entity.Property(e => e.PartName)
                .HasColumnType(GetTextDbType(100))
                .HasColumnName("part_name");
            entity.Property(e => e.PartRecall)
                .HasDefaultValue(0)
                .HasColumnName("part_recall");

            entity.HasOne(d => d.ManufacturePlant).WithMany(p => p.CarParts)
                .HasForeignKey(d => d.ManufacturePlantId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<CarVins>(entity =>
        {
            entity.HasKey(e => e.Vin);

            entity.ToTable("Car_Vins");

            entity.Property(e => e.Vin).HasColumnName("vin");
            entity.Property(e => e.ManufacturedDate)
                .HasColumnType("DATE")
                .HasColumnName("manufactured_date");
            entity.Property(e => e.ManufacturedPlantId).HasColumnName("manufactured_plant_id");
            entity.Property(e => e.ModelId).HasColumnName("model_id");
            entity.Property(e => e.OptionSetId).HasColumnName("option_set_id");

            entity.HasOne(d => d.ManufacturedPlant).WithMany(p => p.CarVins)
                .HasForeignKey(d => d.ManufacturedPlantId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Model).WithMany(p => p.CarVins)
                .HasForeignKey(d => d.ModelId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.OptionSet).WithMany(p => p.CarVins)
                .HasForeignKey(d => d.OptionSetId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<CustomerOwnership>(entity =>
        {
            entity.HasKey(e => new { e.CustomerId, e.Vin });

            entity.ToTable("Customer_Ownership");

            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.Vin).HasColumnName("vin");
            entity.Property(e => e.DealerId).HasColumnName("dealer_id");
            entity.Property(e => e.PurchaseDate)
                .HasColumnType("DATE")
                .HasColumnName("purchase_date");
            entity.Property(e => e.PurchasePrice).HasColumnName("purchase_price");
            entity.Property(e => e.WaranteeExpireDate)
                .HasColumnType("DATE")
                .HasColumnName("warantee_expire_date");

            entity.HasOne(d => d.Customer).WithMany(p => p.CustomerOwnership)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Dealer).WithMany(p => p.CustomerOwnership)
                .HasForeignKey(d => d.DealerId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.VinNavigation).WithMany(p => p.CustomerOwnership)
                .HasForeignKey(d => d.Vin)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Customers>(entity =>
        {
            entity.HasKey(e => e.CustomerId);

            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.Birthdate)
                .HasColumnType("DATE")
                .HasColumnName("birthdate");
            entity.Property(e => e.Email)
                .HasColumnType(GetTextDbType(128))
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasColumnType(GetTextDbType(50))
                .HasColumnName("first_name");
            entity.Property(e => e.Gender)
                .HasColumnType("STRING")
                .HasColumnName("gender");
            entity.Property(e => e.HouseholdIncome).HasColumnName("household_income");
            entity.Property(e => e.LastName)
                .HasColumnType(GetTextDbType(50))
                .HasColumnName("last_name");
            entity.Property(e => e.PhoneNumber).HasColumnName("phone_number");
        });

        modelBuilder.Entity<Dealers>(entity =>
        {
            entity.HasKey(e => e.DealerId);

            entity.Property(e => e.DealerId).HasColumnName("dealer_id");
            entity.Property(e => e.DealerAddress)
                .HasColumnType(GetTextDbType(100))
                .HasColumnName("dealer_address");
            entity.Property(e => e.DealerName)
                .HasColumnType(GetTextDbType(50))
                .HasColumnName("dealer_name");

            entity.HasMany(d => d.Brand).WithMany(p => p.Dealer)
                .UsingEntity<Dictionary<string, object>>(
                    "DealerBrand",
                    r => r.HasOne<Brands>().WithMany()
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    l => l.HasOne<Dealers>().WithMany()
                        .HasForeignKey("DealerId")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    j =>
                    {
                        j.HasKey("DealerId", "BrandId");
                        j.ToTable("Dealer_Brand");
                        j.IndexerProperty<int>("DealerId").HasColumnName("dealer_id");
                        j.IndexerProperty<int>("BrandId").HasColumnName("brand_id");
                    });
        });

        modelBuilder.Entity<ManufacturePlant>(entity =>
        {
            entity.ToTable("Manufacture_Plant");

            entity.Property(e => e.ManufacturePlantId).HasColumnName("manufacture_plant_id");
            entity.Property(e => e.CompanyOwned).HasColumnName("company_owned");
            entity.Property(e => e.PlantLocation)
                .HasColumnType(GetTextDbType(100))
                .HasColumnName("plant_location");
            entity.Property(e => e.PlantName)
                .HasColumnType(GetTextDbType(50))
                .HasColumnName("plant_name");
            entity.Property(e => e.PlantType)
                .HasColumnType(GetTextDbType(7))
                .HasColumnName("plant_type");
        });

        modelBuilder.Entity<Models>(entity =>
        {
            entity.HasKey(e => e.ModelId);

            entity.Property(e => e.ModelId).HasColumnName("model_id");
            entity.Property(e => e.BrandId).HasColumnName("brand_id");
            entity.Property(e => e.ModelBasePrice).HasColumnName("model_base_price");
            entity.Property(e => e.ModelName)
                .HasColumnType(GetTextDbType(50))
                .HasColumnName("model_name");

            entity.HasOne(d => d.Brand).WithMany(p => p.Models)
                .HasForeignKey(d => d.BrandId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
