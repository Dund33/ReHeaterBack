using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReHeaterAPI.AI;
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
            if (!targetTemp.HasValue)
                return Ok("Please provide value for targetTemp");

            var predictedQg = ConsumeModel.Predict(new ModelInput
            {
                Col0 = 15f,
                Col2 = targetTemp.Value
            });
            var message = predictedQg.Score.ToString();
            _mqttService.Publish("test", message);
            return Ok(message);
        }
    }
}
