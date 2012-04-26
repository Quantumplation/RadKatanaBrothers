using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace RadKatanaBrothers
{
    class TextRepresentation : GraphicsRepresentation
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

        Property<String> text;
        Property<Vector2> parentPosition;
        Property<Vector2> offset;
        Property<Color> color;

        SpriteFont font;

        public TextRepresentation(GameParams settings = null)
        {

        }

        public override void Initialize()
        {
            text = Parent.AddProperty<String>("text", "");
            parentPosition = Parent.AddProperty<Vector2>("position", Vector2.Zero);
            offset = Parent.AddProperty<Vector2>("textOffset", Vector2.Zero);
            color = Parent.AddProperty<Color>("color", Color.White);
        }

        public override void LoadContent(ContentManager Content)
        {
            font = Content.Load<SpriteFont>(@"Font/font");
        }

        public override void Update(float elapsedMilliseconds)
        {
            text.Value = (Parent.GetIProperty("score") as Property<int>).Value.ToString();
        }

        public override void Draw(SpriteBatch spriteBatch, BasicEffect basicEffect)
        {
            spriteBatch.DrawString(font, text.Value, parentPosition.Value + offset.Value - (font.MeasureString(text.Value) / 2) * Vector2.UnitX, color.Value);
        }
    }
}
