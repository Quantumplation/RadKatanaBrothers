using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RadKatanaBrothers
{
    public class MeshRepresentation : GraphicsRepresentation
    {
        Property<Vector2> coPosition;
        Property<double> coRotation;
        VertexPositionColor[] vertices;
        Color color;

        /* Settings[0]: the color of the shape
         * All other items in settings are Vector3s representing the vertices
         */
        public MeshRepresentation(GameParams Settings)
        {
            color = (Color)Settings[0].Value;
            vertices = new VertexPositionColor[Settings.Count - 1];
            for (int i = 1; i < Settings.Count; ++i)
                vertices[i - 1] = new VertexPositionColor((Vector3)Settings[i].Value, color);
        }

        public override void Initialize()
        {
            coPosition = Parent.AddProperty<Vector2>("position", Vector2.Zero);
            coRotation = Parent.AddProperty<double>("rotation", 0.0f);
        }

        public override void LoadContent(ContentManager Content)
        { }

        public override void Update(float elapsedMillseconds)
        { }

        public override void Draw(SpriteBatch spriteBatch, BasicEffect basicEffect)
        {
            basicEffect.World = Matrix.CreateRotationZ((float)coRotation.Value) * Matrix.CreateTranslation(new Vector3(coPosition.Value, 0f));
            basicEffect.CurrentTechnique.Passes[0].Apply();
            basicEffect.GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleStrip, vertices, 0, vertices.Count() - 2);
        }
    }
}
