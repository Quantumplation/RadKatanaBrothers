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
            Assert.IsTrue(target.GetManager<RenderManager>(id: "RenderManager") != null);
        }

        /// <summary>
        ///A test for AddEntity and GetEntity
        ///</summary>
        public void AddAndGetEntityTestHelper<T>(T entity, string id) where T : Entity, new()
        {
            World target = new World();
            T expected = entity;
            T actual;
            target.AddEntity<T>(id);
            actual = target.GetEntity<T>(id);
            Assert.IsTrue(actual != null);
            Assert.IsTrue(expected != null);
            //Assert.IsTrue(expected == actual);
        }

        [TestMethod()]
        public void AddAndGetEntityTest()
        {
            AddAndGetEntityTestHelper<Player>(new Player(), "test");
        }

        /// <summary>
        ///A test for AddManager and GetManager
        ///</summary>
        public void AddAndGetManagerTestHelper<T>(T manager, string id) where T : Manager, new()
        {
            World target = new World();
            T expected = manager;
            T actual;
            target.AddManager<T>(id);
            actual = target.GetManager<T>(id);
            //Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void AddAndGetManagerTest()
        {
            //AddAndGetManagerTestHelper<RenderManager>(Factory.Produce<RenderManager>("test"), "test");
        }

        ///<summary>
        ///A test for RunManagers
        ///</summary>
        public void RunManagersTestHelper()
        {
            World target = new World();
            target.AddManager<RenderManager>(id: "test");
            RenderManager test = target.GetManager<RenderManager>(id: "test");
            target.RunManagers();
        }

        [TestMethod()]
        public void RunManagersTest()
        {
            RunManagersTestHelper();
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
