using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace RadKatanaBrothers
{
    public class PhysicsRepresentation : Representation
    {
        GeometryProperty coGeometry;
        Property<double> coMass;
        Property<double> coRotation;
        Property<double> coAngularVelocity;
        Property<double> coAngularAcceleration;
        Property<Vector2> coPosition;
        Property<Vector2> coVelocity;
        Property<Vector2> coAcceleration;
        Dictionary<Vector2, Vector2> coForces;

        public GeometryProperty Geometry
        {
            get { return coGeometry; }
            set { coGeometry = value; }
        }

        public Vector2 Position
        {
            get { return coPosition.Value; }
            set { coPosition.Value = value; }
        }

        public double Rotation
        {
            get { return coRotation.Value; }
            set { coRotation.Value = value; }
        }

        public Dictionary<Vector2, Vector2> Forces
        {
            get { return coForces; }
        }

        public PhysicsRepresentation()
        {
            coForces = new Dictionary<Vector2, Vector2>();
        }

        public override void Initialize()
        {
            coGeometry = Parent.AddIProperty<GeometryProperty>("geometry", new CircleGeometryProperty() { Radius = 1 });
            
            coPosition = Parent.AddProperty<Vector2>("position", Vector2.Zero);
            coRotation = Parent.AddProperty<double>("rotation", 0.0f);
            coMass = Parent.AddProperty<double>("mass", 1.0f);

            coVelocity = Parent.AddProperty<Vector2>("velocity", Vector2.Zero);
            coAngularVelocity = Parent.AddProperty<double>("angularVelocity", 0.0f);

            coAcceleration = Parent.AddProperty<Vector2>("acceleration", Vector2.Zero);
            coAngularAcceleration = Parent.AddProperty<double>("angularAcceleration", 0.0f);

            coGeometry.Position = coPosition.Value;
        }

        public virtual void ApplyForce(Vector2 force) { ApplyForce(force, Vector2.Zero); }
        public virtual void ApplyForce(Vector2 force, Vector2 origin)
        {
            if (!coForces.ContainsKey(origin))
                coForces[origin] = Vector2.Zero;
            coForces[origin] += force;
        }

        public void Update(float elapsedMilliseconds)
        {
            float elapsedSeconds = elapsedMilliseconds / 1000;
            coAcceleration.Value = Vector2.Zero;
            foreach (var force in coForces)
            {
                coAcceleration.Value += force.Value / (float)coMass.Value;
                coAngularAcceleration.Value += (force.Key.X * force.Value.Y - force.Value.X * force.Key.Y) / coMass.Value;
            }
            coForces.Clear();

            coVelocity.Value += coAcceleration.Value * elapsedSeconds;
            coPosition.Value += coVelocity.Value * elapsedSeconds;

            coAngularVelocity.Value += coAngularAcceleration.Value * elapsedSeconds;
            coRotation.Value += coAngularVelocity.Value * elapsedSeconds;

            if (coGeometry is ISweptGeometry)
                (coGeometry as ISweptGeometry).Sweep = coVelocity.Value;
            coGeometry.Position = coPosition.Value;
        }
    }
}
