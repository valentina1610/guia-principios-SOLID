using System;

namespace ej2Solid
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var rules = new IPricingRule[]
        {
            new PremiumFeeRule(),
            new GeneralDiscountRule(),
            new ClearanceExtraOffRule(),
            new SeasonalDiscountRule() 
        };

            var calculator = new PriceCalculator(rules);

            var standard = new Product
            {
                Sku = "STD-001",
                Type = ProductType.Standard,
                BasePrice = 100m,
                Discount = 0.10m
            };

            var premium = new Product
            {
                Sku = "PREM-001",
                Type = ProductType.Premium,
                BasePrice = 100m,
                Discount = 0.10m
            };

            var clearance = new Product
            {
                Sku = "CLR-001",
                Type = ProductType.Clearance,
                BasePrice = 100m,
                Discount = 0.10m
            };

            var seasonal = new Product
            {
                Sku = "SEA-001",
                Type = ProductType.Seasonal,
                BasePrice = 100m,
                Discount = 0.10m,
                SeasonDate = new DateTime(2025, 12, 1)
            };

            Console.WriteLine($"Standard: {calculator.Calculate(standard)}");  
            Console.WriteLine($"Premium: {calculator.Calculate(premium)}");    
            Console.WriteLine($"Clearance: {calculator.Calculate(clearance)}"); 
            Console.WriteLine($"Seasonal: {calculator.Calculate(seasonal)}");  
        }
    }
}
