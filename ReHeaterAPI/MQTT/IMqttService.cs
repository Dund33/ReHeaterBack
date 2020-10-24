using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReHeaterAPI.MQTT
{
    public interface IMqttService
    {
        public void Publish(string topic, string message);
        public void Publish(string topic, byte[] message);
        public IEnumerable<string> GetMessages();
    }
}
