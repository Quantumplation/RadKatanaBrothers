using RadKatanaBrothers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace RKBTest
{
    
    
    /// <summary>
    ///This is a test class for FactoryTests and is intended
    ///to contain all FactoryTests Unit Tests
    ///</summary>
    [TestClass()]
    public class FactoryTests
    {
        [TestMethod]
        public void ProduceTest()
        {
            bool called = false;
            Factory.RegisterCallback<RepresentationTestHelper>((settings) => { called = true; return new RepresentationTestHelper(settings["Value"] as String); });
            Assert.AreEqual(Factory.Produce<RepresentationTestHelper>(new GameParams { { "Value", "True" } }).Value, "True");
            Assert.IsTrue(called);
        }

        protected class ManagerTestHelper : Manager
        {
            public bool added = false;
            public override void AddRepresentation(Representation rep)
            {
                added = true;
            }
        }
        protected class RepresentationTestHelper : Representation
        {
            public RepresentationTestHelper()
            {
            }

            public RepresentationTestHelper(String value)
            {
                Value = value;
            }

            public String Value
            {
                get;
                set;
            }
        }

        [TestMethod]
        public void RegisterManagerTest()
        {
            ManagerTestHelper testHelper = new ManagerTestHelper();
            Factory.RegisterManager<ManagerTestHelper>(testHelper, typeof(RepresentationTestHelper));
            Factory.RegisterCallback<RepresentationTestHelper>((settings) => { return new RepresentationTestHelper(); });
            Factory.Produce<RepresentationTestHelper>(new GameParams { });
            Assert.IsTrue(testHelper.added);
        }
    }
}
