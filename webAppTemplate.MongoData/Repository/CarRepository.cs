using GPA.MongoData.Interface;
using GPA.MongoData.Model;
using GPA.MongoData.Service;

namespace GPA.MongoData.Repository
{
    public class CarRepository : EntityService<Car>, ICarRepository
    {
    }
}
