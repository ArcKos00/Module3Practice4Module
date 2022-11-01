using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewLogger.Enums;

namespace NewLogger.Services
{
    public interface ILogger
    {
        public Task Log(string message, Status status = Status.Info);
    }
}
