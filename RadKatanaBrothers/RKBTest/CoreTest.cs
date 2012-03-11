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
            //world.Load(filename: "celebrian");

            Factory.RegisterManager<RenderManager>(new RenderManager(), typeof(GraphicsRepresentation));
            Factory.RegisterCallback<Entity>((settings) => new Entity());
            Factory.RegisterCallback<Player>((settings) => new Player());
            Factory.RegisterCallback<GraphicsRepresentation>((settings) => new GraphicsRepresentation(settings));

            world.AddEntity<Player>(id: "Player");
            world.AddManager<RenderManager>(id: "RenderManager");

            Entity player = world.GetEntity<Player>(id: "Player");
            RenderManager render = world.GetManager<RenderManager>(id: "RenderManager");
            player.AddRepresentation<GraphicsRepresentation>(id: "Graphics", settings: new GameParams
            {
                {"spriteName", "Sprites/test"},
                {"location", player.AddProperty<Vector2>("location", new Vector2(320, 240))}
            });
            Property<int> health = player.AddProperty<int>(id: "Health", value: 100);
        }
    }
}
