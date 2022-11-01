using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NewLogger.Models
{
    public class Config
    {
        public int Count { get; init; }
        public string BackupPath { get; set; } = string.Empty;
        public string NameLogFile { get; set; } = string.Empty;
        public string LogFileType { get; set; } = string.Empty;
        public string BackupFolder { get; set; } = string.Empty;
        public string FormatBackupFile { get; set; } = string.Empty;
    }
}
