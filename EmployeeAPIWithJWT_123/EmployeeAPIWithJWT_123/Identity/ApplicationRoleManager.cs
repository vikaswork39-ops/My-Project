﻿using Microsoft.AspNetCore.Identity;

namespace EmployeeAPIWithJWT_123.Identity
{
    public class ApplicationRoleManager : RoleManager<ApplicationRole>
    {
        public ApplicationRoleManager(ApplicationRoleStore appRoleStore,
              IEnumerable<IRoleValidator<ApplicationRole>> roleValidators,
               ILookupNormalizer lookupNormalizer, IdentityErrorDescriber identityErrorDescriber,
               ILogger<ApplicationRoleManager> logger) : base(appRoleStore, roleValidators, lookupNormalizer,
              identityErrorDescriber, logger)
        {

        }
    }
}
