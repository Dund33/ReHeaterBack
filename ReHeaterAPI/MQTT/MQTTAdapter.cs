using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace ReHeaterAPI.MQTT
{
    public class MQTTAdapter : IMqttService
    {
        private readonly MqttClient _mqttClient;
        private readonly Queue<string> _messageQueue;

        public MQTTAdapter(string broker)
        {
            _mqttClient = new MqttClient(broker);
            _messageQueue = new Queue<string>();
            var clientName = Guid.NewGuid().ToString();
            _mqttClient.Connect(clientName);
            _mqttClient.MqttMsgPublishReceived += Received;
        }

        private void Received(object sender, MqttMsgPublishEventArgs e)
        {
            var message = e.Message;
            var messageString = Encoding.UTF8.GetString(message);
            _messageQueue.Enqueue(messageString);
        }

        public IEnumerable<string> GetMessages()
        {
            return _messageQueue.ToImmutableArray();
        }

        public void Publish(string topic, string message)
        {
            var messageBytes = Encoding.UTF8.GetBytes(message);
            Publish(topic, messageBytes);
        }

        public void Publish(string topic, byte[] message)
        {
            if (!_mqttClient.IsConnected)
                throw new Exception("Not connected");
            _mqttClient.Publish(topic, message);
        }
    }
}
