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
        PolygonGeometryProperty coGeometry;
        VertexPositionColor[] vertices;
        Color color;

        public MeshRepresentation(GameParams Settings)
        {
            color = (Color)Settings["color"];
        }

        public override void Initialize()
        {
            coPosition = Parent.AddProperty<Vector2>("position", Vector2.Zero);
            coRotation = Parent.AddProperty<double>("rotation", 0.0f);
            coGeometry = Parent.AddIProperty<PolygonGeometryProperty>("geometry", new PolygonGeometryProperty(null));
            List<Vector2> points = coGeometry.Points;
            vertices = new VertexPositionColor[3 * (points.Count + 1)];
            Vector2 center = Vector2.Zero;
            foreach (var point in points)
                center += point;
            center /= points.Count;
            vertices[0] = new VertexPositionColor(new Vector3(center, 0), color);
            vertices[1] = new VertexPositionColor(new Vector3(points[0], 0), color);
            vertices[2] = new VertexPositionColor(new Vector3(points[1], 0), color);
            for (int i = 1; i < points.Count; ++i)
            {
                vertices[3*i] = new VertexPositionColor(new Vector3(center, 0), color);
                vertices[3*i + 1] = new VertexPositionColor(new Vector3(points[i - 1], 0), color);
                vertices[3*i + 2] = new VertexPositionColor(new Vector3(points[i], 0), color);
            }
            vertices[3*points.Count] = new VertexPositionColor(new Vector3(center, 0), color);
            vertices[3*points.Count + 1] = new VertexPositionColor(new Vector3(points[points.Count - 1], 0), color);
            vertices[3*points.Count + 2] = new VertexPositionColor(new Vector3(points[0], 0), color);
        }

        public override void LoadContent(ContentManager Content)
        { }

        public override void Update(float elapsedMillseconds)
        { }

        public override void Draw(SpriteBatch spriteBatch, BasicEffect basicEffect)
        {
            basicEffect.World = Matrix.CreateRotationZ((float)coRotation.Value) * Matrix.CreateTranslation(new Vector3(coPosition.Value, 0f));
            basicEffect.CurrentTechnique.Passes[0].Apply();
            basicEffect.GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleList, vertices, 0, vertices.Count() / 3);
        }
    }
}
