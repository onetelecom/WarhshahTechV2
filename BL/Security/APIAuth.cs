using BL.Infrastructure;
using BL.Security;
using DL.DTO;
using DL.DTOs.SharedDTO;
using DL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Model;
using Model.ApiModels;

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace BL.Security
{
    public interface IAuthenticateService
    {
        User AuthenticateUser(ApiLoginModelDTO request, out string token);
    }
    public interface ICheckUniqes 
    {
        List<string> CheckUniqeValue(UniqeDTO request);
       

    }
    public interface IUserManagementService
    {
        User IsValidUser(string username, string password);

    }
    public class ChekUniqeSer : ICheckUniqes
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserManagementService _userManagementService;
        private readonly TokenManagement _tokenManagement;

        public ChekUniqeSer(IUnitOfWork unitOfWork, IUserManagementService service, IOptions<TokenManagement> tokenManagement)
        {
            _userManagementService = service;
            _tokenManagement = tokenManagement.Value;
            _unitOfWork = unitOfWork;
        }
        public List<string> CheckUniqeValue(UniqeDTO request)
        {



            List<string> Erorrs = new List<string>();
            var Email = _unitOfWork.UserRepository.GetMany(a => a.Email == request.Email).FirstOrDefault();
       
            var Mobil = _unitOfWork.UserRepository.GetMany(a => a.Phone == request.Mobile).FirstOrDefault();
         
            if (Email != null &&request.Email!=null)
            {
                Erorrs.Add("Email Already Exist");
            }
          
            if (Mobil != null)
            {
                Erorrs.Add("Mobile Already Exist");
            }
           
            return Erorrs;
        }















      
    }
    public class TokenAuthenticationService : IAuthenticateService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserManagementService _userManagementService;
        private readonly TokenManagement _tokenManagement;

        public TokenAuthenticationService(IUnitOfWork unitOfWork, IUserManagementService service, IOptions<TokenManagement> tokenManagement)
        {
            _userManagementService = service;
            _tokenManagement = tokenManagement.Value;
            _unitOfWork = unitOfWork;
        }

        public User AuthenticateUser(ApiLoginModelDTO request, out string token)
        {

            token = string.Empty;
            var user = _userManagementService.IsValidUser(request.Cred, request.Password);
            if (user != null)
            {
                ////////////////////


                var AllPers = _unitOfWork.UserpermissionRepository.GetMany(a => a.UserId == user.Id).ToList();

                ///////////////
                List<Claim> ClaimList = new List<Claim>();
                foreach (var item in AllPers)
                {
                    if (item.PermissionId == 10)
                    {
                        item.PermissionId = 11;
                    }
                    var Per = _unitOfWork.PermissionRepository.GetMany(a => a.Id == item.PermissionId).FirstOrDefault();
                    ClaimList.Add(new Claim(ClaimTypes.Role, Per.Name));

                }
                ClaimList.Add(new Claim(ClaimTypes.Name, request.Cred));
                ClaimList.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                if (user.WarshahId!=null)
                {
                  
                    ClaimList.Add(new Claim(ClaimTypes.Sid, user.WarshahId.ToString()));

                }

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenManagement.Secret));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
                var expireDate = DateTime.Now.AddMinutes(_tokenManagement.AccessExpiration);

                var tokenDiscriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(ClaimList),
                    Expires = expireDate,
                    SigningCredentials = credentials
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenObj = tokenHandler.CreateToken(tokenDiscriptor);
                token = tokenHandler.WriteToken(tokenObj);
            }
            return user;
        }
    }
    public class UserManagementService : IUserManagementService
    {
        private readonly IUnitOfWork _uow;
        public UserManagementService(IUnitOfWork uow) { _uow = uow; }

        public User IsValidUser(string Mobile, string password)
        {
            var user = _uow.UserRepository.GetMany(ent => ent.Phone.ToLower() == Mobile.ToLower().Trim() && ent.Password == EncryptANDDecrypt.EncryptText(password) || ent.Email.ToLower() == Mobile.ToLower().Trim() && ent.Password == EncryptANDDecrypt.EncryptText(password)).ToHashSet();
            return user.Count() == 1 ? user.FirstOrDefault() : null;
        }







    }
}
