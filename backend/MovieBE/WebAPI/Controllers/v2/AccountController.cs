﻿using Domain.Core.Bus;
using Domain.Core.Interfaces;
using Domain.Core.Notifications;
using Identity.Data;
using Identity.Models.AccountViewModels;
using Identity.Models;
using Identity.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace WebAPI.Controllers.v2
{
    [Authorize]
    [ApiVersion("2.0")]
    public class AccountController : ApiController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AuthDbContext _dbContext;
        private readonly IUser _user;
        private readonly IJwtFactory _jwtFactory;
        private readonly ILogger _logger;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            AuthDbContext dbContext,
            IUser user,
            IJwtFactory jwtFactory,
            ILoggerFactory loggerFactory,
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator)
            : base(notifications, mediator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _dbContext = dbContext;
            _user = user;
            _jwtFactory = jwtFactory;
            _logger = loggerFactory.CreateLogger<AccountController>();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response();
            }

            // Tìm người dùng theo tên người dùng hoặc email
            var user = await _userManager.FindByNameAsync(model.UsernameOrEmail) ??
                       await _userManager.FindByEmailAsync(model.UsernameOrEmail);

            if (user != null)
            {
                // Kiểm tra mật khẩu
                var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

                if (!result.Succeeded)
                {
                    NotifyError(result.ToString(), "Login failure");
                    return Response();
                }

                // Get User
                var appUser = await _userManager.FindByEmailAsync(user.Email);
                if (appUser is null)
                {
                    return Response();
                }
            }
            else
            {
                NotifyError("Failed", "Login failure");
                return Response();
            }

            // var appUser = _userManager.Users.SingleOrDefault(r => r.Email == model.Email);

            _logger.LogInformation(1, "User logged in.");
            return Response(await GenerateToken(user));
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response();
            }

            // Add User
            var appUser = new ApplicationUser { UserName = model.Username, Email = model.Email };
            var identityResult = await _userManager.CreateAsync(appUser, model.Password);
            if (!identityResult.Succeeded)
            {
                AddIdentityErrors(identityResult);
                return Response();
            }

            // Add UserRoles
            identityResult = await _userManager.AddToRoleAsync(appUser, "Admin");
            if (!identityResult.Succeeded)
            {
                AddIdentityErrors(identityResult);
                return Response();
            }

            // Add UserClaims
            var userClaims = new List<Claim>
        {
            new Claim("Customers_Write", "Write"),
            new Claim("Customers_Remove", "Remove"),
        };
            await _userManager.AddClaimsAsync(appUser, userClaims);

            // SignIn
            // await _signInManager.SignInAsync(user, false);

            _logger.LogInformation(3, "User created a new account with password.");
            return Response();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("refresh")]
        public async Task<IActionResult> Refresh(TokenViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response();
            }

            // Get current RefreshToken
            var refreshTokenCurrent = _dbContext.RefreshTokens.SingleOrDefault(
                x => x.Token == model.RefreshToken && !x.Used && !x.Invalidated);
            if (refreshTokenCurrent is null)
            {
                NotifyError("RefreshToken", "Refresh token does not exist");
                return Response();
            }

            if (refreshTokenCurrent.ExpiryDate < DateTime.UtcNow)
            {
                // Update current RefreshToken
                refreshTokenCurrent.Invalidated = true;
                await _dbContext.SaveChangesAsync();
                NotifyError("RefreshToken", "Refresh token invalid");
                return Response();
            }

            // Get User
            var appUser = await _userManager.FindByIdAsync(refreshTokenCurrent.UserId);
            if (appUser is null)
            {
                NotifyError("User", "User does not exist");
                return Response();
            }

            // Remove current RefreshToken
            // _dbContext.Remove(refreshTokenCurrent);
            // await _dbContext.SaveChangesAsync();

            // Update current RefreshToken
            refreshTokenCurrent.Used = true;
            await _dbContext.SaveChangesAsync();

            return Response(await GenerateToken(appUser));
        }

        [HttpGet]
        [Route("current")]
        public IActionResult GetCurrent()
        {
            return Response(new
            {
                IsAuthenticated = _user.IsAuthenticated(),
                ClaimsIdentity = _user.GetClaimsIdentity().Select(x => new { x.Type, x.Value }),
            });
        }

        private async Task<TokenViewModel> GenerateToken(ApplicationUser appUser)
        {
            if (appUser is null || string.IsNullOrEmpty(appUser.Email))
            {
                return new TokenViewModel();
            }

            // Init ClaimsIdentity
            var claimsIdentity = new ClaimsIdentity();
            claimsIdentity.AddClaim(new Claim(JwtRegisteredClaimNames.Email, appUser.Email));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, appUser.Id));

            // Get UserClaims
            var userClaims = await _userManager.GetClaimsAsync(appUser);
            claimsIdentity.AddClaims(userClaims);

            // Get UserRoles
            var userRoles = await _userManager.GetRolesAsync(appUser);
            claimsIdentity.AddClaims(userRoles.Select(role => new Claim(ClaimsIdentity.DefaultRoleClaimType, role)));

            // ClaimsIdentity.DefaultRoleClaimType & ClaimTypes.Role is the same

            // Get RoleClaims
            foreach (var userRole in userRoles)
            {
                var role = await _roleManager.FindByNameAsync(userRole);
                if (role is not null)
                {
                    var roleClaims = await _roleManager.GetClaimsAsync(role);
                    claimsIdentity.AddClaims(roleClaims);
                }
            }

            // Generate access token
            var jwtToken = await _jwtFactory.GenerateJwtToken(claimsIdentity);

            // Add refresh token
            var refreshToken = new RefreshToken
            {
                Token = Guid.NewGuid().ToString("N"),
                UserId = appUser.Id,
                CreationDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddMinutes(90),
                JwtId = jwtToken.JwtId,
            };
            await _dbContext.RefreshTokens.AddAsync(refreshToken);
            await _dbContext.SaveChangesAsync();

            return new TokenViewModel
            {
                AccessToken = jwtToken.AccessToken,
                RefreshToken = refreshToken.Token,
            };
        }
    }
}
