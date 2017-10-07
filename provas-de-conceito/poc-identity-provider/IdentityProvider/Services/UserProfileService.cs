using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityProvider.Repositories;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;

namespace IdentityProvider.Services
{
    public class UserProfileService : IProfileService
    {
        private readonly IUserRepository _userRepository;

        public UserProfileService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var subjectId = context.Subject.GetSubjectId();
            var userClaims = _userRepository.GetUserClaimsBySubjectId(subjectId);

            context.IssuedClaims = userClaims.Select(c => new Claim(c.ClaimType, c.ClaimValue)).ToList();

            return Task.CompletedTask;
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            var subjectId = context.Subject.GetSubjectId();
            context.IsActive = _userRepository.IsUserActive(subjectId);

            return Task.CompletedTask;
        }
    }
}
