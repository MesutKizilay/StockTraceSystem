using Core.Persistence.Repositories;
using Core.Security.Entities;
using StockTraceSystem.Application.Services.Repositories;
using StockTraceSystem.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTraceSystem.Persistence.Repositories
{
    public class UserRepository : EfRepositoryBase<User, StockTraceSystemContext>, IUserRepository
    {
        public UserRepository(StockTraceSystemContext context) : base(context)
        {
            
        }
    }
}