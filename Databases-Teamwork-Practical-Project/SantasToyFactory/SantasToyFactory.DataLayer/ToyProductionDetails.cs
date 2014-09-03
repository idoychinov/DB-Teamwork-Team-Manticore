namespace SantasToyFactory.Models
{
    using System;

    public class ToyProductionDetails
    {
        public long Id { get; set; }

        public long Quantity { get; set; }

        public decimal CommissionPercent { get; set; }
    }
}
