using System.Collections;
using System.Collections.Generic;
using Debugs;
using UnityEngine;

namespace Shapes
{
    public struct Sphere
    {
        public float radius;

        public List<Vector3> localPos;
        public List<Vector3> normal;

        public int edgeCount;
        public int circleCount;

        public static Sphere CreateSphere(float radius, int edgeCount, int circleCount)
        {
            Sphere sphere = new Sphere();
            sphere.localPos = new List<Vector3>();
            sphere.normal = new List<Vector3>();
            sphere.edgeCount = edgeCount;
            sphere.circleCount = circleCount;
            sphere.radius = radius;

            float perRotationHalfCircle = 180f / edgeCount;
            circleCount = Mathf.Clamp(circleCount, 1, circleCount);
            float perRotationSphere = 360f / circleCount;

            Vector3 initialPos = Vector3.up * radius;
            var endPos = Vector3.down * radius;

            sphere.normal.Add(Vector3.up);
            sphere.localPos.Add(initialPos);

            for (int j = 0; j < circleCount; j++)
            {
                var sphereRotation = Quaternion.AngleAxis(j * perRotationSphere, Vector3.up);
                var oldPos = initialPos;
                for (int i = 1; i < edgeCount; i++)
                {
                    var rotationQ = Quaternion.AngleAxis(i * perRotationHalfCircle, Vector3.forward);
                    var point = rotationQ * Vector3.up * radius;
                    var diff = point - oldPos;
                    diff = Rotate90(diff);
                    sphere.normal.Add(sphereRotation * -diff.normalized);
                    oldPos = point;

                    point = sphereRotation * point;
                    sphere.localPos.Add(point);
                }
            }

            sphere.normal.Add(Vector3.down);
            sphere.localPos.Add(endPos);

            return sphere;
        }

        public void DrawPos()
        {
            var initialPos = localPos[0];
            var endPos = localPos[localPos.Count - 1];

            for (int j = 0; j < circleCount; j++)
            {
                var oldPos = initialPos;
                for (int i = 1; i < edgeCount; i++)
                {
                    var point = localPos[j * (edgeCount - 1) + i];
                    Debug.DrawLine(oldPos, point);
                    oldPos = point;
                }

                Debug.DrawLine(oldPos, endPos);
            }

            for (int i = 0; i < localPos.Count; i++)
            {
                MyDebug.DrawCircle(localPos[i], .1f, Color.black);
            }
        }

        public void DrawNormals()
        {
            for (int i = 0; i < localPos.Count; i++)
            {
                var localPos = this.localPos[i];
                var normal = this.normal[i];
                Debug.DrawLine(localPos, localPos + normal);
            }
        }

        public static Vector3 Rotate90(Vector3 v)
        {
            return new Vector3(-v.y, v.x);
        }
    }
}