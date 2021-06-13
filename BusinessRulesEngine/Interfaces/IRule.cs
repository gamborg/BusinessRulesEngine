using BusinessRulesEngine.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRulesEngine.Interfaces
{
    public interface IRule
    {
        Task Apply(Payment payment);
    }
}
