using RadKatanaBrothers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace RKBTest
{
    
    
    /// <summary>
    ///This is a test class for PolygonGeometryPropertyTests and is intended
    ///to contain all PolygonGeometryPropertyTests Unit Tests
    ///</summary>
    [TestClass()]
    public class PolygonGeometryPropertyTests
    {
        /// <summary>
        ///A test for Furthest
        ///</summary>
        [TestMethod()]
        public void FurthestTest()
        {
            Vector2[] points = {    new Vector2(1, 1), new Vector2(2, 1), 
                                    new Vector2(3, .5f), new Vector2(4, 0), 
                                    new Vector2(3.9f, -0.1f), new Vector2(3.5f, -0.5f),
                                    new Vector2(2, -1.5f), new Vector2(1, -2),
                                    new Vector2(0, -2), new Vector2(-1, -1.5f),
                                    new Vector2(-1, -1), new Vector2(-0.5f, 0)};
            PolygonGeometryProperty target = new PolygonGeometryProperty(points);
            for(double x = 0; x < MathHelper.TwoPi; x+= 0.5)
            {
                Vector2 Direction = new Vector2((float)Math.Cos(x), (float)Math.Sin(x));
                Vector2 maxSoFar = points[0];
                foreach(Vector2 point in points)
                {
                    if(Vector2.Dot(Direction, point) > Vector2.Dot(Direction, maxSoFar) || maxSoFar == Vector2.Zero)
                        maxSoFar = point;
                }
                Vector2 expected = maxSoFar;
                Vector2 actual = target.Furthest(Direction);
                Assert.AreEqual(expected, actual);
            }
            Assert.AreEqual(Vector2.Zero, new PolygonGeometryProperty(new List<Vector2> { Vector2.Zero, Vector2.UnitX, Vector2.One, Vector2.UnitY }).Furthest(Vector2.Zero));
        }

        [TestMethod]
        public void HandlesIncreasingOnReverseCorrectly()
        {
            GeometryProperty objA = new PolygonGeometryProperty(new Vector2[] 
                                                                    { new Vector2(-1, -6),new Vector2(0, -6),
                                                                      new Vector2(7, 1)  ,new Vector2(4, 5),
                                                                      new Vector2(-6, 0) ,new Vector2(-1, -6) }) { Position = Vector2.Zero };

            Vector2 expected = new Vector2(-6, 0);
            Vector2 actual = objA.Furthest(-Vector2.UnitX);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void AccountsForPosition()
        {
            GeometryProperty objA = new PolygonGeometryProperty(new Vector2[] 
                                                                    { new Vector2(-1, -6),new Vector2(0, -6),
                                                                      new Vector2(7, 1)  ,new Vector2(4, 5),
                                                                      new Vector2(-6, 0) ,new Vector2(-1, -6) }) { Position = Vector2.One * 10 };

            Vector2 expected = new Vector2(-6, 0) + Vector2.One * 10;
            Vector2 actual = objA.Furthest(-Vector2.UnitX);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidOperationException))]
        public void EmptyHullTest()
        {
            PolygonGeometryProperty target = new PolygonGeometryProperty(null);
            target.Furthest(Vector2.One);
        }
    }
}
