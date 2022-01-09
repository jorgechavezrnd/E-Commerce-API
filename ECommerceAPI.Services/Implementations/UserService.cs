using ECommerceAPI.DataAccess;
using ECommerceAPI.DataAccess.Repositories;
using ECommerceAPI.Dto.Request;
using ECommerceAPI.Dto.Response;
using ECommerceAPI.Entities;
using ECommerceAPI.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly UserManager<ECommerceUserIdentity> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IOptions<AppSettings> _options;
        private readonly ILogger<UserService> _logger;
        private readonly ICustomerRepository _customerRepository;

        public UserService(UserManager<ECommerceUserIdentity> userManager, RoleManager<IdentityRole> roleManager,
            IOptions<AppSettings> options, ILogger<UserService> logger, ICustomerRepository customerRepository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _options = options;
            _logger = logger;
            _customerRepository = customerRepository;
        }

        public async Task<RegisterUserDtoResponse> RegisterAsync(RegisterUserDtoRequest request)
        {
            var response = new RegisterUserDtoResponse();

            try
            {
                var result = await _userManager.CreateAsync(new ECommerceUserIdentity
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    BirthDate = Convert.ToDateTime(request.BirthDate),
                    Email = request.Email,
                    UserName = request.UserCode
                }, request.Password);

                if (!result.Succeeded)
                {
                    response.ValidationErrors = result.Errors
                        .Select(p => p.Description)
                        .ToList();
                }
                else
                {
                    var userIdentity = await _userManager.FindByEmailAsync(request.Email);
                    if (userIdentity != null)
                    {
                        response.UserId = userIdentity.Id;

                        await _customerRepository.CreateAsync(new Customer
                        {
                            Name = request.FirstName,
                            LastName = request.LastName,
                            BirthDate = Convert.ToDateTime(request.BirthDate),
                            Dni = "99999999",
                            Email = userIdentity.Email
                        });

                        if (!await _roleManager.RoleExistsAsync(Constants.RoleAdministrator))
                            await _roleManager.CreateAsync(new IdentityRole(Constants.RoleAdministrator));

                        if (!await _roleManager.RoleExistsAsync(Constants.RoleCustomer))
                            await _roleManager.CreateAsync(new IdentityRole(Constants.RoleCustomer));

                        if (await _userManager.Users.CountAsync() == 1)
                        {
                            await _userManager.AddToRoleAsync(userIdentity, Constants.RoleAdministrator);
                        }
                        else
                        {
                            await _userManager.AddToRoleAsync(userIdentity, Constants.RoleCustomer);
                        }
                    }
                }

                response.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                response.Success = false;
                response.ValidationErrors = new List<string>
                {
                    ex.Message
                };
            }

            return response;
        }

        public async Task<LoginDtoResponse> LoginAsync(LoginDtoRequest request)
        {
            var response = new LoginDtoResponse();

            var identity = await _userManager.FindByNameAsync(request.UserName);

            if (identity == null)
            {
                response.Success = false;
                response.ErrorMessage = "El usuario no existe";
                return response;
            }

            if (!await _userManager.CheckPasswordAsync(identity, request.Password))
            {
                response.Success = false;
                response.ErrorMessage = "Clave incorrecta";
                return response;
            }

            var expiredDate = DateTime.Now.AddHours(1);

            response.ExpiredTime = expiredDate;
            response.FullName = $"{identity.FirstName} {identity.LastName}";
            response.UserCode = identity.UserName;
            response.UserId = identity.Id;

            var sid = string.Empty;
            var customer = await _customerRepository.GetItemByEmailAsync(identity.Email);
            if (customer != null)
            {
                response.CustomerId = customer.Id;
                sid = customer.Id;
            }

            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, identity.UserName),
                new Claim(ClaimTypes.Email, identity.Email),
                new Claim(ClaimTypes.GivenName, response.FullName),
                new Claim(ClaimTypes.Sid, sid)
            };

            var roles = await _userManager.GetRolesAsync(identity);
            foreach (var role in roles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
                response.MenuOptions = role == Constants.RoleAdministrator
                    ? _options.Value.AdminOptions
                    : _options.Value.CustomerOptions;
            }

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Value.Jwt.SigningKey));

            var credentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var header = new JwtHeader(credentials);

            var payload = new JwtPayload(
                issuer: _options.Value.Jwt.Issuer,
                audience: _options.Value.Jwt.Audience,
                claims: authClaims,
                notBefore: DateTime.Now,
                expires: expiredDate);

            var token = new JwtSecurityToken(header, payload);

            response.Token = new JwtSecurityTokenHandler().WriteToken(token);
            response.Success = true;

            return response;
        }
    }
}
