using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Xml;

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
            RenderManager render = new RenderManager();
            AddManager<RenderManager>(id: "graphics");
            AddManager<PhysicsManager>(id: "physics");
            Factory.RegisterManager<RenderManager>(GetManager<RenderManager>(id: "graphics"), typeof(SpriteRepresentation));
            Factory.RegisterManager<RenderManager>(GetManager<RenderManager>(id: "graphics"), typeof(MeshRepresentation));
            Factory.RegisterManager<PhysicsManager>(GetManager<PhysicsManager>(id: "physics"), typeof(PhysicsRepresentation));
            Factory.RegisterCallback<Entity>((settings) => new Entity());
            Factory.RegisterCallback<Player>((settings) => new Player());
            Factory.RegisterCallback<SpriteRepresentation>((settings) => new SpriteRepresentation(settings));
            Factory.RegisterCallback<MeshRepresentation>((settings) => new MeshRepresentation(settings));
            Factory.RegisterCallback<PhysicsRepresentation>((settings) => new PhysicsRepresentation());
        }

        public void LoadMap(string filename)
        {
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.Load(filename + ".xml");
                XmlNodeList loadEntities = doc.GetElementsByTagName("Entity");
                foreach (XmlNode entity in loadEntities)
                {
                    Entity newEntity = Factory.Produce(entity.Attributes["class"].Value) as Entity;
                    XmlNode node = entity.FirstChild;
                    do
                    {
                        if (node.Name == "Representation")
                        {
                            // TODO: Pi
                            GameParams Settings = new GameParams();
                            foreach (XmlNode child in node.ChildNodes)
                                Settings.Add(child.Name, Factory.Produce(child.Attributes["type"].Value, child.Attributes["value"].Value));
                            newEntity.AddRepresentation(node.Attributes["type"].Value, node.Attributes["name"].Value, Settings);
                        }
                        node = node.NextSibling;
                    }
                    while (node != entity.LastChild);
                    //for (XmlNode node = entity.FirstChild; node != entity.LastChild; node = node.NextSibling)
                        //Add the representations
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void AddEntity<T>(string id) where T : Entity
        {
            entities.Add(id, Factory.Produce<T>());
        }
        public void AddManager<T>(string id) where T : Manager, new()
        {
            managers.Add(id, new T());
        }

        public T GetEntity<T>(string id) where T : Entity
        {
            return (entities[id] as T);
        }

        public T GetManager<T>(string id) where T : Manager
        {
            return (managers[id] as T);
        }

        public void RunAllManagers(float elapsedMilliseconds)
        {
            foreach (var manager in managers.Values)
                manager.Run(elapsedMilliseconds);
                
        }
    }
}
