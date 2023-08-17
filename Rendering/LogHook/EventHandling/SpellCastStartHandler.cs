using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rendering.LogHook.EventHandling
{
    public class SpellCastStartHandler : EventHandler
    {
        public void Handle(string[] args) {
            // 1 - source id
            // 9 - spell id
            var spellId = int.Parse(args[9], CultureInfo.InvariantCulture);
            // breath spell
            if (spellId == 400430) {
                EntityStateMaster.Instance.AddBeamOriginatingFromCreature(spellId.ToString(), args[1], 30, 2000, (199, 80, 131));
            }
        }
    }
}
