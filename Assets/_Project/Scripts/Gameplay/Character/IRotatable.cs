using UnityEngine;

public interface IRotatable
{
    void Rotate(Vector3 rotationDirection, MovementStats movementStats);
}