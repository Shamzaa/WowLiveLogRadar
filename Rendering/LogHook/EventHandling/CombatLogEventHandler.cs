using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rendering.LogHook.EventHandling
{
    public class CombatLogEventHandler
    {
        private readonly Dictionary<string, EventHandler> Handlers = new Dictionary<string, EventHandler>() {
            {
                "WORLD_MARKER_PLACED", new WorldMarkerPlacedHandler()
            },
            // init players when fight starts
            {
                "COMBATANT_INFO", new CombatantInfoHandler()
            },
            // handling debuffs/buff events
            {
                "SPELL_AURA_APPLIED", new SpellAuraAppliedHandler()
            },
            {
                "SPELL_AURA_REMOVED", new SpellAuraRemovedHandler()
            },
            // events with source position, as well as breath finish methink
            {
                "SPELL_CAST_SUCCESS", new SpellCastSuccessHandler()
            },
            // events with target position
            {
                "SPELL_HEAL", new SpellHealHandler()
            },
            {
                "SPELL_PERIODIC_HEAL", new SpellHealHandler() // since handling is identical
            },
            // events that can contain non player positions
            {
                "SPELL_DAMAGE", new SpellDamageHandler()
            },
            // to check when boss does breaht
            {
                "SPELL_CAST_START", new SpellCastStartHandler()
            }


            
                        // todo: features I wanna make for fun
                        // current player highlight
                        // attach line between player and a marker during debuff
                        // arrow showing to go to a marker with debuff

                        // wont care for now:
                        // deaths
                        // wipes? should just start over on next attempt
                        // making stuff generic. It's just a POC to get my point across. Hardcode IDs for days babyyyy
        };

        public void Handle(string eventType, string[] args) {
            if(!Handlers.ContainsKey(eventType)) {
                return;
            }

            Handlers[eventType].Handle(args);
        }
    }
}
