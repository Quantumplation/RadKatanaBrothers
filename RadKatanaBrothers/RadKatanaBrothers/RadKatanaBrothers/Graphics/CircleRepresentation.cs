using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace RadKatanaBrothers
{
    public class CircleRepresentation : GraphicsRepresentation
    {
        public static event Created onCreated;
        public static event Terminated onTerminated;
        public override void Create()
        {
            if (onCreated != null)
                onCreated(this);
        }
        public override void Terminate()
        {
            if (onTerminated != null)
                onTerminated(this);
        }

        Property<Vector2> coPosition;
        Property<double> coRotation;
        CircleGeometryProperty coGeometry;
        Texture2D circle;
        Color color;

        public CircleRepresentation(GameParams Settings)
        {
            color = (Color)(Settings["color"] ?? Color.White);
        }

        public override void Initialize()
        {
            coPosition = Parent.AddProperty<Vector2>("position", Vector2.Zero);
            coRotation = Parent.AddProperty<double>("rotation", 0.0f);
            coGeometry = Parent.AddIProperty<CircleGeometryProperty>("geometry", new CircleGeometryProperty() { Radius = 1 });
        }

        public override void LoadContent(ContentManager Content)
        {
            circle = Content.Load<Texture2D>(@"Sprites/scalableCircle");
        }

        public override void Update(float elapsedMillseconds)
        { }

        public override void Draw(SpriteBatch spriteBatch, BasicEffect basicEffect)
        {
            spriteBatch.Draw(circle, coPosition.Value, null, color, (float)coRotation.Value, new Vector2(circle.Width / 2, circle.Height / 2), (float)(2.0f * (coGeometry as CircleGeometryProperty).Radius / circle.Width), SpriteEffects.None, 0.0f);
        }
    }
}
