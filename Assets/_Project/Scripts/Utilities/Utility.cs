using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public static class Utility
{
    //Half value used for calculating extents
    public  const float HALF_VALUE = 0.5f;
    
    //Calculate the camera's normalized direction and ignores the isometric tilt of the camera
    public static Vector3 CalculateCameraDirection(Camera camera, Vector2 movementDirection)
    {
        Vector3 camForward = GetNormalizeCameraDirection(camera.transform.forward);
        Vector3 camRight = GetNormalizeCameraDirection(camera.transform.right);
        
        //Calculate the direction based on current camera orientation
        Vector3 normalizedDirection = camRight * movementDirection.x +  camForward * movementDirection.y;
        normalizedDirection.Normalize();

        return normalizedDirection;
    }
    
    //Gets the camera's normalized value, and ignores tilting 
    public static Vector3 GetNormalizeCameraDirection(Vector3 normalizedDirection)
    {
        normalizedDirection.y = 0;
        normalizedDirection.Normalize();
        return normalizedDirection;
    }

    public static Vector3 CalculateHalfExtents(float width, float height, float depth)
    {
        return new Vector3(width * HALF_VALUE, height * HALF_VALUE, depth * HALF_VALUE);
    }

    public static Vector3[] CalculateCorners(Vector3 center, Vector3 halfExtents)
    {
        Vector3[] corners = new Vector3[8];
        int index = 0;
        for (int x = -1; x <= 1; x += 2)
        {
            for (int y = -1; y <= 1; y += 2)
            {
                for (int z = -1; z <= 1; z += 2)
                {
                    corners[index++] = center + new Vector3(halfExtents.x * x, halfExtents.y * y, halfExtents.z * z);
                }
            }
        }
        return corners;
    }

    public static int[,] GetBoxEdges()
    {
        // Define the edges of the box as pairs of corner indices.
        return new int[,]
        {
            { 0, 1 }, { 0, 2 }, { 0, 4 },
            { 1, 3 }, { 1, 5 },
            { 2, 3 }, { 2, 6 },
            { 3, 7 },
            { 4, 5 }, { 4, 6 },
            { 5, 7 },
            { 6, 7 }
        };
    }

}