using BL.Repositories;
using DL.DBContext;


namespace BL.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private AppDBContext _ctx;
        public UnitOfWork(AppDBContext ctx)
        {
            _ctx = ctx;
            _ctx.ChangeTracker.LazyLoadingEnabled = true;
        }


        public UserRepository UserRepository => new UserRepository(_ctx);
        public permissionRepository PermissionRepository => new permissionRepository(_ctx);


        public RoleRepository RoleRepository => new RoleRepository(_ctx);
        public VerfiyCodeRepository VerfiyCodeRepository => new VerfiyCodeRepository(_ctx);
        public UserpermissionRepository UserpermissionRepository => new UserpermissionRepository(_ctx);
        public WarshahRepository WarshahRepository => new WarshahRepository(_ctx);

        public MotorMakeRepository MotorMakeRepository => new MotorMakeRepository(_ctx);
        public MotorModelRepository MotorModelRepository => new MotorModelRepository(_ctx);
        public MotorColorRepository MotorColorRepository => new MotorColorRepository(_ctx);
        public MotorYearRepository MotorYearRepository => new MotorYearRepository(_ctx);
        public MotorsRepository MotorsRepository => new MotorsRepository(_ctx);
        public CountryRepository CountryRepository => new CountryRepository(_ctx);
        public CityRepository CityRepository => new CityRepository(_ctx);
        public RegionRepository RegionRepository => new RegionRepository(_ctx);
        public WarshahCustomersRepository WarshahCustomersRepository => new WarshahCustomersRepository(_ctx);
        public ReciptionOrderRepository ReciptionOrderRepository => new ReciptionOrderRepository(_ctx);
        public CategorySparePartsRepository CategorySparePartsRepository => new CategorySparePartsRepository(_ctx);
        public SubCategoryPartsRepository SubCategoryPartsRepository => new SubCategoryPartsRepository(_ctx);
        public SparePartRepository SparePartRepository => new SparePartRepository(_ctx);
        public RepairOrderRepository RepairOrderRepository => new RepairOrderRepository(_ctx);
        public RepairOrderSparePartRepository RepairOrderSparePartRepository => new RepairOrderSparePartRepository(_ctx);
        public RepairOrderImageRepository RepairOrderImageRepository => new RepairOrderImageRepository(_ctx);
        public InvoiceRepository InvoiceRepository => new InvoiceRepository(_ctx);
        public PaymentTypeInvoiceRepository PaymentTypeInvoiceRepository => new PaymentTypeInvoiceRepository(_ctx);
        public DebitNoticeRepository DebitNoticeRepository => new DebitNoticeRepository(_ctx);
        public CreditorNoticeRepository CreditorNoticeRepository => new CreditorNoticeRepository(_ctx);
        public InspectionTemplateRepository InspectionTemplateRepository => new InspectionTemplateRepository(_ctx);
        public ConfigrationRepository ConfigrationRepository => new ConfigrationRepository(_ctx);
        public ServiceCategoryRepository ServiceCategoryRepository => new ServiceCategoryRepository(_ctx);
        public ServiceRepository ServiceRepository => new ServiceRepository(_ctx);
        public ServiceInvoiceItemRepository ServiceInvoiceItemRepository => new ServiceInvoiceItemRepository(_ctx);
        public ServiceInvoiceRepository ServiceInvoiceRepository => new ServiceInvoiceRepository(_ctx);
        public ChatRepository ChatRepository => new ChatRepository(_ctx);



        public InspectionSectionRepository InspectionSectionRepository => new InspectionSectionRepository(_ctx);
        public InspectionItemsRepository InspectionItemsRepository => new InspectionItemsRepository(_ctx);
        public InspectionReportRepository InspectionReportRepository => new InspectionReportRepository(_ctx);

        public ReceiptVouchersRepository ReceiptVouchersRepository => new ReceiptVouchersRepository(_ctx);
        public PaymentAndReceiptVoucherRepository PaymentAndReceiptVoucherRepository => new PaymentAndReceiptVoucherRepository(_ctx);
        public LogRepository LogRepository => new LogRepository(_ctx);
        public ExpensesCategoryRepository ExpensesCategoryRepository => new ExpensesCategoryRepository(_ctx);
        public ExpensesTypeRepository ExpensesTypeRepository => new ExpensesTypeRepository(_ctx);
        public ExpensesTransactionRepository ExpensesTransactionRepository => new ExpensesTransactionRepository(_ctx);
        public DebitAndCreditorRepository DebitAndCreditorRepository => new DebitAndCreditorRepository(_ctx);
        public NoticeProductRepository NoticeProductRepository => new NoticeProductRepository(_ctx);
        public RoHistoryRepository RoHistoryRepository => new RoHistoryRepository(_ctx);

        public SupplierRepository SupplierRepository => new SupplierRepository(_ctx);

       public InvoiceItemRepository InvoiceItemRepository => new InvoiceItemRepository(_ctx);

        public InspectionWarshahReportRepository InspectionWarshahReportRepository => new InspectionWarshahReportRepository(_ctx);

        public BankRepository BankRepository => new BankRepository(_ctx);


        public BalanceBankRepository BalanceBankRepository => new BalanceBankRepository(_ctx);

        public BoxRepository  BoxRepository => new BoxRepository(_ctx);

        public ChequeRepository ChequeRepository => new ChequeRepository(_ctx);

        public AuthrizedPersonRepository AuthrizedPersonRepository => new AuthrizedPersonRepository(_ctx);
        public AppointmentRepository AppointmentRepository => new AppointmentRepository(_ctx);
        public WarshahParamsRepository WarshahParamsRepository => new WarshahParamsRepository(_ctx);
        public SpicialistsRepository SpicialistsRepository => new SpicialistsRepository(_ctx);
        public WarshahServiceTypeRepository WarshahServiceTypeRepository => new WarshahServiceTypeRepository(_ctx);
        public WarshahTypeRepository WarshahTypeRepository => new WarshahTypeRepository(_ctx);
        public SalesRequestRepository SalesRequestRepository => new SalesRequestRepository(_ctx);


        public WarshahBankRepository WarshahBankRepository => new WarshahBankRepository(_ctx);
        public TaseerSupplierRepository TaseerSupplierRepository => new TaseerSupplierRepository(_ctx);
        public SparePartTaseerRepository SparePartTaseerRepository => new SparePartTaseerRepository(_ctx);
        public SalesRequestListRepository SalesRequestListRepository => new SalesRequestListRepository(_ctx);
        public DurationRepository DurationRepository => new DurationRepository(_ctx);
        public SubscribtionRepository SubscribtionRepository => new SubscribtionRepository(_ctx);

        public ServicesWarshahRepository ServicesWarshahRepository => new ServicesWarshahRepository(_ctx);

        public WarshahModelCarRepository WarshahModelCarRepository => new WarshahModelCarRepository(_ctx);


        public WarshahFixedTypeRepository WarshahFixedTypeRepository => new WarshahFixedTypeRepository(_ctx);


        public SubscribtionInvoicerepository SubscribtionInvoicerepository => new SubscribtionInvoicerepository(_ctx);
        public PersonDiscountRepository PersonDiscountRepository => new PersonDiscountRepository(_ctx);
        public WarshahShiftRepository WarshahShiftRepository => new WarshahShiftRepository(_ctx);


        public WarshahVatRepository WarshahVatRepository => new WarshahVatRepository(_ctx);


        public FixedCountryMotorRepository FixedCountryMotorRepository => new FixedCountryMotorRepository(_ctx);

        public WarshahCountryMotorRepository WarshahCountryMotorRepository => new WarshahCountryMotorRepository(_ctx);
        public WorkTimeRepository WorkTimeRepository => new WorkTimeRepository(_ctx);


        public FixedBankRepository FixedBankRepository => new FixedBankRepository(_ctx);
        public LoyalityPointRepository LoyalityPointRepository => new LoyalityPointRepository(_ctx);
        public NotificationRepository NotificationRepository => new NotificationRepository(_ctx);


        public OpeningBalanceRepository OpeningBalanceRepository => new OpeningBalanceRepository(_ctx);
        public LoyalitySettingRepository LoyalitySettingRepository => new LoyalitySettingRepository(_ctx);

        public TransactionTodayRepository TransactionTodayRepository => new TransactionTodayRepository(_ctx);


        public OpenBankBalanceRepository OpenBankBalanceRepository => new OpenBankBalanceRepository(_ctx);

        public OpenSpartPartRepository OpenSpartPartRepository => new OpenSpartPartRepository(_ctx);

        public OpenCheaqeRepository OpenCheaqeRepository => new OpenCheaqeRepository(_ctx);
        public LoyalitySettingRevarseRepository LoyalitySettingRevarseRepository => new LoyalitySettingRevarseRepository(_ctx);
        public SMSCountRepository SMSCountRepository => new SMSCountRepository(_ctx);
        public SMSPackageRepository SMSPackageRepository => new SMSPackageRepository(_ctx);
        public SMSInvoiceRepository SMSInvoiceRepository => new SMSInvoiceRepository(_ctx);


        public NationaltyRepository NationaltyRepository => new NationaltyRepository(_ctx);

        public StatusEmployementRepository StatusEmployementRepository => new StatusEmployementRepository(_ctx);

        public EmployeeShiftRepository EmployeeShiftRepository => new EmployeeShiftRepository(_ctx);

        public ContractTypeRepository ContractTypeRepository => new ContractTypeRepository(_ctx);

        public GenderRepository GenderRepository => new GenderRepository(_ctx);
        public MartialStatusRepository MartialStatusRepository => new MartialStatusRepository(_ctx);

        public DataEmployeeRepository DataEmployeeRepository => new DataEmployeeRepository(_ctx);
        public ContactUSRepository ContactUSRepository => new ContactUSRepository(_ctx);

        public BonusTechnicalRepository BonusTechnicalRepository => new BonusTechnicalRepository(_ctx); 

        public ItemSalaryRepository ItemSalaryRepository => new ItemSalaryRepository(_ctx); 
        public RecordBonusRepository RecordBonusRepository => new RecordBonusRepository(_ctx);
        public CuponRepository CuponRepository => new CuponRepository(_ctx);
        public WarshahDisableReasonRepository WarshahDisableReasonRepository => new WarshahDisableReasonRepository(_ctx);

        public TransactionInventoryRepository TransactionInventoryRepository => new TransactionInventoryRepository(_ctx);

        public BoxBankRepository BoxBankRepository => new BoxBankRepository(_ctx);
        public GlobalSettingRepository GlobalSettingRepository => new GlobalSettingRepository(_ctx);
        public ResonToRejectOrderRepository ResonToRejectOrderRepository => new ResonToRejectOrderRepository(_ctx);
        public RepairOrderServicesRepository RepairOrderServicesRepository => new RepairOrderServicesRepository(_ctx);
        public SMSHistoryRepository SMSHistoryRepository => new SMSHistoryRepository(_ctx);

        public JobtitleRepository JobtitleRepository => new JobtitleRepository(_ctx);
        public ClientRequestRepository ClientRequestRepository => new ClientRequestRepository(_ctx);

        public ClaimInvoiceRepository ClaimInvoiceRepository => new ClaimInvoiceRepository(_ctx);

        public ChequeBankAccountRepository ChequeBankAccountRepository => new ChequeBankAccountRepository(_ctx);

        public TransferRepository TransferRepository => new TransferRepository(_ctx);

        public TransferBankAccountRepository TransferBankAccountRepository => new TransferBankAccountRepository(_ctx);
        public CuponHistoryRepository CuponHistoryRepository => new CuponHistoryRepository(_ctx);

        public NotificationSettingRepository NotificationSettingRepository => new NotificationSettingRepository(_ctx);

        public NameNotificationRepository NameNotificationRepository => new NameNotificationRepository(_ctx);


        public OldjobRepository OldjobRepository => new OldjobRepository (_ctx);


        public DelayOrderRepository DelayOrderRepository => new DelayOrderRepository(_ctx);

     public   NotificationRepairOrderAddingRepository NotificationRepairOrderAddingRepository => new NotificationRepairOrderAddingRepository(_ctx);

       public NotificationRepairOrderRepository NotificationRepairOrderRepository => new NotificationRepairOrderRepository(_ctx);


        public RequestTypeRepository RequestTypeRepository => new RequestTypeRepository(_ctx);
        public WorkTypeRepository WorkTypeRepository => new WorkTypeRepository(_ctx);

        public OldInvoicesRepository OldInvoicesRepository => new OldInvoicesRepository(_ctx);

        public OldItemInvoiceRepository OldItemInvoiceRepository => new OldItemInvoiceRepository(_ctx);

       public WarshahCarOwnersRepository WarshahCarOwnersRepository => new WarshahCarOwnersRepository(_ctx);

        public SalesInvoiceRepository SalesInvoiceRepository => new SalesInvoiceRepository(_ctx);


        public SalesInvoiceItemRepository SalesInvoiceItemRepository => new SalesInvoiceItemRepository(_ctx);

        public WarshahTechServiceRepository WarshahTechServiceRepository => new WarshahTechServiceRepository(_ctx);

        public WarshahMobileRepository WarshahMobileRepository => new WarshahMobileRepository(_ctx);

        public PriceOfferRepository PriceOfferRepository => new PriceOfferRepository(_ctx);

        public PriceOfferItemRepository PriceOfferItemRepository => new PriceOfferItemRepository(_ctx);

        public ServicePriceOfferRepository ServicePriceOfferRepository => new ServicePriceOfferRepository(_ctx);

        public WarshahReportRepository WarshahReportRepository => new WarshahReportRepository(_ctx);

        public RegisterFormRepository RegisterFormRepository => new RegisterFormRepository(_ctx);

        public GTaxControlRepository GTaxControlRepository => new GTaxControlRepository(_ctx);  

        public InstantPartRepository InstantPartRepository => new InstantPartRepository(_ctx);

        public void Dispose()
        {
            _ctx.Dispose();
        }



        public int Save()
        {
            return _ctx.SaveChanges();
        }
    }
}
