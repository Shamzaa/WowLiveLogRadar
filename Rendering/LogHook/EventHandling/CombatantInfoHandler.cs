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
            // ...
            // 24 - specid

            var playerClass = GetClassNameFromSpecId(int.Parse(args[24]));

            EntityStateMaster.Instance.InitiatePlayer(args[1], playerClass, false);
        }



        // https://wowpedia.fandom.com/wiki/SpecializationID
        private string GetClassNameFromSpecId(int specId) {
            switch (specId) {
                case 250:
                case 251:
                case 252:
                    return "Death Knight";
                case 577:
                case 581:
                    return "Demon Hunter";
                case 102:
                case 103:
                case 104:
                case 105:
                    return "Druid";
                case 1467:
                case 1468:
                case 1473:
                    return "Evoker";
                case 253:
                case 254:
                case 255:
                    return "Hunter";
                case 62:
                case 63:
                case 64:
                    return "Mage";
                case 268:
                case 270:
                case 269:
                    return "Monk";
                case 65:
                case 66:
                case 70:
                    return "Paladin";
                case 256:
                case 257:
                case 258:
                    return "Priest";
                case 259:
                case 260:
                case 261:
                    return "Rogue";
                case 262:
                case 263:
                case 264:
                    return "Shaman";
                case 265:
                case 266:
                case 267:
                    return "Warlock";
                case 71:
                case 72:
                case 73:
                    return "Warrior";
                default:
                    throw new Exception($"unhandled spec {specId}");

            }
        }
    }
}
