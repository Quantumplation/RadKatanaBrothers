using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace RadKatanaBrothers
{
    public class Player : Entity
    {
        public Player(GameParams settings = null)
        {
            if (settings == null)
                settings = new GameParams();
            AddIProperty<GeometryProperty>(id: "geometry", value: new CircleGeometryProperty() { Radius = 16 });
            AddProperty<Vector2>(id: "position", value: (Vector2)(settings["position"] ?? Vector2.Zero));
            AddProperty<Vector2>(id: "acceleration", value: Vector2.UnitY * 50);
            AddRepresentation<CircleRepresentation>(id: "graphics", settings: new GameParams()
            {
                {"color", Color.Gainsboro}
            });
            AddRepresentation<PhysicsRepresentation>(id: "physics", settings: new GameParams());
            AddRepresentation<GameplayRepresentation>(id: "gameplay", settings: new GameParams());
            Events.AddEvent<Action<Entity>>("onCollision", (Entity other) =>
            {
                if (other.AddProperty<bool>("deadly", false).Value)
                    World.PrepareToRemoveEntity(ID);
            });
        }
    }
}
