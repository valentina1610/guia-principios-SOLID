using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using ej1Solid.Classes;

namespace ej1Solid
{
    public class OrderItem
    {
        public string Sku { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public int Qty { get; set; }
    }

    public class Order
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("N");
        public string CustomerEmail { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public List<OrderItem> Items { get; set; } = new();
        public decimal Total => CalculateTotal();

        private decimal CalculateTotal()
        {
            decimal sum = 0;
            foreach (var it in Items) sum += it.UnitPrice * it.Qty;
            return sum;
        }
    }

    // Clase "Dios"
    public class OrderProcessor
    {
        public void Process(Order order)
        {
            // 1) Validación
            Validator val = new Validator();
            val.Validate(order);

            // 2) Log de inicio
            Logger log = new Logger();
            log.Log($"[LOG] Processing order {order.Id} at {DateTime.UtcNow:o}");

            // 3) Persistencia en un "archivo DB"
            Repository repo = new Repository();
            repo.Save($"{order.Id}|{order.CustomerEmail}|{order.Total}|{order.CreatedAt:o}");

            // 4) Envío de email
            Notification notif = new Notification();
            notif.SendEmail(order);

            // 5) Log de fin
            log.Log($"[LOG] Order {order.Id} processed successfully.");
        }
    }
}
