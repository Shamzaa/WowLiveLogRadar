using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogHook.EventHandling
{
    public class WorldMarkerPlacedHandler : EventHandler
    {
        public void Handle(string[] args) {
            /*
             * 8/10 19:49:27.948  WORLD_MARKER_PLACED,2569,7,2384.24,2511.68
                8/10 19:49:27.949  WORLD_MARKER_PLACED,2569,3,2460.05,2516.53
                8/10 19:49:27.949  WORLD_MARKER_PLACED,2569,0,2458.94,2445.48
                8/10 19:49:27.949  WORLD_MARKER_PLACED,2569,6,2384.50,2451.47
            */

            // 1 - instanceid?
            // 2 - marker id
            // 3 - pos y?
            // 4 - pos -x?


            float x = -float.Parse(args[4], CultureInfo.InvariantCulture);
            float y = float.Parse(args[3], CultureInfo.InvariantCulture);

            EntityStateMaster.Instance.PlaceWorldMarker(args[2], args[2], x, y);
        }
    }
}
