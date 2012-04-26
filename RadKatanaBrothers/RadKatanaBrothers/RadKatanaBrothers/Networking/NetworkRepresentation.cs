using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace RadKatanaBrothers
{
    public class NetworkRepresentation : Representation
    {
        Property<Vector2> position;


        public NetworkRepresentation(GameParams settings = null)
        {
        }

        public void Run(NetworkManager manager)
        {
            if (manager.HasProperty(Parent.ID))
                position.Value = manager.ReadProperty(Parent.ID);
            else
                manager.UpdateProperty(Parent.ID, position.Value);
        }

        public override void Initialize()
        {
            position = Parent.AddProperty<Vector2>("position", Vector2.Zero);
        }
    }
}
