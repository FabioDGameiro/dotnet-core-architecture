using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityProvider.Database;
using IdentityProvider.Repositories;
using IdentityProvider.ViewModels.UserRegistration;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdentityProvider.Controllers.UserRegistration
{
    public class UserRegistrationController : Controller
    {
        private readonly IUserRepository _marvinUserRepository;
        private readonly IIdentityServerInteractionService _interaction;

        public UserRegistrationController(IUserRepository marvinUserRepository,
             IIdentityServerInteractionService interaction)
        {
            _marvinUserRepository = marvinUserRepository;
            _interaction = interaction;
        }

        [HttpGet]
        public IActionResult RegisterUser(RegistrationInputModel registrationInputModel)
        {
            var vm = new RegisterUserViewModel
            {
                ReturnUrl = registrationInputModel.ReturnUrl,
                Provider = registrationInputModel.Provider,
                ProviderUserId = registrationInputModel.ProviderUserId
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterUser(RegisterUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                // create user + claims
                var userToCreate = new User();
                userToCreate.Password = model.Password;
                userToCreate.Username = model.Username;
                userToCreate.IsActive = true;
                userToCreate.Claims.Add(new UserClaim("country", model.Country));
                userToCreate.Claims.Add(new UserClaim("address", model.Address));
                userToCreate.Claims.Add(new UserClaim("given_name", model.Firstname));
                userToCreate.Claims.Add(new UserClaim("family_name", model.Lastname));
                userToCreate.Claims.Add(new UserClaim("email", model.Email));
                userToCreate.Claims.Add(new UserClaim("subscriptionlevel", "FreeUser"));

                // if we're provisioning a user via external login, we must add the provider &
                // user id at the provider to this user's logins
                if (model.IsProvisioningFromExternal)
                {
                    userToCreate.Logins.Add(new UserLogin()
                    {
                        LoginProvider = model.Provider,
                        ProviderKey = model.ProviderUserId
                    });
                }

                // add it through the repository
                _marvinUserRepository.AddUser(userToCreate);

                if (!_marvinUserRepository.Save())
                {
                    throw new Exception($"Creating a user failed.");
                }

                if (!model.IsProvisioningFromExternal)
                {
                    await HttpContext.SignInAsync(userToCreate.SubjectId, userToCreate.Username);
                }

                // continue with the flow     
                if (_interaction.IsValidReturnUrl(model.ReturnUrl) || Url.IsLocalUrl(model.ReturnUrl))
                {
                    return Redirect(model.ReturnUrl);
                }

                return Redirect("~/");
            }

            // ModelState invalid, return the view with the passed-in model
            // so changes can be made
            return View(model);
        }

    }
}