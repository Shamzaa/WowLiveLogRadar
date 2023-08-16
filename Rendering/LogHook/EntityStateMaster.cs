using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rendering.LogHook
{
    // will keep track of all entities to render
    public class EntityStateMaster
    {
        private Dictionary<string, Entity> PlayersToRender = new Dictionary<string, Entity>();
        private Dictionary<string, Entity> WorldMarkersToRender = new Dictionary<string, Entity>();
        private List<Entity> DebuffDropLocationsToRender = new List<Entity>();

        public static EntityStateMaster Instance { get { return Nested.instance; } }

        private class Nested
        {
            // Explicit static constructor to tell C# compiler
            // not to mark type as beforefieldinit
            static Nested() {
            }

            internal static readonly EntityStateMaster instance = new EntityStateMaster();
        }

        private EntityStateMaster() { }

        public void InitiatePlayer(string id, string playerClass, bool isOnField = true) {
            PlayersToRender.Add(id, new Entity() {
                Id = id,
                RenderIdentifier = playerClass,
                IsOnField = isOnField
            });
        }

        public void SetPlayerPosition(string id, float x, float y, bool isOnField = true) {
            if (PlayersToRender.ContainsKey(id)) {
                var entity = PlayersToRender[id];
                entity.X = x;
                entity.Y = y;
                entity.IsOnField = isOnField;
            }
        }

        public void PlaceWorldMarker(string id, string markerName, float x, float y, bool isOnField = true) {
            WorldMarkersToRender[id] = new Entity() {
                Id = id,
                X = x,
                Y = y,
                RenderIdentifier=markerName,
                IsOnField=isOnField
            };
        }

        public void FlagPlayerAsHighlighted(string id, (int R, int G, int B) highlightColour) {
            var player = PlayersToRender[id];
            player.IsHighlighted = true;
            player.HighlightColour = highlightColour;
        }

        public void RemovePlayerHighlight(string id) {
            var player = PlayersToRender[id];
            player.IsHighlighted = false;
        }

        public void PlaceIndicatorOnPlayerPosition(string playerId, string idOfIndicator, (int R, int G, int B) highlightColour) {
            var player = PlayersToRender[playerId];
            DebuffDropLocationsToRender.Add(new Entity() {
                Id = idOfIndicator,
                X = player.X,
                Y = player.Y,
                IsOnField = true,
                IsHighlighted = true,
                HighlightColour = highlightColour
            });
        }

        public string DebugEntityPositions() {
            var sb = new StringBuilder();
            foreach (var entity in PlayersToRender) {
                sb.Append($"entity: {entity.Key}, X: {entity.Value.X}, Y: {entity.Value.Y} \n");
            }

            return sb.ToString();
        }

        public List<Entity> GetPlayersToRender() {
            return PlayersToRender.Values.ToList();
        }

        public List<Entity> GetWorldMarkersToRender() {
            return WorldMarkersToRender.Values.ToList();
        }

        public List<Entity> GetIndicatorsToRender() {
            return DebuffDropLocationsToRender;
        }


    }

    public class Entity {
        public string Id { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public string RenderIdentifier { get; set; }
        public bool IsOnField { get; set; } = false;
        public bool IsHighlighted { get; set; } = false;
        public (int R, int G, int B) HighlightColour { get; set; }
    }

    public class ListEntity
    {
        public string Id { get; set; }
        public List<string> Descriptions { get; set; }
    }
}
