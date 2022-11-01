using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewLogger.Enums;

namespace NewLogger.Services
{
    public class Actions : IActions
    {
        private ILogger _logger;
        public Actions(ILogger logger)
        {
            _logger = logger;
        }

        public async Task Method1()
        {
            await _logger.Log("Safe start method", Status.Info);
        }

        public async Task Method2()
        {
            await _logger.Log("Warning, this method has some errors", Status.Warning);
        }

        public async Task Method3()
        {
            await _logger.Log("ERROR", Status.Error);
        }
    }
}
