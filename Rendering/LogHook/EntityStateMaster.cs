using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogHook
{
    // will keep track of all entities to render
    public class EntityStateMaster
    {
        private Dictionary<string, Entity> EntitiesToRender = new Dictionary<string, Entity>();

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

        public void InitiateEntity(string id, char renderCharacter, bool isOnField = true) {
            EntitiesToRender.Add(id, new Entity() {
                Id = id,
                RenderCharacter = renderCharacter,
                IsOnField = isOnField
            });
        }

        public void SetEntityPosition(string id, float x, float y, bool isOnField = true) {
            if (EntitiesToRender.ContainsKey(id)) {
                var entity = EntitiesToRender[id];
                entity.X = x;
                entity.Y = y;
                entity.IsOnField = isOnField;
            }
        }

        public string DebugEntityPositions() {
            var sb = new StringBuilder();
            foreach (var entity in EntitiesToRender) {
                sb.Append($"entity: {entity.Key}, X: {entity.Value.X}, Y: {entity.Value.Y} \n");
            }

            return sb.ToString();
        }

        public List<Entity> GetEntitiesToRender() {
            return EntitiesToRender.Values.ToList();
        }
    }

    public class Entity {
        public string Id { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public char RenderCharacter { get; set; }
        public bool IsOnField { get; set; }
    }

    public class ListEntity
    {
        public string Id { get; set; }
        public List<string> Descriptions { get; set; }
    }
}
