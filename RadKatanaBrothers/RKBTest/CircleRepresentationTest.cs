using RadKatanaBrothers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace RKBTest
{
    
    
    /// <summary>
    ///This is a test class for CircleRepresentationTest and is intended
    ///to contain all CircleRepresentationTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CircleRepresentationTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for CircleRepresentation Constructor
        ///</summary>
        [TestMethod()]
        public void CircleRepresentationConstructorTest()
        {
            World.Initialize();
            Entity entity = Factory.Produce<Entity>();
            entity.AddIProperty<CircleGeometryProperty>("data", new CircleGeometryProperty() { Radius = 160 });
            entity.AddRepresentation<CircleRepresentation>("circle", new GameParams()
            {
                {"color", Color.White}
            });
            entity.GetRepresentation<CircleRepresentation>("circle");
        }

        /// <summary>
        ///A test for Initialize
        ///</summary>
        [TestMethod()]
        public void InitializeTest()
        {
            World.Initialize();
            Entity entity = Factory.Produce<Entity>();
            entity.AddIProperty<CircleGeometryProperty>("data", new CircleGeometryProperty() { Radius = 160 });
            entity.AddRepresentation<CircleRepresentation>("circle", new GameParams()
            {
                {"color", Color.White}
            });
            entity.Initialize();
        }
    }
}
