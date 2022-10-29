using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NewLogger.Configs
{
    public class Config
    {
        public int Count { get; init; }
        public string SomePath { get; set; } = string.Empty;
    }
}
