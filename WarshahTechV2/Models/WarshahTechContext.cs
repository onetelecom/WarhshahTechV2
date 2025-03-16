 using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WarshahTechV2.Models
{
    public partial class WarshahTechContext : DbContext
    {
        public WarshahTechContext()
        {
        }

        public WarshahTechContext(DbContextOptions<WarshahTechContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AccountStatus> AccountStatuses { get; set; }
        public virtual DbSet<Attachment> Attachments { get; set; }
        public virtual DbSet<AttachmentType> AttachmentTypes { get; set; }
        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<Catch> Catches { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Country1> Countries1 { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<DamageMarker> DamageMarkers { get; set; }
        public virtual DbSet<Dashboard> Dashboards { get; set; }
        public virtual DbSet<DashboardTbl> DashboardTbls { get; set; }
        public virtual DbSet<ExamValue> ExamValues { get; set; }
        public virtual DbSet<ExaminationItem> ExaminationItems { get; set; }
        public virtual DbSet<ExaminationReport> ExaminationReports { get; set; }
        public virtual DbSet<ExceptionLogger> ExceptionLoggers { get; set; }
        public virtual DbSet<FastService> FastServices { get; set; }
        public virtual DbSet<FastServiceType> FastServiceTypes { get; set; }
        public virtual DbSet<Feature> Features { get; set; }
        public virtual DbSet<Financial> Financials { get; set; }
        public virtual DbSet<FinancialType> FinancialTypes { get; set; }
        public virtual DbSet<GuaranteePeriod> GuaranteePeriods { get; set; }
        public virtual DbSet<InvioceSource> InvioceSources { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<InvoiceReportView> InvoiceReportViews { get; set; }
        public virtual DbSet<InvoiceStatus> InvoiceStatuses { get; set; }
        public virtual DbSet<InvoiceType> InvoiceTypes { get; set; }
        public virtual DbSet<JobTitle> JobTitles { get; set; }
        public virtual DbSet<MigrationHistory> MigrationHistories { get; set; }
        public virtual DbSet<Motor> Motors { get; set; }
        public virtual DbSet<MotorColor> MotorColors { get; set; }
        public virtual DbSet<MotorMake> MotorMakes { get; set; }
        public virtual DbSet<MotorModel> MotorModels { get; set; }
        public virtual DbSet<MotorYear> MotorYears { get; set; }
        public virtual DbSet<NewcategorySpare> NewcategorySpares { get; set; }
        public virtual DbSet<OilTransaction> OilTransactions { get; set; }
        public virtual DbSet<OilType> OilTypes { get; set; }
        public virtual DbSet<PartOrderType> PartOrderTypes { get; set; }
        public virtual DbSet<PaymentInfo> PaymentInfos { get; set; }
        public virtual DbSet<PaymentOp> PaymentOps { get; set; }
        public virtual DbSet<Plan> Plans { get; set; }
        public virtual DbSet<Pricing> Pricings { get; set; }
        public virtual DbSet<Receipt> Receipts { get; set; }
        public virtual DbSet<ReceptionistRepairOrder> ReceptionistRepairOrders { get; set; }
        public virtual DbSet<Region> Regions { get; set; }
        public virtual DbSet<Renewal> Renewals { get; set; }
        public virtual DbSet<RepairOrder> RepairOrders { get; set; }
        public virtual DbSet<RepairOrderImage> RepairOrderImages { get; set; }
        public virtual DbSet<RepairOrderPart> RepairOrderParts { get; set; }
        public virtual DbSet<RepairOrderReportView> RepairOrderReportViews { get; set; }
        public virtual DbSet<RepairOrderStatus> RepairOrderStatuses { get; set; }
        public virtual DbSet<RetuernMoneyNot> RetuernMoneyNots { get; set; }
        public virtual DbSet<ReturnSubscribtion> ReturnSubscribtions { get; set; }
        public virtual DbSet<ReturnSubscribtion1> ReturnSubscribtions1 { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<SalesUser> SalesUsers { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<SparePart> SpareParts { get; set; }
        public virtual DbSet<Stock> Stocks { get; set; }
        public virtual DbSet<SubCategory> SubCategories { get; set; }
        public virtual DbSet<Subscribtion> Subscribtions { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<UnderCwarshah> UnderCwarshahs { get; set; }
        public virtual DbSet<UnderProcess> UnderProcesses { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<Users1> Users1s { get; set; }
        public virtual DbSet<UsersReportView> UsersReportViews { get; set; }
        public virtual DbSet<VerfiyCode> VerfiyCodes { get; set; }
        public virtual DbSet<ViewOilTransaction> ViewOilTransactions { get; set; }
        public virtual DbSet<ViewSubscribtion> ViewSubscribtions { get; set; }
        public virtual DbSet<Warshah> Warshahs { get; set; }
        public virtual DbSet<WarshahCustomer> WarshahCustomers { get; set; }
        public virtual DbSet<WarshahTheme> WarshahThemes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=intelcarddbserver.database.windows.net;User ID=WarshahTechAdminDB;Password=P@ssw0rD$%^F0R@dMiN2o21;Database=WarshahTech;Trusted_Connection=False;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<AccountStatus>(entity =>
            {
                entity.ToTable("AccountStatus");

                entity.Property(e => e.AccountStatusId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Attachment>(entity =>
            {
                entity.Property(e => e.AttachmentId).ValueGeneratedNever();

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.HasOne(d => d.AttachmentType)
                    .WithMany(p => p.Attachments)
                    .HasForeignKey(d => d.AttachmentTypeId)
                    .HasConstraintName("FK_dbo.Attachments_dbo.AttachmentTypes_AttachmentTypeId");
            });

            modelBuilder.Entity<AttachmentType>(entity =>
            {
                entity.Property(e => e.AttachmentTypeId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Car>(entity =>
            {
                entity.ToTable("Car");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(250);
            });

            modelBuilder.Entity<Catch>(entity =>
            {
                entity.ToTable("Catch");

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.AmountType)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Discriptotion).IsRequired();

                entity.Property(e => e.DocNumber)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.InvoiceId).IsRequired();
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.Property(e => e.NameAr).HasMaxLength(50);
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK_dbo.Client");

                entity.ToTable("Client");

                entity.Property(e => e.UserId).ValueGeneratedNever();

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Idiqama)
                    .HasMaxLength(50)
                    .HasColumnName("IDIqama");

                entity.Property(e => e.LicenseExpiryDate).HasColumnType("datetime");

                entity.Property(e => e.Nationality).HasMaxLength(100);

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasKey(e => e.CountryCode)
                    .HasName("PK__Countrie__3436E9A47CD69678");

                entity.Property(e => e.CountryCode)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("country_code")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.CountryArName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("country_arName")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.CountryArNationality)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("country_arNationality")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.CountryEnName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("country_enName")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.CountryEnNationality)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("country_enNationality")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Sort).HasColumnType("numeric(18, 0)");
            });

            modelBuilder.Entity<Country1>(entity =>
            {
                entity.ToTable("country");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CurrancyAr).HasMaxLength(50);

                entity.Property(e => e.CurrancyEn).HasMaxLength(50);

                entity.Property(e => e.NameAr).HasMaxLength(50);

                entity.Property(e => e.NameEn).HasMaxLength(50);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.CustomerId).ValueGeneratedNever();

                entity.Property(e => e.Birthdate).HasColumnType("date");

                entity.Property(e => e.Cn)
                    .HasMaxLength(20)
                    .HasColumnName("CN");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.LicenseExpiryDate).HasColumnType("datetime");

                entity.Property(e => e.Nationality).HasMaxLength(100);

                entity.Property(e => e.Passport).HasMaxLength(50);
            });

            modelBuilder.Entity<DamageMarker>(entity =>
            {
                entity.ToTable("DamageMarker");

                entity.Property(e => e.XPosition).HasColumnName("xPosition");

                entity.Property(e => e.YPosition).HasColumnName("yPosition");
            });

            modelBuilder.Entity<Dashboard>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Dashboard");

                entity.Property(e => e.PaidInvoiceTotal).HasColumnType("decimal(38, 2)");
            });

            modelBuilder.Entity<DashboardTbl>(entity =>
            {
                entity.ToTable("DashboardTbl");

                entity.Property(e => e.DashboardTblId).ValueGeneratedNever();
            });

            modelBuilder.Entity<ExamValue>(entity =>
            {
                entity.ToTable("ExamValue");

                entity.Property(e => e.ExamValueId).ValueGeneratedNever();

                entity.Property(e => e.ExamValueIdDesc).HasMaxLength(50);

                entity.Property(e => e.ExamValueIdDescAr).HasMaxLength(50);
            });

            modelBuilder.Entity<ExaminationItem>(entity =>
            {
                entity.HasKey(e => e.ExaminationItemsId);

                entity.Property(e => e.ExaminationItemsId).ValueGeneratedNever();

                entity.Property(e => e.ExaminationItemsDesc).HasMaxLength(50);

                entity.Property(e => e.ExaminationItemsDescAr).HasMaxLength(50);
            });

            modelBuilder.Entity<ExaminationReport>(entity =>
            {
                entity.ToTable("ExaminationReport");

                entity.HasOne(d => d.ExamValue)
                    .WithMany(p => p.ExaminationReports)
                    .HasForeignKey(d => d.ExamValueId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExaminationReport_ExamValue");

                entity.HasOne(d => d.ExaminationItems)
                    .WithMany(p => p.ExaminationReports)
                    .HasForeignKey(d => d.ExaminationItemsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExaminationReport_ExaminationItems");
            });

            modelBuilder.Entity<ExceptionLogger>(entity =>
            {
                entity.ToTable("ExceptionLogger");

                entity.Property(e => e.ControllerName).HasMaxLength(200);

                entity.Property(e => e.ExceptionLogTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<FastService>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.NameEn).HasMaxLength(50);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<FastServiceType>(entity =>
            {
                entity.ToTable("FastServiceType");

                entity.Property(e => e.ServiceTypeName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ServiceTypeNameEn).HasMaxLength(50);

                entity.Property(e => e.ServiceTypePrice).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<Feature>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Feature1)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Feature");
            });

            modelBuilder.Entity<Financial>(entity =>
            {
                entity.ToTable("Financial");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Ammount).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.InvoiceSerial)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name).IsRequired();

                entity.HasOne(d => d.FinancialType)
                    .WithMany(p => p.Financials)
                    .HasForeignKey(d => d.FinancialTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Financial_FinancialType");

                entity.HasOne(d => d.InvoiceSource)
                    .WithMany(p => p.Financials)
                    .HasForeignKey(d => d.InvoiceSourceId)
                    .HasConstraintName("FK_Financial_InvioceSource");
            });

            modelBuilder.Entity<FinancialType>(entity =>
            {
                entity.ToTable("FinancialType");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<GuaranteePeriod>(entity =>
            {
                entity.ToTable("GuaranteePeriod");

                entity.Property(e => e.GuaranteePeriodId).ValueGeneratedNever();

                entity.Property(e => e.GuaranteePeriodDesc).HasMaxLength(50);

                entity.Property(e => e.GuaranteePeriodDescAr).HasMaxLength(50);
            });

            modelBuilder.Entity<InvioceSource>(entity =>
            {
                entity.ToTable("InvioceSource");

                entity.Property(e => e.InvioceSource1)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("InvioceSource");

                entity.Property(e => e.InvioceTaxNo)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("invioceTaxNo");
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.Property(e => e.InvoiceId).ValueGeneratedNever();

                entity.Property(e => e.CarCheckPrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Discount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.InvoiceSerial).ValueGeneratedOnAdd();

                entity.Property(e => e.RepairPrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.SparePartsPrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Subtotal).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Total).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

                entity.Property(e => e.Vat)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("VAT");

                entity.HasOne(d => d.InvoiceStatus)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.InvoiceStatusId)
                    .HasConstraintName("FK_dbo.Invoices_dbo.InvoiceStatus_InvoiceStatusId");

                entity.HasOne(d => d.InvoiceType)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.InvoiceTypeId)
                    .HasConstraintName("FK_Invoices_InvoiceTypes");

                entity.HasOne(d => d.RepairOrder)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.RepairOrderId)
                    .HasConstraintName("FK_dbo.Invoices_dbo.RepairOrders_RepairOrderId");
            });

            modelBuilder.Entity<InvoiceReportView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("InvoiceReportView");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Discount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Subtotal).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Total).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Vat)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("VAT");
            });

            modelBuilder.Entity<InvoiceStatus>(entity =>
            {
                entity.ToTable("InvoiceStatus");

                entity.Property(e => e.InvoiceStatusId).ValueGeneratedNever();
            });

            modelBuilder.Entity<InvoiceType>(entity =>
            {
                entity.Property(e => e.InvoiceTypeId).ValueGeneratedNever();
            });

            modelBuilder.Entity<JobTitle>(entity =>
            {
                entity.ToTable("JobTitle");

                entity.Property(e => e.JobTitleId).ValueGeneratedNever();

                entity.Property(e => e.TitleName).HasMaxLength(100);

                entity.Property(e => e.TitleNameAr).HasMaxLength(100);
            });

            modelBuilder.Entity<MigrationHistory>(entity =>
            {
                entity.HasKey(e => new { e.MigrationId, e.ContextKey })
                    .HasName("PK_dbo.__MigrationHistory");

                entity.ToTable("__MigrationHistory");

                entity.Property(e => e.MigrationId).HasMaxLength(150);

                entity.Property(e => e.ContextKey).HasMaxLength(300);

                entity.Property(e => e.Model).IsRequired();

                entity.Property(e => e.ProductVersion)
                    .IsRequired()
                    .HasMaxLength(32);
            });

            modelBuilder.Entity<Motor>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.HasOne(d => d.MotorColor)
                    .WithMany(p => p.Motors)
                    .HasForeignKey(d => d.MotorColorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Motors_MotorColor");

                entity.HasOne(d => d.MotorYear)
                    .WithMany(p => p.Motors)
                    .HasForeignKey(d => d.MotorYearId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Motors_MotorYear");
            });

            modelBuilder.Entity<MotorColor>(entity =>
            {
                entity.ToTable("MotorColor");
            });

            modelBuilder.Entity<MotorMake>(entity =>
            {
                entity.ToTable("MotorMake");

                entity.Property(e => e.MotorMakeId).ValueGeneratedNever();
            });

            modelBuilder.Entity<MotorModel>(entity =>
            {
                entity.ToTable("MotorModel");

                entity.Property(e => e.MotorModelId).ValueGeneratedNever();

                entity.HasOne(d => d.MotorMake)
                    .WithMany(p => p.MotorModels)
                    .HasForeignKey(d => d.MotorMakeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MotorModel_MotorMake");
            });

            modelBuilder.Entity<MotorYear>(entity =>
            {
                entity.ToTable("MotorYear");

                entity.Property(e => e.MotorYearId).ValueGeneratedNever();

                entity.HasOne(d => d.MotorModel)
                    .WithMany(p => p.MotorYears)
                    .HasForeignKey(d => d.MotorModelId)
                    .HasConstraintName("FK_MotorYear_MotorModel");
            });

            modelBuilder.Entity<NewcategorySpare>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Newcategory_Spare");

                entity.Property(e => e.Category).HasMaxLength(255);

                entity.Property(e => e.CategoryAr).HasMaxLength(255);

                entity.Property(e => e.SubCategory).HasMaxLength(255);

                entity.Property(e => e.SubCategoryAr).HasMaxLength(255);
            });

            modelBuilder.Entity<OilTransaction>(entity =>
            {
                entity.HasKey(e => e.Idkey);

                entity.ToTable("Oil_Transaction");

                entity.Property(e => e.Idkey).HasColumnName("IDKey");

                entity.Property(e => e.CreatedDate).HasColumnType("smalldatetime");

                entity.Property(e => e.IdreceptionOrder).HasColumnName("IDReceptionOrder");

                entity.Property(e => e.OilPrice).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.OilTypeId).HasColumnName("OilTypeID");

                entity.Property(e => e.TotalPrice).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.OilType)
                    .WithMany(p => p.OilTransactions)
                    .HasForeignKey(d => d.OilTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Oil_Transaction_OilType");
            });

            modelBuilder.Entity<OilType>(entity =>
            {
                entity.ToTable("OilType");

                entity.Property(e => e.OilTypeId).HasColumnName("OilTypeID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.OilArabicName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.OilEnglishName).HasMaxLength(100);

                entity.Property(e => e.OilPrice).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.OilTypeArName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.OilTypeEnName).HasMaxLength(100);

                entity.HasOne(d => d.Warshah)
                    .WithMany(p => p.OilTypes)
                    .HasForeignKey(d => d.WarshahId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OilType_Warshahs");
            });

            modelBuilder.Entity<PartOrderType>(entity =>
            {
                entity.Property(e => e.PartOrderTypeId).ValueGeneratedNever();
            });

            modelBuilder.Entity<PaymentInfo>(entity =>
            {
                entity.ToTable("PaymentInfo");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Apis).HasColumnName("APIs");

                entity.Property(e => e.PackageAmount).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.PaymentDate).HasColumnType("datetime");

                entity.Property(e => e.PaymentEnd)
                    .HasColumnType("datetime")
                    .HasColumnName("paymentEnd");

                entity.Property(e => e.PaymentMcount).HasColumnName("paymentMCount");

                entity.Property(e => e.PaymentMethod).IsRequired();

                entity.Property(e => e.PaymentPackage).IsRequired();

                entity.Property(e => e.PaymentTotal).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<PaymentOp>(entity =>
            {
                entity.Property(e => e.Api).HasColumnName("API");

                entity.Property(e => e.ExpiryDate).HasColumnType("datetime");

                entity.Property(e => e.PaymentDate).HasColumnType("datetime");

                entity.Property(e => e.PaymentPackage)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TransactionRef)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Plan>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.PlanName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Pricing>(entity =>
            {
                entity.ToTable("Pricing");

                entity.Property(e => e.CarMake).IsRequired();

                entity.Property(e => e.CarModel).IsRequired();

                entity.Property(e => e.CarYear).IsRequired();

                entity.Property(e => e.Category).IsRequired();

                entity.Property(e => e.ChassaiNum).IsRequired();

                entity.Property(e => e.Describtion).IsRequired();

                entity.Property(e => e.IsDone).HasColumnName("isDone");

                entity.Property(e => e.Qty).HasColumnName("QTY");

                entity.Property(e => e.SubCategory).IsRequired();
            });

            modelBuilder.Entity<Receipt>(entity =>
            {
                entity.ToTable("Receipt");

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.AmountType)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ClientName).IsRequired();

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Discriptotion).IsRequired();

                entity.Property(e => e.DocNumber)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<ReceptionistRepairOrder>(entity =>
            {
                entity.Property(e => e.ReceptionistRepairOrderId).ValueGeneratedNever();

                entity.Property(e => e.CheckingPrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.KmIn).HasColumnName("KM_IN");

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

                entity.HasOne(d => d.Motor)
                    .WithMany(p => p.ReceptionistRepairOrders)
                    .HasForeignKey(d => d.MotorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReceptionistRepairOrders_Motors");
            });

            modelBuilder.Entity<Region>(entity =>
            {
                entity.ToTable("Region");

                entity.Property(e => e.NameAr)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.NameEn).HasMaxLength(50);
            });

            modelBuilder.Entity<Renewal>(entity =>
            {
                entity.ToTable("Renewal");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Total).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.WarshahId).IsRequired();
            });

            modelBuilder.Entity<RepairOrder>(entity =>
            {
                entity.Property(e => e.RepairOrderId).ValueGeneratedNever();

                entity.Property(e => e.CheckingPrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Discount)
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.FixingPrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.KmOut).HasColumnName("KM_OUT");

                entity.Property(e => e.RepairOrderSerial).ValueGeneratedOnAdd();

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

                entity.HasOne(d => d.GuaranteePeriod)
                    .WithMany(p => p.RepairOrders)
                    .HasForeignKey(d => d.GuaranteePeriodId)
                    .HasConstraintName("FK_RepairOrders_GuaranteePeriod");

                entity.HasOne(d => d.Motor)
                    .WithMany(p => p.RepairOrders)
                    .HasForeignKey(d => d.MotorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RepairOrders_Motors");

                entity.HasOne(d => d.PartOrderType)
                    .WithMany(p => p.RepairOrders)
                    .HasForeignKey(d => d.PartOrderTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RepairOrders_PartOrderTypes");

                entity.HasOne(d => d.RepairOrderStatus)
                    .WithMany(p => p.RepairOrders)
                    .HasForeignKey(d => d.RepairOrderStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RepairOrders_RepairOrderStatus");
            });

            modelBuilder.Entity<RepairOrderImage>(entity =>
            {
                entity.Property(e => e.ImageName).IsRequired();

                entity.Property(e => e.Roid).HasColumnName("ROID");
            });

            modelBuilder.Entity<RepairOrderPart>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.PeacePrice).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.RepairOrder)
                    .WithMany(p => p.RepairOrderParts)
                    .HasForeignKey(d => d.RepairOrderId)
                    .HasConstraintName("FK_dbo.SpareParts_dbo.RepairOrders_RepairOrderId");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.RepairOrderParts)
                    .HasForeignKey(d => d.ServiceId)
                    .HasConstraintName("FK_RepairOrderParts_Services");

                entity.HasOne(d => d.SparePart)
                    .WithMany(p => p.RepairOrderParts)
                    .HasForeignKey(d => d.SparePartId)
                    .HasConstraintName("FK_RepairOrderParts_SpareParts");
            });

            modelBuilder.Entity<RepairOrderReportView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("RepairOrderReportView");

                entity.Property(e => e.CheckingPrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Discount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.FixingPrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<RepairOrderStatus>(entity =>
            {
                entity.ToTable("RepairOrderStatus");

                entity.Property(e => e.RepairOrderStatusId).ValueGeneratedNever();
            });

            modelBuilder.Entity<RetuernMoneyNot>(entity =>
            {
                entity.Property(e => e.RemainMoney).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.ReturnMoney).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.VatMoney).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.Invoice)
                    .WithMany(p => p.RetuernMoneyNots)
                    .HasForeignKey(d => d.InvoiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RetuernMoneyNots_RetuernMoneyNots");
            });

            modelBuilder.Entity<ReturnSubscribtion>(entity =>
            {
                entity.HasKey(e => e.Idkey);

                entity.ToTable("ReturnSubscribtion");

                entity.Property(e => e.Idkey).HasColumnName("IDKey");

                entity.Property(e => e.BeforeDiscount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Discount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Subtotal).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Total).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

                entity.Property(e => e.Vat)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("VAT");
            });

            modelBuilder.Entity<ReturnSubscribtion1>(entity =>
            {
                entity.HasKey(e => e.Idkey);

                entity.ToTable("ReturnSubscribtions");

                entity.Property(e => e.Idkey).HasColumnName("IDKey");

                entity.Property(e => e.BeforeDiscount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Discount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Subtotal).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Tag).HasMaxLength(250);

                entity.Property(e => e.Total).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

                entity.Property(e => e.Vat)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("VAT");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.RoleId).ValueGeneratedNever();
            });

            modelBuilder.Entity<SalesUser>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("SalesUser");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Password).IsRequired();

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.Property(e => e.ServiceId).ValueGeneratedNever();
            });

            modelBuilder.Entity<SparePart>(entity =>
            {
                entity.Property(e => e.Buyingprice)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("buyingprice");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.PeacePrice).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<Stock>(entity =>
            {
                entity.ToTable("Stock");

                entity.Property(e => e.StockId).ValueGeneratedNever();
            });

            modelBuilder.Entity<SubCategory>(entity =>
            {
                entity.HasOne(d => d.Category)
                    .WithMany(p => p.SubCategories)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_SubCategories_Categories");
            });

            modelBuilder.Entity<Subscribtion>(entity =>
            {
                entity.HasKey(e => e.InvoiceId);

                entity.Property(e => e.InvoiceId).ValueGeneratedNever();

                entity.Property(e => e.BeforeDiscount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Discount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.InvoiceSerial).ValueGeneratedOnAdd();

                entity.Property(e => e.Subtotal).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Total).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

                entity.Property(e => e.Vat)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("VAT");
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.Property(e => e.SupplierId).ValueGeneratedNever();

                entity.Property(e => e.SupplierMobileNo).HasMaxLength(50);
            });

            modelBuilder.Entity<UnderCwarshah>(entity =>
            {
                entity.HasKey(e => e.WarshahId);

                entity.ToTable("UnderCWarshah");

                entity.Property(e => e.WarshahId).ValueGeneratedNever();

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnName("address");

                entity.Property(e => e.City).IsRequired();

                entity.Property(e => e.Phone).HasMaxLength(50);

                entity.Property(e => e.Vat)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("vat");

                entity.Property(e => e.WarshahName).IsRequired();
            });

            modelBuilder.Entity<UnderProcess>(entity =>
            {
                entity.ToTable("UnderProcess");

                entity.Property(e => e.Api).HasColumnName("API");

                entity.Property(e => e.PaymentPackage)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PyamentDate).HasColumnType("datetime");

                entity.Property(e => e.Ref).HasColumnName("REF");

                entity.Property(e => e.Store).HasColumnName("store");

                entity.Property(e => e.UserEmail).IsRequired();

                entity.Property(e => e.UserName).IsRequired();

                entity.Property(e => e.UserNationlity).HasMaxLength(50);

                entity.Property(e => e.UserPhone)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.WarshahAddress).IsRequired();

                entity.Property(e => e.WarshahCity).IsRequired();

                entity.Property(e => e.WarshahId).HasColumnName("WarshahID");

                entity.Property(e => e.WarshahPhone)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.WharshahName).IsRequired();
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId).ValueGeneratedNever();

                entity.Property(e => e.ActivatedByMail).HasDefaultValueSql("((0))");

                entity.Property(e => e.Birthdate).HasColumnType("date");

                entity.Property(e => e.Cr).HasColumnName("CR");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CrexpiryDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CRExpiryDate");

                entity.Property(e => e.Idiqama)
                    .HasMaxLength(50)
                    .HasColumnName("IDIqama");

                entity.Property(e => e.LicenseExpiryDate).HasColumnType("datetime");

                entity.Property(e => e.Nationality).HasMaxLength(100);

                entity.Property(e => e.Passport).HasMaxLength(50);

                entity.Property(e => e.PerAddFixCommand).HasColumnName("Per_AddFixCommand");

                entity.Property(e => e.PerAddMotor).HasColumnName("Per_AddMotor");

                entity.Property(e => e.PerAddMotorOwner).HasColumnName("Per_AddMotorOwner");

                entity.Property(e => e.PerIssueInvoice).HasColumnName("Per_IssueInvoice");

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_dbo.UserRoles_dbo.Roles_RoleId");
            });

            modelBuilder.Entity<Users1>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("users1");

                entity.Property(e => e.Cr).HasColumnName("CR");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CrexpiryDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CRExpiryDate");

                entity.Property(e => e.LicenseExpiryDate).HasColumnType("datetime");

                entity.Property(e => e.PerAddFixCommand).HasColumnName("Per_AddFixCommand");

                entity.Property(e => e.PerAddMotor).HasColumnName("Per_AddMotor");

                entity.Property(e => e.PerAddMotorOwner).HasColumnName("Per_AddMotorOwner");

                entity.Property(e => e.PerIssueInvoice).HasColumnName("Per_IssueInvoice");

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<UsersReportView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("UsersReportView");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Nationality).HasMaxLength(100);

                entity.Property(e => e.TitleNameAr).HasMaxLength(100);
            });

            modelBuilder.Entity<VerfiyCode>(entity =>
            {
                entity.ToTable("VerfiyCode");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ViewOilTransaction>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_Oil_Transactions");

                entity.Property(e => e.CreatedDate).HasColumnType("smalldatetime");

                entity.Property(e => e.Idkey).HasColumnName("IDKey");

                entity.Property(e => e.IdreceptionOrder).HasColumnName("IDReceptionOrder");

                entity.Property(e => e.OilPrice).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.OilTypeArName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.OilTypeEnName).HasMaxLength(100);

                entity.Property(e => e.OilTypeId).HasColumnName("OilTypeID");

                entity.Property(e => e.TotalPrice).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<ViewSubscribtion>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_Subscribtions");

                entity.Property(e => e.BeforeDiscount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Discount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PaymentPackage).HasMaxLength(50);

                entity.Property(e => e.Subtotal).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Total).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

                entity.Property(e => e.Vat)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("VAT");
            });

            modelBuilder.Entity<Warshah>(entity =>
            {
                entity.Property(e => e.WarshahId).ValueGeneratedNever();

                entity.Property(e => e.BranchFullId).HasMaxLength(50);

                entity.Property(e => e.Commercialregistercopy).HasColumnName("commercialregistercopy");

                entity.Property(e => e.Cr)
                    .HasMaxLength(20)
                    .HasColumnName("CR");

                entity.Property(e => e.Hidden).HasColumnName("hidden");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Ref).HasColumnName("REF");

                entity.Property(e => e.TaxNumber).HasMaxLength(20);

                entity.Property(e => e.Taxcertificatecopy).HasColumnName("taxcertificatecopy");

                entity.Property(e => e.Vat)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("VAT")
                    .HasDefaultValueSql("((15))");

                entity.Property(e => e.WarshahSequence).ValueGeneratedOnAdd();

                entity.HasOne(d => d.WarshahTheme)
                    .WithMany(p => p.Warshahs)
                    .HasForeignKey(d => d.WarshahThemeId)
                    .HasConstraintName("FK_Warshahs_WarshahThemes");
            });

            modelBuilder.Entity<WarshahCustomer>(entity =>
            {
                entity.HasKey(e => new { e.WarshahId, e.CustomerId });

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.HasOne(d => d.Warshah)
                    .WithMany(p => p.WarshahCustomers)
                    .HasForeignKey(d => d.WarshahId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WarshahCustomers_Warshahs");
            });

            modelBuilder.Entity<WarshahTheme>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.BackgroundColor).HasMaxLength(20);

                entity.Property(e => e.Header).HasMaxLength(20);

                entity.Property(e => e.MainIcons).HasMaxLength(20);

                entity.Property(e => e.Sidebare).HasMaxLength(20);

                entity.Property(e => e.SidebareActiveText).HasMaxLength(20);

                entity.Property(e => e.SidebareText).HasMaxLength(20);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
