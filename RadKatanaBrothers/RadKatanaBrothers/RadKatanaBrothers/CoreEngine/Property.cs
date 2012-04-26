using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.ComponentModel;

namespace RadKatanaBrothers
{
    public abstract class IProperty : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void propChanged(string value = "Value")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(value));
        }
    }

    public class Property<T> : IProperty
    {
        T value;
        public T Value
        {
            get { return value; }
            set { this.value = value; propChanged(); }
        }

        public Property(T value)
        {
            Value = value;
        }
    }

    public abstract class GeometryProperty : IProperty
    {
        public Vector2 Position
        {
            get;
            set;
        }

        public abstract Vector2 Furthest(Vector2 Direction);
    }

    public class CircleGeometryProperty : GeometryProperty
    {
        public float Radius
        {
            get;
            set;
        }

        public override Vector2 Furthest(Vector2 Direction)
        {
            if (Direction == Vector2.Zero)
                return Direction;
            Direction.Normalize();
            return Position + Direction * Radius;
        }
    }

    public interface ISweptGeometry
    {
        Vector2 Sweep
        {
            get;
            set;
        }
    }

    public class SweptCircleGeometryProperty : CircleGeometryProperty, ISweptGeometry
    {
        public Vector2 Sweep
        {
            get;
            set;
        }

        public override Vector2 Furthest(Vector2 Direction)
        {
            if (Direction == Vector2.Zero)
                return Direction;
            float dot = Vector2.Dot(Direction, Sweep);
            if (dot <= 0)
                return base.Furthest(Direction);
            else
            {
                Direction.Normalize();
                return base.Furthest(Direction) + Sweep;
            }
        }
    }

    public class PolygonGeometryProperty : GeometryProperty
    {
        List<Vector2> coConvex;
        public List<Vector2> Points
        {
            get { return coConvex; }
            set { }
        }

        public PolygonGeometryProperty(IEnumerable<Vector2> points)
        {
            coConvex = points != null ? new List<Vector2>(points) : new List<Vector2>();
        }

        public override Vector2 Furthest(Vector2 Direction)
        {
            if (coConvex.Count == 0)
                throw new InvalidOperationException("Hull is empty.");
            if (Direction == Vector2.Zero)
                return Direction;
            float prevDot = Vector2.Dot(coConvex[0], Direction);
            Vector2 prevPoint = coConvex[0];
            bool increasing = false;
            int dir = Vector2.Dot(coConvex[1], Direction) > prevDot ? 1 : -1;
            for(int x = (coConvex.Count + dir) % coConvex.Count;; x = ((x + coConvex.Count) + dir)%coConvex.Count)
            {
                float dot = Vector2.Dot(coConvex[x], Direction);
                if (dot > prevDot)
                    increasing = true;
                else
                    if (increasing)
                        return Position + prevPoint;
                prevDot = dot;
                prevPoint = coConvex[x]; 
            }
            throw new InvalidOperationException();
        }
    }

    public class SweptPolygonGeometryProperty : PolygonGeometryProperty, ISweptGeometry
    {
        public Vector2 Sweep
        {
            get;
            set;
        }

        public SweptPolygonGeometryProperty(IEnumerable<Vector2> points) : base(points)
        {
        }

        public override Vector2 Furthest(Vector2 Direction)
        {
            if (Direction == Vector2.Zero)
                return Direction;
            float dot = Vector2.Dot(Direction, Sweep);
            if (dot <= 0)
                return base.Furthest(Direction);
            else
            {
                Direction.Normalize();
                return base.Furthest(Direction) + Sweep;
            }
        }
    }
}
