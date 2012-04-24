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
        ///A test for AddEntity and GetEntity
        ///</summary>
        [TestMethod()]
        public void AddAndGetEntityTest()
        {
            World.Initialize();
            Entity expected = new Entity();
            Entity actual;
            World.AddEntity<Entity>("test");
            actual = World.GetEntity<Entity>("test");
            Assert.IsTrue(actual != null);
            Assert.IsTrue(expected != null);
            //Assert.IsTrue(expected == actual);
        }

        /// <summary>
        ///A test for AddManager and GetManager
        ///</summary>
        public void AddAndGetManagerTestHelper<T>(T manager, string id) where T : Manager, new()
        {
            World.Initialize();
            T expected = manager;
            T actual;
            World.AddManager<T>(id);
            actual = World.GetManager<T>(id);
            //Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void AddAndGetManagerTest()
        {
            AddAndGetManagerTestHelper<RenderManager>(Factory.Produce<RenderManager>(), "test");
        }

        ///<summary>
        ///A test for RunManagers
        ///</summary>
        public void RunManagersTestHelper()
        {
            World.Initialize();
            World.AddManager<RenderManager>(id: "test");
            RenderManager test = World.GetManager<RenderManager>(id: "test");
            //target.RunManagers();
        }

        [TestMethod()]
        public void RunManagersTest()
        {
            RunManagersTestHelper();
        }
    }
}
