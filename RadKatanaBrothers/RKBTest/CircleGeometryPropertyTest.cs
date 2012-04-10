using RadKatanaBrothers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.Xna.Framework;

namespace RKBTest
{
    
    
    /// <summary>
    ///This is a test class for CircleGeometryPropertyTests and is intended
    ///to contain all CircleGeometryPropertyTests Unit Tests
    ///</summary>
    [TestClass()]
    public class CircleGeometryPropertyTests
    {
        /// <summary>
        ///A test for Furthest
        ///</summary>
        [TestMethod()]
        public void FurthestTest()
        {
            Vector2 position = Vector2.One * 3;
            for (float x = 0; x < Math.PI * 2; x += 0.01f)
            {
                Vector2 Direction = new Vector2((float)Math.Cos(x), (float)Math.Sin(x));
                Direction.Normalize();
                for (int r = 1; r < 10; r++)
                {
                    CircleGeometryProperty target = new CircleGeometryProperty() { Position = position, Radius = r };
                    Vector2 expected = position + Direction * r;
                    Vector2 actual;
                    actual = target.Furthest(Direction);
                    Assert.AreEqual(expected, actual);
                }
            }
            Assert.AreEqual(Vector2.Zero, new CircleGeometryProperty() { Radius = 10 }.Furthest(Vector2.Zero));
        }
    }
}
