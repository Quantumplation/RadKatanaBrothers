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
            AddProperty<int>(id: "score", value: 25000);
            AddProperty<String>(id: "text", value: "Hello World!");
            AddProperty<Vector2>(id: "textOffset", value: new Vector2(0, 32));
            AddRepresentation<CircleRepresentation>(id: "graphics", settings: new GameParams()
            {
                {"color", Color.Gainsboro}
            });
            AddRepresentation<PhysicsRepresentation>(id: "physics", settings: new GameParams());
            if(!(bool)(settings["remote"] ?? false))
                AddRepresentation<GameplayRepresentation>(id: "gameplay", settings: new GameParams());
            AddRepresentation<NetworkRepresentation>(id: "network", settings: new GameParams() { { "position", true } });
            AddRepresentation<TextRepresentation>(id: "text", settings: new GameParams());
            Events.AddEvent<Action<Entity>>("onCollision", (Entity other) =>
            {
                (GetIProperty("score") as Property<int>).Value -= 10;
                if (other.AddProperty<bool>("deadly", false).Value)
                {
                    (GetIProperty("score") as Property<int>).Value = 1;
                    World.PrepareToRemoveEntity(ID);
                }
                if (other.AddProperty<bool>("victory", false).Value)
                    World.PrepareToRemoveEntity(ID);
            });
        }
    }
}
