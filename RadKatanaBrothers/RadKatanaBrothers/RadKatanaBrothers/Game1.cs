using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace RadKatanaBrothers
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        World world;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            world = new World();
            world.LoadMap("test");
            world.AddEntity<Player>(id: "Player");
            world.AddEntity<Entity>(id: "Shape");
            Entity player = world.GetEntity<Player>(id: "Player");
            player.AddRepresentation<SpriteRepresentation>(id: "Graphics", settings: new GameParams
            {
                {"spriteName", "Sprites/PARTYHARD"},
                {"location", player.AddProperty<Vector2>("location", Vector2.Zero)},
                {"numOfImages", 2},
                {"numOfColumns", 2},
                {"numOfRows", 1},
                {"animations", new Dictionary<string, Animation>
                {
                    {"default", new Animation(start: 0, end: 1, imagesPerSecond: 2.0f)}
                }},
            });
            Entity shape = world.GetEntity<Entity>(id: "Shape");
            shape.AddRepresentation<MeshRepresentation>(id: "Power", settings: new GameParams
            {
                {"color", Color.DarkGoldenrod},
                {"first", new Vector3(636, 272, 0)},
                {"second", new Vector3(652, 304, 0)},
                {"third", new Vector3(620, 304, 0)}
            });
            shape.AddRepresentation<MeshRepresentation>(id: "Wisdom", settings: new GameParams
            {
                {"color", Color.DarkGoldenrod},
                {"first", new Vector3(620, 304, 0)},
                {"second", new Vector3(636, 336, 0)},
                {"third", new Vector3(604, 336, 0)}
            });
            shape.AddRepresentation<MeshRepresentation>(id: "Courage", settings: new GameParams
            {
                {"color", Color.Yellow},
                {"first", new Vector3(652, 304, 0)},
                {"second", new Vector3(668, 336, 0)},
                {"third", new Vector3(636, 336, 0)}
            });
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            world.GetManager<RenderManager>(id: "graphics").LoadContent(Content, GraphicsDevice);
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
                || Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            // TODO: Add your update logic here
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            world.RunAllManagers(gameTime);
            base.Draw(gameTime);
        }
    }
}
