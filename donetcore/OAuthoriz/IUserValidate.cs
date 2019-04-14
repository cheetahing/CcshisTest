using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace donetcore.OAuthoriz
{
    public interface IUserValidate
    {
        UserModel GetUserByContext(string userName, string password);
    }
}
