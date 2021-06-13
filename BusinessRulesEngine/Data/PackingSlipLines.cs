using System;

namespace BusinessRulesEngine.Data
{
    public class PackingSlipLine
    {
        public Guid Id { get; set; }
        public string Department { get; set; }
        public string ProductName { get; set; }
        public string ProductType { get; set; }
        public int Order { get; set; }
    }
}
