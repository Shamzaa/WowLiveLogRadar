using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogHook.EventHandling
{
    public class CombatLogEventHandler
    {
        private readonly Dictionary<string, EventHandler> Handlers = new Dictionary<string, EventHandler>() {
            {
                "WORLD_MARKER_PLACED", new WorldMarkerPlacedHandler()
            },
            {
                "COMBATANT_INFO", new CombatantInfoHandler()
            },
            {
                "SPELL_CAST_SUCCESS", new SpellCastSuccessHandler()
            }
        };

        public void Handle(string eventType, string[] args) {
            if(!Handlers.ContainsKey(eventType)) {
                return;
            }

            Handlers[eventType].Handle(args);
        }
    }
}
