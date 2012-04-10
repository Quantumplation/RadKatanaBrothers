using RadKatanaBrothers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;

namespace RKBTest
{
    
    
    /// <summary>
    ///This is a test class for EntityTest and is intended
    ///to contain all EntityTest Unit Tests
    ///</summary>
    [TestClass()]
    public class EntityTest
    {
        /// <summary>
        ///A test for Entity Constructor
        ///</summary>
        [TestMethod()]
        public void EntityConstructorTest()
        {
            Entity target = new Entity();
        }

        /// <summary>
        ///A test for AddProperty
        ///</summary>
        public void AddPropertyTestHelper<T>(string id, T test1, T test2)
        {
            Entity target = new Entity();
            Property<T> value1 = target.AddProperty<T>(id, test1);
            Assert.IsTrue(value1 == target.AddProperty<T>(id, test1));
            Assert.IsTrue(value1 == target.AddProperty<T>(id, test2));
        }

        [TestMethod()]
        public void AddPropertyTest()
        {
            AddPropertyTestHelper<int>("Health", 100, 42);
        }

        /// <summary>
        ///A test for AddRepresentation and GetRepresentation
        ///</summary>
        [TestMethod()]
        public void GraphicsRepresentationTest()
        {
            Entity target = new Entity();
            target.AddRepresentation<SpriteRepresentation>(id: "Graphics", settings: new GameParams
            {
                {"spriteName", "Sprites/test"},
                {"location", target.AddProperty<Vector2>("location", Vector2.Zero)},
                {"numOfImages", 2},
                {"numOfColumns", 2},
                {"numOfRows", 1},
                {"animations", new Dictionary<string, Animation>
                {
                    {"default", new Animation(start: 0, end: 1, imagesPerSecond: 1.0f)}
                }},
                {"currentAnimation", "default"},
            });
            target.AddRepresentation<SpriteRepresentation>(id: "Default", settings: new GameParams());
            SpriteRepresentation gr = target.GetRepresentation<SpriteRepresentation>(id: "Graphics");
        }
    }
}
