using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogHook.EventHandling
{
    public class SpellCastSuccessHandler : EventHandler
    {
        public void Handle(string[] args) {
            // 0 - eventType
            // 1 - player id
            // if we're considering x,y grid with 0,0 in the bottom left corner
            // 24 Y
            // 25 -X
            float x = -float.Parse(args[25], CultureInfo.InvariantCulture);
            float y = float.Parse(args[24], CultureInfo.InvariantCulture);

            EntityStateMaster.Instance.SetPlayerPosition(args[1], x, y);
        }
    }
}
