using Core.Security.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTraceSystem.Application.Feature.Auth.Commands.Login
{
    public class LoggedResponse
    {
        public AccessToken? AccessToken { get; set; }
    }
}