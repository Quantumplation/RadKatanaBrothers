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

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Window.AllowUserResizing = false;
            Window.Title = "Rad Katana Bros. and the Insane Jetpack of Magic";
            graphics.PreferredBackBufferWidth = 720;
            graphics.PreferredBackBufferHeight = 720;
            graphics.ApplyChanges();
            IsMouseVisible = true;
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
            World.Initialize();
            World.LoadLevelOne();
            //world.AddEntity<Entity>(id: "Shape");
            //world.AddEntity<Player>(id: "Player");
            //Entity player = world.GetEntity<Player>(id: "Player");
            //player.AddIProperty<GeometryProperty>(id: "geometry", value: new CircleGeometryProperty() { Radius = 50 });
            //player.AddProperty<Vector2>("position", new Vector2(300, 250));
            //player.AddRepresentation<SpriteRepresentation>(id: "Graphics", settings: new GameParams
            //{
            //    {"spriteName", "Sprites/Circle"},
            //    {"numOfImages", 1},
            //    {"numOfColumns", 1},
            //    {"numOfRows", 1},
            //    {"origin", Vector2.One * 50},
            //    {"animations", new Dictionary<string, Animation>
            //    {
            //        {"default", new Animation(start: 0, end: 0, imagesPerSecond: 2.0f)}
            //    }},
            //});
            //player.AddRepresentation<PhysicsRepresentation>(id: "Physics", settings: null);
            //player.AddRepresentation<GameplayRepresentation>(id: "Gameplay", settings: null);
            //player.GetRepresentation<PhysicsRepresentation>(id: "Physics").ApplyForce(Vector2.UnitY * -3000);
            //player.Initialize();

            //Entity shape = world.GetEntity<Entity>(id: "Shape");
            //GeometryProperty geo = new PolygonGeometryProperty(new List<Vector2>
            //{
            //    new Vector2(0, -16),
            //    new Vector2(48, 96),
            //    new Vector2(7, 2),
            //    new Vector2(-51, 6),
            //    new Vector2(-13, -7)
            //});
            //shape.AddIProperty<GeometryProperty>("geometry", geo);
            //shape.AddProperty<Vector2>("position", new Vector2(250, 100));
            //shape.AddRepresentation<PhysicsRepresentation>("physics", null);
            //shape.AddRepresentation<MeshRepresentation>(id: "Power", settings: new GameParams
            //{
            //    {"color", Color.DarkGoldenrod}
            //});
            ////shape.AddRepresentation<MeshRepresentation>(id: "Wisdom", settings: new GameParams
            ////{
            ////    {"color", Color.DarkGoldenrod},
            ////    {"first", new Vector3(-16, 0, 0)},
            ////    {"second", new Vector3(0, 32, 0)},
            ////    {"third", new Vector3(-32, 32, 0)}
            ////});
            ////shape.AddRepresentation<MeshRepresentation>(id: "Courage", settings: new GameParams
            ////{
            ////    {"color", Color.Yellow},
            ////    {"first", new Vector3(16, 0, 0)},
            ////    {"second", new Vector3(32, 32, 0)},
            ////    {"third", new Vector3(0, 32, 0)}
            ////});

            //shape.Initialize();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            World.GetManager<RenderManager>(id: "graphics").LoadContent(Content, GraphicsDevice);
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
            World.RunAllManagers((float)gameTime.ElapsedGameTime.TotalMilliseconds);
            base.Draw(gameTime);
        }
    }
}
