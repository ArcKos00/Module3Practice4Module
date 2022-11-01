using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewLogger.Services
{
    public interface IActions
    {
        public Task Method1();
        public Task Method2();
        public Task Method3();
    }
}
