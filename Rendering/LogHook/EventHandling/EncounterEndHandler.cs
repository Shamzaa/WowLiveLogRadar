using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rendering.LogHook.EventHandling
{
    internal class EncounterEndHandler : EventHandler
    {
        public void Handle(string[] args) {
            EntityStateMaster.Instance.IsInCombat = false;
            EntityStateMaster.Instance.ClearState();
        }
    }
}
