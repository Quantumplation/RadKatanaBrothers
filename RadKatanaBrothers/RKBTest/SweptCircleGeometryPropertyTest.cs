using RadKatanaBrothers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.Xna.Framework;

namespace RKBTest
{
    
    
    /// <summary>
    ///This is a test class for SweptCircleGeometryPropertyTests and is intended
    ///to contain all SweptCircleGeometryPropertyTests Unit Tests
    ///</summary>
    [TestClass()]
    public class SweptCircleGeometryPropertyTests
    {
        /// <summary>
        ///A test for Furthest
        ///</summary>
        [TestMethod()]
        public void FurthestTest()
        {
            for (double x = 0; x < MathHelper.TwoPi; x += 0.5)
            {
                Vector2 sweep = new Vector2((float)Math.Cos(x), (float)Math.Sin(x));
                for (double y = 0.01; y < 5; y++)
                {
                    sweep *= (float)y;
                    for (double z = 0.01; z < 10; z += 0.5f)
                    {
                        SweptCircleGeometryProperty target = new SweptCircleGeometryProperty() { Position = Vector2.Zero, Radius = (float)z, Sweep = sweep }; // TODO: Initialize to an appropriate value
                        for (double w = 0; w < MathHelper.TwoPi; w += Math.PI / 50)
                        {
                            Vector2 Direction = new Vector2((float)Math.Cos(w), (float)Math.Sin(w));
                            Direction.Normalize();
                            Vector2 expected = Vector2.Zero;
                            if (Vector2.Dot(sweep, Direction) > 0)
                                expected = sweep + Direction * (float)z;
                            else
                                expected = Direction * (float)z;
                            Vector2 actual;
                            actual = target.Furthest(Direction);
                            Assert.IsTrue(Math.Abs((expected - actual).LengthSquared()) < 0.1f);
                        }
                    }
                    sweep /= (float)y;
                }
            }
            Assert.AreEqual(Vector2.Zero, new SweptCircleGeometryProperty() { Radius = 10, Sweep = Vector2.One }.Furthest(Vector2.Zero));
        }

        [TestMethod()]
        public void RightAngleFurthest()
        {
            for (double w = 0; w < MathHelper.TwoPi; w += MathHelper.Pi / 50)
            {
                Vector2 Sweep = new Vector2((float)Math.Cos(w), (float)Math.Sin(w));
                Vector2 Direction = new Vector2((float)Math.Cos(w + Math.PI/2), (float)Math.Sin(w + Math.PI/2));
                SweptCircleGeometryProperty target = new SweptCircleGeometryProperty() { Position = Vector2.Zero, Radius = (float)10, Sweep = Sweep }; // TODO: Initialize to an appropriate value
                Direction.Normalize();
                Vector2 expected = Vector2.Zero;
                if (Vector2.Dot(Sweep, Direction) > 0)
                    expected = Sweep + Direction * 10;
                else
                    expected = Direction * 10;
                Vector2 actual;
                actual = target.Furthest(Direction);
                Assert.IsTrue(Math.Abs((expected - actual).LengthSquared()) < 0.1f);
            }
        }
    }
}
