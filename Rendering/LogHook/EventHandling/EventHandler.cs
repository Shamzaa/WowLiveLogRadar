using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogHook.EventHandling
{
    internal interface EventHandler
    {
        void Handle(string[] args);
    }
}
