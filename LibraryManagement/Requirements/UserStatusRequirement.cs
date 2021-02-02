using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace LibraryManagement.Requirements
{
    public class UserStatusRequirement
    {
        public class UserBlockedStatusRequirement : IAuthorizationRequirement
        {
            public bool IsBlocked { get; }
            public UserBlockedStatusRequirement(bool isBlocked)
            {
                IsBlocked = isBlocked;
            }

        }
    }
}
