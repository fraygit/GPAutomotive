using GPA.MongoData.Model;
using GPA.MongoData.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPA.MongoData.Interface
{
    public interface IUserCreditCardRepository : IEntityService<UserCreditCard>
    {
        Task<UserCreditCard> GetDefaultCard(string username);
    }
}
