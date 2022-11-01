using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewLogger.Enums;
using NewLogger.Services;

namespace NewLogger
{
    public class Logger : ILogger
    {
        private int _counter = 0;
        private IFileService _service;
        private SemaphoreSlim _semaphore = new SemaphoreSlim(1);
        public Logger(IFileService service)
        {
            _service = service;
        }

        public event Action? NeedBackup;
        public async Task Log(string message, Status status)
        {
            await _semaphore.WaitAsync();
            await _service.WriteLogFileAsync($"{DateTime.Now}: {status}: {message}");
            _counter++;
            if (_counter % _service.GetCount == 0)
            {
                NeedBackup?.Invoke();
            }

            _semaphore.Release();
        }
    }
}
