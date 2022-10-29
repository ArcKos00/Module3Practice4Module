using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewLogger.Services
{
    public class Actions : IActions
    {
        private ILogger _logger;
        public Actions(ILogger logger)
        {
            _logger = logger;
        }

        public void Method1()
        {
            _logger.Log("Safe start method", Status.Info);
        }

        public void Method2()
        {
            _logger.Log("Warning, this method has some errors", Status.Warning);
        }

        public void Method3()
        {
            _logger.Log("ERROR", Status.Error);
        }
    }
}
