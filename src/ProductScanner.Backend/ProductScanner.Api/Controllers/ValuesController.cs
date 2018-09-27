using Microsoft.AspNetCore.Mvc;
using ProductScanner.Gateway.Events;
using ProductScanner.Gateway.Interfaces;
using System.Collections.Generic;

namespace ProductScanner.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IEventBus _eventBus;

        public ValuesController(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            var report = new ReportEvent();
            _eventBus.Publish(report);
            return new string[] { "value1", "value2" };
        }
    }
}
