using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rendering.LogHook.EventHandling
{
    public class SpellHealHandler : EventHandler
    {
        public void Handle(string[] args) {
            // 0 - eventType
            // 1 - source player id
            // 5 - target player id
            // if we're considering x,y grid with 0,0 in the bottom left corner
            // 24 Y target
            // 25 -X target
            float x = -float.Parse(args[25], CultureInfo.InvariantCulture);
            float y = float.Parse(args[24], CultureInfo.InvariantCulture);

            EntityStateMaster.Instance.SetPlayerPosition(args[5], x, y);
        }
    }
}
