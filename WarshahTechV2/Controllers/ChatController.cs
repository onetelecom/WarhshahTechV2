using BL.Infrastructure;
using DL.Entities;
using Helper;
using HELPER;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WarshahTechV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]

    public class ChatController : ControllerBase
    {
        // take instance from IUnitOfWork
        private readonly IUnitOfWork _uow;

        private readonly IHubContext<ChatHub> hubContext;


        // Constractor for controller 
        public ChatController(IHubContext<ChatHub> hubContext,IUnitOfWork uow)
        {
          
            _uow = uow;
            this.hubContext = hubContext;
     
        }
        [HttpPost,Route("AddMessage")]
        public IActionResult AddMessage(Chat chat)
        {
            chat.Date = DateTime.Now;
            _uow.ChatRepository.Add(chat);
            _uow.Save();
            SendMessage(new ChatMessage { ConnectionId = "mosa", DateTime = DateTime.Now, Text = "mosa", userId = "mosa" });
            return Ok(chat);
        }
        [HttpPost]
        public async Task SendMessage(ChatMessage message)
        {
            //additional business logic 

            await this.hubContext.Clients.All.SendAsync("messageReceivedFromApi", message);
            //await this.hubContext.Clients.User(message.userId).SendAsync("s",message);


            //additional business logic 
        }
      
        [HttpGet,Route("GetChatMessages")]
        public IActionResult GetChatMessages(int RepairOrderId)
        {
            var AllChats = _uow.ChatRepository.GetMany(a => a.RepairOrderId == RepairOrderId).ToHashSet();
            return Ok(AllChats);
        }
        [HttpGet, Route("GetChatByReciverId")]
        public IActionResult GetChatByReciverId(int senderId,int reciverId)
        {
            var AllChats = _uow.ChatRepository.GetMany(a => a.SenderId == senderId&&a.ReciverId==reciverId).ToHashSet();
            return Ok(AllChats);
        }

        [HttpGet,Route("GetUserChats")]
        public IActionResult GetUserChats(int UserId)
        {
            var SendedChatsMessages = _uow.ChatRepository.GetMany(a=>a.SenderId== UserId).ToHashSet();
            var RecivedChatsMessages = _uow.ChatRepository.GetMany(a=>a.ReciverId== UserId).ToHashSet();
            var AllChats = new List<Chat>();
            AllChats.AddRange(SendedChatsMessages);
            AllChats.AddRange(RecivedChatsMessages);
           var NoDupChats = AllChats.Distinct().ToList();

             List<object> ChatList  = new List<object>();

            foreach (var item in NoDupChats)
            {
                if (item.SenderId!=UserId)
                {
                    var User = _uow.UserRepository.GetById(item.SenderId);
                    var Op = new
                    {
                        User = User,
                        ChatId = item.Id
                    };
                ChatList.Add(Op);
                }
                if (item.ReciverId != UserId)
                {
                    var User = _uow.UserRepository.GetById(item.ReciverId);
                    var Op = new
                    {
                        User = User,
                        ChatId = item.Id
                    };
                    ChatList.Add(Op);


                }
            }
            return Ok(ChatList);
        }
        [HttpGet, Route("GetOutChatMessages")]
        public IActionResult GetOutChatMessages(int ChatId)
        {
            var AllChats = _uow.ChatRepository.GetMany(a => a.Id == ChatId).ToHashSet();
            return Ok(AllChats);
        }


    }
}
