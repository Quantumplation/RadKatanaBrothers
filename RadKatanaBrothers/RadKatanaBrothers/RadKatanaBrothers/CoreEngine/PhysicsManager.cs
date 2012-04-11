﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace RadKatanaBrothers
{
    public class PhysicsManager : Manager
    {
        List<PhysicsRepresentation> caPhysicalObjects;

        public PhysicsManager()
        {
            caPhysicalObjects = new List<PhysicsRepresentation>();
        }

        public override void AddRepresentation(Representation rep)
        {
            caPhysicalObjects.Add(rep as PhysicsRepresentation);
        }

        public override void Run(float elapsedMilliseconds)
        {
            List<Tuple<PhysicsRepresentation, PhysicsRepresentation>> resolvedPairs = new List<Tuple<PhysicsRepresentation, PhysicsRepresentation>>();
            foreach (var objA in caPhysicalObjects)
            {
                foreach (var objB in caPhysicalObjects)
                {
                    var tuple = Tuple.Create(objB, objA);
                    if (CheckCollision(objA.Geometry, objB.Geometry) && objA != objB && !resolvedPairs.Contains(tuple))
                    {
                        // Collision response here: Need to implement the EPA algorithm
                        // For now just apply a force directly away on both objects.
                        objA.ApplyForce((objA.Position - objB.Position));
                        objB.ApplyForce((objB.Position - objA.Position));
                        resolvedPairs.Add(Tuple.Create(objA, objB));
                    }
                }
            }

            foreach (var obj in caPhysicalObjects)
                obj.Update(elapsedMilliseconds);
        }

        public static bool CheckCollision(GeometryProperty objA, GeometryProperty objB)
        {
            Vector2 currentPoint = Support(objA, objB, objB.Position - objA.Position);
            List<Vector2> points = new List<Vector2>(){ currentPoint };
            Vector2 direction = -currentPoint;

            do
            {
                Vector2 A = Support(objA, objB, direction);
                if (Vector2.Dot(A, direction) < 0)
                    return false;
                points.Add(A);
            } while(!DoSimplex(points, ref direction));
            return true;
        }

        public static Vector2 Support(GeometryProperty objA, GeometryProperty objB, Vector2 direction)
        {
            if (direction == Vector2.Zero)
                direction = Vector2.UnitX;
            return objA.Furthest(direction) - objB.Furthest(-direction);
        }

        public static bool DoSimplex(List<Vector2> points, ref Vector2 direction)
        {
            switch (points.Count)
            {
                case 2:
                    {
                        Vector2 AB = points[0] - points[1];
                        Vector2 AO = -points[1];
                        if (Vector2.Dot(AB, AO) > 0)
                        {
                            direction = -(AB.X * AO.Y - AB.Y * AO.X) * (new Vector2(AB.Y, -AB.X));
                        }
                        else
                        {
                            points.RemoveAt(0);
                            direction = AO;
                        }
                        return false;
                    }
                case 3:
                    {
                        Vector2 AB = points[1] - points[2];
                        Vector2 AC = points[0] - points[2];
                        Vector2 AO = -points[2];

                        float ABC = AB.X * AC.Y - AB.Y * AC.X;
                        float ABO = AB.X * AO.Y - AB.Y * AO.X;
                        float ACO = AC.X * AO.Y - AC.Y * AO.X;
                        if (Math.Sign(ABO) == Math.Sign(ABC))
                            if (Math.Sign(-ABC) == Math.Sign(ACO))
                                return true;
                            else
                            {
                                points.RemoveAt(1);
                                direction = -ACO * new Vector2(AC.Y, -AC.X);
                            }
                        else
                        {
                            points.RemoveAt(0);
                            direction = -ABO * new Vector2(AB.Y, -AB.X);
                        }

                        return false;
                    }

                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
