using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RadKatanaBrothers
{
    public class Entity
    {
        Dictionary<string, Representation> representations;
        Dictionary<string, IProperty> properties;
        public EventContainer Events;
        public string ID
        {
            get;
            set;
        }

        public Entity(GameParams settings = null)
        {
            representations = new Dictionary<string, Representation>();
            properties = new Dictionary<string, IProperty>();
            Events = new EventContainer();
        }

        public void Initialize()
        {
            foreach (var rep in representations.Values)
            {
                rep.Parent = this;
                rep.Initialize();
            }
        }

        public void AddRepresentation<T>(string id, GameParams settings) where T : Representation
        {
            representations.Add(id, Factory.Produce<T>(settings));
        }

        public T GetRepresentation<T>(string id) where T : Representation
        {
            return (representations[id] as T);
        }

        public IProperty GetIProperty(string id)
        {
            if (properties.ContainsKey(id))
                return properties[id];
            return null;
        }

        public Property<T> AddProperty<T>(string id, T value)
        {
            if (!properties.ContainsKey(id))
                properties.Add(id, new Property<T>(value));
            return (properties[id] as Property<T>);
        }
        public T AddIProperty<T>(string id, T value) where T : IProperty
        {
            if (!properties.ContainsKey(id))
                properties.Add(id, value);
            return (T)properties[id];
        }

        public void Terminate()
        {
            foreach (var representation in representations.Values)
                representation.Terminate();
        }
    }
}
