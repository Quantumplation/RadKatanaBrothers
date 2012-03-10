using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RadKatanaBrothers
{
    public class GameParams : List<KeyValuePair<String, object>>
    {
        public void Add(String key, object value)
        {
            Add(new KeyValuePair<string, object>(key, value));
        }

        public object this[String key]
        {
            get { return this.Find((k) => k.Key == key).Value; }
        }
    }
}
