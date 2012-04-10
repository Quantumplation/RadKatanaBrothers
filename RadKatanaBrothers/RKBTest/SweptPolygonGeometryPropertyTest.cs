using RadKatanaBrothers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace RKBTest
{
    
    
    /// <summary>
    ///This is a test class for SweptPolygonGeometryPropertyTests and is intended
    ///to contain all SweptPolygonGeometryPropertyTests Unit Tests
    ///</summary>
    [TestClass()]
    public class SweptPolygonGeometryPropertyTests
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
            for (float y = 0.1f; y < 10; y++)
            {
                for (double w = 0; w < MathHelper.TwoPi; w += 0.5)
                {
                    Vector2 sweep = new Vector2((float)Math.Cos(w), (float)Math.Sin(w)) * y;
                    SweptPolygonGeometryProperty target = new SweptPolygonGeometryProperty(points) { Sweep = sweep };
                    for (double x = 0; x < MathHelper.TwoPi; x += 0.5)
                    {
                        Vector2 Direction = new Vector2((float)Math.Cos(x), (float)Math.Sin(x));
                        Vector2 maxSoFar = points[0];
                        foreach (Vector2 point in points)
                        {
                            if (Vector2.Dot(Direction, point) > Vector2.Dot(Direction, maxSoFar) || maxSoFar == Vector2.Zero)
                                maxSoFar = point;
                        }
                        Vector2 expected;
                        if (Vector2.Dot(Direction, sweep) <= 0)
                            expected = maxSoFar;
                        else
                            expected = maxSoFar + sweep;
                        Vector2 actual = target.Furthest(Direction);
                        Assert.AreEqual(expected, actual);
                    }
                }
            }
            Assert.AreEqual(Vector2.Zero, new SweptPolygonGeometryProperty(new List<Vector2> { Vector2.Zero, Vector2.UnitX, Vector2.One, Vector2.UnitY }) { Sweep = Vector2.One }.Furthest(Vector2.Zero));
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidOperationException))]
        public void EmptyHullTest()
        {
            SweptPolygonGeometryProperty target = new SweptPolygonGeometryProperty(null);
            target.Furthest(Vector2.One);
        }
    }
}
