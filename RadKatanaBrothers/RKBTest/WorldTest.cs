using RadKatanaBrothers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace RKBTest
{
    
    
    /// <summary>
    ///This is a test class for WorldTest and is intended
    ///to contain all WorldTest Unit Tests
    ///</summary>
    [TestClass()]
    public class WorldTest
    {
        /// <summary>
        ///A test for World Constructor
        ///</summary>
        [TestMethod()]
        public void WorldConstructorTest()
        {
            World target = new World();
            Assert.IsFalse(target.GetManager<RenderManager>(id: "RenderManager") != null);
        }

        /// <summary>
        ///A test for AddEntity and GetEntity
        ///</summary>
        public void AddAndGetEntityTestHelper<T>(string id) where T : Entity
        {
            World target = new World();
            Entity expected = null;
            Entity actual;
            target.AddEntity<T>(id);
            actual = target.GetEntity<T>(id);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void AddAndGetEntityTest()
        {
            AddAndGetEntityTestHelper<Player>("test");
        }

        /// <summary>
        ///A test for AddManager and GetManager
        ///</summary>
        public void AddAndGetManagerTestHelper<T>(T manager, string id) where T : Manager
        {
            World target = new World();
            Manager expected = manager;
            Manager actual;
            target.AddManager<T>(id);
            actual = target.GetManager<T>(id);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void AddAndGetManagerTest()
        {
 //           AddAndGetManagerTestHelper<RenderManager>(Factory.Produce<RenderManager>("test"), "test");
        }

        /// <summary>
        ///A test for Load
        ///</summary>
        //[TestMethod()]
        public void LoadTest()
        {
            World target = new World(); // TODO: Initialize to an appropriate value
            string filename = string.Empty; // TODO: Initialize to an appropriate value
            target.Load(filename);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }
    }
}
