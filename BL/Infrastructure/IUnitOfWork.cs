using BL.Repositories;
using DL.Entities;
using System;

namespace BL.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {

        UserRepository UserRepository { get; }
        RoleRepository RoleRepository { get; }
      
        VerfiyCodeRepository VerfiyCodeRepository { get; }
        WarshahRepository WarshahRepository { get; }

        LogRepository LogRepository { get; }
        permissionRepository PermissionRepository { get; }
        UserpermissionRepository UserpermissionRepository { get; }

        MotorMakeRepository  MotorMakeRepository { get; }

        MotorModelRepository MotorModelRepository { get; }

        MotorColorRepository MotorColorRepository { get; }

        MotorYearRepository MotorYearRepository { get; }

        MotorsRepository MotorsRepository { get; }
        CountryRepository CountryRepository { get; }
        RegionRepository RegionRepository { get; }
        CityRepository CityRepository { get; }
        WarshahCustomersRepository WarshahCustomersRepository { get; }
        ReciptionOrderRepository ReciptionOrderRepository { get; }
        CategorySparePartsRepository CategorySparePartsRepository { get; }
        SubCategoryPartsRepository SubCategoryPartsRepository { get; }
        SparePartRepository SparePartRepository { get; }
        RepairOrderRepository RepairOrderRepository { get; }
        RepairOrderSparePartRepository RepairOrderSparePartRepository { get; }
        RepairOrderImageRepository RepairOrderImageRepository { get; }
        InvoiceRepository InvoiceRepository { get; }
        PaymentTypeInvoiceRepository PaymentTypeInvoiceRepository { get; }
        DebitNoticeRepository DebitNoticeRepository { get; }
        CreditorNoticeRepository CreditorNoticeRepository { get; }
        InspectionTemplateRepository InspectionTemplateRepository { get; }
        InspectionSectionRepository InspectionSectionRepository { get; }
        InspectionItemsRepository InspectionItemsRepository { get; }
        InspectionReportRepository InspectionReportRepository { get; }
        ConfigrationRepository ConfigrationRepository { get; }
        ServiceCategoryRepository ServiceCategoryRepository { get; }
        ServiceInvoiceItemRepository ServiceInvoiceItemRepository { get; }
        ServiceRepository ServiceRepository { get; }
        ServiceInvoiceRepository ServiceInvoiceRepository { get; }
        ChatRepository ChatRepository { get; }
        ReceiptVouchersRepository ReceiptVouchersRepository { get; }
        PaymentAndReceiptVoucherRepository PaymentAndReceiptVoucherRepository { get; }
        ExpensesCategoryRepository ExpensesCategoryRepository { get; }
        ExpensesTypeRepository ExpensesTypeRepository { get; }
        ExpensesTransactionRepository ExpensesTransactionRepository { get; }
        DebitAndCreditorRepository DebitAndCreditorRepository { get; }
        NoticeProductRepository NoticeProductRepository { get; }
        RoHistoryRepository RoHistoryRepository { get; }

        SupplierRepository SupplierRepository { get; }
     
        InvoiceItemRepository  InvoiceItemRepository { get; }

        InspectionWarshahReportRepository InspectionWarshahReportRepository { get; }

        BankRepository BankRepository { get; }

        BalanceBankRepository BalanceBankRepository { get; }

        BoxRepository BoxRepository { get; }

        ChequeRepository ChequeRepository { get; }
        AuthrizedPersonRepository AuthrizedPersonRepository { get; }
        AppointmentRepository AppointmentRepository { get; }
        WarshahParamsRepository WarshahParamsRepository { get; }
        WarshahServiceTypeRepository WarshahServiceTypeRepository { get; }
        SpicialistsRepository SpicialistsRepository { get; }    
        WarshahTypeRepository WarshahTypeRepository { get; }
        SalesRequestRepository SalesRequestRepository { get; }

        WarshahBankRepository WarshahBankRepository { get; }
        TaseerSupplierRepository TaseerSupplierRepository { get; }
        SparePartTaseerRepository SparePartTaseerRepository { get; }
        SalesRequestListRepository SalesRequestListRepository { get; }


        ServicesWarshahRepository ServicesWarshahRepository { get; }

        WarshahModelCarRepository WarshahModelCarRepository { get; }

        WarshahFixedTypeRepository WarshahFixedTypeRepository { get; }


        SubscribtionRepository SubscribtionRepository { get; }
        DurationRepository DurationRepository { get; }


