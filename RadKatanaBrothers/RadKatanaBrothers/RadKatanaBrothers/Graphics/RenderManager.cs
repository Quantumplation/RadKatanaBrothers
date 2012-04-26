using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace RadKatanaBrothers
{
    public class RenderManager : Manager
    {
        SpriteBatch spriteBatch;
        BasicEffect basicEffect;
        ContentManager Content;
        List<GraphicsRepresentation> representations;

        public RenderManager()
        {
            representations = new List<GraphicsRepresentation>();
            CircleRepresentation.onCreated += this.AddRepresentation;
            MeshRepresentation.onCreated += this.AddRepresentation;
            SpriteRepresentation.onCreated += this.AddRepresentation;
            CircleRepresentation.onTerminated += this.RemoveRepresentation;
            MeshRepresentation.onTerminated += this.RemoveRepresentation;
            SpriteRepresentation.onTerminated += this.RemoveRepresentation;
        }

        public override void AddRepresentation(Representation rep)
        {
            representations.Add(rep as GraphicsRepresentation);
        }

        public override void ClearRepresentations()
        {
            representations.Clear();
        }

        public override void RemoveRepresentation(Representation rep)
        {
            representations.Remove(rep as GraphicsRepresentation);
        }

        public void LoadContent(ContentManager content, GraphicsDevice GraphicsDevice)
        {
            Content = content;
            basicEffect = new BasicEffect(GraphicsDevice);
            basicEffect.VertexColorEnabled = true;
            basicEffect.Projection = Matrix.CreateOrthographicOffCenter(
                0.0f,
                GraphicsDevice.Viewport.Width,
                GraphicsDevice.Viewport.Height,
                0,
                0,
                1);
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        public override void Run(float elapsedMilliseconds)
        {
            if (!World.Running)
                return;
            try
            {
                spriteBatch.Begin();
                foreach (var representation in representations)
                    representation.Update(elapsedMilliseconds);
                foreach (var representation in representations)
                    representation.Draw(spriteBatch, basicEffect);
            }
            catch (NullReferenceException)
            {
                foreach (var representation in representations)
                    representation.LoadContent(Content);
            }
            finally
            {
                spriteBatch.End();
            }
        }
    }
}
