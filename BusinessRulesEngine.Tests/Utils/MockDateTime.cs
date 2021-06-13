using BusinessRulesEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessRulesEngine.Tests.Utils
{
    internal class MockDateTime : IDateTime
    {
        public DateTime Now { get; }

        public DateTime UtcNow { get; }

        public MockDateTime(DateTime dateTime)
        {
            Now = dateTime;
            UtcNow = dateTime;
        }

    }
}
