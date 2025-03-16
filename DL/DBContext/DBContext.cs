using DL.Configuration;
using DL.DTOs.BalanceDTO;
using DL.Entities;
using DL.Entities.HR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL.DBContext
{
    public class AppDBContext : DbContext
    {

        public AppDBContext(DbContextOptions<AppDBContext> options)
        : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new PermissionConfiguration());
            modelBuilder.ApplyConfiguration(new NameNotificationConfiguration());
            modelBuilder.ApplyConfiguration(new RepairOrderNotificationConfiguration());
            modelBuilder.ApplyConfiguration(new FixedBankConfiguration()); 
            modelBuilder.ApplyConfiguration(new PaymentTypeInvoiceConfiguration());
            modelBuilder.ApplyConfiguration(new InspectionTemplateConfiguration());
            modelBuilder.ApplyConfiguration(new InspectionSectionConfiguration());
            modelBuilder.ApplyConfiguration(new InspectionItemsConfiguration());
            modelBuilder.ApplyConfiguration(new ExpensesCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new WarshahFixedTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CountriesWarshahModelConfiguration());
            modelBuilder.ApplyConfiguration(new WarshahServiceTypeConfiguration());
            modelBuilder.ApplyConfiguration(new RequestTypeConfiguration());
            modelBuilder.ApplyConfiguration(new WorkTypeConfiguration());

        }


        public virtual DbSet<User> User { get; set; }
     
        public virtual DbSet<Role> Roles { get; set; }
       
        public virtual DbSet<VerfiyCode> VerfiyCodes { get; set; }
        public virtual DbSet<permission> Permissions { get; set; }
        public virtual DbSet<UserPermission> UserPermissions { get; set; }

        public virtual DbSet<MotorMake> MotorMakes { get; set; }
        public virtual DbSet<MotorModel> MotorModels { get; set; }
        public virtual DbSet<MotorColor> MotorColors { get; set; }
        public virtual DbSet<MotorYear> MotorYears { get; set; }
        public virtual DbSet<Motors> Motors { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Region> Regions { get; set; }
        public virtual DbSet<ReciptionOrder> ReciptionOrders { get; set; }
        public virtual DbSet<WarshahCustomers> WarshahCustomers { get; set; }
        public virtual DbSet<CategorySpareParts> CategorySpareParts { get; set; }
        public virtual DbSet<SubCategoryParts> SubCategoryParts { get; set; }
        public virtual DbSet<SparePart> SpareParts { get; set; }
        public virtual DbSet<RepairOrder> RepairOrders { get; set; }
        public virtual DbSet<RepairOrderSparePart> RepairOrderSpareParts { get; set; }
        public virtual DbSet<RepairOrderImage> RepairOrderImages { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<PaymentTypeInvoice> PaymentTypeInvoices { get; set; }
        public virtual DbSet<DebitNotice> DebitNotices { get; set; }
        public virtual DbSet<CreditorNotice> CreditorNotices { get; set; }
        public virtual DbSet<InspectionTemplate> InspectionTemplates { get; set; }
        public virtual DbSet<InspectionSection> InspectionSections { get; set; }
        public virtual DbSet<InspectionItem> InspectionItems { get; set; }
        public virtual DbSet<InspectionReport> InspectionReports { get; set; }
        public virtual DbSet<Chat> Chats { get; set; }
        public virtual DbSet<Configration> Configrations { get; set; }
        public virtual DbSet<ReceiptVoucher> ReceiptVouchers { get; set; }
        public virtual DbSet<PaymentAndReceiptVoucher> PaymentAndReceiptVouchers { get; set; }
        public virtual DbSet<ExpensesCategory> ExpensesCategories { get; set; }
        public virtual DbSet<ExpensesType> ExpensesTypes { get; set; }
        public virtual DbSet<ExpensesTransaction> ExpensesTransactions { get; set; }
        public virtual DbSet<DebitAndCreditor> DebitAndCreditors { get; set; }
        public virtual DbSet<NoticeProduct> NoticeProducts { get; set; }
        public virtual DbSet<RoHistory> RoHistories { get; set; }
        public virtual DbSet<AuthrizedPerson> AuthrizedPersons { get; set; }
        public virtual DbSet<ServiceCategory> ServiceCategories { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<ServiceInvoice> ServiceInvoices { get; set; }
        public virtual DbSet<ServiceInvoiceItem> ServiceInvoiceItems { get; set; }

        public virtual DbSet<Supplier> Suppliers { get; set; }

        public virtual DbSet<InvoiceItem> InvoiceItems { get; set; }

        public virtual DbSet<InspectionWarshahReport> InspectionWarshahReports { get; set; }


        public virtual DbSet<Bank> Banks { get; set; }

        public virtual DbSet<BalanceBank> BalanceBanks { get; set; }

        public virtual DbSet<BoxMoney> Boxes { get; set; }
        public virtual DbSet<Cheque> Cheques { get; set; }

        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<TaseerItem> TaseerItems { get; set; }
        public virtual DbSet<Appointment> Appointments { get; set; }
        public virtual DbSet<WarshahParams> WarshahParams { get; set; }
        public virtual DbSet<WarshahServiceType> WarshahServiceType { get; set; }
        public virtual DbSet<WarshahType> WarshahType { get; set; }
        public virtual DbSet<Spicialists> Spicialists { get; set; }
        public virtual DbSet<SalesRequest> SalesRequests { get; set; }

        public virtual DbSet<WarshahBank> WarshahBanks { get; set; }
        public virtual DbSet<TaseerSupplier> TaseerSuppliers { get; set; }
        public virtual DbSet<SparePartTaseer> SparePartTaseers { get; set; }
        public virtual DbSet<SalesRequestList> SalesRequestLists { get; set; }
        public virtual DbSet<Duration> Durations { get; set; }
        public virtual DbSet<Subscribtion> Subscribtions { get; set; }


        public virtual DbSet<ServiceWarshah> ServiceWarshahs { get; set; }



        public virtual DbSet<WarshahModelsCar> WarshahModelsCars { get; set; }



        public virtual DbSet<WarshahFixedType> WarshahFixedTypes { get; set; }

        public virtual DbSet<SubscribtionInvoice> SubscribtionInvoices { get; set; }
        public virtual DbSet<PersonDiscount> PersonDiscounts { get; set; }
        public virtual DbSet<WarshahShift> WarshahShift { get; set; }

        public virtual DbSet<WarshahVat> WarshahVats { get; set; }

        public virtual DbSet<FixedCountryMotor> FixedCountryMotors { get; set; }

        public virtual DbSet<WarshahCountryMotor> WarshahCountryMotors { get; set; }
        public virtual DbSet<WorkTime> WorkTimes { get; set; }


        public virtual DbSet<FixedBank> FixedBanks { get; set; }
        public virtual DbSet<LoyalityPoint> LoyalityPoints { get; set; }

        public virtual DbSet<OpeningBalance> OpeningBalances { get; set; }

        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<LoyalitySetting> LoyalitySettings { get; set; }

        public virtual DbSet<TransactionsToday> TransactionsTodays { get; set; }



    

        public virtual DbSet<OpenSpartPart> OpenSpartParts { get; set; }

        public virtual DbSet<OpenBalanceCheque> OpenBalanceCheques { get; set; }

        public virtual DbSet<OpenBalanceBank> OpenBalanceBanks { get; set; }
        public virtual DbSet<LoyalitySettingRevarse> LoyalitySettingRevarses { get; set; }
        public virtual DbSet<SMSCount> SMSCounts { get; set; }
        public virtual DbSet<SMSInvoice> SMSInvoices { get; set; }
        public virtual DbSet<SMSPackage> SMSPackages { get; set; }

        public virtual DbSet<Nationality> Nationalities { get; set; }

        public virtual DbSet<StatusEmployment> StatusEmployments { get; set; }

        public virtual DbSet<EmployeeShift> EmployeeShifts { get; set; }

        public virtual DbSet<ContractType> ContractTypes { get; set; }

        public virtual DbSet<Gender> Genders { get; set; }

        public virtual DbSet<MaritalStatus> MaritalStatuses { get; set; }

        public virtual DbSet<DataEmployee> DataEmployees { get; set; }
        public virtual DbSet<ContactUs> ContactUs { get; set; }

        public virtual DbSet<BonusTechnical> BonusTechnicals { get; set; }

        public virtual DbSet<ItemSalary> ItemSalaries { get; set; }

        public virtual DbSet<RecordBonusTechnical> RecordBonusTechnicals { get; set; }
        public virtual DbSet<Cupon> Cupons { get; set; }
        public virtual DbSet<WarshahDisableReason> WarshahDisableReasons { get; set; }

        public virtual DbSet<TransactionInventory> TransactionInventories { get; set; }

        public virtual DbSet<BoxBank> BoxBanks { get; set; }
        public virtual DbSet<GlobalSetting> GlobalSettings { get; set; }
        public virtual DbSet<ResonToRejectOrder> ResonToRejectOrders { get; set; }
        public virtual DbSet<RepairOrderServices> RepairOrderServicess { get; set; }
        public virtual DbSet<SMSHistory> SMSHistorys { get; set; }

        public virtual DbSet<JobTitle> JobTitles { get; set; }

        public virtual DbSet<ClaimInvoice> ClaimInvoices { get; set; }
        public virtual DbSet<ClientRequest> ClientRequests { get; set; }

        public virtual DbSet<ChequeBankAccount> ChequeBankAccounts { get; set; }

        public virtual DbSet<Transfer> Transfers { get; set; }

        public virtual DbSet<TransferBankAccount> TransferBankAccounts { get; set; }
        public virtual DbSet<CuponHistory> CuponHistories { get; set; }

        public virtual DbSet<NotificationSetting> NotificationSettings { get; set; }

        public virtual DbSet<NameNotification> NameNotifications { get; set; }

        public virtual DbSet<OldJobtitle> OldJobtitles { get; set; }

        public virtual DbSet<DelayRepairOrder> DelayRepairOrders { get; set; }

        public virtual DbSet<NotificationRepairOrder> NotificationRepairOrders { get; set; }

        public virtual DbSet<NotificationRepairOrderAdding> NotificationRepairOrderAddings { get; set; }

        public virtual DbSet<WorkType> WorkType { get; set; }
        public virtual DbSet<RequestType> RequestType { get; set; }

        public virtual DbSet<OldInvoice> OldInvoices  { get; set; }

        public virtual DbSet<OldInvoiceItem> OldInvoiceItems { get; set; }

        public virtual DbSet<WarshahWithCarOwner> WarshahWithCarOwners { get; set; }

        public virtual DbSet<SalesInvoice> SalesInvoices { get; set; }

        public virtual DbSet<SalesInvoiceItem> SalesInvoiceItems { get; set; }

        public virtual DbSet<WarshahTechService> WarshahTechServices { get; set; }
        public virtual DbSet<WarshahMobile> WarshahMobiles { get; set; }

        public virtual DbSet<PriceOffer> PriceOffers { get; set; }

        public virtual DbSet<PriceOfferItem> PriceOfferItems { get; set; }

        public virtual DbSet<ServicePriceOffer> ServicePriceOffers { get; set; }


        public virtual DbSet<WarshahReport> WarshahReports { get; set; }

        public virtual DbSet<RegisterForm> RegisterForms { get; set; }

        public virtual DbSet<GTaxControl> GTaxControl { get; set; }

        public virtual DbSet<InstantPart> InstantParts { get; set; }


    }
}
