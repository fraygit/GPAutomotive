using GPA.MongoData.Interface;
using GPA.MongoData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace GPA.API.Controllers
{
    public class BlockedDateController : ApiController
    {
        private readonly IBlockedDateRepository blockedDateRepository;

        public BlockedDateController(IBlockedDateRepository blockedDateRepository)
        {
            this.blockedDateRepository = blockedDateRepository;
        }

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpPut]
        public async Task<string> Add(RequestAddBlockDate blockedDate)
        {
            var newBlockedDate = new BlockedDate
            {
                Blocked = blockedDate.BlockedDate
            };
            await blockedDateRepository.CreateSync(newBlockedDate);
            return newBlockedDate.Id.ToString();
        }

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpDelete]
        public async Task<bool> Delete(string id)
        {
            var blockedDate = await blockedDateRepository.Delete(id);
            return true;
        }

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpGet]
        public async Task<List<BlockedDate>> GetAll()
        {
            var allBlockedDate = await blockedDateRepository.ListAll();
            return allBlockedDate;
        }
    }

    public class RequestAddBlockDate
    {
        public DateTime BlockedDate { get; set; }
    }
}
