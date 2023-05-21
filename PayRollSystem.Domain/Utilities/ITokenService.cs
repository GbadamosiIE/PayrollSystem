
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayRollSystem.Domain.Utilities
{
    public interface ITokenService
    {
        string CreateToken(UserModel user);
        RefreshToken SetRefreshToken();
    }
}
