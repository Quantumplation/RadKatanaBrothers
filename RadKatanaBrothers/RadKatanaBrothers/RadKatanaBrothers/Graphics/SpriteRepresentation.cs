using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace RadKatanaBrothers
{
    public struct Animation
    {
        public Animation(int start, int end, float imagesPerSecond)
        {
            StartFrame = start;
            EndFrame = end;
            AnimationSpeed = 1.0f / imagesPerSecond;
        }

        public int StartFrame;
        public int EndFrame;
        public float AnimationSpeed;
    }

    public class SpriteRepresentation : GraphicsRepresentation
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

        string spriteName;
        Texture2D sprite;
        int numOfRows, numOfColumns;
        Rectangle[] images;
        int currentIndex;
        float timeElapsed;
        Vector2 origin;

        Property<Vector2> coLocation;
        Property<double> coRotation;

        Dictionary<string, Animation> animations;
        Animation currAnimation;
        public Animation CurrentAnimation
        {
            get { return currAnimation; }
            set
            {
                currAnimation = value;
                currentIndex = value.StartFrame;
                timeElapsed = 0.0f;
            }
        }


        /*Parameters:
         * string spriteName: the sprite's filename
         * Vector2 location: the onscreen location of the object
         * int numOfImages, numOfColumns, numOfRows: self-explanatory
         * Dictionary <string, Animation> animations: every animation, with the name as the key
         * string CurrentAnimation: the animation being displayed
         */
        public SpriteRepresentation(GameParams Settings)
        {
            spriteName = (string)Settings["spriteName"] ?? "Sprites/test";
            images = new Rectangle[(int)(Settings["numOfImages"] ?? 2)];
            numOfColumns = (int)(Settings["numOfColumns"] ?? 2);
            numOfRows = (int)(Settings["numOfRows"] ?? 1);
            animations = (Dictionary<string, Animation>)Settings["animations"] ?? new Dictionary<string, Animation>();
            if (Settings.Any( kvp => kvp.Key == "origin" ))
                origin = (Vector2)Settings["origin"];
            if (!animations.ContainsKey("default"))
                animations.Add("default", new Animation(0, images.Length - 1, 1.0f));
            CurrentAnimation = animations[(string)Settings["currentAnimation"] ?? "default"];
        }

        public override void Initialize()
        {
            coLocation = Parent.AddProperty<Vector2>("position", Vector2.Zero);
            coRotation = Parent.AddProperty<double>("rotation", 0.0f);
        }

        public override void LoadContent(ContentManager Content)
        {
            sprite = Content.Load<Texture2D>(spriteName);
            int imageWidth = sprite.Bounds.Width / numOfColumns;
            int imageHeight = sprite.Bounds.Height / numOfRows;
            for (int i = 0; i < images.Length; ++i)
                images[i] = new Rectangle(
                    (i % numOfColumns) * imageWidth,
                    (i / numOfColumns) * imageHeight,
                    imageWidth,
                    imageHeight);
        }

        public override void Update(float elapsedMilliseconds)
        {
            timeElapsed += elapsedMilliseconds / 1000.0f;
            if (timeElapsed >= CurrentAnimation.AnimationSpeed)
            {
                if (++currentIndex > CurrentAnimation.EndFrame)
                    currentIndex -= (CurrentAnimation.EndFrame - CurrentAnimation.StartFrame + 1);
                timeElapsed -= CurrentAnimation.AnimationSpeed;
            }
        }

        public override void Draw(SpriteBatch spriteBatch, BasicEffect basicEffect)
        {
            spriteBatch.Draw(sprite, coLocation.Value, images[currentIndex], Color.White, (float)coRotation.Value, origin, 1.0f, SpriteEffects.None, 0.0f);
        }
    }
}
