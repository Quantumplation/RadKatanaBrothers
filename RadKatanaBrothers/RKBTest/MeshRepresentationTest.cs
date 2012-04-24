using RadKatanaBrothers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace RKBTest
{
    
    
    /// <summary>
    ///This is a test class for MeshRepresentationTest and is intended
    ///to contain all MeshRepresentationTest Unit Tests
    ///</summary>
    [TestClass()]
    public class MeshRepresentationTest
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
        ///A test for Initialize
        ///</summary>
        [TestMethod()]
        public void MeshRepresentationConstructorTest()
        {
            World.Initialize();
            List<Vector2> points = new List<Vector2>()
            {
                new Vector2(0, 16),
                new Vector2(16, 0),
                new Vector2(-16, 0)
            };
            Entity entity = Factory.Produce<Entity>();
            entity.AddIProperty<PolygonGeometryProperty>("triangle", new PolygonGeometryProperty(points));
            entity.AddRepresentation<MeshRepresentation>("mesh", new GameParams()
            {
                {"color", Color.White}
            });
            List<Vector2> testPoints = entity.AddIProperty<PolygonGeometryProperty>("triangle", null).Points;
            Assert.AreEqual(testPoints.Count, points.Count);
            for (int i = 0; i < points.Count; ++i)
                Assert.AreEqual(testPoints[i], points[i]);
        }

        [TestMethod()]
        public void InitializationTest()
        {
            World.Initialize();
            List<Vector2> points = new List<Vector2>()
            {
                new Vector2(0, 16),
                new Vector2(16, 0),
                new Vector2(-16, 0)
            };
            Entity entity = Factory.Produce<Entity>();
            entity.AddIProperty<PolygonGeometryProperty>("geometry", new PolygonGeometryProperty(points));
            entity.AddRepresentation<MeshRepresentation>("mesh", new GameParams()
            {
                {"color", Color.White}
            });
            entity.Initialize();
        }
    }
}
