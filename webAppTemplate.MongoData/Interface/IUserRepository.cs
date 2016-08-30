using GPA.MongoData.Model;
using GPA.MongoData.Service;
using System.Threading.Tasks;

namespace GPA.MongoData.Interface
{
    public interface IUserRepository : IEntityService<User>
    {
        Task<User> GetUser(string username);
    }
}
