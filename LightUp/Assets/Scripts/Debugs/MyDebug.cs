using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Debugs
{
    public static class MyDebug
    {
        public static void  DrawCircle(Vector3 center, float radius,Color color, float nEdge = 20)
        {
            var perRotation = 360f / nEdge;
            var pos = center + Vector3.up * radius;
            var oldPos = pos;
            
            for (int i = 1; i < nEdge; i++)
            {
                var rotateQ = Quaternion.AngleAxis(perRotation * i,Vector3.forward);
                var point = center + rotateQ * Vector3.up * radius;
                UnityEngine.Debug.DrawLine(oldPos,point,color);
                oldPos = point;
            }
            UnityEngine.Debug.DrawLine(oldPos,pos,color);
        }

        public static void DrawSphere(Vector3 center,float radius, int edgeCount = 20, int circleCount = 20)
        {
            float perRotationHalfCircle = 180f / edgeCount;
            circleCount = Mathf.Clamp(circleCount, 1, circleCount);
            float perRotationSphere = 360f / circleCount;
            Vector3 initialPos = center + Vector3.up * radius;
            var endPos = center + Vector3.down * radius;
            for (int j = 0; j < circleCount; j++)
            {
                var sphereRotation = Quaternion.AngleAxis(j * perRotationSphere, Vector3.up);
                var oldPos = initialPos;
                for (int i = 1; i < edgeCount; i++)
                {
                    var rotationQ = Quaternion.AngleAxis(i * perRotationHalfCircle, Vector3.forward);
                    var point = center + sphereRotation* rotationQ * Vector3.up * radius;
                    Debug.DrawLine(oldPos,point);
                    oldPos = point;
                }
                Debug.DrawLine(oldPos,endPos);
            }
        }
    }

}

