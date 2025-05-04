

using AutoMapper;
using DL.DTOs;
using DL.DTOs.AddressDTOs;
using DL.DTOs.BalanceDTO;
using DL.DTOs.ExpensesDTOs;
using DL.DTOs.HR;
using DL.DTOs.InspectionDTOs;
using DL.DTOs.InvoiceDTOs;
using DL.DTOs.JobCardDtos;
using DL.DTOs.MotorsDTOs;
using DL.DTOs.RequestType;
using DL.DTOs.Sales;
using DL.DTOs.SparePartsDTOs;
using DL.DTOs.SubscribtionDTOs;
using DL.DTOs.SuppliersDTOs;
using DL.DTOs.SupportServiceDTO;
using DL.DTOs.TransactionsDTO;
using DL.DTOs.UserDTOs;
using DL.DTOs.WorkType;
using DL.Entities;
using DL.Entities.HR;
using DL.MailModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL.Mapping
{

    public class MappingConfigration : Profile
    {

        
        public MappingConfigration()
        {
            // Add as many of these lines as you need to map your objects
           CreateMap<User, UserDTO>(MemberList.Source).ReverseMap();
           CreateMap<User, UserBackofficeDTO>(MemberList.Source).ReverseMap();
           CreateMap<Warshah, WarshahDTO>(MemberList.Source).ForMember(x => x.WarshahLogo, opt => opt.Ignore()).ReverseMap();
           CreateMap<Warshah, EditWarshahDTO>(MemberList.Source).ReverseMap();
           CreateMap<MotorMake, MotorMakeDTO>(MemberList.Source).ReverseMap();
           CreateMap<MotorModel, MotorModelDTO>(MemberList.Source).ReverseMap();
           CreateMap<MotorMake, EditMotorMakeDTO>(MemberList.Source).ReverseMap();
           CreateMap<MotorModel, EditMotorModelDTO>(MemberList.Source).ReverseMap();
           CreateMap<MotorColor, MotorColorDTO>(MemberList.Source).ReverseMap();
           CreateMap<MotorColor, EditMotorColorDTO>(MemberList.Source).ReverseMap();
            CreateMap<MotorYear, MotorYearDTO>(MemberList.Source).ReverseMap();
            CreateMap<MotorYear, EditMotorYearDTO>(MemberList.Source).ReverseMap();
            CreateMap<Motors, MotorsDTO>(MemberList.Source).ReverseMap();
            CreateMap<Motors, EditMotorsDTO>(MemberList.Source).ReverseMap();
            CreateMap<Country, CountryDTO>(MemberList.Source).ReverseMap();
            CreateMap<Country, EditCountryDTO>(MemberList.Source).ReverseMap();
            CreateMap<Region, RegionDTO>(MemberList.Source).ReverseMap();
            CreateMap<Region, EditRegionDTO>(MemberList.Source).ReverseMap();
            CreateMap<City, CityDTO>(MemberList.Source).ReverseMap();
            CreateMap<City, EditCityDTO>(MemberList.Source).ReverseMap();
            CreateMap<ReciptionOrder, ReciptionOrderDTO>(MemberList.Source).ReverseMap();
            CreateMap<CategorySpareParts, CategorySparePartsDTO>(MemberList.Source).ReverseMap();
            CreateMap<CategorySpareParts, EditCategorySparePartsDTO>(MemberList.Source).ReverseMap();
            CreateMap<SubCategoryParts, SubCategoryPartsDTO>(MemberList.Source).ReverseMap();
            CreateMap<SubCategoryParts, EditSubCategoryPartsDTO>(MemberList.Source).ReverseMap();
            CreateMap<SparePart, SparePartDTO>(MemberList.Source).ReverseMap();
            CreateMap<SparePart, EditSparePartDTO>(MemberList.Source).ReverseMap();
            CreateMap<User, EditUserDTO>(MemberList.Source).ReverseMap();
            CreateMap<RepairOrder, RepairOrderDTO>(MemberList.Source).ReverseMap();
            CreateMap<RepairOrderImage, RepairOrderImageDTO>(MemberList.Source).ForMember(x => x.ImageName, opt => opt.Ignore()).ReverseMap();
            CreateMap<RepairOrderSparePart, RepairOrderSparePartsDTO>(MemberList.Source).ReverseMap();
            CreateMap<Invoice, InvoiceDTO>(MemberList.Source).ReverseMap();
            CreateMap<Invoice, EditInvoiceDTO>(MemberList.Source).ReverseMap();
            CreateMap<PaymentTypeInvoice, PaymentTypeInvoiceDTO>(MemberList.Source).ReverseMap();
            CreateMap<PaymentTypeInvoice, EditPaymentInvoiceDTO>(MemberList.Source).ReverseMap();
            CreateMap<User, CarOwnerDTO>(MemberList.Source).ForMember(x => x.IdImage, opt => opt.Ignore()).ReverseMap();
            CreateMap<DebitNotice, DebitNoticeDTO>(MemberList.Source).ReverseMap();
            CreateMap<CreditorNotice, CreditorNoticeDTO>(MemberList.Source).ReverseMap();
            CreateMap<CreditorNotice, EditCreaditorNoticeDTO>(MemberList.Source).ReverseMap();
            CreateMap<InspectionTemplate, InspectionTemplateDTO>(MemberList.Source).ReverseMap();
            CreateMap<InspectionTemplate, EditInspectionTemplateDTO>(MemberList.Source).ReverseMap();
            CreateMap<InspectionSection, InspectionSectionDTO>(MemberList.Source).ReverseMap();
            CreateMap<InspectionSection, EditInspectionSectionDTO>(MemberList.Source).ReverseMap();
            CreateMap<InspectionItem, InspectionItemsDTO>(MemberList.Source).ReverseMap();
            CreateMap<InspectionItem, EditInspectionItemsDTO>(MemberList.Source).ReverseMap();
            CreateMap<InspectionReport, InspectionReportDTO>(MemberList.Source).ReverseMap();
            CreateMap<InspectionReport, EditInspectionReportDTO>(MemberList.Source).ReverseMap();
            CreateMap<Service, ServiceDTO>(MemberList.Source).ReverseMap();
            CreateMap<ServiceCategory, ServiceCategoryDTO>(MemberList.Source).ReverseMap();
            CreateMap<ServiceInvoice, ServiceInvoiceDTO>(MemberList.Source).ReverseMap();
            CreateMap<ServiceInvoiceItem, ServiceInvoiceItemDTO>(MemberList.Source).ReverseMap();
            CreateMap<RepairOrder, EditRepairOrderDTO>(MemberList.Source).ReverseMap();
            CreateMap<ReceiptVoucher, ReceiptVoucherDTO>(MemberList.Source).ReverseMap();
            CreateMap<ReceiptVoucher, EditReceiptVoucherDTO>(MemberList.Source).ReverseMap();
            CreateMap<PaymentAndReceiptVoucher, PaymentAndReceiptVoucherDTO>(MemberList.Source).ReverseMap();
            CreateMap<PaymentAndReceiptVoucher, EditPaymentAndReceiptVoucherDTO>(MemberList.Source).ReverseMap();
            CreateMap<ExpensesCategory, ExpensesCategoryDTO>(MemberList.Source).ReverseMap();
            CreateMap<ExpensesCategory, EditExpensesCategoryDTO>(MemberList.Source).ReverseMap();
            CreateMap<ExpensesType, ExpensesTypeDTO>(MemberList.Source).ReverseMap();
            CreateMap<ExpensesType, EditExpensesTypeDTO>(MemberList.Source).ReverseMap();
            CreateMap<ExpensesTransaction, ExpensesTransactionDTO>(MemberList.Source).ReverseMap();
            CreateMap<ExpensesTransaction, EditExpensesTransactionDTO>(MemberList.Source).ReverseMap();
            CreateMap<DebitAndCreditor, DebitAndCreditorDTO>(MemberList.Source).ReverseMap();
            CreateMap<DebitAndCreditor, EditDebitAndCreditorDTO>(MemberList.Source).ReverseMap();
            CreateMap<NoticeProduct, NoticeProductDTO>(MemberList.Source).ReverseMap();
            CreateMap<NoticeProduct, EditNoticeProductDTO>(MemberList.Source).ReverseMap();
            CreateMap<RoHistory, RoHistoryDTO>(MemberList.Source).ReverseMap();
            CreateMap<Supplier, SupplierDTO>(MemberList.Source).ReverseMap();
            CreateMap<Supplier, EditSupplierDTO>(MemberList.Source).ReverseMap();
            CreateMap<InvoiceItem, InvoiceItemDTO>(MemberList.Source).ReverseMap();
            CreateMap<InspectionWarshahReport, InspectionWarshahReportDTO>(MemberList.Source).ReverseMap();
            CreateMap<Bank, BankDTO>(MemberList.Source).ReverseMap();
            CreateMap<Bank, EditBankDTO>(MemberList.Source).ReverseMap();
            CreateMap<BalanceBank, BalanceBankDTO>(MemberList.Source).ReverseMap();
            CreateMap<BalanceBank, EditBalanceBankDTO>(MemberList.Source).ReverseMap();
            CreateMap<BoxMoney, BoxMoneyDTO>(MemberList.Source).ReverseMap();
            CreateMap<BoxMoney, EditBoxMoneyDTO>(MemberList.Source).ReverseMap();
            CreateMap<Cheque, ChequeDTO>(MemberList.Source).ReverseMap();
            CreateMap<Cheque, EditChequeDTO>(MemberList.Source).ReverseMap();
            CreateMap<WarshahBank, WarshahBankDTO>(MemberList.Source).ReverseMap();
            CreateMap<WarshahBank, EditWarshahBankDTO>(MemberList.Source).ReverseMap();
            CreateMap<TaseerSupplier, TaseerSupplierDTO>(MemberList.Source).ReverseMap();
            CreateMap<TaseerSupplier, EditTaseerSupplierDTO>(MemberList.Source).ReverseMap();
      
            CreateMap<SparePartTaseer, TaseerSparePartDTO>(MemberList.Source).ReverseMap();
            CreateMap<SparePartTaseer, EditTaseerSparePartDTO>(MemberList.Source).ReverseMap();

            CreateMap<WarshahType, WarshahTypeDTO>(MemberList.Source).ReverseMap();

            CreateMap<ServiceWarshah, ServicesWarshahDTO>(MemberList.Source).ReverseMap();

            CreateMap<WarshahModelsCar, WarshahModelCarDTO>(MemberList.Source).ReverseMap();
            CreateMap<WarshahFixedType, WarshahFixedTypeDTO>(MemberList.Source).ReverseMap();
            CreateMap<SubscribtionInvoice, SubscribtionInvoiceDTO>(MemberList.Source).ReverseMap();

            CreateMap<OpeningBalance, OpenBalanceDTO>(MemberList.Source).ReverseMap();
            CreateMap<OpeningBalance, EditOpenBalanceDTO>(MemberList.Source).ReverseMap();

            CreateMap<TransactionsToday, TransactionsTodayDTO>(MemberList.Source).ReverseMap();

           

            CreateMap<OpenSpartPart, OpenSpartPartDTO>(MemberList.Source).ReverseMap();


            CreateMap<OpenBalanceCheque, OpenChequesDTO>(MemberList.Source).ReverseMap();

            CreateMap<OpenBalanceBank, OpenBankDTO >(MemberList.Source).ReverseMap();
            CreateMap<Warshah, WarshahFromAdminDTO>(MemberList.Source).ReverseMap();
            CreateMap<User, WarshahFromAdminDTO>(MemberList.Source).ReverseMap();

            CreateMap<Nationality, NationalityDTO>(MemberList.Source).ReverseMap();

            CreateMap<StatusEmployment, StatusEmploymentDTO>(MemberList.Source).ReverseMap();
            CreateMap<EmployeeShift, EmployeeShiftDTO>(MemberList.Source).ReverseMap();
            CreateMap<ContractType, ContractTypeDT>(MemberList.Source).ReverseMap();
            CreateMap<Gender , GenderDTO>(MemberList.Source).ReverseMap();

            CreateMap<MaritalStatus, MaritalStatusDTO>(MemberList.Source).ReverseMap();

            CreateMap<DataEmployee , DataEmployeeDTO>(MemberList.Source).ReverseMap();

            CreateMap<DataEmployee , EditDataEmployee>(MemberList.Source).ReverseMap();

            CreateMap<BonusTechnical , BonusTechnicalDTO>(MemberList.Source).ReverseMap();

            CreateMap<BonusTechnical , EditBonusTechnical>(MemberList.Source).ReverseMap();  

            CreateMap<ItemSalary , ItemSalaryDTO>(MemberList.Source).ReverseMap();

            CreateMap<ItemSalary , EditItemSalary>(MemberList.Source).ReverseMap();
            
            CreateMap<RecordBonusTechnical , RecordBonusTechnicalDTO>(MemberList.Source).ReverseMap();

            CreateMap<BoxBank, BoxBankDTO>(MemberList.Source).ReverseMap();

            CreateMap<JobTitle, JobTitleDTO>(MemberList.Source).ReverseMap();
            CreateMap<ClaimInvoice, ClaimInvoiceDTO>(MemberList.Source).ReverseMap();

            CreateMap<ChequeBankAccount, ChequeBankAccountDTO>(MemberList.Source).ReverseMap();
            CreateMap<ChequeBankAccount, EditChequeBankAccountDTO>(MemberList.Source).ReverseMap();


            CreateMap<Transfer, TransferDTO>(MemberList.Source).ReverseMap();


            CreateMap<Transfer, EditTransferDTO>(MemberList.Source).ReverseMap();


            CreateMap<NameNotification, NameNotificationDTO>(MemberList.Source).ReverseMap();

            CreateMap<NotificationSetting, NotificationSettingDTO>(MemberList.Source).ReverseMap();
            CreateMap<NotificationSetting, EditNotificationSettingDTO>(MemberList.Source).ReverseMap();


            CreateMap<DelayRepairOrder, DelayRepairOrderDTO>(MemberList.Source).ReverseMap();


            CreateMap<DelayRepairOrder, EditDelayRepairOrderDTO>(MemberList.Source).ReverseMap();


            CreateMap<NotificationRepairOrderAdding, NotificationRepairOrderAddingDTO>(MemberList.Source).ReverseMap();

            CreateMap<NotificationRepairOrder, NotificationRepairOrderDTO>(MemberList.Source).ReverseMap();

            CreateMap<NotificationRepairOrder, EditNotificationRepairOrderDTO>(MemberList.Source).ReverseMap();

            CreateMap<WorkType, WorkTypeDTO>(MemberList.Source).ReverseMap();
            CreateMap<RequestType, RequestTypeDto>(MemberList.Source).ReverseMap();

            CreateMap<OldInvoice, OldInvoiceDTO>(MemberList.Source).ReverseMap();
            CreateMap<OldInvoice, EditOldInvoiceDTO>(MemberList.Source).ReverseMap();

            CreateMap<SalesInvoice, SalesInvoiceDTO>(MemberList.Source).ReverseMap();
            CreateMap<SalesInvoice, EditSalesInvoiceDTO>(MemberList.Source).ReverseMap();

            CreateMap<WarshahMobile, WarshahMobileDTO>(MemberList.Source).ReverseMap();

            CreateMap<PriceOffer, PriceOfferDTO>(MemberList.Source).ReverseMap();

            CreateMap<PriceOffer, EditPriceOfferDTO>(MemberList.Source).ReverseMap();

            CreateMap<PriceOfferItem, PriceOfferItemDTO>(MemberList.Source).ReverseMap();


            CreateMap<PriceOffer, DataPriceOfferDTO>(MemberList.Source).ReverseMap();

            CreateMap<ServicePriceOffer, ServicePriceOfferDTO>(MemberList.Source).ReverseMap();


            CreateMap<InspectionWarshahReport, EditInspectionWarshahReportDTO>(MemberList.Source).ReverseMap();

            CreateMap<RegisterForm, RegisterFormto>(MemberList.Source).ReverseMap();

            CreateMap<InstantPart, InstantPartDTO>(MemberList.Source).ReverseMap();
            CreateMap<InstantPart, EditInstantPartDTO>(MemberList.Source).ReverseMap();

            CreateMap<SupportService, SupportServiceDTO>(MemberList.Source).ReverseMap();
            CreateMap<SupportService, EditSupportServiceDTO>(MemberList.Source).ReverseMap();

        }
    }
}
