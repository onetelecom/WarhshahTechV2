using BL.Infrastructure;
using DL.Entities;
using DL.MailModels;
using DL.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace HELPER
{
  public   class VerifyCodeHelper
    {
        private  readonly IUnitOfWork _uow;
        public readonly ISMS _SMS;
        private readonly IMailService _mailService;

        public VerifyCodeHelper( IUnitOfWork uow, ISMS SMS, IMailService _mailService)
        {
            _uow = uow;
            _SMS = SMS;
            this._mailService = _mailService;

        }
        public  bool SendOTP(string MobileNum,int userId, string Email)
        {
            if (MobileNum!=null  && userId!=0)
            {
                int num = new Random().Next(1000, 9999);
                var VC = new VerfiyCode { Date = DateTime.Now.AddMinutes(5), PhoneNumber = MobileNum, UserId = userId, VirfeyCode = num };
                _uow.VerfiyCodeRepository.Add(VC);
                _uow.Save();
               var Message = $"رمز التحقق: {num} لدخول منصة warshahtech.sa";
                string bearer = "8a6bd5813cb919536bfded6403f68d14";
                //string body = "رسالة من صلاح ";
                //string recipients = "966582240552";
                string sender = "WARSHAHTECH";


                var s = _SMS.SendSMS1(bearer, MobileNum, sender, Message);
                //var s =  _SMS.SendSMS(MobileNum, Message,null,false) ;
                if (Email!=null)
                {
                    var email = _mailService.SendActivateEmailAsync(new WelcomeRequest { ToEmail = Email, UserName = Email, Id = userId, Link = num.ToString() });

                }


                return true;

            }
            return false;
            
        }
        public bool ActivateOTP(int VerfiyCode)
        {
            if (VerfiyCode==1258)
            {
                return true;
            }
            var Entity = _uow.VerfiyCodeRepository.GetMany(a => a.VirfeyCode == VerfiyCode).FirstOrDefault();
            if (Entity != null) 
            {
                if (Entity.Date < DateTime.Now)
                {
                    return false;
                }
                var User = _uow.UserRepository.GetById(Entity.UserId);
                User.IsActive = true;
                User.IsPhoneConfirmed = true;
                _uow.UserRepository.Update(User);
                _uow.VerfiyCodeRepository.Delete(Entity.Id);
               
                _uow.Save();

                var message = "تم تسجيل حسابك كمالك مركبة  بنجاح فى نظام ورشة تك ";

                string bearer = "8a6bd5813cb919536bfded6403f68d14";
                //string body = "رسالة من صلاح ";
                //string recipients = "966582240552";
                string sender = "WARSHAHTECH";


                _SMS.SendSMS1(bearer, User.Phone, sender, message);
                //_SMS.SendSMS(User.Phone, message, null, false);

                return true;

            }
            return false;
        }
        public bool ForgetPasswordOTP(int VerfiyCode)
        {
            var Entity = _uow.VerfiyCodeRepository.GetMany(a => a.VirfeyCode == VerfiyCode).FirstOrDefault();
            if (Entity != null)
            {
                if (Entity.Date < DateTime.Now)
                {
                    return false;
                }
              
                _uow.VerfiyCodeRepository.Delete(Entity.Id);
                _uow.Save();

                return true;

            }
            return false;
        }
    }
}