        SubscribtionInvoicerepository SubscribtionInvoicerepository { get; }
        PersonDiscountRepository PersonDiscountRepository { get; }
        WarshahShiftRepository WarshahShiftRepository { get; }

        WarshahVatRepository WarshahVatRepository { get; }


        WarshahCountryMotorRepository WarshahCountryMotorRepository { get; } 

        FixedCountryMotorRepository FixedCountryMotorRepository { get; }
        WorkTimeRepository WorkTimeRepository { get; }

        FixedBankRepository FixedBankRepository { get; }
        LoyalityPointRepository LoyalityPointRepository { get; }
        NotificationRepository NotificationRepository { get; }


        OpeningBalanceRepository OpeningBalanceRepository { get; }
        LoyalitySettingRepository LoyalitySettingRepository { get; }

        TransactionTodayRepository TransactionTodayRepository { get; }

        OpenBankBalanceRepository OpenBankBalanceRepository { get; }


        OpenSpartPartRepository OpenSpartPartRepository { get; }

        OpenCheaqeRepository OpenCheaqeRepository { get; }
        LoyalitySettingRevarseRepository LoyalitySettingRevarseRepository { get; }
        SMSCountRepository SMSCountRepository { get; }
        SMSInvoiceRepository SMSInvoiceRepository { get; }
        SMSPackageRepository SMSPackageRepository { get; }

        NationaltyRepository NationaltyRepository { get; }

        StatusEmployementRepository StatusEmployementRepository { get; }

        EmployeeShiftRepository EmployeeShiftRepository { get; }
        ContractTypeRepository ContractTypeRepository { get; }

        GenderRepository GenderRepository { get; }

        MartialStatusRepository MartialStatusRepository { get; }

        DataEmployeeRepository DataEmployeeRepository { get; }
        ContactUSRepository ContactUSRepository { get; }

        BonusTechnicalRepository BonusTechnicalRepository { get; }

        ItemSalaryRepository ItemSalaryRepository { get; }  

        RecordBonusRepository RecordBonusRepository { get; }    
        CuponRepository CuponRepository { get; }    
        WarshahDisableReasonRepository WarshahDisableReasonRepository { get; }

        TransactionInventoryRepository TransactionInventoryRepository { get; }

        BoxBankRepository BoxBankRepository { get; }
        GlobalSettingRepository GlobalSettingRepository { get; }
        ResonToRejectOrderRepository ResonToRejectOrderRepository { get; }
        RepairOrderServicesRepository RepairOrderServicesRepository { get; }
        SMSHistoryRepository SMSHistoryRepository { get; }

        JobtitleRepository JobtitleRepository { get; }
        ClientRequestRepository ClientRequestRepository { get; }
      

        ClaimInvoiceRepository ClaimInvoiceRepository { get; }

        ChequeBankAccountRepository ChequeBankAccountRepository { get; }

        TransferRepository TransferRepository { get; }

        TransferBankAccountRepository TransferBankAccountRepository { get; }
        CuponHistoryRepository CuponHistoryRepository { get; }


        NameNotificationRepository NameNotificationRepository { get; }


        NotificationSettingRepository NotificationSettingRepository  { get; }

       OldjobRepository OldjobRepository { get; }

        DelayOrderRepository DelayOrderRepository { get; }

        NotificationRepairOrderRepository NotificationRepairOrderRepository { get; }

        NotificationRepairOrderAddingRepository NotificationRepairOrderAddingRepository { get; }
        WorkTypeRepository WorkTypeRepository { get; }
        RequestTypeRepository RequestTypeRepository { get; }

        OldInvoicesRepository OldInvoicesRepository { get; }

        OldItemInvoiceRepository OldItemInvoiceRepository { get; }



        WarshahCarOwnersRepository WarshahCarOwnersRepository { get; }

        SalesInvoiceRepository SalesInvoiceRepository { get; }  

        SalesInvoiceItemRepository SalesInvoiceItemRepository { get; }

        WarshahTechServiceRepository WarshahTechServiceRepository { get; }

        WarshahMobileRepository WarshahMobileRepository { get; }

        PriceOfferRepository PriceOfferRepository { get; }

        PriceOfferItemRepository PriceOfferItemRepository { get; }

        ServicePriceOfferRepository ServicePriceOfferRepository { get; }

        WarshahReportRepository WarshahReportRepository { get; }

        RegisterFormRepository RegisterFormRepository { get; }

        GTaxControlRepository GTaxControlRepository { get; }

        InstantPartRepository InstantPartRepository { get; }

        SupportServiceRepository SupportServiceRepository { get; }


        int Save();
      
    }
}
