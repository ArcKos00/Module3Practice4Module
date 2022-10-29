using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewLogger.Services
{
    public interface IFileService
    {
        public int GetCount { get; }
        public Task WriteLogFileAsync(string message);
        public Task BackupAsync();
    }
}
