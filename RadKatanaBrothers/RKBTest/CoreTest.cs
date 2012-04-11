using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RadKatanaBrothers;
using Microsoft.Xna.Framework;

namespace RKBTest
{
    [TestClass]
    public class CoreTest
    {
        [TestMethod]
        public void APITest()
        {
            World world = new World();
            world.LoadMap(filename: "test");

            Factory.RegisterManager<RenderManager>(new RenderManager(), typeof(SpriteRepresentation));
            Factory.RegisterCallback<Entity>((settings) => new Entity());
            Factory.RegisterCallback<Player>((settings) => new Player());
            Factory.RegisterCallback<SpriteRepresentation>((settings) => new SpriteRepresentation(settings));

            world.AddEntity<Player>(id: "Player");
            world.AddManager<RenderManager>(id: "RenderManager");

            Entity player = world.GetEntity<Player>(id: "Player");
            RenderManager render = world.GetManager<RenderManager>(id: "RenderManager");
            player.AddRepresentation<SpriteRepresentation>(id: "Graphics", settings: new GameParams
            {
                {"spriteName", "Sprites/PARTYHARD"},
                {"numOfImages", 2},
                {"numOfColumns", 2},
                {"numOfRows", 1},
                {"imagesPerSecond", 2.0f}
            });
            
            Property<int> health = player.AddProperty<int>(id: "Health", value: 100);
        }
    }
}
