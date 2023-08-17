using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rendering.LogHook.EventHandling
{
    internal class EncounterStartHandler : EventHandler
    {
        public void Handle(string[] args) {
            EntityStateMaster.Instance.IsInCombat = true;
        }
    }
}
