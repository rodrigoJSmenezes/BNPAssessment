﻿namespace BNPTest.Logic.Models
{
    public class Security
    {
        public int Id { get; set; }
        public string ISIN { get; set; }
        public decimal Price { get; set; } = decimal.Zero;
    }
}
