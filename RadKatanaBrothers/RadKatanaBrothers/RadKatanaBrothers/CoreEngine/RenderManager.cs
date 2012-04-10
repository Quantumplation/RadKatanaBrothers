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
        List<GraphicsRepresentation> representations;

        public RenderManager()
        {
            representations = new List<GraphicsRepresentation>();
        }

        public override void AddRepresentation(Representation rep)
        {
            representations.Add(rep as GraphicsRepresentation);
        }

        public void LoadContent(ContentManager Content, GraphicsDevice GraphicsDevice)
        {
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

            foreach (var representation in representations)
                representation.LoadContent(Content);
        }

        public override void Run(GameTime gameTime)
        {
            try
            {
                foreach (var representation in representations)
                    representation.Update(gameTime);
                spriteBatch.Begin();
                foreach (var representation in representations)
                    representation.Draw(spriteBatch, basicEffect);
                spriteBatch.End();
            }
            catch (NullReferenceException e)
            {
                Console.Error.WriteLine(e.Message);
                return;
            }
        }
    }
}
