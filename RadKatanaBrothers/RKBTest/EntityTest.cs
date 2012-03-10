using RadKatanaBrothers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

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
        public void AddAndGetRepresentationTestHelper<T>(T representation, string id)
            where T : Representation
        {
            Entity target = new Entity();
            T expected = representation;
            T actual;
            //target.AddRepresentation<T>(id);
            //actual = target.GetRepresentation<T>(id);
            //Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void AddAndGetRepresentationTest()
        {
//            AddAndGetRepresentationTestHelper<GraphicsRepresentation>(Factory.Produce<GraphicsRepresentation>("test"), "test");
        }
    }
}
