using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CosmosTodoAPI.Models
{
    public class TodoItem
    {

        [JsonProperty(PropertyName = "name")]

        public string Name { get; set; }
        [JsonProperty(PropertyName = "date")]

        public string Date { get; set; }

        [JsonProperty(PropertyName = "status")]

        public string status { get; set; }

    }
}
