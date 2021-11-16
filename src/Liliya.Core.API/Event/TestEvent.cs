using Liliya.EventBus;

namespace Liliya.Core.API.Event
{
    public class TestEvent : EventData
    {
        public string Name { get; set; }

        public string Remork { get; set; }
    }
}
