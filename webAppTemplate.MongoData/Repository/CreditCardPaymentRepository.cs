using GPA.MongoData.Interface;
using GPA.MongoData.Model;
using GPA.MongoData.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPA.MongoData.Repository
{
    public class CreditCardPaymentRepository : EntityService<CreditCardPayments>, ICreditCardPaymentRepository
    {
    }
}
