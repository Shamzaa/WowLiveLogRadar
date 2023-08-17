using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rendering.LogHook.EventHandling
{
    public class SpellCastSuccessHandler : EventHandler
    {
        public void Handle(string[] args) {
            // 0 - eventType
            // 1 - source id
            // 2 - source name
            // 9 - spell id
            // if we're considering x,y grid with 0,0 in the bottom left corner
            // 24 Y
            // 25 -X
            if (args[1].StartsWith("Player-")) {
                float x = -float.Parse(args[25], CultureInfo.InvariantCulture);
                float y = float.Parse(args[24], CultureInfo.InvariantCulture);

                var instance = EntityStateMaster.Instance;
                instance.SetPlayerPosition(args[1], x, y);
                instance.SetNameOnPlayer(args[1], args[2].Split('-')[0]);
            }


            var spellId = int.Parse(args[9], CultureInfo.InvariantCulture);
            // todo: end beam from boss if it's breath
            // breath spell
            if (spellId == 400430) {
                EntityStateMaster.Instance.RemoveBeamOriginatingFromCreature(spellId.ToString());
            }

        }
    }
}
