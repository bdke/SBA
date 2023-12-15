﻿using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using SBASeatingPlan.Auth0;

namespace SBASeatingPlan.Auth0
{
    internal class Auth0AuthenticationStateProvider(Auth0Client client) : AuthenticationStateProvider
    {
        private ClaimsPrincipal currentUser = new(new ClaimsIdentity());
        private readonly Auth0Client auth0Client = client;

        public override Task<AuthenticationState> GetAuthenticationStateAsync() =>
            Task.FromResult(new AuthenticationState(currentUser));

        public Task LogInAsync()
        {
            var loginTask = LogInAsyncCore();
            NotifyAuthenticationStateChanged(loginTask);

            return loginTask;

            async Task<AuthenticationState> LogInAsyncCore()
            {
                var user = await LoginWithAuth0Async();
                currentUser = user;

                return new AuthenticationState(currentUser);
            }
        }

        private async Task<ClaimsPrincipal> LoginWithAuth0Async()
        {
            var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity());
            var loginResult = await auth0Client.LoginAsync();

            if (!loginResult.IsError)
            {
                authenticatedUser = loginResult.User;
            }
            return authenticatedUser;
        }

        public async void LogOut()
        {
            await auth0Client.LogoutAsync();
            currentUser = new ClaimsPrincipal(new ClaimsIdentity());
            NotifyAuthenticationStateChanged(
                Task.FromResult(new AuthenticationState(currentUser)));
        }
    }
}
