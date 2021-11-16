using Liliya.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Liliya.Core.API.Event
{
    public class TestEvent : IEvent
    {
        public TestEvent(string testName)
        {
            Id = Guid.NewGuid();
            Timestamp = DateTime.Now;
            TestName = testName;
        }

        public Guid Id { get; }

        public DateTime Timestamp { get; }

        public string TestName { get; }
    }
}
