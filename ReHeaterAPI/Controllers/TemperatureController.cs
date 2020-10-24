using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ReHeaterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemperatureController : ControllerBase
    {
        public TemperatureController(IMqttService mqttService)
        {

        }

        [HttpGet]
        public async Task<IActionResult> Get(float? roomArea, float? targetTemp)
        {
            return Ok(roomArea);
        }
    }
}
