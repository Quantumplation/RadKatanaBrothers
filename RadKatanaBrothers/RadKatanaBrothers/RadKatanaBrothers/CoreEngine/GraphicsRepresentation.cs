using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace RadKatanaBrothers
{
    public class GraphicsRepresentation : Representation
    {
        string spriteName;
        Property<Vector2> location;
        Texture2D sprite;
        public GraphicsRepresentation(GameParams Settings)
        {
            spriteName = (string)Settings["spriteName"];
            location = (Property<Vector2>)Settings["location"];
        }

        public void LoadContent(ContentManager Content)
        {
            sprite = Content.Load<Texture2D>(spriteName);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, location.Value, Color.White);
        }
    }
}
