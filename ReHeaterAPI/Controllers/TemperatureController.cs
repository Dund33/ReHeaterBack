using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
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
        public IActionResult Get(float? roomArea, float? startTemp, float? targetTemp, float? outsideTemp, int? roomId)
        {
            if (!targetTemp.HasValue)
                return Ok("Please provide value for targetTemp");
            if (!outsideTemp.HasValue)
                return Ok("Please provide value for outsideTemp");
            if (!startTemp.HasValue)
                return Ok("Please provide value for startTemp");
            if (!roomArea.HasValue)
                return Ok("Please provide value for roomArea");

            var predictedQg = ConsumeModel.Predict(new ModelInput
            {
                Tzew = outsideTemp.Value,
                Tstart = startTemp.Value,
                Area = roomArea.Value,
                Tend = targetTemp.Value
            });
            var message = predictedQg.Score.ToString();
            _mqttService.Publish("test", message);
            return Ok(predictedQg);
        }
    }
}
