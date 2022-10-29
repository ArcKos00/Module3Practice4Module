using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewLogger.Services;

namespace NewLogger
{
    public class Logger : ILogger
    {
        private int _counter = 0;
        private IFileService _service;
        public Logger(IFileService service)
        {
            _service = service;
        }

        public event Action? NeedBackup;
        public void Log(string message, Status status = Status.Info)
        {
            _service.WriteLogFileAsync($"{DateTime.Now}: {status}: {message}");
            _counter++;
            if (_counter % _service.GetCount == 0)
            {
                NeedBackup?.Invoke();
            }
        }
    }
}
