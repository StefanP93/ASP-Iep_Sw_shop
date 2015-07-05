using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IEPProjekatPS120511.Controllers
{
    public class OrdersServiceController : ApiController
    {
        // GET: api/OrdersService
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/OrdersService/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/OrdersService
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/OrdersService/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/OrdersService/5
        public void Delete(int id)
        {
        }
    }
}
