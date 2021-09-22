using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace SpyDuh.API.Repositories
{
    public class AssignmentsRepo
    {
        readonly string _connectionString;

        public AssignmentsRepo(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("SpyDuh");
        }
    }
}
