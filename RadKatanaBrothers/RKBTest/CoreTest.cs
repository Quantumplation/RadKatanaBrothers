using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RadKatanaBrothers;

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
            Factory.RegisterCallback<GraphicsRepresentation>((settings) => new GraphicsRepresentation(settings));

            world.AddEntity<Player>(id: "Player");

            Entity player = world.GetEntity<Player>(id: "Player");
            Manager render = world.GetManager<RenderManager>(id: "RenderManager");

            if (player != null)
            {
                player.AddRepresentation<GraphicsRepresentation>(id: "Graphics");
                Property<int> health = player.AddProperty<int>(id: "Health", value: 100);
            }
        }
    }
}
