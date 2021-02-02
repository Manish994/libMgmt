using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagement.Request;
using LibraryManagement.Response;

namespace LibraryManagement.Interfaces
{
    public interface ICustomerService
    {
        Task<LoginResponse> Signin(LoginRequest loginRequest);
    }
}
