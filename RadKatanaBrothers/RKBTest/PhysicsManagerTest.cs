using RadKatanaBrothers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace RKBTest
{
    
    
    /// <summary>
    ///This is a test class for PhysicsManagerTests and is intended
    ///to contain all PhysicsManagerTests Unit Tests
    ///</summary>
    [TestClass()]
    public class PhysicsManagerTests
    {
        /// <summary>
        ///A test for CheckCollision
        ///</summary>
        [TestMethod()]
        public void CheckCircleCollisionTest()
        {
            for (float x = 0; x < 40; x+= 0.1f)
            {
                GeometryProperty objA = new CircleGeometryProperty() { Position = Vector2.Zero, Radius = 10.0f }; // TODO: Initialize to an appropriate value
                GeometryProperty objB = new CircleGeometryProperty() { Position = Vector2.UnitX * x, Radius = 5.0f }; // TODO: Initialize to an appropriate value
                bool expected = x < 15;
                bool actual;
                actual = PhysicsManager.CheckCollision(objA, objB);
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod()]
        public void CheckCircleSweptCollisionTest()
        {
            for (float y = 0; y < MathHelper.TwoPi; y += 0.1f)
            {
                for (float x = 0; x < 40; x += 0.1f)
                {
                    Vector2 position = new Vector2((float)Math.Cos(y) * x, (float)Math.Sin(y) * x);
                    GeometryProperty objA = new SweptCircleGeometryProperty() { Position = Vector2.Zero, Radius = 10.0f }; // TODO: Initialize to an appropriate value
                    GeometryProperty objB = new SweptCircleGeometryProperty() { Position = position, Radius = 5.0f }; // TODO: Initialize to an appropriate value
                    bool expected = position.Length() < 15;
                    bool actual;
                    actual = PhysicsManager.CheckCollision(objA, objB);
                    Assert.AreEqual(expected, actual);
                }
            }
        }

        [TestMethod()]
        public void CheckPolygonCollisionTest()
        {
            GeometryProperty objA = new PolygonGeometryProperty(new Vector2[] 
                                                                    { new Vector2(-1, -6),new Vector2(0, -6),
                                                                      new Vector2(7, 1)  ,new Vector2(4, 5),
                                                                      new Vector2(-6, 0) ,new Vector2(-1, -6) }) { Position = Vector2.Zero };

            for (int x = 0; x < 40; x++)
            {
                GeometryProperty objB = new PolygonGeometryProperty(new Vector2[] 
                                                                { new Vector2(-1, -6),new Vector2(0, -6),
                                                                    new Vector2(7, 1)  ,new Vector2(4, 5),
                                                                    new Vector2(-6, 0) ,new Vector2(-1, -6) }) { Position = new Vector2(x, 0) };
                bool expected = x <= 12;
                bool actual = PhysicsManager.CheckCollision(objA, objB);
                Assert.AreEqual(expected, actual);
            }
        }

        /// <summary>
        ///A test for Run
        ///</summary>
        [TestMethod()]
        public void RunTest()
        {
            PhysicsManager target = new PhysicsManager(); // TODO: Initialize to an appropriate value
            PhysicsRepHelper.onCreated += target.AddRepresentation;
            PhysicsRepHelper.onTerminated += target.RemoveRepresentation;
            Factory.RegisterManager<PhysicsManager>(target, typeof(PhysicsRepHelper));
            Factory.RegisterCallback<PhysicsRepHelper>((settings) => new PhysicsRepHelper());
            Entity entA = new Entity();
            entA.AddProperty<Vector2>("position", Vector2.Zero);
            entA.AddIProperty<GeometryProperty>("geometry", new CircleGeometryProperty() { Radius = 10 });
            entA.AddRepresentation<PhysicsRepHelper>("physics", new GameParams());
            //PhysicsRepHelper objA = new PhysicsRepHelper() { Parent = entA };
            //objA.Create();

            Entity entB = new Entity();
            entB.AddProperty<Vector2>("position", Vector2.UnitX * 5);
            entB.AddIProperty<GeometryProperty>("geometry", new CircleGeometryProperty() { Radius = 10 });
            entB.AddRepresentation<PhysicsRepHelper>("physics", new GameParams());
            //PhysicsRepHelper objB = new PhysicsRepHelper() { Parent = entB };
            //objB.Create();

            entA.Initialize();
            entB.Initialize();

            //target.AddRepresentation(objA);
            //target.AddRepresentation(objB);
            target.Run(1000f);

            Assert.AreEqual(entA.GetRepresentation<PhysicsRepHelper>(id: "physics").appliedForce - PhysicsManager.Gravity, Vector2.UnitX * -5f);
            Assert.AreEqual(entB.GetRepresentation<PhysicsRepHelper>(id: "physics").appliedForce - PhysicsManager.Gravity, Vector2.UnitX * 5f);
        }
    }

    public class PhysicsRepHelper : PhysicsRepresentation
    {
        public Vector2 appliedForce;
        public Vector2 appliedOrigin;
        public override void ApplyForce(Vector2 force, Vector2 origin)
        {
            appliedForce += force;
            appliedOrigin += origin;
            base.ApplyForce(force, origin);
        }
    }
}
