using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogHook.EventHandling
{
    public class CombatantInfoHandler : EventHandler
    {
        public void Handle(string[] args) {
            // initiate and create entities for players in the raid
            // 0 - eventType
            // 1 - player id
            // 2... etc

            EntityStateMaster.Instance.InitiateEntity(args[1], 'p', false);
        }
    }
}
