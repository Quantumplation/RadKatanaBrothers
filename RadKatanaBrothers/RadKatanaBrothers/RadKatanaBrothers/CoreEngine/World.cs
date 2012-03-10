using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RadKatanaBrothers
{
    public class World
    {
        Dictionary<string, Entity> entities;
        Dictionary<string, Manager> managers;

        public World()
        {
            entities = new Dictionary<string, Entity>();
            managers = new Dictionary<string, Manager>();
            AddManager<RenderManager>(id: "RenderManager");
        }
        public void Load(string filename)
        {
            throw new NotImplementedException();
        }
        public void AddEntity<T>(string id) where T : Entity, new()
        {
            entities.Add(id, Factory.Produce<T>());
        }
        public void AddManager<T>(string id) where T : Manager, new()
        {
            managers.Add(id, Factory.Produce<T>());
        }

        public T GetEntity<T>(string id) where T : Entity
        {
            return (entities[id] as T);
        }

        public T GetManager<T>(string id) where T : Manager
        {
            return (managers[id] as T);
        }

        public void RunManagers()
        {
            foreach (var manager in managers.Values)
                manager.Run();
        }
    }
}
