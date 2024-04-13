using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RealStateApp.Core.Application.Dto.Acccount;
using RealStateApp.Core.Application.Dto.Acccount.AuthenticateDtos;
using RealStateApp.Core.Application.Dto.Acccount.ForgotPassword;
using RealStateApp.Core.Application.Dto.Acccount.Register;
using RealStateApp.Core.Application.Dto.Acccount.ResetPassword;
using RealStateApp.Core.Application.Dto.Email;
using RealStateApp.Core.Application.Dto.JWT;
using RealStateApp.Core.Application.Enum;
using RealStateApp.Core.Application.Interfaces.IService;
using RealStateApp.Core.Domain.Settings;
using RealStateApp.Infrastructure.Identity.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace RealStateApp.Infrastructure.Identity.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IEmailService _emailService;
        private readonly  JWTSettings _jwtSettings;

        public AccountService(UserManager<AppUser> userManager,
                             //
                             SignInManager<AppUser> signInManager,
                             //
                             IEmailService emailService,
                             //
                             IOptions<JWTSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
        }

        //UPDATE USER
        public async Task<ServiceResult> Update(RegisterRequest request)
        {
            ServiceResult respponse = new();
            AppUser user = new AppUser
            {
                Name = request.FirstName,
                LastName = request.LastName,
                ImageURl = request.ImageURL,
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                Id = request.Id,
                IsActive = request.IsActive,
            };

            if (request.Password != null)
            {
                var Token = await _userManager.GeneratePasswordResetTokenAsync(user);
                await _userManager.ResetPasswordAsync(user, Token, request.Password);
            }
            var result = await _userManager.UpdateAsync(user);
            if(!result.Succeeded)
            {
                respponse.HasError = true;
                respponse.Error = $"There was an error while trying to update the user{user.UserName}";
            }
            return respponse;
        }

        //GETBYID
        public async Task<DtoAccount> GetUserById (string Id)
        {
           var user = await _userManager.FindByIdAsync(Id);
            DtoAccount dto = new()
            {
                Id = Id,
                FirstName = user.Name,
                LastName = user.LastName,
                Email = user.Email,
                IsActive = user.IsActive,
                ImageURl = user.ImageURl,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber,
                Password = user.PasswordHash,
            };
            return dto;
        }


        //CHANGE USER STATUS
        public async Task<ServiceResult> ChangeUserStatus(RegisterRequest request)
        {
            ServiceResult response = new();
            var userget = await _userManager.FindByIdAsync(request.Id);
            {
                userget.IsActive = request.IsActive;
            }
            var result = await _userManager.UpdateAsync(userget);
            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"There was an error while trying to update the user{userget.UserName}";
            }
            return response;
        }


        //GETALLUSERS
        public async Task<List<DtoAccount>> GetAllUsers()
        {

            var userList = await _userManager.Users.ToListAsync();
            List<DtoAccount> DtoUserList = new();
            foreach (var user in userList)
            {
                var userDto = new DtoAccount();

                userDto.ImageUrl = user.ImageURl;
                userDto.FirstName = user.Name;
                userDto.LastName = user.LastName;
                userDto.IsActive = user.IsActive;
                userDto.Email = user.Email;
                userDto.PhoneNumber = user.PhoneNumber;
                userDto.Id = user.Id;
                userDto.Roles = _userManager.GetRolesAsync(user).Result.ToList();
                DtoUserList.Add(userDto);
            }
            return DtoUserList;
        }

        //GETALL
        public async Task<List<DtoAccount>> FilterByUser(string Roles)
        {
            var userlist = await GetAllUsers();
            return userlist = userlist.Where(u => u.Roles.Contains(Roles) && u.IsActive == true)
                .OrderBy(x => x.FirstName).ToList();
        }

        //RESETPASSWORD
        public async Task<ServiceResult> ResetPasswordAsync(ResetPasswordRequest request)
        {
            ServiceResult response = new();

            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                response.HasError = true;
                response.Error = $"No Accounts registered with {request.Email}";
                return response;
            }

            request.Token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Token));
            var result = await _userManager.ResetPasswordAsync(user, request.Token, request.Password);

            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"An error occurred while reset password";
                return response;
            }

            return response;
        }

        //FORGOT PASSWORD
        public async Task<ServiceResult> ForgotPasswordAsync(ForgotPasswordRequest request , string origin)
        {
            ServiceResult response = new();

            var user = await _userManager.FindByEmailAsync(request.Email);

            if(user == null)
            {
                response.HasError = true;
                response.Error = $"No accounts registered with {request.Email}";
                return response;
            }

            var verificationURI = await SendForgotPasswordUri(user , origin);

            await _emailService.SendAsync(new EmailRequest()
            {
                To = user.Email,
                Body = $"Plase confirm your account visiting this URL {verificationURI}",
                Subject = "Confirm registration"
            });

            return response;
        }

        //CONFIRMACCOUNT
        public async Task<string> ConfirmAccountAysnc(string uesrId , string token)
        {
            var user = await _userManager.FindByIdAsync(uesrId);
            if(user == null)
            {
                return $"No user registered under this {user.Email}";
            }

            token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return $"Account confirm for {user.Email}";
            }
            else
            {
                return $"An error ocurred while confirming {user.Email}";
            }
        }

        //AUTHENTICATE API
        public async Task<AuthenticateResponseJWT> AuthenticateAysncAPI(AuthenticationRequest request)
        {
            AuthenticateResponseJWT response = new()
            {
                HasError = false,
            };

            var user = await _userManager.FindByEmailAsync(request.Email);
            if(user == null)
            {
                response.HasError = true;
                response.Error = $"No Accounts registered wuth {request.Email}";
                return response;
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName , request.Password , false, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"Invalid credentials for {request.Email}";
                return response;
            }

            JwtSecurityToken jwtSecutriyToken = await GenreateJWT(user);

            response.Email = user.Email;
            response.UserName = user.UserName;
            response.FirstName = user.Name;
            response.LastName = user.LastName;
            response.IsActive = user.IsActive;
            response.JWTtoken = new JwtSecurityTokenHandler().WriteToken(jwtSecutriyToken);

            var roleList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            response.Roles = roleList.ToList();
            response.IsVerified = true;
            var refreshToken = GenerateRefreshToken();
            response.RefreshTokwn = refreshToken.Token;

            return response;

        }

        //CREATE DEV 
        public async Task<ServiceResult> RegisterHighRolesUsers(RegisterRequest request, string UserRole)
        {
            ServiceResult response = new();

            var userWithSameUserName = await _userManager.FindByEmailAsync(request.UserName);
            if (userWithSameUserName != null)
            {
                response.HasError = true;
                response.Error = $"Username {request.UserName} is already taken";
                return response;
            }

            var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userWithSameEmail != null)
            {
                response.HasError = true;
                response.Error = $"Email {request.Email} is already taken ";
            }

            var user = new AppUser
            {
                Email = request.Email,
                Name = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                IsActive = true,
                ImageURl = request.ImageURL,
                PhoneNumber = request.PhoneNumber,
                EmailConfirmed = true,
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            await _userManager.AddToRoleAsync(user, UserRole);
            
            return response;
        }
        
        //USERSELECTOR
        private async Task UserRegisterSelector(RegisterRequest request, string origin ,string Role="")
        {
           var agent = RolesEnum.Agent.ToString();
           if (Role.Contains(agent) || Role.Contains(RolesEnum.Client.ToString()))
           {
               await RegisterLowRolesUser(request, origin, Role);
           }
           else if (Role.Contains(RolesEnum.Admin.ToString()) || Role.Contains(RolesEnum.Developer.ToString()))
           {
               await RegisterHighRolesUsers(request, Role);
           }
        }

        //AUTHENTICATE ACCOUNT
        public async Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request)
        {
            AuthenticationResponse response = new()
            {
                HasError = false
            };

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                response.HasError = true;
                response.Error = $"No accounts registered under Email {request.Email}";
                return response;
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"Invalid credentials for {request.Email}";
            }
            if (!user.EmailConfirmed)
            {
                response.HasError = true;
                response.Error = $"Account not confirmed for {request.Email}";
                return response;
            }
            if (user.IsActive == false)
            {
                response.HasError = true;
                response.Error = $"Your account user {request.Email} is not active please get in contact with a manager";
                return response;
            }

            response.Id = user.Id;
            response.Email = user.Email;
            response.UserName = user.UserName;

            var roleList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);

            response.Roles = roleList.ToList();
            response.IsVerified = user.EmailConfirmed;
            response.FirstName = user.Name;
            response.LastName = user.LastName;
            response.IsActive = true;
            response.ImageUrl = user.ImageURl;

            return response;
        }

        //REGISTER CLIENT
        public async Task<ServiceResult> RegisterLowRolesUser(RegisterRequest request, string origin , string UserRole)
        {
            ServiceResult response = new();

            var userWithSameUserName = await _userManager.FindByEmailAsync(request.UserName);
            if(userWithSameUserName != null)
            {
                response.HasError = true;
                response.Error = $"Username {request.UserName} is already taken";
                return response;
            }

            var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userWithSameEmail != null)
            {
                response.HasError = true;
                response.Error = $"Email {request.Email} is already taken ";
            }

            var user = new AppUser
            {
                Email = request.Email,
                Name = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                IsActive = request.IsActive,
                ImageURl = request.ImageURL,
                PhoneNumber = request.PhoneNumber
            };

            if(UserRole == RolesEnum.Client.ToString())
            {
                user.IsActive = true;
            }

            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, UserRole);
                var verificationURI = await SendVerificationUri(user, origin);
                await _emailService.SendAsync(new EmailRequest()
                {
                    To = user.Email,
                    Body = $"Please confirm your account visiting this URL {verificationURI}",
                    Subject = "Confirm registration"
                });
            }
            else
            {
                response.HasError = true;
                response.Error = $"An error occurred trying to register the user.";
                return response;
            }

            response.Error = "Please confirm your account";
            return response;
        }

        //SIGNOUT
        public async Task SignOutAync()
        {
            await _signInManager.SignOutAsync();
        }

        //CONFIRMACCOUNT
        public async Task<string> ConfirmAccountAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return $"No user register under this {user.Email} account";
            }

            token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return $"Account confirm for {user.Email} you can now  use the app";
            }
            else
            {
                return $"An error occurred wgile confirming {user.Email}.";
            }
        }

        #region PrivateMethods

        //SENDFORGOTURI
        private async Task<string> SendForgotPasswordUri(AppUser user, string origin)
        {
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var route = "User/ResetPassword";
            var Uri = new Uri(string.Concat($"{origin}/", route));
            var verificationUri = QueryHelpers.AddQueryString(Uri.ToString(), "Token", code);

            return verificationUri;
        }

        //SENDVERIFICATIONURI
        private async Task<string> SendVerificationUri(AppUser user, string origin)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var route = "User/ConfirmEmail";
            var Uri = new Uri(string.Concat($"{origin}/", route));
            var verificationUri = QueryHelpers.AddQueryString(Uri.ToString(), "userId", user.Id);
            verificationUri = QueryHelpers.AddQueryString(verificationUri, "Token", code);

            return verificationUri;
        }

        private async Task<JwtSecurityToken> GenreateJWT(AppUser user)
        {
            var userClaim = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaim = new List<Claim>();

            foreach (var role in roles)
            {
                roleClaim.Add(new Claim("roles", role));
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid",user.Id),
            }
            .Union(userClaim)
            .Union(roleClaim);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(24)
                );

            return jwtSecurityToken;
        }

        private RefreshToken GenerateRefreshToken()
        {
            return new RefreshToken
            {
                Token = RandomTokenString(),
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow

            };
        }

        private string RandomTokenString()
        {
            using var rngCrytoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[40];
            rngCrytoServiceProvider.GetBytes(randomBytes);

            return BitConverter.ToString(randomBytes).Replace("-", "");
        }

        #endregion
    }
}
