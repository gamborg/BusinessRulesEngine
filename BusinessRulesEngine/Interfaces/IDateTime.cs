using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessRulesEngine.Interfaces
{
    public interface IDateTime
    {
        System.DateTime Now { get; }
        System.DateTime UtcNow { get; }
    }
}
