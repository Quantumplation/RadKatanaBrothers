using RadKatanaBrothers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.Xna.Framework;

namespace RKBTest
{
    
    
    /// <summary>
    ///This is a test class for PhysicsRepresentationTests and is intended
    ///to contain all PhysicsRepresentationTests Unit Tests
    ///</summary>
    [TestClass()]
    public class PhysicsRepresentationTests
    {
        /// <summary>
        ///A test for ApplyForce
        ///</summary>
        [TestMethod()]
        public void ApplyForceTest()
        {
            Entity e = new Entity();
            PhysicsRepresentation target = new PhysicsRepresentation() { Parent = e };
            target.Initialize();
            Vector2 force = Vector2.UnitX;
            Vector2 origin = Vector2.Zero;
            int sum = 0;
            for (int x = 0; x < 10; x++)
            {
                sum += x;
                target.ApplyForce(force * x, origin);
            }
            Assert.AreEqual(sum*sum, target.Forces[origin].LengthSquared());
        }

        /// <summary>
        ///A test for Update
        ///</summary>
        [TestMethod()]
        public void UpdateLinearTest()
        {
            Entity e = new Entity();
            e.AddProperty<double>("mass", 5.0f );
            PhysicsRepresentation target = new PhysicsRepresentation() { Parent = e };
            target.Initialize();
            target.ApplyForce(Vector2.UnitX * 15);
            float elapsedMilliseconds = 1000f; // TODO: Initialize to an appropriate value
            target.Update(elapsedMilliseconds);
            Assert.AreEqual(9, target.Position.LengthSquared());
        }

        [TestMethod()]
        public void UpdateAngularTest()
        {
            Entity e = new Entity();
            e.AddProperty<double>("mass", 5.0f);
            PhysicsRepresentation target = new PhysicsRepresentation() { Parent = e };
            target.Initialize();
            target.ApplyForce(Vector2.UnitX * 15, Vector2.UnitY * 5);
            float elapsedMilliseconds = 1000f;
            target.Update(elapsedMilliseconds);
            Assert.AreEqual(9, target.Position.LengthSquared());
            Assert.AreEqual(-15, target.Rotation);
        }
    }
}
