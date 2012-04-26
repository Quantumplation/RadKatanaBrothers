using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RadKatanaBrothers
{
    public class NetworkRepresentation : Representation
    {
        List<string> _settings;
        Dictionary<IProperty, bool> _monitors;

        public IEnumerable<IProperty> Monitors
        {
            get { return _monitors.Where(o => o.Value).Select(o => o.Key); }
        }

        public void SetNeutral()
        {
            for (int x = 0; x < _monitors.Count; x++)
                _monitors[_monitors.ElementAt(x).Key] = true;
        }

        public NetworkRepresentation(GameParams settings = null)
        {
            _monitors = new Dictionary<IProperty,bool>();
            _settings = new List<string>();
            foreach (var set in settings)
                _settings.Add(set.Key);
        }

        public override void Initialize()
        {
            foreach (var set in _settings)
            {
                var prop = Parent.GetIProperty(set);
                _monitors[prop] = false;
                prop.PropertyChanged += (o, e) => _monitors[prop] = false;
            }
        }
    }
}
