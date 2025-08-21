using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ej2Solid
{
    public enum ProductType
    {
        Standard,
        Premium,
        Clearance,
        Seasonal
    }

    public class Product
    {
        public string Sku { get; init; }
        public ProductType Type { get; init; }
        public decimal BasePrice { get; init; }
        public decimal Discount { get; init; } // 0..1
        public DateTime? SeasonDate { get; init; }
    }

    public interface IPricingRule
    {
        int Priority { get; }

        bool IsMatch(Product product);

        decimal Compute(decimal currentPrice, Product product);
    }

    public class PriceCalculator
    {
        private readonly IReadOnlyList<IPricingRule> _rules;

        public PriceCalculator(IEnumerable<IPricingRule> rules)
        {
            _rules = rules.OrderBy(r => r.Priority).ToList();
            if (_rules.Count == 0)
                throw new ArgumentException("Debe registrar al menos una IPricingRule.");
        }

        public decimal Calculate(Product product)
        {
            decimal price = product.BasePrice;

            foreach (var rule in _rules)
            {
                if (rule.IsMatch(product))
                {
                    price = rule.Compute(price, product);
                }
            }

            if (price < 0) price = 0;
            return Math.Round(price, 2, MidpointRounding.AwayFromZero);
        }
    }

    public class PremiumFeeRule : IPricingRule
    {
        public int Priority => throw new NotImplementedException();

        public decimal Compute(decimal currentPrice, Product product)
        {
            return (product.BasePrice * 1.10m) * (1 - product.Discount);
        }

        public bool IsMatch(Product product)
        {
            bool flagMatch = false;

            if (product.Type == ProductType.Premium)
            {
                flagMatch = true;
            }

            return flagMatch;
        }
    }

    public class GeneralDiscountRule : IPricingRule
    {
        public int Priority => 20;

        public bool IsMatch(Product product) => product.Discount > 0;

        public decimal Compute(decimal currentPrice, Product product)
            => currentPrice * (1 - product.Discount);
    }
    public class ClearanceExtraOffRule : IPricingRule
    {
        public int Priority => 30;

        public bool IsMatch(Product product) => product.Type == ProductType.Clearance;

        public decimal Compute(decimal currentPrice, Product product)
            => currentPrice * 0.70m;
    }


    public class SeasonalDiscountRule : IPricingRule
    {
        public int Priority => 25;

        public bool IsMatch(Product product)
            => product.Type == ProductType.Seasonal
               && product.SeasonDate.HasValue
               && IsInSeason(product.SeasonDate.Value);

        public decimal Compute(decimal currentPrice, Product product)
            => currentPrice * 0.85m;

        private static bool IsInSeason(DateTime date)
        {
            return date.Month == 12 || date.Month == 1 || date.Month == 2;
        }
    }
}
