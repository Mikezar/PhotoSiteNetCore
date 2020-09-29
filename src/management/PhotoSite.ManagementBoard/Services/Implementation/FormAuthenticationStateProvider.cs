﻿using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PhotoSite.ManagementBoard.Services.Implementation
{
    internal sealed class FormAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly SessionStorage _storage;

        public FormAuthenticationStateProvider(SessionStorage storage)
        {
            _storage = storage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            return await Task.FromResult(_storage.IsAuth ? CreateAuthorizedState() : CreateAnonymousState());
        }

        private AuthenticationState CreateAnonymousState()
        {
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        private AuthenticationState CreateAuthorizedState()
        {
            var identity = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, "Admin"),
                    new Claim(ClaimTypes.Role, "Admin")
            }, "Form");

            return new AuthenticationState(new ClaimsPrincipal(identity));
        }
    }
}
