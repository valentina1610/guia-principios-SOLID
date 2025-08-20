using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ej1Solid.Interfaces;

namespace ej1Solid.Classes
{
    class Repository : IOrderRepository
    {
        private readonly string _dbPath = "orders_db.txt";
        public void Save(string line)
        {
            File.AppendAllText(_dbPath, line + Environment.NewLine);
        }
    }
}
