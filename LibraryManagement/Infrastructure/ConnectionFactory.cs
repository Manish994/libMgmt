using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Infrastructure
{
    public interface IConnectionFactory
    {
        IDbConnection GetConnection { get; }
    }

    public class ConnectionFactory : IConnectionFactory
    {
        private IConfiguration Configuration;
        private IHttpContextAccessor _httpContextAccessor;

        public ConnectionFactory(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            Configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public IDbConnection GetConnection
        {
            get
            {
                var connectionString = Configuration.GetConnectionString("DefaultConnection") as string;
                return new SqlConnection(connectionString);
            }
        }
    }
}
