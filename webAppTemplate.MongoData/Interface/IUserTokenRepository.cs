using GPA.MongoData.Model;
using GPA.MongoData.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPA.MongoData.Interface
{
    public interface IUserTokenRepository : IEntityService<UserToken>
    {
        Task<UserToken> GetUserToken(string username);
        Task<bool> IsTokenValid(string token);
        Task<UserToken> GetUserTokenDetailByToken(string token);
    }
}
