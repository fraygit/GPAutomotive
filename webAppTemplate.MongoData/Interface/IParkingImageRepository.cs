﻿using GPA.MongoData.Model;
using GPA.MongoData.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPA.MongoData.Interface
{
    public interface IParkingImageRepository : IEntityService<ParkingImage>
    {
        Task<ParkingImage> GetByParkingSpace(string parkingSpaceId);
    }
}
