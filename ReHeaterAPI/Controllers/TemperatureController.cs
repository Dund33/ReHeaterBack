using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReHeaterAPI.MQTT;

namespace ReHeaterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemperatureController : ControllerBase
    {
        private IMqttService _mqttService;
        public TemperatureController(IMqttService mqttService)
        {
            _mqttService = mqttService;
        }

        [HttpGet]
        public IActionResult Get(float? roomArea, float? targetTemp, int? roomId)
        {
            var message = $"room {roomId} with area {roomArea} setting temperature {targetTemp}";
            return Ok(message);
        }
    }
}
