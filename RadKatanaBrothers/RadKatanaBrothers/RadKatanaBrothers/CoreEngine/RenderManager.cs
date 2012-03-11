using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RadKatanaBrothers
{
    public class RenderManager : Manager
    {
        SpriteBatch spriteBatch;
        List<GraphicsRepresentation> representations;

        public RenderManager()
        {
            representations = new List<GraphicsRepresentation>();
        }

        public override void AddRepresentation(Representation rep)
        {
            representations.Add(rep as GraphicsRepresentation);
        }

        public void LoadContent(ContentManager Content, SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;
            foreach (var representation in representations)
                representation.LoadContent(Content);
        }

        public override void Run()
        {
            try
            {
                spriteBatch.Begin();
                foreach (var representation in representations)
                    representation.Draw(spriteBatch);
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
