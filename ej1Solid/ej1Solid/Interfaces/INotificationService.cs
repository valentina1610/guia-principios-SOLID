using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ej1Solid.Interfaces
{
    interface INotificationService
    {
        public void SendEmail(Order order);
    }
}
