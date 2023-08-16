using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rendering.LogHook.EventHandling
{
    public class SpellAuraAppliedHandler : EventHandler
    {
        // 1 - source id
        // 2 - name of source
        // 5 - target id
        // 6 - target name
        // 9 - spell id
        // 10 - name of debuff
        // 12 - "DEBUFF" or "BUFF"
        public void Handle(string[] args) {
            var spellId = int.Parse(args[9]);
            // dread rift
            if (spellId == 406525 && args[12] == "DEBUFF") {
                var playerWithDebuff = args[5];
                EntityStateMaster.Instance.FlagPlayerAsHighlighted(playerWithDebuff, (47, 180, 237));
                // TODO: add current active debuff to list of debuffs 
                // TODO: attach line between player and skull marker or something
            }
        }
    }
}
