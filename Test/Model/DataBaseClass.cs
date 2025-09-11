using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Model
{
    internal class DataBaseClass
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime CreateTime { get; set; }
        public int Age { get; set; }
        public double Price { get; set; }
    }
}
