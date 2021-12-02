﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Spg.SpengerBanger.Business.Domain.Exceptions;
using Spg.SpengerBanger.Business.Domain.Interfaces;
using Spg.SpengerBanger.Business.Domain.Model;
using Spg.SpengerBanger.Business.Services.AuthService;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace Spg.SpengerBanger.MvcFrontEnd.Services
{
    public class HttpAuthService : IAuthService
    {
        private readonly HttpContext _httpContext;
        private readonly IAuthProvider _authProvider;

        public HttpAuthService(IAuthProvider authProvider,
            IHttpContextAccessor httpContext)
        {
            _authProvider = authProvider;
            _httpContext = httpContext.HttpContext;
        }

        public async Task Login(string eMail)
        {
            var (userInfo, errorMessage) = _authProvider.CheckUser(eMail);
            if (string.IsNullOrEmpty(errorMessage))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userInfo.EMail),
                    new Claim(nameof(User), JsonSerializer.Serialize(userInfo)),
                    new Claim(ClaimTypes.Role, userInfo.UserRole.ToString()),
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims,
                    Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    ExpiresUtc = DateTimeOffset.UtcNow.AddHours(3),
                };

                await _httpContext.SignInAsync(
                    Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);
            }
        }

        public Task Logout()
        {
            return _httpContext.SignOutAsync(
                Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public bool IsAuthenticated
            => _httpContext.User.Identity.IsAuthenticated;

        public string Username()
            => _httpContext.User.Identity.Name;

        public bool HasRole(string role)
            => _httpContext.User.IsInRole(role.ToString());
    }
}
