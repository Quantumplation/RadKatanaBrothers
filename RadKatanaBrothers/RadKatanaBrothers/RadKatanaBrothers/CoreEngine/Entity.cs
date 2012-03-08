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

        public Entity()
        {
            representations = new Dictionary<string, Representation>();
            properties = new Dictionary<string, IProperty>();
        }

        public void AddRepresentation<T>(string id) where T : Representation
        {
            representations.Add(id, Factory.Produce<T>(id: id));
        }
        public T GetRepresentation<T>(string id) where T : Representation
        {
            return (representations[id] as T);
        }

        public Property<T> AddProperty<T>(string id, T value)
        {
            if (!properties.ContainsKey(id))
                properties.Add(id, new Property<T>(value));
            return (properties[id] as Property<T>);
        }
    }
}
