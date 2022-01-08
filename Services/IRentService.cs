using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IRentService
    {
        Task<Token> GetTokenInfo(UserRequest userRequest);
    }
}
