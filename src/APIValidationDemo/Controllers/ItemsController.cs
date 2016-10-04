using APIValidationDemo.Commands;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APIValidationDemo.Controllers
{
    public class ItemsController : ApiController
    {
        private static List<string> items = new List<string> { "Item 1", "Item 2", "Item 3 " };

        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(items);
        }

        public HttpResponseMessage Post(CreateItemCommand command)
        {
            return Request.CreateResponse(HttpStatusCode.Created);
        }
    }
}
