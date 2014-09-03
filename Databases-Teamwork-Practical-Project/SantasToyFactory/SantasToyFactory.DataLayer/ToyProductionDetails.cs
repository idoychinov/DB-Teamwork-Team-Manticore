namespace SantasToyFactory.Models
{
    using System;

    public class ToyProductionDetails
    {
        public int Id { get; set; }

        public int Quantity { get; set; }

        public decimal CommissionPercent { get; set; }
    }
}
