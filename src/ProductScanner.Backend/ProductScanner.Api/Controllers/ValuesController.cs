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
            //var data = new ImagePreprocessingEvent()
            //{
            //    Id = 1,
            //    Entries = new List<ImageClasificationEventResultEntry>()
            //    {
            //        new ImageClasificationEventResultEntry()
            //        {
            //            Id =1,
            //            Category = "pepsi",
            //            Position= new double[]{1,1,1,1},
            //            Score = 1
            //        },
            //        new ImageClasificationEventResultEntry()
            //        {
            //            Id= 2,
            //            Category = "cola",
            //            Position= new double[]{0,0,1,1},
            //            Score = 1
            //        }
            //    }
            //};

            //_eventBus.Publish(data);
            return new string[] { "value1", "value2" };
        }
    }
}
