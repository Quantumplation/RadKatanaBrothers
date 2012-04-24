using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace RadKatanaBrothers
{
    class StaticSolid : Entity
    {
        public StaticSolid(GameParams settings = null)
        {
            if (settings == null)
                settings = new GameParams();
            AddProperty<bool>("deadly", (bool)(settings["deadly"] ?? false));
            bool collisionMaskVisible = (bool)(settings["collisionMaskVisible"] ?? false);
            if (settings["polygonVertices"] != null)
            {
                AddIProperty<GeometryProperty>("geometry", new PolygonGeometryProperty((List<Vector2>)settings["polygonVertices"]));
                if (collisionMaskVisible)
                    AddRepresentation<MeshRepresentation>("polygonMask", settings);
            }
            if (settings["circleRadius"] != null)
            {
                AddProperty<Vector2>("position", (Vector2)(settings["position"] ?? Vector2.Zero));
                Console.WriteLine((float)(settings["circleRadius"] ?? 0.0f));
                AddIProperty<GeometryProperty>("geometry", new CircleGeometryProperty() { Radius = (float)(settings["circleRadius"] ?? 0.0f)});
                if (collisionMaskVisible)
                    AddRepresentation<CircleRepresentation>("circleMask", settings);
            }
            AddProperty<double>("mass", double.PositiveInfinity);
            //AddRepresentation<PhysicsRepresentation>("physics", new GameParams());
            if (settings["spriteName"] != null)
                AddRepresentation<SpriteRepresentation>("graphics", settings);
        }
    }
}
