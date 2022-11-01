using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NewLogger.Models;
using NewLogger.Services;

namespace NewLogger
{
    public class JsonReader
    {
        private readonly string _config = "config.json";
        public Config ReadJsonFile()
        {
            var json = File.ReadAllText(_config);
            var config = JsonConvert.DeserializeObject<Config>(json);
            if (config != null)
            {
                return config;
            }

            return new Config()
            {
                Count = byte.MaxValue,
                BackupPath = string.Empty,
                BackupFolder = string.Empty,
                LogFileType = ".txt",
                NameLogFile = "log"
            };
        }
    }
}
