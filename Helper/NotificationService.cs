using BL.Infrastructure;
using DL.DTOs.InvoiceDTOs;
using DL.Entities;
using DL.MailModels;
using HELPER;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Helper
{
    public interface INotificationService
    {
        void SetNotification(int UserId, string Text);

        void SetNotificationTaqnyat(int UserId, string Text);

       

    }
    public class NotificationService : INotificationService
    {
        private string bearer = "8a6bd5813cb919536bfded6403f68d14";
        private string sender = "WARSHAHTECH";
        private readonly IUnitOfWork _uow;
        public readonly ISMS _SMS;
        private readonly IMailService _mailService;
     //  private readonly IChatHub _chatHub;

        public NotificationService(/*IChatHub chatHub,*/IUnitOfWork uow, ISMS SMS, IMailService _mailService)
        {
            _uow = uow;
            _SMS = SMS;
            this._mailService = _mailService;
           //_chatHub = chatHub;

        }
        public void SetNotification(int UserId , string Text)
        {
            try
            {
                //Set Notification In Table 
                var Noti = new Notification();
                Noti.NotificationText = Text;
                Noti.UserId = UserId;
                Noti.CreatedOn = DateTime.Now;
                Noti.UpdatedOn = DateTime.Now;
                _uow.NotificationRepository.Add(Noti);
                _uow.Save();

                //Send Notification In SMS And Mail

                var User = _uow.UserRepository.GetById(UserId);
                if (User != null)
                {
                    var s = _SMS.SendSMS(User.Phone, Text, User.WarshahId, false);
                    if (User.Email != null)
                    {
                        var email = _mailService.Notification(User.Email, Text);

                    }
                }
                //_chatHub.ReceiveNotification(User.WarshahId.ToString());
            }
            catch (Exception ex)
            {

               
            }
           
        }

        public void SetNotificationTaqnyat(int UserId, string Text)
        {
            try
            {
                //Set Notification In Table 
                var Noti = new Notification();
                Noti.NotificationText = Text;
                Noti.UserId = UserId;
                Noti.CreatedOn = DateTime.Now;
                Noti.UpdatedOn = DateTime.Now;
                _uow.NotificationRepository.Add(Noti);
                _uow.Save();

                //Send Notification In SMS And Mail

                var User = _uow.UserRepository.GetById(UserId);
                if (User != null)
                {
                    var s = _SMS.SendSMS1(bearer, User.Phone, sender, Text);
                    if (User.Email != null)
                    {
                        var email = _mailService.Notification(User.Email, Text);

                    }
                }
                //_chatHub.ReceiveNotification(User.WarshahId.ToString());
            }
            catch (Exception ex)
            {


            }

        }
    }
}
