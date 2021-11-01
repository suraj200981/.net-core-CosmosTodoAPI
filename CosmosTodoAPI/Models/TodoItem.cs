using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CosmosTodoAPI.Models
{
    public class TodoItem
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public string status { get; set; }

    }
}
