using System.Linq;
using System.Threading.Tasks;
using LibraryManagement.Helpers;
using LibraryManagement.Interfaces;
using LibraryManagement.Request;
using LibraryManagement.Response;
using LibraryManagement.ViewModel;

namespace LibraryManagement.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly LibraryDBContext libraryDBContext;
        public CustomerService(LibraryDBContext libraryDBContext)
        {
            this.libraryDBContext = libraryDBContext;
        }

        public async Task<LoginResponse> Signin(LoginRequest loginRequest)
        {
            var user = libraryDBContext.Logins.SingleOrDefault(user => user.Username == loginRequest.Username && user.Password == loginRequest.Password);
            if (user == null)
            {
                return null;
            }

            var token = await Task.Run(() => TokenHelper.GenerateToken(user));
            return new LoginResponse { Username = user.Username, Token = token };
        }
    }
}
